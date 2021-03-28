using BusinessAdministration.Aplication.Core.Base.Exceptions;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Employed
{
    public class NotExistAreaException : SypException
    {
        public NotExistAreaException() { }
        public NotExistAreaException(string message): base(message) { }
    }
}
