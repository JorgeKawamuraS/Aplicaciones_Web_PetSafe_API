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
    class VeterinaryProfileServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoVeterinaryProfileFoundReturnsVeterinaryProfileNotFoundResponse()
        {
            //Arrange
            var mockVeterinaryProfileRepository = GetDefaultIVeterinaryProfileRepositoryInstance();
            var mockVeterinarySpecialtyRepository = GetDefaultIVeterinarySpecialtyRepositoryInstance();
            var mockVetVeterinaryRepository = GetDefaultIVetVeterinaryRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockCityRepository = GetDefaultICityRepositoryInstance();
            var mockProvinceRepository = GetDefaultIProvinceRepositoryInstance();
            var veterinaryProfileId = 1;
            mockVeterinaryProfileRepository.Setup(r => r.FindById(veterinaryProfileId))
                .Returns(Task.FromResult<VeterinaryProfile>(null));

            var service = new VeterinaryProfileService(mockVeterinaryProfileRepository.Object, mockVeterinarySpecialtyRepository.Object
                , mockVetVeterinaryRepository.Object, mockUnitOfWork.Object,mockProvinceRepository.Object,mockCityRepository.Object);

            //Act
            VeterinaryProfileResponse result = await service.GetByIdAsync(veterinaryProfileId);
            var message = result.Message;

            //Assert
            message.Should().Be("Veterinary not found");
        }

        [Test]
        public async Task SaveAsyncWhenSaveReturnsSaved()
        {
            //Arrange
            var mockVeterinaryProfileRepository = GetDefaultIVeterinaryProfileRepositoryInstance();
            var mockVeterinarySpecialtyRepository = GetDefaultIVeterinarySpecialtyRepositoryInstance();
            var mockVetVeterinaryRepository = GetDefaultIVetVeterinaryRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockCityRepository = GetDefaultICityRepositoryInstance();
            var mockProvinceRepository = GetDefaultIProvinceRepositoryInstance();

            Province province = new Province { Id = 1, Name = "Lima" };
            City city = new City { Id = 10, Name = "SJL" , ProvinceId=1};
            VeterinaryProfile veterinaryProfile = new VeterinaryProfile { Id = 10, Name = "Huellitas" };

            mockProvinceRepository.Setup(p => p.AddAsync(province))
                .Returns(Task.FromResult<Province>(province));

            mockProvinceRepository.Setup(p => p.FindById(1))
                .Returns(Task.FromResult<Province>(province));

            mockCityRepository.Setup(r => r.AddAsync(city))
                .Returns(Task.FromResult<City>(city));


            mockCityRepository.Setup(p => p.FindById(10))
                .Returns(Task.FromResult<City>(city));

            mockVeterinaryProfileRepository.Setup(r => r.AddAsync(veterinaryProfile))
                .Returns(Task.FromResult<VeterinaryProfile>(veterinaryProfile));

            var service = new VeterinaryProfileService(mockVeterinaryProfileRepository.Object, mockVeterinarySpecialtyRepository.Object
                , mockVetVeterinaryRepository.Object, mockUnitOfWork.Object, mockProvinceRepository.Object, mockCityRepository.Object);

            //Act
            VeterinaryProfileResponse result = await service.SaveAsync(10,1,veterinaryProfile);
            
            //Assert
            result.Resource.Should().Be(veterinaryProfile);
        }



        private Mock<IVeterinaryProfileRepository> GetDefaultIVeterinaryProfileRepositoryInstance()
        {
            return new Mock<IVeterinaryProfileRepository>();
        }
        private Mock<IVeterinarySpecialtyRepository> GetDefaultIVeterinarySpecialtyRepositoryInstance()
        {
            return new Mock<IVeterinarySpecialtyRepository>();
        }
        private Mock<IVetVeterinaryRepository> GetDefaultIVetVeterinaryRepositoryInstance()
        {
            return new Mock<IVetVeterinaryRepository>();
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
