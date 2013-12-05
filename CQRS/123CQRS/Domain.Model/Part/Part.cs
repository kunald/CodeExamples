using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Events;
using Insight123.Base;
using Insight123.Contract;

namespace Domain.Model.Part
{
    public class Part : AggregateRoot,
         IHandle<PartCreated>, IHandle<PartDescriptionChanged>
    {
        public string PartNumber { get; private set; }
        public string PartDescription { get; private set; }
        public int UnitOfMeasure { get; private set; }
        public int SalesLeadTime { get; private set; }

        public Part()
        {

        }

        public Part(Guid partId, string partNumber, string partDescription, int unitOfMeasure, int salesLeadTime)
            : this()
        {
            // Should check for business validation here 
            ApplyChange(new PartCreated(partId, partNumber, partDescription, unitOfMeasure, salesLeadTime));
        }

        public static Part CreateNewPart(Guid partId, string partNumber, string partDescription, int unitOfMeasure, int salesLeadTime)
        {
            return new Part(partId, partNumber, partDescription, unitOfMeasure, salesLeadTime);
        }

        public void ChangePartDescription(string partDescription)
        {
            IsPartCreated();
            ApplyChange(new PartDescriptionChanged(Id, partDescription));
        }


        public void Handle(PartCreated e)
        {
            Id = e.AggregateId;
            SalesLeadTime = e.SalesLeadTime;
            UnitOfMeasure = e.UnitOfMeasure;
            PartDescription = e.PartDescription;
            PartNumber = e.PartNumber;

        }

        public void Handle(PartDescriptionChanged e)
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
