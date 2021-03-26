using BusinessAdministration.Aplication.Dto.Base;
using System;

namespace BusinessAdministration.Aplication.Dto.PeopleManagement.Employed
{
    public class EmployedRequestDto : DataTransferObject
    {
        public Guid EmployedId { get; set; }
    }
}
