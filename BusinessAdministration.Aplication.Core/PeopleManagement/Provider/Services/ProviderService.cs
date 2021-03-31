using AutoMapper;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.DocumentType;
using BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Person;
using BusinessAdministration.Aplication.Dto.PeopleManagement.Provider;
using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
using BusinessAdministration.Domain.Core.PeopleManagement.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Provider.Services
{
    internal class ProviderService : IProviderService
    {
        private readonly IProviderRepository _repoProvider;
        private readonly IDocumentTypeRepository _repoDocumentType;
        private readonly IMapper _mapper;

        public ProviderService(IProviderRepository repoProvider, IMapper mapper, IDocumentTypeRepository repoDocumentType)
        {
            _repoProvider = repoProvider;
            _repoDocumentType = repoDocumentType;
            _mapper = mapper;
        }
        public async Task<Guid> AddProvider(ProviderDto request)
        {
            ValidateRequireFields(request);
            var providers = _repoProvider.GetAll<ProviderEntity>();
            ValidateIfExistTheSameIdentification(request, providers);
            ValidateIfExistSameName(request, providers);
            ValidateDontHaveDocumentTypeDiferentNit(request, providers);
            var response = await _repoProvider.Insert(_mapper.Map<ProviderEntity>(request)).ConfigureAwait(false);
            return response.ProviderId;
        }

        #region validations generals for people
        public void ValidateIfExistTheSameIdentification(ProviderDto request, IEnumerable<ProviderEntity> people)
        {
            var validateByIdentificactionNumber = people.Where(x =>
                        x.IdentificationNumber == request.IdentificationNumber && x.DocumentTypeId == request.DocumentTypeId);
            if (validateByIdentificactionNumber.Any())
                throw new AlreadyExistException($"ya existe alguien el mismo numero de indentificación y tipo de documento: {request.IdentificationNumber}");
        }
        public void ValidateIfExistSameName(ProviderDto request, IEnumerable<ProviderEntity> people)
        {
            var providersByName = people
                .Where(e => e.PersonName == request.PersonName && e.PersonBusinessName == request.PersonBusinessName);
            if (providersByName.Any())
                throw new AlreadyExistException($"ya existe alguien con el nombre : {request.PersonName} y razon social:{request.PersonBusinessName}");
        }
        public void ValidateDontHaveDocumentTypeDiferentNit(ProviderDto request, IEnumerable<ProviderEntity> people)
        {
            var documentIdExist = _repoDocumentType
                .SearchMatching<DocumentTypeEntity>(dt => dt.DocumentTypeId == request.DocumentTypeId);
            if (!documentIdExist.Any())
                throw new NoExistDocumentTypeException();
            var documentIsNit = documentIdExist.Where(x => x.DocumentType.ToLower() != "nit");

            if (documentIsNit.Any())
                throw new CannotBeCorporatePersonException("Una persona no puede tener un tipo de documento diferente a Nit");
        }
        #endregion validations generals for people
        public bool DeleteProvider(ProviderDto request)
        {
            if (request.ProviderId == default) throw new IdCannotNullOrEmptyException();
            ValidateProviderIdExist(request);
            return _repoProvider.Delete(_mapper.Map<ProviderEntity>(request));
        }

        public async Task<IEnumerable<ProviderDto>> GetAll()
        {
            var response = await Task.FromResult(_mapper.Map<IEnumerable<ProviderDto>>(_repoProvider.GetAll<ProviderEntity>()))
                .ConfigureAwait(false);
            return response;
        }

        public bool UpdateProvider(ProviderDto request)
        {
            if (request.ProviderId == default) throw new IdCannotNullOrEmptyException();
            var ProviderIdExist = _repoProvider
                .SearchMatching<ProviderEntity>(p => p.ProviderId == request.ProviderId);
            if (!ProviderIdExist.Any()) throw new DontExistIdException();
            var entityUpdate = ProviderIdExist.FirstOrDefault();

            entityUpdate.DocumentTypeId = request.DocumentTypeId;
            entityUpdate.IdentificationNumber = request.IdentificationNumber;
            entityUpdate.PersonType = request.PersonType;
            entityUpdate.PersonName = request.PersonName;
            entityUpdate.PersonBusinessName = request.PersonBusinessName;
            entityUpdate.PersonLastName = request.PersonLastName;
            entityUpdate.PersonDateOfBirth = request.PersonDateOfBirth;
            entityUpdate.CreationDate = request.CreationDate;
            entityUpdate.PersonPhoneNumber = request.PersonPhoneNumber;
            entityUpdate.PersonEmail = request.PersonEmail;
            return _repoProvider.Update(entityUpdate);
        }
        private void ValidateProviderIdExist(ProviderDto request)
        {
            var ProviderIdExist = _repoProvider
                .SearchMatching<ProviderEntity>(p => p.ProviderId == request.ProviderId).Any();
            if (!ProviderIdExist) throw new DontExistIdException();
        }
        private static void ValidateRequireFields(ProviderDto request)
        {
            if (request.DocumentTypeId == Guid.Empty) throw new DocumentTypeIdNotDefinedException();
            if (request.PersonDateOfBirth == default) throw new DateOfBirthNotDefinedException();
            if (request.CreationDate == default) throw new CreationDateNotDefinedException();
        }
    }
}
