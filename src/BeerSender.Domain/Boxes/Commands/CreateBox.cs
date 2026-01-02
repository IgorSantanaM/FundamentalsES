namespace BeerSender.Domain.Boxes.Commands
{
    public record CreateBox(
       Guid BoxId,
       int DesiredNumbersOfSpots
    );

    public class CraeteboxCommandHanlder(IEventStore eventStore) : CommandHandler<CreateBox>(eventStore)
    {
        public override void Handle(CreateBox command)
        {
            var boxStream = GetStream<Box>(command.BoxId);

            var boxCapacity = new BoxCapacity(command.DesiredNumbersOfSpots);

            boxStream.Append(new BoxCreated(boxCapacity));
        }
    }
}
