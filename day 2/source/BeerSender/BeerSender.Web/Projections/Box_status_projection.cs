using BeerSender.Domain;
using BeerSender.Domain.Boxes;
using BeerSender.Web.Read_database;

namespace BeerSender.Web.Projections;

public class Box_status_projection : IProjection
{
    private readonly Read_context _context;

    public Box_status_projection(Read_context context)
    {
        _context = context;
    }

    public TimeSpan Wait_time => TimeSpan.FromMilliseconds(5000);

    public int Batch_size => 500;

    public Type[] Events_types => new Type[]
    {
        typeof(Box_created),
        typeof(Beer_added),
        typeof(Box_closed),
        typeof(Box_shipped)
    };

    public async Task Handle_batch(IEnumerable<Event_message> events)
    {
        foreach (var event_message in events)
        {
            await Handle_message(event_message);
        }

        await _context.SaveChangesAsync();
    }

    private async Task Handle_message(Event_message event_message)
    {
        switch (event_message.Event)
        {
            case Box_created created:

                await _context.Box_statuses.AddAsync(new Box_status()
                {
                    Aggregate_id = event_message.Aggregate_id,
                    Number_of_bottles = 0,
                    Shipment_status = Shipment_status.Open,
                });
                break;

            case Beer_added added:
                {
                    var record = await _context.Box_statuses.FindAsync(event_message.Aggregate_id);
                    record!.Number_of_bottles++;
                    break;
                }
            case Box_closed closed:
                {
                    var record = await _context.Box_statuses.FindAsync(event_message.Aggregate_id);
                    record!.Shipment_status = Shipment_status.Closed;
                    break;
                }

            case Box_shipped shipped:
                {
                    var record = await _context.Box_statuses.FindAsync(event_message.Aggregate_id);
                    record!.Shipment_status = Shipment_status.Shipped;
                    break;
                }
        }
    }
}