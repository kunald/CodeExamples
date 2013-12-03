using System;
using Insight123.Contract;

namespace Insight123.Base
{
    [Serializable]
    public class Command : ICommand
    {
        public Guid Id { get; private set; }
        public int Version { get; private set; }
        public Command(Guid id, int version)
        {
            Version = version;
            Id = id;
        }
    }
}
