using AutoMapper;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Area;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Area;
using BusinessAdministration.Domain.Core.PeopleManagement.Area;
using BusinessAdministration.Domain.Core.PeopleManagement.Employed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Area.Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _repoArea;
        private readonly IEmployedRepository _repoEmployed;
        private readonly IMapper _mapper;

        public AreaService(IAreaRepository repo, IMapper mapper, IEmployedRepository repoEmployed)
        {
            _repoArea = repo;
            _repoEmployed = repoEmployed;
            _mapper = mapper;
        }
        public async Task<Guid?> AddArea(AreaRequestDto request)
        {
            ValidateRequireFields(request);

            var employedIdExist = _repoEmployed
                .SearchMatching<EmployedEntity>(employed => employed.EmployedId == request.ResponsableEmployedId)
                .Any();
            if (!employedIdExist)
                throw new AreaEmployeIdDontExistException(request.ResponsableEmployedId.ToString());

            var employedLiableExist = _repoArea
                .SearchMatching<AreaEntity>(area => area.ResponsableEmployedId == request.ResponsableEmployedId)
                .Any();

            if (employedLiableExist)
                throw new AreaLiableAlreadyExistException(request.ResponsableEmployedId.ToString());

            var response = await _repoArea.Insert(_mapper.Map<AreaEntity>(request)).ConfigureAwait(false);
            return response.AreaId;
        }

        private static void ValidateRequireFields(AreaRequestDto request)
        {
            if (string.IsNullOrEmpty(request.AreaName))
                throw new AreaNameNotDefinedException();

            if (request.ResponsableEmployedId == Guid.Empty)
                throw new AreaLiableEmployeedIdNotDefinedException();
        }

        public async Task<IEnumerable<AreaDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool DeleteArea(AreaDto request)
        {
            throw new NotImplementedException();
        }


        public bool UpdateArea(AreaDto request)
        {
            throw new NotImplementedException();
        }
    }
}
