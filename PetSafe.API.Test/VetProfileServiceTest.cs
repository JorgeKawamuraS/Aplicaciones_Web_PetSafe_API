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
            var vetProfileId = 1;
            mockVetProfileRepository.Setup(r => r.FindById(vetProfileId))
                .Returns(Task.FromResult<VetProfile>(null));

            var service = new VetProfileService(mockVetProfileRepository.Object, mockVetVeterinaryRepository.Object, mockUnitOfWork.Object);

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
            VetProfile vetProfile = new VetProfile { Id = 10, Name = "Jose", Code = 15 };
            mockVetProfileRepository.Setup(r => r.AddAsync(vetProfile))
                .Returns(Task.FromResult<VetProfile>(null));

            var service = new VetProfileService(mockVetProfileRepository.Object, mockVetVeterinaryRepository.Object, mockUnitOfWork.Object);

            //Act
            VetProfileResponse result = await service.SaveAsync(vetProfile);
            
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
    }
}
