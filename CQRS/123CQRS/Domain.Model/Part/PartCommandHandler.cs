using System;
using Domain.Commands;
using Domain.Model.Part;
using Insight123.Contract;

namespace Domain.CommandHandlers
{
    public class PartCommandHandler : ICommandHandler<CreatePartCommand>, ICommandHandler<ChangePartDescriptionCommand>
    {
        private IRepository<IEvent> _repository;

        public PartCommandHandler(IRepository<IEvent> repository)
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
            _repository.Save(aggregate, command.Version);
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

            var aggregate = _repository.GetById<Part>(command.Id);
            aggregate.ChangePartDescription(command.PartDescription);
            _repository.Save(aggregate, command.Version);
        }
    }
}
