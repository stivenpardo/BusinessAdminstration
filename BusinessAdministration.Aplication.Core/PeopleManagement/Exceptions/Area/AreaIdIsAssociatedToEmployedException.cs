using BusinessAdministration.Aplication.Core.Base.Exceptions;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Area
{
    public class AreaIdIsAssociatedToEmployedException : SypException
    {
        public AreaIdIsAssociatedToEmployedException() { }
        public AreaIdIsAssociatedToEmployedException(string message): base(message) { }
    }
}
