using System;
using Insight123.Base;

namespace Domain.Model.Part
{
    public class PartDescriptionChanged : Event
    {
        public string PartDescription { get; private set; }
        public PartDescriptionChanged(Guid aggregateId, string partDescription)
        {
            AggregateId = aggregateId;
            PartDescription = partDescription;
        }
    }
}
