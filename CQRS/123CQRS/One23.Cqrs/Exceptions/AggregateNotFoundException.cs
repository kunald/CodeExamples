using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insight.Cqrs.Exceptions
{
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException(string message) : base(message) { }
    }
}
