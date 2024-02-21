using BeerSender.Domain;
using Microsoft.EntityFrameworkCore;

namespace BeerSender.Web.Event_stream;

public sealed class Event_stream_service
{
    private readonly Event_context _context;

    public Event_stream_service(Event_context context)
    {
        _context = context;
    }

    public IEnumerable<Event_message> Get_messages(Guid aggregate_id)
    {
        var events = _context.Events
            .Where(e => e.Aggregate_id == aggregate_id)
            .OrderBy(e => e.Sequence_nr)
            .ToList();

        return events.Map_messages_from_models();
    }

    public void Handle_event_message(Event_message message)
    {
        _context.Events.Add(message.Map_model_from_message());
        _context.SaveChanges();
    }
}

public static class Event_Mapping
{
    public static Event_model Map_model_from_message(this Event_message message)
    {
        return new Event_model()
        {
            Aggregate_id = message.Aggregate_id,
            Timestamp = DateTime.Now,
            Event = message.Event,
            Sequence_nr = message.Sequence,

        };
    }

    public static Event_message Map_message_from_model(this Event_model model)
    {
        return new Event_message(model.Aggregate_id, model.Sequence_nr, model.Event);
    }

    public static IEnumerable<Event_message> Map_messages_from_models(this IEnumerable<Event_model> models)
    {
        return models.Select(model => model.Map_message_from_model()).ToList();
    }
}