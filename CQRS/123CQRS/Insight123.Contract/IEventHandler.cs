using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insight123.Contract
{
    public interface IEventHandler<TEvent>
    {
        void Handle(TEvent handle);
    }
}
