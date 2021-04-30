using BusinessAdministration.Aplication.Dto.PeopleManagement.Area;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.ClientesHttp
{
    public interface IAreaClienteHttp
    {
        Task<IEnumerable<AreaDto>> GetAll();
    }
}