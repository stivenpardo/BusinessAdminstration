using BusinessAdministration.Aplication.Dto.PeopleManagement.Area;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Area.HttpClientArea
{
    public interface IAreaClienteHttp
    {
        Task<IEnumerable<AreaDto>> GetAll();
    }
}