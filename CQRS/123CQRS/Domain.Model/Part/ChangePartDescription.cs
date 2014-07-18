using Insight123.Base;
using System;

namespace Domain.Commands
{
    [Serializable]
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
