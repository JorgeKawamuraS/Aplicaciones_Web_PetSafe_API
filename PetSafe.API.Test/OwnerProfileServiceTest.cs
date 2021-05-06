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
            var ownerProfileId = 1;
            mockOwnerProfileRepository.Setup(r => r.FindById(ownerProfileId))
                .Returns(Task.FromResult<OwnerProfile>(null));

            var service = new OwnerProfileService(mockUnitOfWork.Object, mockPetOwnerRepository.Object, mockOwnerLocationRepository.Object, mockOwnerProfileRepository.Object);

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
            OwnerProfile ownerProfile = new OwnerProfile { Id = 10, Name = "Julio" };
            mockOwnerProfileRepository.Setup(r => r.AddAsync(ownerProfile))
                .Returns(Task.FromResult<OwnerProfile>(ownerProfile));


            var service = new OwnerProfileService(mockUnitOfWork.Object, mockPetOwnerRepository.Object, mockOwnerLocationRepository.Object, mockOwnerProfileRepository.Object);

            //Act
            OwnerProfileResponse result = await service.SaveAsync(ownerProfile);

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
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }

    }
}
