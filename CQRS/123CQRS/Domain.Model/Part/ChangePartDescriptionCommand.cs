using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight.Cqrs.Commands;

namespace Domain.Commands
{
    public class ChangePartDescriptionCommand : Command
    {
        public string PartDescription { get; private set; }

        public ChangePartDescriptionCommand(Guid aggregateId, string partDescription) : base(aggregateId)
        {
            PartDescription = partDescription;
        }
    }
}
