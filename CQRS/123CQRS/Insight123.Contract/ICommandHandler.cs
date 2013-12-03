﻿namespace Insight123.Contract
{
    public interface ICommandHandler<in TCommand>
    {
        void Execute(TCommand command);
    }
}
