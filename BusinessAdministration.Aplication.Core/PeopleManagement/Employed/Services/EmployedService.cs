using AutoMapper;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Area;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.DocumentType;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Employed;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Person;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
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
        private readonly IMapper _mapper;

        public EmployedService(IEmployedRepository repoEmployed, IMapper mapper)
        {
            _repoEmployed = repoEmployed;
            _mapper = mapper;
        }

        public async Task<Guid?> AddEmployed(EmployedDto request)
        {
            ValidateRequireFields(request);
            var employees = _repoEmployed.GetAll<EmployedEntity>();
            ValidateIfExistTheSameIdentification(request, employees);
            ValidateIfExistSameName(request, employees);
            ValidateDontHaveDocumentTypeNit(request, employees);
            ValidateCannotBeCorporatePerson(request, employees);
            throw new NotImplementedException();
        }

        public void ValidateDontHaveDocumentTypeNit(EmployedDto request, IEnumerable<EmployedEntity> people)
        {
            var validateByDocumentType = people.Where(x => x.DocumentType.DocumentType == request.DocumentType);

            if (validateByDocumentType.Any())
                throw new CannotBeCorporatePersonException($"Una persona no puede tener un tipo de documento: { request.DocumentType}");
        }
        public void ValidateCannotBeCorporatePerson(EmployedDto request, IEnumerable<EmployedEntity> people)
        {
            var validateCorporatePerson = people.Where(x => x.PersonType == request.PersonType);

            if (!validateCorporatePerson.Any())
                throw new CannotBeCorporatePersonException($"Una empleado no puede ser: { request.PersonType}");
        }

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
