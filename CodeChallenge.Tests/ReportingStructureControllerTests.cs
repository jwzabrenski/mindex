using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Net;
using System.Net.Http;
using System.Diagnostics;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

namespace CodeChallenge.Tests.Integration
{
    [TestClass]
    public class ReportingStructureControllerTests
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
        public void JohnLennon_Returns_Ok()
        {
            //Should be one due to employee update in EmployeeControllerTests
            RunTest("16a596ae-edd3-4847-99fe-c4518e82c86f", "John", "Lennon", 1);
        }

        [TestMethod]
        public void PaulMcCartney_Returns_Ok()
        {
            //Should be zero
            RunTest("b7839309-3348-463b-a7e3-5de1c168beb3", "Paul", "McCartney");
        }

        [TestMethod]
        public void RingoStarr_Returns_Ok()
        {
            //Should be Pete Best and zero due to employee update in EmployeeControllerTests
            RunTest("03aa1462-ffa9-4978-901b-7c001562cf6f", "Pete", "Best");
        }
        
        private void RunTest(string employeeId, string expectedFirstName, string expectedLastName, int expectedDirectReports = 0)
        {
            var getRequestTask = _httpClient.GetAsync($"api/reportingStructure/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var reportingStructure = response.DeserializeContent<ReportingStructure>();
            
            Assert.AreEqual(expectedFirstName, reportingStructure.Employee.FirstName);
            Assert.AreEqual(expectedLastName, reportingStructure.Employee.LastName);
            Assert.AreEqual(expectedDirectReports, reportingStructure.NumberOfReports);
            Debug.WriteLine(reportingStructure.NumberOfReports.ToString());
        }
    }
}