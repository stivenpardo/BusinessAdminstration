using BusinessAdministration.Aplication.Core.Base.Exceptions;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Person
{
    public class CannotBeNaturalPersonException : SypException
    {
        public CannotBeNaturalPersonException() { }
        public CannotBeNaturalPersonException(string message): base(message) { }
    }
}
