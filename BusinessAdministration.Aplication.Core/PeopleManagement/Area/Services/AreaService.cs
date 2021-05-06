using AutoMapper;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Area;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Person;
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
            if (string.IsNullOrEmpty(request.LiableEmployerId.ToString()))
            {
                var responseArea = await _repoArea.Insert(_mapper.Map<AreaEntity>(request)).ConfigureAwait(false);
                return responseArea.AreaId;
            }
            var employedIdExist = _repoEmployed
                .SearchMatching<EmployedEntity>(employed => employed.EmployedId == request.LiableEmployerId).Any();
            if (!employedIdExist)
                throw new AreaEmployeIdDontExistException($"El empleado con el id: {request.LiableEmployerId} no existe");

            var employedLiableExist = _repoArea
                .SearchMatching<AreaEntity>(area => area.LiableEmployerId == request.LiableEmployerId)
                .Any();
            if (employedLiableExist)
                throw new AreaLiableAlreadyExistException($"El empleado con el id: {request.LiableEmployerId} ya esta asignado a una area");

            var response = await _repoArea.Insert(_mapper.Map<AreaEntity>(request)).ConfigureAwait(false);
            return response.AreaId;
        }

        public async Task<IEnumerable<AreaDto>> GetAll()
        {
            var response = _mapper.Map<IEnumerable<AreaDto>>(_repoArea.GetAll<AreaEntity>());
            if (!response.Any()) throw new AreaEntityIsEmptyException();
            return response;
        }

        public bool DeleteArea(AreaDto request)
        {
            ValidateAreIdRequired(request);
            ValidationAreIdExist(request);
            var lialEmployedId = _repoEmployed
                .SearchMatching<EmployedEntity>(employed => employed.AreaId == request.AreaId)
                .Any();
            if (lialEmployedId)
                throw new AreaIdIsAssociatedToEmployedException($"Este id: {request.AreaId} ya esta asociado con un empleado");

            return _repoArea.Delete(_mapper.Map<AreaEntity>(request));
        }
        public bool UpdateArea(AreaDto request)
        {
            ValidateAreIdRequired(request);
            var areaIdExist = _repoArea.SearchMatching<AreaEntity>(a => a.AreaId == request.AreaId);
            if (!areaIdExist.Any()) throw new DontExistIdException();
            var entityUpdate = areaIdExist.FirstOrDefault();
            entityUpdate.AreaName = request.AreaName;
            entityUpdate.LiableEmployerId = request.LiableEmployerId;
            return _repoArea.Update(entityUpdate);
        }
        private static void ValidateRequireFields(AreaRequestDto request)
        {
            if (string.IsNullOrEmpty(request.AreaName)) throw new AreaNameNotDefinedException();
            if (request.LiableEmployerId == Guid.Empty) throw new AreaLiableEmployeedIdNotDefinedException();
        }
        private static void ValidateAreIdRequired(AreaDto request)
        {
            if (request.AreaId == Guid.Empty)
                throw new AreaIdNotDefinedException();
        }
        private void ValidationAreIdExist(AreaDto request)
        {
            var areaIdExist = _repoArea.SearchMatching<AreaEntity>(a => a.AreaId == request.AreaId).Any();
            if (!areaIdExist) throw new DontExistIdException();
        }
    }
}
