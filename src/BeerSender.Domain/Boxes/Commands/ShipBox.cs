namespace BeerSender.Domain.Boxes.Commands
{
    public record ShipBox(Guid BoxId);

    public class ShipBoxHandler(IEventStore eventStore) : CommandHandler<ShipBox>(eventStore)
    {
        public override void Handle(ShipBox command)
        {
            var boxStream = GetStream<Box>(command.BoxId);

            var box = boxStream.GetEntity();

            if (box.Status == Status.Closed)
                boxStream.Append(new BoxShipped());
            else
                boxStream.Append(new BoxFailedToShip(BoxFailedToShip.FailReason.BoxNotReady));
        }
    }
}
