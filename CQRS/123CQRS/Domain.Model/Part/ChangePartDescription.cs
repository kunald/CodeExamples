using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight123.Base;

namespace Domain.Commands
{
    public class ChangePartDescription : Command
    {
        public string PartDescription { get; private set; }

        public ChangePartDescription(Guid aggregateId, string partDescription, int version)
            : base(aggregateId, version)
        {
            PartDescription = partDescription;
        }
    }
}
