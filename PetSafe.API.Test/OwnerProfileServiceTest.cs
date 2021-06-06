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
    class OwnerProfileServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoOwnerProfileFoundReturnsOwnerProfileNotFoundResponse()
        {
            //Arrange
            var mockOwnerProfileRepository = GetDefaultIOwnerProfileRepositoryInstance();
            var mockOwnerLocationRepository = GetDefaultIOwnerLocationRepositoryInstance();
            var mockPetOwnerRepository = GetDefaultIPetOwnerRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockProvinceRepository = GetDefaultIProvinceRepositoryInstance();
            var mockCityRepository = GetDefaultICityRepositoryInstance();
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var ownerProfileId = 1;
            mockOwnerProfileRepository.Setup(r => r.FindById(ownerProfileId))
                .Returns(Task.FromResult<OwnerProfile>(null));

            var service = new OwnerProfileService(mockUnitOfWork.Object, mockPetOwnerRepository.Object, mockOwnerLocationRepository.Object, 
                mockOwnerProfileRepository.Object,mockProvinceRepository.Object,mockCityRepository.Object,mockUserRepository.Object);

            //Act
            OwnerProfileResponse result = await service.GetByIdAsync(ownerProfileId);
            var message = result.Message;

            //Assert
            message.Should().Be("Owner Profile not found");
        }

        
        [Test]
        public async Task SaveAsyncWhenSaveReturnsSaved()
        {
            //Arrange
            var mockOwnerProfileRepository = GetDefaultIOwnerProfileRepositoryInstance();
            var mockOwnerLocationRepository = GetDefaultIOwnerLocationRepositoryInstance();
            var mockPetOwnerRepository = GetDefaultIPetOwnerRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockProvinceRepository = GetDefaultIProvinceRepositoryInstance();
            var mockCityRepository = GetDefaultICityRepositoryInstance();
            var mockUserRepository = GetDefaultIUserRepositoryInstance();

            User user = new User { Id=1, Mail="a@gmail.com", UserTypeVet=false};
            Province province = new Province { Id = 1, Name = "Lima" };
            City city = new City { Id = 10, Name = "SJL" , ProvinceId=1};
            OwnerProfile ownerProfile = new OwnerProfile { Id = 10, Name = "Julio" };

            mockUserRepository.Setup(p => p.AddAsync(user))
                .Returns(Task.FromResult<User>(user));

            mockUserRepository.Setup(p => p.FindByIdAsync(1))
                .Returns(Task.FromResult<User>(user));

            mockProvinceRepository.Setup(p => p.AddAsync(province))
                .Returns(Task.FromResult<Province>(province));

            mockProvinceRepository.Setup(p => p.FindById(1))
                .Returns(Task.FromResult<Province>(province));

            mockCityRepository.Setup(r => r.AddAsync(city))
                .Returns(Task.FromResult<City>(city));

            mockCityRepository.Setup(p => p.FindById(10))
                .Returns(Task.FromResult<City>(city));

            mockOwnerProfileRepository.Setup(r => r.AddAsync(ownerProfile))
                .Returns(Task.FromResult<OwnerProfile>(ownerProfile));

            var service = new OwnerProfileService(mockUnitOfWork.Object, mockPetOwnerRepository.Object, mockOwnerLocationRepository.Object,
                mockOwnerProfileRepository.Object, mockProvinceRepository.Object, mockCityRepository.Object, mockUserRepository.Object);

            //Act
            OwnerProfileResponse result = await service.SaveAsync(1,10,1,ownerProfile);

            //Arrange
            result.Resource.Should().Be(ownerProfile);
        }

        private Mock<IOwnerLocationRepository> GetDefaultIOwnerLocationRepositoryInstance()
        {
            return new Mock<IOwnerLocationRepository>();
        }
        private Mock<IOwnerProfileRepository> GetDefaultIOwnerProfileRepositoryInstance()
        {
            return new Mock<IOwnerProfileRepository>();
        }
        private Mock<IPetOwnerRepository> GetDefaultIPetOwnerRepositoryInstance()
        {
            return new Mock<IPetOwnerRepository>();
        }
        private Mock<ICityRepository> GetDefaultICityRepositoryInstance()
        {
            return new Mock<ICityRepository>();
        }
        private Mock<IUserRepository> GetDefaultIUserRepositoryInstance()
        {
            return new Mock<IUserRepository>();
        }
        private Mock<IProvinceRepository> GetDefaultIProvinceRepositoryInstance()
        {
            return new Mock<IProvinceRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }

    }
}
