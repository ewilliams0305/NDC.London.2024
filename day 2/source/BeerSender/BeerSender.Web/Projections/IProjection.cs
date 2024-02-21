using BeerSender.Domain;

namespace BeerSender.Web.Projections;

public interface IProjection
{
    TimeSpan Wait_time { get; }

    int Batch_size { get; }

    Type[] Events_types { get; }

    Task Handle_batch(IEnumerable<Event_message> events);
}