﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insight123.Base.Exceptions
{
    public class ConcurrencyException : Exception
    {
        public ConcurrencyException(string message) : base(message) { }
    }
}
