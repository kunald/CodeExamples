using System;
using Domain.Commands;
using Domain.Model.Part;
using Insight.Cqrs.CommandHandler;
using Insight.Cqrs.Storage;

namespace Domain.CommandHandlers
{
    public class PartCommandHandler : ICommandHandler<CreatePartCommand>,ICommandHandler<ChangePartDescriptionCommand>
    {
        private IRepository<Part> _repository;

        public PartCommandHandler(IRepository<Part> repository)
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
            var aggregate = Part.CreateNewPart(command.Id, command.PartNumber, command.PartDescription, command.UnitOfMeasure, command.SalesLeadTime);
            _repository.Save(aggregate, aggregate.Version);
        }

        public void Execute(ChangePartDescriptionCommand command)
        {
             if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (_repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }

            var aggregate = _repository.GetById(command.Id);
            aggregate.ChangePartDescription(command.PartDescription);
            _repository.Save(aggregate, aggregate.Version);
        }
    }
}
