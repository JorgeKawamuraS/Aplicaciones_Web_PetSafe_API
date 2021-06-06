using NUnit.Framework;
using Moq;
using FluentAssertions;
using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using PetSafe.API.Domain.Persistence.Repositories;
using PetSafe.API.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PetSafe.API.Test
{
    class PetProfileServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoCityFoundReturnsCityNotFoundResponse()
        {
            //Arrange 
            var mockPetProfileRepository = GetDefaultIPetProfileRepositoryInstance();
            var mockPetOwnerRepository = GetDefaultIPetOwnerRepositoryInstance();
            var mockPetIllnessRepository = GetDefaultIPetIllnessRepositoryInstance();
            var mockPetTreatmentRepository = GetDefaultIPetTreatmentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockCityRepository = GetDefaultICityRepositoryInstance();
            var mockProvinceRepository = GetDefaultIProvinceRepositoryInstance();
            var petProfileId = 1;
            mockPetProfileRepository.Setup(r => r.FindById(petProfileId))
                .Returns(Task.FromResult<PetProfile>(null));

            var service = new PetProfileService(mockUnitOfWork.Object, mockPetTreatmentRepository.Object, mockPetIllnessRepository.Object
                , mockPetOwnerRepository.Object, mockPetProfileRepository.Object,mockProvinceRepository.Object,mockCityRepository.Object);

            //Act
            PetProfileResponse result = await service.GetByIdAsync(petProfileId);
            var message = result.Message;

            //Assert
            message.Should().Be("Pet Profile not found");
        }

        [Test]
        public async Task SaveAsyncWhenSaveReturnsSaved()
        {
            //Arrange 
            var mockPetProfileRepository = GetDefaultIPetProfileRepositoryInstance();
            var mockPetOwnerRepository = GetDefaultIPetOwnerRepositoryInstance();
            var mockPetIllnessRepository = GetDefaultIPetIllnessRepositoryInstance();
            var mockPetTreatmentRepository = GetDefaultIPetTreatmentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockCityRepository = GetDefaultICityRepositoryInstance();
            var mockProvinceRepository = GetDefaultIProvinceRepositoryInstance();

            Province province = new Province { Id = 1, Name = "Lima" };
            City city = new City { Id = 10, Name = "SJL" , ProvinceId=1};
            PetProfile petProfile = new PetProfile { Id = 10, Name = "Toby" };

            mockProvinceRepository.Setup(p => p.AddAsync(province))
                .Returns(Task.FromResult<Province>(province));

            mockProvinceRepository.Setup(p => p.FindById(1))
                .Returns(Task.FromResult<Province>(province));

            mockCityRepository.Setup(r => r.AddAsync(city))
                .Returns(Task.FromResult<City>(city));

            mockCityRepository.Setup(p => p.FindById(10))
                .Returns(Task.FromResult<City>(city));

            mockPetProfileRepository.Setup(r => r.AddAsync(petProfile))
                .Returns(Task.FromResult<PetProfile>(petProfile));

            var service = new PetProfileService(mockUnitOfWork.Object, mockPetTreatmentRepository.Object, mockPetIllnessRepository.Object
                , mockPetOwnerRepository.Object, mockPetProfileRepository.Object, mockProvinceRepository.Object, mockCityRepository.Object);

            //Act
            PetProfileResponse result = await service.SaveAsync(10,1,petProfile);

            //Assert
            result.Resource.Should().Be(petProfile);

        }

        private Mock<IPetProfileRepository> GetDefaultIPetProfileRepositoryInstance()
        {
            return new Mock<IPetProfileRepository>();
        }
        private Mock<IPetOwnerRepository> GetDefaultIPetOwnerRepositoryInstance()
        {
            return new Mock<IPetOwnerRepository>();
        }
        private Mock<IPetIllnessRepository> GetDefaultIPetIllnessRepositoryInstance()
        {
            return new Mock<IPetIllnessRepository>();
        }
        private Mock<IPetTreatmentRepository> GetDefaultIPetTreatmentRepositoryInstance()
        {
            return new Mock<IPetTreatmentRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
        private Mock<ICityRepository> GetDefaultICityRepositoryInstance()
        {
            return new Mock<ICityRepository>();
        }
        private Mock<IProvinceRepository> GetDefaultIProvinceRepositoryInstance()
        {
            return new Mock<IProvinceRepository>();
        }
    }
}
