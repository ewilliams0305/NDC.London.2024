namespace BeerSender.Domain.Boxes;

public class Box : Aggregate
{
    public Capacity? Capacity { get; private set; }

    public override void Apply(object @event)
    {
        throw new Exception($"Event type {@event.GetType()} not implemented for {this.GetType()}.");
    }

    public void Apply(Box_created @event)
    {
        Capacity = @event.Capacity;
    }
}