using AutoMapper;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Area;
using BusinessAdministration.Domain.Core.PeopleManagement.Area;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Area.Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _repoArea;
        private readonly IMapper _mapper;

        public AreaService(IAreaRepository repo, IMapper mapper)
        {
            _repoArea = repo;
            _mapper = mapper;
        }
        public async Task<Guid?> AddArea(AreaRequestDto request)
        {
            throw new NotImplementedException();
        }

        public bool DeleteArea(AreaDto request)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AreaDto>> GetAll(AreaRequestDto request)
        {
            throw new NotImplementedException();
        }

        public bool UpdateArea(AreaDto request)
        {
            throw new NotImplementedException();
        }
    }
}
