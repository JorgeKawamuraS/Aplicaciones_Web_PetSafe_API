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
            var profileId = 1;
            mockProfileRepository.Setup(r => r.FindByIdAsync(profileId))
                .Returns(Task.FromResult<Profile>(null));

            var service = new ProfileService(mockUnitOfWork.Object, mockProfileRepository.Object);

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
            Profile profile = new Profile { Name = "Julio", Id = 10 };
            mockProfileRepository.Setup(r => r.AddAsync(profile))
                .Returns(Task.FromResult<Profile>(profile));

            var service = new ProfileService(mockUnitOfWork.Object, mockProfileRepository.Object);

            //Act
            ProfileResponse result = await service.SaveAsync(profile);
            
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
    }
}
