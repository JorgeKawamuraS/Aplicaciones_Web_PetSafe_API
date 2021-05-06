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
    class TreatmentServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoTreatmentFoundReturnsTreatmentNotFoundResponse()
        {
            //Arrange
            var mockTreatmentRepository = GetDefaultITreatmentRepositoryInstance();
            var mockPetTreatmentRepository = GetDefaultIPetTreatmentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var treatmentId = 1;
            mockTreatmentRepository.Setup(r => r.FindById(treatmentId))
                .Returns(Task.FromResult<Treatment>(null));

            var service = new TreatmentService(mockTreatmentRepository.Object, mockPetTreatmentRepository.Object, mockUnitOfWork.Object);

            //Act
            TreatmentResponse result = await service.GetByIdAsync(treatmentId);
            var message = result.Message;

            //Assert
            message.Should().Be("Treatment not found");

        }

        [Test]
        public async Task SaveAsyncWhenSaveReturnsSaved()
        {

            //Arrange
            var mockTreatmentRepository = GetDefaultITreatmentRepositoryInstance();
            var mockPetTreatmentRepository = GetDefaultIPetTreatmentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            Treatment treatment= new Treatment { Id=10,Name="Pastillas"};
            mockTreatmentRepository.Setup(r => r.AddAsync(treatment))
                .Returns(Task.FromResult<Treatment>(treatment));

            var service = new TreatmentService(mockTreatmentRepository.Object, mockPetTreatmentRepository.Object, mockUnitOfWork.Object);

            //Act
            TreatmentResponse result = await service.SaveAsync(treatment);

            //Assert
            result.Resource.Should().Be(treatment);
        }

        private Mock<ITreatmentRepository> GetDefaultITreatmentRepositoryInstance()
        {
            return new Mock<ITreatmentRepository>();
        }
        private Mock<IPetTreatmentRepository> GetDefaultIPetTreatmentRepositoryInstance()
        {
            return new Mock<IPetTreatmentRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
