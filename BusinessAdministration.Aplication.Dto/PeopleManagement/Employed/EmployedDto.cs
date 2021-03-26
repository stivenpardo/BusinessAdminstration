using BusinessAdministration.Domain.Core.PeopleManagement;
using System;

namespace BusinessAdministration.Aplication.Dto.PeopleManagement.Employed
{
    public class EmployedDto : PersonDto
    {
        public Guid EmployedId { get; set; }
        public  PersonType PersonType { get; set; }
        public Guid EmployeeCode { get; set; }
        public string EmployedPosition { get; set; }
        public Guid AreaId { get; set; }
        public Guid ResponsableEmployedId { get; set; }
    }
}
