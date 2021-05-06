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
    class PetProfileServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoCityFoundReturnsCityNotFoundResponse()
        {
            //Arrange 
            var mockPetProfileRepository = GetDefaultIPetProfileRepositoryInstance();
            var mockPetOwnerRepository = GetDefaultIPetOwnerRepositoryInstance();
            var mockPetIllnessRepository = GetDefaultIPetIllnessRepositoryInstance();
            var mockPetTreatmentRepository = GetDefaultIPetTreatmentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var petProfileId = 1;
            mockPetProfileRepository.Setup(r => r.FindById(petProfileId))
                .Returns(Task.FromResult<PetProfile>(null));

            var service = new PetProfileService(mockUnitOfWork.Object, mockPetTreatmentRepository.Object, mockPetIllnessRepository.Object
                , mockPetOwnerRepository.Object, mockPetProfileRepository.Object);

            //Act
            PetProfileResponse result = await service.GetByIdAsync(petProfileId);
            var message = result.Message;

            //Assert
            message.Should().Be("Pet Profile not found");
        }

        [Test]
        public async Task SaveAsyncWhenSaveReturnsSaved()
        {
            //Arrange 
            var mockPetProfileRepository = GetDefaultIPetProfileRepositoryInstance();
            var mockPetOwnerRepository = GetDefaultIPetOwnerRepositoryInstance();
            var mockPetIllnessRepository = GetDefaultIPetIllnessRepositoryInstance();
            var mockPetTreatmentRepository = GetDefaultIPetTreatmentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            PetProfile petProfile = new PetProfile { Id = 10, Name = "Toby" };
            mockPetProfileRepository.Setup(r => r.AddAsync(petProfile))
                .Returns(Task.FromResult<PetProfile>(petProfile));
            
            var service = new PetProfileService(mockUnitOfWork.Object, mockPetTreatmentRepository.Object, mockPetIllnessRepository.Object
               , mockPetOwnerRepository.Object, mockPetProfileRepository.Object);

            //Act
            PetProfileResponse result = await service.SaveAsync(petProfile);

            //Assert
            result.Resource.Should().Be(petProfile);

        }

        private Mock<IPetProfileRepository> GetDefaultIPetProfileRepositoryInstance()
        {
            return new Mock<IPetProfileRepository>();
        }
        private Mock<IPetOwnerRepository> GetDefaultIPetOwnerRepositoryInstance()
        {
            return new Mock<IPetOwnerRepository>();
        }
        private Mock<IPetIllnessRepository> GetDefaultIPetIllnessRepositoryInstance()
        {
            return new Mock<IPetIllnessRepository>();
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
