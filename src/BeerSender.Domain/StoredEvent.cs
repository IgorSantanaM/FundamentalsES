using System;
using System.Collections.Generic;
using System.Text;

namespace BeerSender.Domain;
public record StoredEvent(
    Guid AggregateId, 
    int SequenceNumber,
    DateTime TimeStamp, 
    object EventData
    
);