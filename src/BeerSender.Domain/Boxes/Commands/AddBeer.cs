using System;
using System.Collections.Generic;
using System.Text;

namespace BeerSender.Domain.Boxes.Commands
{
    public record AddBeer(Guid BoxId);

    public class AddBeerCommandHandler(IEventStore eventStore) : CommandHandler<AddBeer>(eventStore)
    {
        public override void Handle(AddBeer command)
        {
            var boxStream = GetStream<Box>(command.BoxId);

            var box = boxStream.GetEntity();

            if (box.NumberOfBottles < box.Capacity?.NumberOfSpots)
                boxStream.Append(new BeerAdded(box.NumberOfBottles + 1));

            else
                boxStream.Append(new BottleFailedToAdd(BottleFailedToAdd.FailReason.BoxIsFull));
        }
    }
}
