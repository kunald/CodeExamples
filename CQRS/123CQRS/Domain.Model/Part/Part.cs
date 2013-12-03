using System;
using Domain.Events;
using Insight123.Base;
using Insight123.Contract;

namespace Domain.Model.Part
{
    public class Part : AggregateRoot<IEvent>,
         IHandle<PartCreatedEvent>,IHandle<PartDescriptionChangedEvent>
    {
        public string PartNumber { get; private set; }
        public string PartDescription { get; private set; }
        public int UnitOfMeasure { get; private set; }
        public int SalesLeadTime { get; private set; }

        public Part()
        {

        }

        public Part(Guid partId, string partNumber, string partDescription, int unitOfMeasure, int salesLeadTime) : this()
        {
            // Should check for business validation here 
            ApplyChange(new PartCreatedEvent(partId, partNumber, partDescription, unitOfMeasure, salesLeadTime));
        }

        public static Part CreateNewPart(Guid partId, string partNumber, string partDescription, int unitOfMeasure, int salesLeadTime)
        {
            return new Part(partId, partNumber, partDescription, unitOfMeasure, salesLeadTime);
        }

        public void ChangePartDescription(string partDescription)
        {
            IsPartCreated();
            ApplyChange(new PartDescriptionChangedEvent(Id, partDescription));
        }


        public void Handle(PartCreatedEvent e)
        {
            Id = e.AggregateId;
            SalesLeadTime = e.SalesLeadTime;
            UnitOfMeasure = e.UnitOfMeasure;
            PartDescription = e.PartDescription;
            PartNumber = e.PartNumber;

        }

        public void Handle(PartDescriptionChangedEvent e)
        {
            Id = e.AggregateId;
            PartDescription = e.PartDescription;

        }

        private void IsPartCreated()
        {
            if (Id == Guid.Empty)
                throw new NonExistingPartException("The Part is not created.");
        }
    }
}
