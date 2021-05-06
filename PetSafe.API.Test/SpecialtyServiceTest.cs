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
    class SpecialtyServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoSpecialtyFoundReturnsSpecialtyNotFoundResponse()
        {
            //Assert
            var mockSpecialtyRepository = GetDefaultISpecialtyRepositoryInstance();
            var mockVeterinarySpecialtyRepository = GetDefaultIVeterinarySpecialtyRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            int specialtyId = 1;
            mockSpecialtyRepository.Setup(r => r.FindById(specialtyId))
                .Returns(Task.FromResult<Specialty>(null));

            var service = new SpecialtyService(mockSpecialtyRepository.Object, mockVeterinarySpecialtyRepository.Object, mockUnitOfWork.Object);

            //Act
            SpecialtyResponse result = await service.GetByIdAsync(specialtyId);
            var message = result.Message;

            //Assert
            message.Should().Be("Specialty not found");
        }

        [Test]
        public async Task SaveAsyncWhenSaveReturnsSaved()
        {
            //Assert
            var mockSpecialtyRepository = GetDefaultISpecialtyRepositoryInstance();
            var mockVeterinarySpecialtyRepository = GetDefaultIVeterinarySpecialtyRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            Specialty specialty = new Specialty { Id=10, Name="Baños"};
            mockSpecialtyRepository.Setup(r => r.AddAsync(specialty))
                .Returns(Task.FromResult<Specialty>(specialty));

            var service = new SpecialtyService(mockSpecialtyRepository.Object, mockVeterinarySpecialtyRepository.Object, mockUnitOfWork.Object);

            //Act
            SpecialtyResponse result = await service.SaveAsync(specialty);
            
            //Assert
            result.Resource.Should().Be(specialty);
        }

        private Mock<ISpecialtyRepository> GetDefaultISpecialtyRepositoryInstance()
        {
            return new Mock<ISpecialtyRepository>();
        }
        private Mock<IVeterinarySpecialtyRepository> GetDefaultIVeterinarySpecialtyRepositoryInstance()
        {
            return new Mock<IVeterinarySpecialtyRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
