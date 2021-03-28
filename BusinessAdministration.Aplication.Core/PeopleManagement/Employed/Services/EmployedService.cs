using AutoMapper;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Area;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.DocumentType;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Employed;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Person;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
using BusinessAdministration.Domain.Core.PeopleManagement.Area;
using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
using BusinessAdministration.Domain.Core.PeopleManagement.Employed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Employed.Services
{
    internal class EmployedService : IEmployedService
    {
        private readonly IEmployedRepository _repoEmployed;
        private readonly IAreaRepository _repoArea;
        private readonly IDocumentTypeRepository _repoDocumentType;
        private readonly IMapper _mapper;

        public EmployedService(IEmployedRepository repoEmployed, IMapper mapper, IAreaRepository repoArea, IDocumentTypeRepository repoDocumentType)
        {
            _repoEmployed = repoEmployed;
            _repoArea = repoArea;
            _repoDocumentType = repoDocumentType;
            _mapper = mapper;
        }

        public async Task<Guid> AddEmployed(EmployedDto request)
        {
            ValidateRequireFields(request);
            var employees = _repoEmployed.GetAll<EmployedEntity>();
            ValidateIfExistTheSameIdentification(request, employees);
            ValidateIfExistSameName(request, employees);
            ValidateDontHaveDocumentTypeNit(request, employees);
            ValidateCannotBeCorporatePerson(request, employees);
            ValidateHaveUniqueCode(request, employees);
            ValidateAsginatedArea(request, employees);

            var areaExist = _repoArea.SearchMatching<AreaEntity>(a => a.AreaId == request.AreaId).Any();
            if (!areaExist) throw new NotExistAreaException($"No existe el area del siguiente Id: { request.AreaId}");
            var response = await _repoEmployed.Insert(_mapper.Map<EmployedEntity>(request)).ConfigureAwait(false);
            return response.EmployedId;
        }

        #region validations generals for people
        public void ValidateIfExistTheSameIdentification(EmployedDto request, IEnumerable<EmployedEntity> people)
        {
            var validateByIdentificactionNumber = people.Where(x =>
                        x.IdentificationNumber == request.IdentificationNumber && x.DocumentTypeId == request.DocumentTypeId);
            if (validateByIdentificactionNumber.Any())
                throw new AlreadyExistException($"ya existe alguien el mismo numero de indentificación y tipo de documento: {request.IdentificationNumber}");
        }
        public void ValidateIfExistSameName(EmployedDto request, IEnumerable<EmployedEntity> people)
        {
            var employeesByName = people.Where(e => e.PersonName == request.PersonName);
            if (employeesByName.Any())
                throw new AlreadyExistException($"ya existe alguien con el nombre:  {request.PersonName}");
        }
        public void ValidateDontHaveDocumentTypeNit(EmployedDto request, IEnumerable<EmployedEntity> people)
        {
            var documentIdExist = _repoDocumentType
                .SearchMatching<DocumentTypeEntity>(dt => dt.DocumentTypeId == request.DocumentTypeId);
            if (!documentIdExist.Any())
                throw new NoExistDocumentTypeException();
            var documentIsNit = documentIdExist.Where(x => x.DocumentType.ToLower() == "nit");

            if (documentIsNit.Any())
                throw new CannotBeCorporatePersonException("Una persona no puede tener un tipo de documento Nit");
        }
        #endregion validations generals for people
        #region validations for employed
        public void ValidateCannotBeCorporatePerson(EmployedDto request, IEnumerable<EmployedEntity> people)
        {
            var validateCorporatePerson = people.Where(x => x.PersonType == request.PersonType);

            if (!validateCorporatePerson.Any())
                throw new CannotBeCorporatePersonException($"Una empleado no puede ser: { request.PersonType}");
        }
        public void ValidateHaveUniqueCode(EmployedDto request, IEnumerable<EmployedEntity> people)
        {
            var validateUniqueCode= people.Where(x => x.EmployedCode == request.EmployedCode);

            if (validateUniqueCode.Any())
                throw new CannotBeCorporatePersonException($"El empleado no tiene un código unnico: { request.EmployedCode}");
        } 
        public void ValidateAsginatedArea(EmployedDto request, IEnumerable<EmployedEntity> people)
        {
            var validateAreaExist= people.Where(x => x.AreaId == request.AreaId);

            if (validateAreaExist.Any())
                throw new AlreadyExistException($"La area : { request.AreaId} ya fue asignada");
        }

        #endregion 
        public bool DeleteEmployed(EmployedRequestDto request)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EmployedDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool UpdateEmployed(EmployedDto request)
        {
            throw new NotImplementedException();
        }
        private static void ValidateRequireFields(EmployedDto request)
        {
            if (request.EmployedCode == default) throw new EmployedCodeNotDefinedException();
            if (request.AreaId == Guid.Empty) throw new AreaIdNotDefinedException();
            if (request.DocumentTypeId == Guid.Empty) throw new DocumentTypeIdNotDefinedException();
            if (request.PersonDateOfBirth == default) throw new DateOfBirthNotDefinedException();
            if (request.CreationDate == default) throw new CreationDateNotDefinedException();
        }
    }
}
