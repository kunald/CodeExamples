using System;

namespace Domain.Model.Part
{
    public class NonExistingPartException : Exception
    {
        public NonExistingPartException(string message): base(message){}
    }
}
