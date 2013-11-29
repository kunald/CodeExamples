using Insight.Cqrs.CommandHandler;
using Insight.Cqrs.Commands;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Insight.Cqrs.Handlers
{
    public class StructureMapCommandHandlerFactory : ICommandHandlerFactory
    {
        public ICommandHandler<T> GetHandler<T>() where T : Command
        {
            var handlers = GetHandlerTypes<T>().ToList();

            var cmdHandler = handlers.Select(handler => 
                (ICommandHandler<T>)ObjectFactory.GetInstance(handler)).FirstOrDefault();
            
            return cmdHandler;
        }
        
        private IEnumerable<Type> GetHandlerTypes<T>() where T : Command
        {
            var handlers = typeof(T).Assembly.GetExportedTypes()
                .Where(x => x.GetInterfaces()
                    .Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(ICommandHandler<>) ))
                    .Where(h=>h.GetInterfaces()
                        .Any(ii=>ii.GetGenericArguments()
                            .Any(aa=>aa==typeof(T)))).ToList();

           
            return handlers;
        }

    }
}
