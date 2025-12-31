using System.Numerics;

namespace BeerSender.Domain.Boxes;

public class Box : AggregateRoot
{
    public BoxCapacity? Capacity { get; private set; }
    public ShippingLabel? ShippingLabel { get; private set; }

    public void Apply(BoxCreated @event)
    {
        Capacity = @event.Capacity;
    }

    public void Apply(ShippingLabelAdded @event)
    {
        ShippingLabel = @event.Label;
    }
}

public record CreateBox(
    Guid BoxId, 
    int DesiredNumbersOfSpots
);

public record BoxCreated(BoxCapacity Capacity);
public record ShippingLabelAdded(
    ShippingLabel Label
);

public record ShippingLabelFailedToAdd(ShippingLabelFailedToAdd.FailReason Reason)
{
    public enum FailReason
    {
        TrackingCodeInvalid
    }
}

public record ShippingLabel(Carrier Carrier, string TrackingCode)
{
    public bool IsValid()
        => Carrier switch
        {
            Carrier.UPS => TrackingCode.StartsWith("ABC"),
            Carrier.FedEx => TrackingCode.StartsWith("DEF"),
            Carrier.BPost => TrackingCode.StartsWith("GHI"),
            _ => throw new ArgumentOutOfRangeException(nameof(Carrier), Carrier, null),
        };
}

public record BoxCapacity(int NumberOfSpots)
{
    public static BoxCapacity Create(int desiredNumberOfSpots)
    {
        return desiredNumberOfSpots switch
        {
            <= 6 => new BoxCapacity(6),
            <= 12 => new BoxCapacity(12),
            _ => new BoxCapacity(24)
        };
    }
}

public enum Carrier
{
    UPS,
    FedEx,
    BPost
}