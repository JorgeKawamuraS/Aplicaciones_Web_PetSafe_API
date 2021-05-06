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
    class IllnessServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoCityFoundReturnsIllnesNotFoundResponse()
        {
            //Arrange
            var mockIllnessRepository = GetDefaultIIllnessRepositoryInstance();
            var mockPetIllnessRepository = GetDefaultIPetIllnessRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var illnessId = 1;
            mockIllnessRepository.Setup(r => r.FindById(illnessId))
                .Returns(Task.FromResult<Illness>(null));

            var service = new IllnessService(mockUnitOfWork.Object,mockPetIllnessRepository.Object, mockIllnessRepository.Object);

            //Act
            IllnessResponse result = await service.GetByIdAsync(illnessId);
            var message = result.Message;

            //Assert
            message.Should().Be("Illness not found");
        }

        [Test]
        public async Task SaveAsyncWhenSaveReturnsSaved()
        {
            //Arrange
            var mockIllnessRepository = GetDefaultIIllnessRepositoryInstance();
            var mockPetIllnessRepository = GetDefaultIPetIllnessRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            Illness illness = new Illness { Id=10,Name="Diabetes"};
            mockIllnessRepository.Setup(r => r.AddAsync(illness))
                .Returns(Task.FromResult<Illness>(illness));

            var service = new IllnessService(mockUnitOfWork.Object, mockPetIllnessRepository.Object, mockIllnessRepository.Object);

            //Act 
            IllnessResponse result = await service.SaveAsync(illness);

            //Assert
            result.Resource.Should().Be(illness);
        }

        private Mock<IIllnessRepository> GetDefaultIIllnessRepositoryInstance()
        {
            return new Mock<IIllnessRepository>();
        }
        private Mock<IPetIllnessRepository> GetDefaultIPetIllnessRepositoryInstance()
        {
            return new Mock<IPetIllnessRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
