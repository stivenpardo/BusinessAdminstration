using BusinessAdministration.Aplication.Core.Base.Exceptions;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Area
{
    public class AreaEmployeIdDontExistException : SypException
    {
        public AreaEmployeIdDontExistException() { }
        public AreaEmployeIdDontExistException(string message): base(message) { }
    }
}
