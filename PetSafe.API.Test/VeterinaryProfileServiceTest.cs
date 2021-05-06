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
            var veterinaryProfileId = 1;
            mockVeterinaryProfileRepository.Setup(r => r.FindById(veterinaryProfileId))
                .Returns(Task.FromResult<VeterinaryProfile>(null));

            var service = new VeterinaryProfileService(mockVeterinaryProfileRepository.Object, mockVeterinarySpecialtyRepository.Object
                , mockVetVeterinaryRepository.Object, mockUnitOfWork.Object);

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
            VeterinaryProfile veterinaryProfile = new VeterinaryProfile { Id = 10, Name = "Huellitas" };
            mockVeterinaryProfileRepository.Setup(r => r.AddAsync(veterinaryProfile))
                .Returns(Task.FromResult<VeterinaryProfile>(veterinaryProfile));

            var service = new VeterinaryProfileService(mockVeterinaryProfileRepository.Object, mockVeterinarySpecialtyRepository.Object
                , mockVetVeterinaryRepository.Object, mockUnitOfWork.Object);

            //Act
            VeterinaryProfileResponse result = await service.SaveAsync(veterinaryProfile);
            
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
    }
}
