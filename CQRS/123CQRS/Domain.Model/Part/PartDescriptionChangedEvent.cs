using System;
using Insight.Cqrs.Events;

namespace Domain.Model.Part
{
    public class PartDescriptionChangedEvent : Event
    {
        public string PartDescription { get; private set; }
        public PartDescriptionChangedEvent(Guid aggregateId, string partDescription)
        {
            AggregateId = aggregateId;
            PartDescription = partDescription;
        }
    }
}
