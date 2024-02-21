namespace BeerSender.Domain.Boxes.Handlers;

public class Apply_shipping_label_handler : Command_handler<Box, Apply_shipping_label>
{
    /// <inheritdoc />
    public Apply_shipping_label_handler(IEnumerable<object> event_stream, Action<object> publish_event) : base(event_stream, publish_event)
    {
    }

    #region Overrides of Command_handler<Box,Apply_shipping_label>

    /// <inheritdoc />
    protected override IEnumerable<object> HandleInternal(Box aggregate, Apply_shipping_label command)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    protected override Box CreateAggregate()
    {
        throw new NotImplementedException();
    }

    #endregion
}

public class Add_beer_handler : Command_handler<Box, Add_beer>
{
    /// <inheritdoc />
    public Add_beer_handler(
        IEnumerable<object> event_stream, 
        Action<object> publish_event) 
        : base(event_stream, publish_event)
    {
    }

    #region Overrides of Command_handler<Box,Add_beer>

    /// <inheritdoc />
    protected override IEnumerable<object> HandleInternal(Box aggregate, Add_beer command)
    {
        yield return new Add_beer(command.Beer);
    }

    /// <inheritdoc />
    protected override Box CreateAggregate()
    {
        return new Box();
    }

    #endregion
}