using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insight123.Base.Exceptions
{
    public class AggregateDeletedException : Exception
    {
        public AggregateDeletedException(string message) : base(message) { }
    }
}
