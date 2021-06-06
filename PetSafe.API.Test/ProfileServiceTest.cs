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
    class ProfileServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoCityFoundReturnsCityNotFoundResponse()
        {
            var mockProfileRepository = GetDefaultIProfileRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockCityRepository = GetDefaultICityRepositoryInstance();
            var mockProvinceRepository = GetDefaultIProvinceRepositoryInstance();
            var profileId = 1;
            mockProfileRepository.Setup(r => r.FindByIdAsync(profileId))
                .Returns(Task.FromResult<Profile>(null));

            var service = new ProfileService(mockUnitOfWork.Object, mockProfileRepository.Object
                , mockProvinceRepository.Object, mockCityRepository.Object);

            //Act
            ProfileResponse result = await service.GetByIdAsync(profileId);
            var message = result.Message;

            //Assert
            message.Should().Be("Profile not found");

        }

        [Test]
        public async Task SaveAsyncWhenSaveReturnsSaved()
        {
            var mockProfileRepository = GetDefaultIProfileRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockCityRepository = GetDefaultICityRepositoryInstance();
            var mockProvinceRepository = GetDefaultIProvinceRepositoryInstance();

            Province province = new Province { Id = 1, Name = "Lima" };
            City city = new City { Id = 10, Name = "SJL" ,ProvinceId=1};
            Profile profile = new Profile { Name = "Julio", Id = 10 };

            mockProvinceRepository.Setup(p => p.AddAsync(province))
                .Returns(Task.FromResult<Province>(province));

            mockProvinceRepository.Setup(p => p.FindById(1))
                .Returns(Task.FromResult<Province>(province));

            mockCityRepository.Setup(r => r.AddAsync(city))
                .Returns(Task.FromResult<City>(city));

            mockCityRepository.Setup(p => p.FindById(10))
                .Returns(Task.FromResult<City>(city));

            mockProfileRepository.Setup(r => r.AddAsync(profile))
                .Returns(Task.FromResult<Profile>(profile));

            var service = new ProfileService(mockUnitOfWork.Object, mockProfileRepository.Object
                , mockProvinceRepository.Object, mockCityRepository.Object);

            //Act
            ProfileResponse result = await service.SaveAsync(10,1,profile);
            
            //Assert
            result.Resource.Should().Be(profile);
        }


        private Mock<IProfileRepository> GetDefaultIProfileRepositoryInstance()
        {
            return new Mock<IProfileRepository>();
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
