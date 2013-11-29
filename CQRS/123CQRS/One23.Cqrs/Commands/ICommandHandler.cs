using Insight.Cqrs.Commands;
namespace Insight.Cqrs.CommandHandler
{
    public interface ICommandHandler<in TCommand> where TCommand : Command
    {
        void Execute(TCommand command);
    }
}
