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
                .SearchMatching<EmployedEntity>(employed => employed.EmployedId == request.LiableEmployerId)
                .Any();
            if (!employedIdExist)
                throw new AreaEmployeIdDontExistException(request.LiableEmployerId.ToString());

            var employedLiableExist = _repoArea
                .SearchMatching<AreaEntity>(area => area.LiableEmployerId == request.LiableEmployerId)
                .Any();

            if (employedLiableExist)
                throw new AreaLiableAlreadyExistException(request.LiableEmployerId.ToString());

            var response = await _repoArea.Insert(_mapper.Map<AreaEntity>(request)).ConfigureAwait(false);
            return response.AreaId;
        }
        private static void ValidateRequireFields(AreaRequestDto request)
        {
            if (string.IsNullOrEmpty(request.AreaName)) throw new AreaNameNotDefinedException();
            if (request.LiableEmployerId == Guid.Empty) throw new AreaLiableEmployeedIdNotDefinedException();
        }

        public async Task<IEnumerable<AreaDto>> GetAll()
        {
            var response = _mapper.Map<IEnumerable<AreaDto>>(_repoArea.GetAll<AreaEntity>());
            if (response.Count() == 0) throw new AreaEntityIsEmptyException();
            return response;
        }

        public bool DeleteArea(AreaDto request)
        {
            if (request.AreaId == Guid.Empty) throw new AreaIdNotDefinedException();

            var AreaIdExist = _repoEmployed
                .SearchMatching<EmployedEntity>(employed => employed.AreaId == request.AreaId)
                .Any();
            if (AreaIdExist)
                throw new AreaIdIsAssociatedToEmployedException(request.AreaId.ToString());

            return _repoArea.Delete(_mapper.Map<AreaEntity>(request));
        }
        public bool UpdateArea(AreaDto request)
        {
            if (request.AreaId == Guid.Empty) throw new AreaIdNotDefinedException();
            return _repoArea.Update(_mapper.Map<AreaEntity>(request));
        }
    }
}
