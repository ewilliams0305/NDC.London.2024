namespace BeerSender.Domain;

//public interface IAggregate
//{
//    void Apply(object @event);
//}

public abstract class Aggregate
{
    public abstract void Apply(object @event);
}