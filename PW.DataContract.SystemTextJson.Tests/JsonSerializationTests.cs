using Microsoft.VisualStudio.TestTools.UnitTesting;
using PW.DataContract.SystemTextJson.Tests.Models;
using System.Text.Json;

namespace PW.DataContract.SystemTextJson.Tests
{
    [TestClass]
    public class JsonSerializationTests
    {
        [TestMethod]
        public void Serialize_DataMember_IgnoreDataMember_WithoutDataContract()
        {
            var user = new UserWithProperties
            {
                Id = 1,
                FirstName = "Bill",
                LastName = "Gates",
                Permission = Permission.Administrator
            };

            var options = new JsonSerializerOptions
            {
                TypeInfoResolver = new DataContractJsonResolver()
            };

            var actualJson = JsonSerializer.Serialize(user, options);
            var expectedJson = """{"Id":1,"FirstName":"Bill","Surname":"Gates","Permission":0}""";

            Assert.AreEqual(expectedJson, actualJson);
        }
    }
}