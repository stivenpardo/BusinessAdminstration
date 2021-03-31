using AutoMapper;
using BusinessAdministration.Aplication.Core.ExportAndImportJSON;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.DocumentType;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Person;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Employed;
using BusinessAdministration.Domain.Core.PeopleManagement.Customer;
using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Customer.Services
{
    internal class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repoCustomer;
        private readonly IDocumentTypeRepository _repoDocumentType;
        private readonly IExportAndImportJson _imporExportjson;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository repoCustomer, IMapper mapper, IDocumentTypeRepository repoDocumentType, IExportAndImportJson imporExportjson)
        {
            _repoCustomer = repoCustomer;
            _repoDocumentType = repoDocumentType;
            _imporExportjson = imporExportjson;
            _mapper = mapper;
        }
        public async Task<Guid> AddCustomer(CustomerDto request)
        {
            ValidateRequireFields(request);
            var customers = _repoCustomer.GetAll<CustomerEntity>();
            ValidateIfExistTheSameIdentification(request, customers);
            ValidateIfExistSameName(request, customers);
            ValidateDontHaveDocumentTypeNit(request, customers);
            var response = await _repoCustomer.Insert(_mapper.Map<CustomerEntity>(request)).ConfigureAwait(false);
            return response.CustomerId;
        }

        #region validations generals for people
        public void ValidateIfExistTheSameIdentification(CustomerDto request, IEnumerable<CustomerEntity> people)
        {
            var validateByIdentificactionNumber = people.Where(x =>
                        x.IdentificationNumber == request.IdentificationNumber && x.DocumentTypeId == request.DocumentTypeId);
            if (validateByIdentificactionNumber.Any())
                throw new AlreadyExistException($"ya existe alguien el mismo numero de indentificación y tipo de documento: {request.IdentificationNumber}");
        }
        public void ValidateIfExistSameName(CustomerDto request, IEnumerable<CustomerEntity> people)
        {
            var customersByName = people.Where(e => e.PersonName == request.PersonName);
            if (customersByName.Any())
                throw new AlreadyExistException($"ya existe alguien con el nombre:  {request.PersonName}");
        }
        public void ValidateDontHaveDocumentTypeNit(CustomerDto request, IEnumerable<CustomerEntity> people)
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
        public bool DeleteCustomer(CustomerDto request)
        {
            if (request.CustomerId == default) throw new IdCannotNullOrEmptyException();
            ValidateCustomerIdExist(request);
            return _repoCustomer.Delete(_mapper.Map<CustomerEntity>(request));
        }

        public async Task<IEnumerable<CustomerDto>> GetAll()
        {
            var response = await Task.FromResult(_mapper.Map<IEnumerable<CustomerDto>>(_repoCustomer.GetAll<CustomerEntity>()))
                .ConfigureAwait(false);
            return response;
        }

        public bool UpdateCustomer(CustomerDto request)
        {
            if (request.CustomerId == default) throw new IdCannotNullOrEmptyException();
            var CustomerIdExist = _repoCustomer
                .SearchMatching<CustomerEntity>(c => c.CustomerId == request.CustomerId);
            if (!CustomerIdExist.Any()) throw new DontExistIdException();
            var entityUpdate = CustomerIdExist.FirstOrDefault();

            entityUpdate.DocumentTypeId = request.DocumentTypeId;
            entityUpdate.IdentificationNumber = request.IdentificationNumber;
            entityUpdate.PersonType = request.PersonType;
            entityUpdate.PersonName = request.PersonName;
            entityUpdate.PersonLastName = request.PersonLastName;
            entityUpdate.PersonDateOfBirth = request.PersonDateOfBirth;
            entityUpdate.CreationDate = request.CreationDate;
            entityUpdate.PersonPhoneNumber = request.PersonPhoneNumber;
            entityUpdate.PersonEmail = request.PersonEmail;
            return _repoCustomer.Update(entityUpdate);
        }
        public async Task<string> ExportAll()
        {
            var listEntity = _repoCustomer.GetAll<CustomerEntity>();
            return await _imporExportjson.ExportJson("ExportAllProveedor", _mapper.Map<IEnumerable<CustomerDto>>(listEntity)).ConfigureAwait(false);
        }
        public async Task<IEnumerable<CustomerDto>> ImportAll()
        {
            var customerDto = await _imporExportjson.ImportJson<IEnumerable<CustomerDto>>("ExportAllProveedor").ConfigureAwait(false);
            foreach (CustomerDto element in customerDto)
            {
                UpdateCustomer(element);
            }
            return customerDto;
        }
        private void ValidateCustomerIdExist(CustomerDto request)
        {
            var CustomerIdExist = _repoCustomer
                .SearchMatching<CustomerEntity>(c => c.CustomerId == request.CustomerId).Any();
            if (!CustomerIdExist) throw new DontExistIdException();
        }
        private static void ValidateRequireFields(CustomerDto request)
        {
            if (request.DocumentTypeId == Guid.Empty) throw new DocumentTypeIdNotDefinedException();
            if (request.PersonDateOfBirth == default) throw new DateOfBirthNotDefinedException();
            if (request.CreationDate == default) throw new CreationDateNotDefinedException();
        }
    }
}
