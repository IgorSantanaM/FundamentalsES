using System.Numerics;

namespace BeerSender.Domain.Boxes;

public class Box : AggregateRoot
{
    public BoxCapacity? Capacity { get; private set; }
    public ShippingLabel? ShippingLabel { get; private set; }
    public int? NumberOfBottles { get; private set; }
    public Status Status { get; private set; }

    public void Apply(BoxCreated @event)
    {
        Status = Status.Opened;
        Capacity = @event.Capacity;
    }

    public void Apply(ShippingLabelAdded @event)
    {
        ShippingLabel = @event.Label;
    }

    public void Apply(BeerAdded @event)
    {
        NumberOfBottles = @event.NumberOfBottles;
    }

    public void Apply(BoxClosed @event)
    {
        Status = Status.Closed;
    }
    public void Apply(BoxShipped @event)
    {
        Status = Status.Shipped;
    }
}


public enum Status
{
    Opened = 1,
    Closed = 2,
    Shipped = 3
}

public record BeerAdded(int? NumberOfBottles);
public record BoxClosed();
public record BoxShipped();
public record BoxFailedToClose(BoxFailedToClose.FailReason Reason)
{
    public enum FailReason
    {
        BoxIsEmpty
    }
}
public record BoxFailedToShip(BoxFailedToShip.FailReason Reason)
{
    public enum FailReason
    {
        BoxNotReady 
    }
}
public record BottleFailedToAdd(BottleFailedToAdd.FailReason Reason)
{
    public enum FailReason
    {
        BoxIsFull
    }
}
public record BoxCreated(BoxCapacity? Capacity);
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