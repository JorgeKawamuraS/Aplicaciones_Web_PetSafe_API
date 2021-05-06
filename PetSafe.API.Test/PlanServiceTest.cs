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
    class PlanServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetByIdAsyncWhenNoPlanFoundReturnsPlanNotFoundResponse()
        {
            //Arrange
            var mockPlanRepository = GetDefaultIPlanRepositoryInstance();
            var mockPlanUserRepository = GetDefaultIUserPlanRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var planId = 1;
            mockPlanRepository.Setup(r => r.FindById(planId))
                .Returns(Task.FromResult<Plan>(null));

            var service = new PlanService(mockPlanRepository.Object, mockPlanUserRepository.Object, mockUnitOfWork.Object);

            //Act
            PlanResponse result = await service.GetByIdAsync(planId);
            var message = result.Message;

            //Assert
            message.Should().Be("Plan not found");
        }

        [Test]
        public async Task SaveAsyncWhenSaveReturnsSaved()
        {
            //Arrange
            var mockPlanRepository = GetDefaultIPlanRepositoryInstance();
            var mockPlanUserRepository = GetDefaultIUserPlanRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            Plan plan = new Plan { Name = "Free", Price = 20 };
            mockPlanRepository.Setup(r => r.AddAsync(plan))
                .Returns(Task.FromResult<Plan>(plan));

            var service = new PlanService(mockPlanRepository.Object, mockPlanUserRepository.Object, mockUnitOfWork.Object);

            //Act
            PlanResponse result = await service.SaveAsync(plan);

            //Assert
            result.Resource.Should().Be(plan);

        }

        private Mock<IPlanRepository> GetDefaultIPlanRepositoryInstance()
        {
            return new Mock<IPlanRepository>();
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
