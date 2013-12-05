using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Insight123.Base;

namespace Domain.Commands
{
    public class CreatePart : Command
    {
        public string PartNumber { get; private set; }
        [Required]
        public string PartDescription { get; private set; }
        public int UnitOfMeasure { get; private set; }
        public int SalesLeadTime { get; private set; }

        public CreatePart(Guid aggregateId, string partNumber, string partDescription, int unitOfMeasure, int salesLeadTime)
            : base(aggregateId, -1)
        {
            SalesLeadTime = salesLeadTime;
            UnitOfMeasure = unitOfMeasure;
            PartDescription = partDescription;
            PartNumber = partNumber;
            var isValid = ValidateCommand(this);
            if (isValid.Count > 0)
                throw new Exception(isValid[0].ErrorMessage);
        }


        private static List<ValidationResult> ValidateCommand(object command)
        {
            var validationContext = new ValidationContext(command, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(command, validationContext, validationResults, true);

            return validationResults;
        }
    }
}
