using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetSafe.API.Domain.Models;
using SpecFlowPetSafe.Features;
using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowPetSafe.Steps
{
    [Binding]
    public class UserCreationStepDefinition : BaseTest
    {
        private string UserEndPoint { get; set; }

        public UserCreationStepDefinition(ScenarioContext scenarioContext)
        {
            UserEndPoint = $"{ApiUri}api/user";
        }


        [When("user want to create an account")]
        public void UserWantToCreateAnAccount(Table dto)
        {
            try
            {
                var user = dto.CreateInstance<User>();
                var data = JsonData(user);
                var result = Task.Run(async()=>await Client.PostAsync(UserEndPoint,data)).Result;
                //Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK,"Add User Integration Test Completed");
                result.StatusCode.Should().Be(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                Assert.IsTrue(false,ex.Message);
            }
        }

    }
}
