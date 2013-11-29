using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight.Cqrs.CommandHandler;
using Insight.Cqrs.Storage;
using Domain.Model;

namespace Domain.Model
{
    public class CreatePartCommandHandler : ICommandHandler<CreatePartCommand>
    {
        private IRepository<Part> _repository;

        public CreatePartCommandHandler(IRepository<Part> repository)
        {
            _repository = repository;
        }
        public void Execute(CreatePartCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (_repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }
            var aggregate = new Part(command.Id, command.PartNumber, command.PartDescription, command.UnitOfMeasure, command.SalesLeadTime);
            aggregate.SetVersion(-1);
            _repository.Save(aggregate, aggregate.Version);
        }
    }
}
