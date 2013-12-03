using System;

namespace Insight123.Contract
{
    public interface ICommand
    {
        Guid Id { get; }
        int Version { get; }
    }
}
