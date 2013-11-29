using System;

namespace Insight.Cqrs.Commands
{
    public interface ICommand
    {
        Guid Id { get; }
    }
}
