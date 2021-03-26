using BusinessAdministration.Aplication.Core.Base.Exceptions;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Employed
{
    public class EmployeIdDontExistException : SypException
    {
        public EmployeIdDontExistException() { }
        public EmployeIdDontExistException(string message): base(message) { }
    }
}
