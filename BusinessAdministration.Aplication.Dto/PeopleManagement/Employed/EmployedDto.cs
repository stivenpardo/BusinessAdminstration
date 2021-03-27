using BusinessAdministration.Domain.Core.PeopleManagement;
using BusinessAdministration.Domain.Core.PeopleManagement.Employed;
using System;

namespace BusinessAdministration.Aplication.Dto.PeopleManagement.Employed
{
    public class EmployedDto : PersonDto
    {
        public Guid EmployedId { get; set; }
        public  PersonType PersonType { get; set; }
        public EmployedCode EmployedCode { get; set; }
        public EmployedPosition EmployedPosition { get; set; }
        public Guid AreaId { get; set; }
    }
}
