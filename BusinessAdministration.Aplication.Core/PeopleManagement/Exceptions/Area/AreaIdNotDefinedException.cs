using BusinessAdministration.Aplication.Core.Base.Exceptions;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Area
{
    public class AreaIdNotDefinedException : SypException
    {
        public AreaIdNotDefinedException() { }
        public AreaIdNotDefinedException(string message) : base(message) { }
        
    }
}
