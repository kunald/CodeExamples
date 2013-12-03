using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insight123.Base.Exceptions
{
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException(string message) : base(message) { }
    }
}
