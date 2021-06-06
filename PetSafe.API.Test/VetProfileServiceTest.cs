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
    class VetProfileServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoVetProfileFoundReturnsVetProfileNotFoundResponse()
        {
            //Arrange
            var mockVetProfileRepository = GetDefaultIVetProfileRepositoryInstance();
            var mockVetVeterinaryRepository = GetDefaultIVetVeterinaryRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockCityRepository = GetDefaultICityRepositoryInstance();
            var mockProvinceRepository = GetDefaultIProvinceRepositoryInstance();
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var vetProfileId = 1;
            mockVetProfileRepository.Setup(r => r.FindById(vetProfileId))
                .Returns(Task.FromResult<VetProfile>(null));

            var service = new VetProfileService(mockVetProfileRepository.Object, mockVetVeterinaryRepository.Object, mockUnitOfWork.Object,
                mockProvinceRepository.Object,mockCityRepository.Object,mockUserRepository.Object);

            //Act
            VetProfileResponse result = await service.GetByIdAsync(vetProfileId);
            var message = result.Message;

            //Assert
            message.Should().Be("VetProfile not found");
        }

        [Test]
        public async Task SaveAsyncWhenSaveReturnsSaved()
        {
            //Arrange
            var mockVetProfileRepository = GetDefaultIVetProfileRepositoryInstance();
            var mockVetVeterinaryRepository = GetDefaultIVetVeterinaryRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockCityRepository = GetDefaultICityRepositoryInstance();
            var mockProvinceRepository = GetDefaultIProvinceRepositoryInstance();
            var mockUserRepository = GetDefaultIUserRepositoryInstance();

            User user = new User { Id = 1, Mail = "a@gmail.com" ,UserTypeVet=true};
            Province province = new Province { Id = 1, Name = "Lima" };
            City city = new City { Id = 10, Name = "SJL" ,ProvinceId=1};
            VetProfile vetProfile = new VetProfile { Id = 10, Name = "Jose", Code = 15 };

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


            mockVetProfileRepository.Setup(r => r.AddAsync(vetProfile))
                .Returns(Task.FromResult<VetProfile>(null));

            var service = new VetProfileService(mockVetProfileRepository.Object, mockVetVeterinaryRepository.Object, mockUnitOfWork.Object,
                mockProvinceRepository.Object, mockCityRepository.Object, mockUserRepository.Object);

            //Act
            VetProfileResponse result = await service.SaveAsync(1,10,1,vetProfile);
            
            //Assert
            result.Resource.Should().Be(vetProfile);
        }

        private Mock<IVetProfileRepository> GetDefaultIVetProfileRepositoryInstance()
        {
            return new Mock<IVetProfileRepository>();
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
        private Mock<IUserRepository> GetDefaultIUserRepositoryInstance()
        {
            return new Mock<IUserRepository>();
        }
    }
}
