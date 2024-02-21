using BeerSender.Web.Event_stream;
using BeerSender.Web.Read_database;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BeerSender.Web.Projections
{
    public class Projector_background_service<TProjection> : BackgroundService where TProjection : class, IProjection
    {
        private readonly ILogger<Projector_background_service<TProjection>> _logger;
        private readonly IServiceProvider _serviceProvider;

        public Projector_background_service(ILogger<Projector_background_service<TProjection>> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        #region Overrides of BackgroundService

        /// <inheritdoc />
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var checkpoint = await Get_checkpoint();

                while (!stoppingToken.IsCancellationRequested)
                {
                    await using var scope = _serviceProvider.CreateAsyncScope();

                    var event_context = scope.ServiceProvider.GetRequiredService<Event_context>();
                    var read_context = scope.ServiceProvider.GetRequiredService<Read_context>();
                    var transaction = await read_context.Database.BeginTransactionAsync(stoppingToken);
                    var projection = scope.ServiceProvider.GetRequiredService<TProjection>();
                    var events = await Get_batch(checkpoint, event_context, projection);

                    if (events.Any())
                    {
                        await projection.Handle_batch(events.Map_messages_from_models());
                        checkpoint = events.Last().Row_version;

                        await Write_checkpoint(read_context, checkpoint);
                    }
                    else
                    {
                        await Task.Delay(projection.Wait_time, stoppingToken);
                    }

                    await transaction.CommitAsync(stoppingToken);
                    await Task.Delay(500, stoppingToken);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to execute projection background service.");
            }
        }

        private async Task<ulong> Get_checkpoint()
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            var ctx = scope.ServiceProvider.GetRequiredService<Read_context>();
            var checkpoint = await ctx.Projection_checkpoints.FindAsync(typeof(TProjection).Name);

            if (checkpoint == null)
            {
                checkpoint = new Projection_checkpoint()
                {
                    Projection_name = typeof(TProjection).Name,
                    Event_version = 0
                };
                await ctx.Projection_checkpoints.AddAsync(checkpoint);
                await ctx.SaveChangesAsync();
            }
            return checkpoint.Event_version;
        }

        private async Task<IList<Event_model>> Get_batch(ulong checkpoint, Event_context event_context, IProjection projection)
        {
            var typeList = projection.Events_types.Select(t => t.Name).ToList();

            var batch = await event_context.Events
                .Where(e => typeList.Contains(e.Event_type))
                .Where(e => e.Row_version > checkpoint)
                .OrderBy(e => e.Row_version)
                .Take(projection.Batch_size)
                .ToListAsync();

            return batch;
        }

        private async Task Write_checkpoint(Read_context read_context, ulong checkpoint)
        {
            var checkpoint_record = await read_context.Projection_checkpoints.FindAsync(typeof(TProjection).Name);
            checkpoint_record!.Event_version = checkpoint;
            await read_context.SaveChangesAsync();
        }

        #endregion
    }
}
