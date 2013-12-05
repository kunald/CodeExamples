using System;
using Insight123.Base;

namespace Domain.Events
{
    public class PartCreated : Event
    {
        public string PartNumber { get; private set; }
        public string PartDescription { get; private set; }
        public int UnitOfMeasure { get; private set; }
        public int SalesLeadTime { get; private set; }

        public PartCreated(Guid aggregateId, string partNumber, string partDescription, int unitOfMeasure, int salesLeadTime)
        {
            AggregateId = aggregateId;
            SalesLeadTime = salesLeadTime;
            UnitOfMeasure = unitOfMeasure;
            PartDescription = partDescription;
            PartNumber = partNumber;
        }
    }
}
