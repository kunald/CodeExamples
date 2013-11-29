using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Part;
using Insight.Cqrs.EventHandlers;
using Insight.Cqrs.ReadOnlyStorage;

namespace Domain.EventHandlers
{
    public class PartDescriptionChangedEventHandler : IEventHandler<PartDescriptionChangedEvent>
    {
        private readonly IReadRepository _reportDatabase;
        public PartDescriptionChangedEventHandler(IReadRepository reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(PartDescriptionChangedEvent handle)
        {
            //ToDo: Call the update() for the repository
            var item = _reportDatabase.GetById(handle.AggregateId);
            item.PartDescription = handle.PartDescription;
        }
    }
}
