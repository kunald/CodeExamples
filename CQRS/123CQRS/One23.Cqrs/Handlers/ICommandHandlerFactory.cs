
using Insight.Cqrs.CommandHandler;
using Insight.Cqrs.Commands;
namespace Insight.Cqrs.Handlers
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<T> GetHandler<T>() where T : Command;
    }
}
