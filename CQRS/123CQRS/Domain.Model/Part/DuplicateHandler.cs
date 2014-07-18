using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Commands;
using Insight123.Contract;

namespace Domain.Model.Part
{
    public class DuplicateHandler: ICommandHandler<CreatePart>
    {
         private IDomainRepository _repository;

         public DuplicateHandler(IDomainRepository repository)
        {
            _repository = repository;
        }
        public void Execute(CreatePart command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (_repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }
            var aggregate = Part.CreateNewPart(command.Id, command.PartNumber, command.PartDescription, command.UnitOfMeasure, command.SalesLeadTime);
            _repository.Save(aggregate, command.Version);
        }

       }
}
