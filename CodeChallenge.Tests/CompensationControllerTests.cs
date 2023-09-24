using System;
using System.Net;
using System.Net.Http;
using System.Text;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            // Arrange
            var employee = new Employee()
            {
                EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                Department = "Music",
                FirstName = "John",
                LastName = "Lennon",
                Position = "Vocals",
            };

            var compensation = new Compensation
            {
                EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                Salary = 87500m,
                EffectiveDate = new System.DateTime(1961, 03, 18)
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
            new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensation.CompensationId);
            Assert.AreEqual(compensation.Salary, newCompensation.Salary);
            Assert.AreEqual(compensation.EffectiveDate, newCompensation.EffectiveDate);
            Assert.AreEqual(compensation.EmployeeId, newCompensation.EmployeeId);
        }

        [TestMethod]
        public void GetCompensationByEmployeeById_Returns_Ok()
        {
            // Arrange
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var expectedSalary = 87500m;
            var expectedEffectiveDate = new System.DateTime(1961, 03, 18);

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var compensation = response.DeserializeContent<Compensation>();
            
            Assert.AreEqual(expectedSalary, compensation.Salary);
            Assert.AreEqual(expectedEffectiveDate, compensation.EffectiveDate);
            Assert.AreEqual(employeeId, compensation.EmployeeId);
        }
    }
}