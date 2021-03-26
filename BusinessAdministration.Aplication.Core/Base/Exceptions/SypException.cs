using System;

namespace BusinessAdministration.Aplication.Core.Base.Exceptions
{
    public class SypException : Exception
    {
        public SypException() { }
        public SypException(string? message) : base(message) { }
    }
}
