using BusinessAdministration.Aplication.Core.Base.Exceptions;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Person
{
    public class AlreadyExistException : SypException
    {
        public AlreadyExistException() { }
        public AlreadyExistException(string message): base(message) { }
    }
}
