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
    public class CityServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoCityFoundReturnsCityNotFoundResponse()
        {
            //Arrange
            var mockCityRepository = GetDefaultICityRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockOwnerLocationRepository = GetDefaultIOwnerLocationRepositoryInstance();
            var mockProvinceRepository = GetDefaultIProvinceRepositoryInstance();
            var cityId = 1;
            mockCityRepository.Setup(r => r.FindById(cityId))
                .Returns(Task.FromResult<City>(null));

            var service = new CityService(mockCityRepository.Object,mockOwnerLocationRepository.Object,
                mockUnitOfWork.Object,mockProvinceRepository.Object);

            //Act
            CityResponse result = await service.GetByIdAsync(cityId);
            var message = result.Message;

            //Assert
            message.Should().Be("City not found");

        }

        [Test]
        public async Task SaveAsyncWhenSaveReturnsSaved()
        {
            //Arrange
            var mockCityRepository = GetDefaultICityRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockOwnerLocationRepository = GetDefaultIOwnerLocationRepositoryInstance();
            var mockProvinceRepository = GetDefaultIProvinceRepositoryInstance();
            Province province = new Province { Id = 1, Name = "Lima" };
            City city = new City { Id = 10, Name = "SJL"};

            mockProvinceRepository.Setup(p => p.AddAsync(province))
                .Returns(Task.FromResult<Province>(province));

            mockProvinceRepository.Setup(p=>p.FindById(1))
                .Returns(Task.FromResult<Province>(province));

            mockCityRepository.Setup(r => r.AddAsync(city))
                .Returns(Task.FromResult<City>(city));


            var service = new CityService(mockCityRepository.Object, mockOwnerLocationRepository.Object,
                mockUnitOfWork.Object, mockProvinceRepository.Object);

            //Act
            CityResponse result = await service.SaveAsync(1,city);

            //Assert
            result.Resource.Should().Be(city);
        }

        private Mock<IOwnerLocationRepository> GetDefaultIOwnerLocationRepositoryInstance()
        {
            return new Mock<IOwnerLocationRepository>();
        }
        private Mock<ICityRepository> GetDefaultICityRepositoryInstance()
        {
            return new Mock<ICityRepository>();
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