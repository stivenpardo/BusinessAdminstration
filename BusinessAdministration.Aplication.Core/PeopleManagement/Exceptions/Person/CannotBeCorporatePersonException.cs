using BusinessAdministration.Aplication.Core.Base.Exceptions;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Person
{
    public class CannotBeCorporatePersonException : SypException
    {
        public CannotBeCorporatePersonException() { }
        public CannotBeCorporatePersonException(string message): base(message) { }
    }
}
