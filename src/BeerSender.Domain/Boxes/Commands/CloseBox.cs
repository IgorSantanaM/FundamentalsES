using System;
using System.Collections.Generic;
using System.Text;

namespace BeerSender.Domain.Boxes.Commands
{
    public record CloseBox(Guid BoxId);

    public class CloseBoxHandler(IEventStore eventStore) : CommandHandler<CloseBox>(eventStore)
    {
        public override void Handle(CloseBox command)
        {
            var boxStream = GetStream<Box>(command.BoxId);

            var box = boxStream.GetEntity();

            if (box.NumberOfBottles != 0)
                boxStream.Append(new BoxClosed());
            else
                boxStream.Append(new BoxFailedToClose(BoxFailedToClose.FailReason.BoxIsEmpty));
        }
    }
}
