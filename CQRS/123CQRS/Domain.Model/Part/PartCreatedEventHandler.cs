using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Events;
using Insight.Cqrs.EventHandlers;
using Insight.Cqrs.ReadOnlyStorage;

namespace Domain.EventHandlers
{
    public class PartCreatedEventHandler : IEventHandler<PartCreatedEvent>
    {
        private readonly IReadRepository _reportDatabase;
        public PartCreatedEventHandler(IReadRepository reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(PartCreatedEvent handle)
        {
            PartDto item = new PartDto()
            {
                Id = handle.AggregateId,
                PartNumber=handle.PartNumber,
                PartDescription=handle.PartDescription,
                SalesLeadTime=handle.SalesLeadTime,
                UnitOfMeasure=handle.UnitOfMeasure,
            };

            _reportDatabase.Add(item);
        }
    }
}
