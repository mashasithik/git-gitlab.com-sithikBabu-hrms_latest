using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Threading.Tasks;

namespace HRMS.Adecco.WebAPI.xUnitTest
{
    public class IntegrationTest : IDisposable
    {
        private readonly HttpClient _client;
        readonly string _apiURL;
        public IntegrationTest()
        {
            //_apiURL = "https://localhost:44388/api";
            //Use a configuration file to store the base Address
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44388/api")  //  Replace with your test API URL
            };
        }

        [Fact]
        public async Task TestEmployeeList()
        {
            //Arrange
            //var response = await _client.GetAsync("/Transaction/Employee/getEmployeeDetails");
            //Act
            //var response = await client.GetAsync("/Transaction/Employee/getEmployeeDetails");
            //int code = (int)response.StatusCode;
            //Assert
            //Assert.Equal(200, code);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}