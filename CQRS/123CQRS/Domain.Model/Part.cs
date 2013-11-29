using System;

namespace Domain.Model
{
    public class Part
    {
        public Guid Id { get; private set; }
        public string PartNumber { get; private set; }
        public string PartDescription { get; private set; }
        public int UnitOfMeasure { get; private set; }
        public int SalesLeadTime { get; private set; }

        public Part(Guid id, string partNumber, string partDescription, int unitOfMeasure, int salesLeadTime)
        {
            SalesLeadTime = salesLeadTime;
            UnitOfMeasure = unitOfMeasure;
            PartDescription = partDescription;
            PartNumber = partNumber;
            Id = id;
        }
    }
}
