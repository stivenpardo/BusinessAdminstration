using BusinessAdministration.Aplication.Dto.Base;
using System;

namespace BusinessAdministration.Aplication.Dto.PeopleManagement.Area
{
    public class AreaRequestDto : DataTransferObject
    {
        public string AreaName { get; set; }
        public Guid? LiableEmployerId { get; set; }
    }
}
