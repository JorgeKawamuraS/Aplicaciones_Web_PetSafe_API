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
    class UserServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoUserFoundReturnsUserNotFoundResponse()
        {
            //Arrange
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var mockUserPlanReposiroty = GetDefaultIUserPlanRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var userId = 1;
            mockUserRepository.Setup(r => r.FindByIdAsync(userId))
                .Returns(Task.FromResult<User>(null));

            var service = new UserService(mockUserRepository.Object, mockUserPlanReposiroty.Object, mockUnitOfWork.Object);

            //Act
            UserResponse result = await service.GetByIdAsync(userId);
            var message = result.Message;

            //Assert
            message.Should().Be("User not found");
        }

        [Test]
        public async Task SaveAsyncWhenSaveReturnsSaved()
        {
            //Arrange
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var mockUserPlanReposiroty = GetDefaultIUserPlanRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            User user = new User { Id=10,Mail="ab@gmail.com",Password="password"};
            mockUserRepository.Setup(r => r.AddAsync(user))
                .Returns(Task.FromResult<User>(user));

            var service = new UserService(mockUserRepository.Object, mockUserPlanReposiroty.Object, mockUnitOfWork.Object);

            //Act
            UserResponse result = await service.SaveAsync(user);
            
            //Assert
            result.Resource.Should().Be(user);
        }




        private Mock<IUserRepository> GetDefaultIUserRepositoryInstance()
        {
            return new Mock<IUserRepository>();
        }
        private Mock<IUserPlanRepository> GetDefaultIUserPlanRepositoryInstance()
        {
            return new Mock<IUserPlanRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
