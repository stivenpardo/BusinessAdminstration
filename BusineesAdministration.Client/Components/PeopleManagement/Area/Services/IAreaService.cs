using BusinessAdministration.Aplication.Dto.PeopleManagement.Area;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusineesAdministration.Client.Components.PeopleManagement.Area.Services
{
    public interface IAreaService
    {
        Task<IEnumerable<AreaDto>> GetAllAreas();
    }
}
