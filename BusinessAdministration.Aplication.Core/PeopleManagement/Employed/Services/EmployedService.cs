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
    public class EmployedService : IEmployedService
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
            //TODO: Implemented test for each one aceptation criterial for persons an employed 
            ValidateRequireFields(request);
            var employeesByName = _repoEmployed
                .SearchMatching<EmployedEntity>(e => e.PersonName == request.Name);

            if (employeesByName.Any())
                throw new AlreadyExistException($"ya existe alguien con el nombre:  {request.IdentificationNumber}");

            var employeesByNameAndId = employeesByName.Where(x => x.IdentificationNumber == request.IdentificationNumber);
            if (employeesByNameAndId.Any())
                throw new AlreadyExistException($"ya existe alguien con la combinacion de nombre: {request.Name} el nombre:  {request.IdentificationNumber}");

            throw new NotImplementedException();
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
            if (request.EmployeeCode == Guid.Empty) throw new EmployedCodeNotDefinedException();
            if (request.AreaId == Guid.Empty) throw new AreaIdNotDefinedException();
            if (request.DocumentTypeId == Guid.Empty) throw new DocumentTypeIdNotDefinedException();
            if (request.DateOfBirth == default) throw new DateOfBirthNotDefinedException();
            if (request.creationDate == default) throw new CreationDateNotDefinedException();

        }
    }
}
