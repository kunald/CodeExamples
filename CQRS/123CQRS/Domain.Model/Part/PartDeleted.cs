using Insight123.Base;
using System;

namespace Domain.Model.Part
{
    [Serializable]
    public class PartDeleted : Command
    {
        public PartDeleted(Guid id, int version) : base(id, version)
        {
        }
    }
}
