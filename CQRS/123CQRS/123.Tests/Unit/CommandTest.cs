using Domain.CommandHandlers;
using Domain.Commands;
using Insight123.Base;
using Insight123.Contract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _123.Tests.Unit
{
    [TestClass]
    public class CommandTest
    {
        [TestMethod]
        public void All_commands_will_have_the_Serializable_attribute_assigned()
        {

            var domainEventTypes = typeof(CreatePart).Assembly.GetExportedTypes().Where(x => x.BaseType == typeof(Command)).ToList();
            foreach (var commandType in domainEventTypes)
            {
                if (commandType.IsSerializable)
                    continue;

                throw new Exception(string.Format("Command '{0}' is not Serializable", commandType.FullName));
            }
        }

        [TestMethod]
        public void All_commands_must_have_a_handler()
        {
            var domainEventTypes = typeof(CreatePart).Assembly.GetExportedTypes().Where(x => x.BaseType == typeof(Command)).ToList();
            var commandHandlers = GetCommandHandlers();
            foreach (var commandType in domainEventTypes)
            {
                var result = from handler in commandHandlers
                             where  
                                 handler.GetInterfaces().FirstOrDefault(r => r.GenericTypeArguments[0] == commandType) !=
                                 null
                             select handler;
                if (result.FirstOrDefault() == null)
                    throw new Exception(string.Format("No command handler found for command '{0}'", commandType.FullName));
            }
        }

        public static IList<Type> GetCommandHandlers()
        {
            IDictionary<Type, IList<Type>> commands = new Dictionary<Type, IList<Type>>();
            var handlers = typeof(PartCommandHandler)
                .Assembly
                .GetExportedTypes()
                .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
                .ToList();
            return handlers;
        }


        private static void AddItem(IDictionary<Type, IList<Type>> dictionary, Type type)
        {
            var command = type.GetInterfaces().First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICommandHandler<>))
                .GetGenericArguments()
                .First();

            if (!dictionary.ContainsKey(command))
                dictionary.Add(command, new List<Type>());

            dictionary[command].Add(type);
        }
    }
}
