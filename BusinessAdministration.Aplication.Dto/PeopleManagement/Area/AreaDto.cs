using BusinessAdministration.Aplication.Dto.Base;
using System;

namespace BusinessAdministration.Aplication.Dto.PeopleManagement.Area
{
    public class AreaDto : DataTransferObject
    {
        public Guid AreaId { get; set; }
        public string AreaName { get; set; }
        public Guid ResponsableEmployedId { get; set; }
    }
}
