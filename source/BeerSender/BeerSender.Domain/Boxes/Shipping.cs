namespace BeerSender.Domain.Boxes;

public record ShippingAddress(int Number, string Street, int PostalCode);

public record CreateShippingLabel(ShippingAddress Address);

public record LabelWasInvalid(ShippingLabel Label);

public record LabelWasApplied(ShippingAddress Label, Box Box);

public class ShippingLabel
{
    public ShippingAddress Address { get; private set; }
    
    public ShippingLabel(ShippingAddress address)
    {
        Address = address;
    }
}


public record ShipBox(Box BoxToShip, ShippingLabel Label);

public record BoxShipped(Box ShippedBox);

public record BoxNotReadyToShip(Box NotReadyBox);