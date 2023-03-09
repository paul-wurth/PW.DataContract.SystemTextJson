using Microsoft.VisualStudio.TestTools.UnitTesting;
using PW.DataContract.SystemTextJson.Tests.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace PW.DataContract.SystemTextJson.Tests
{
    [TestClass]
    public class NewtonsoftCompatibilityTests
    {
        private static IEnumerable<object[]> GetTestCases()
        {
            yield return new object[] { new Person() };
            yield return new object[] { new Person() { FullName = "John Doe" } };
            yield return new object[] { new Person() { Age = 21 } };
            yield return new object[] { new Person() { FullName = "John Doe", Age = 21 } };

            yield return new object[] { new PersonWithNonPublicMember() };
            yield return new object[] { new PersonWithNonPublicMember() { FullName = "John Doe" } };

            yield return new object[] { new PersonContractWithoutDataMember() };
            yield return new object[] { new PersonContractWithoutDataMember() { FullName = "John Doe" } };

            yield return new object[] { new PersonWithIgnore() };
            yield return new object[] { new PersonWithIgnore() { FullName = "John Doe" } };
            yield return new object[] { new PersonWithIgnore() { Age = 21 } };
            yield return new object[] { new PersonWithIgnore() { FullName = "John Doe", Age = 21 } };

            yield return new object[] { new PersonWithoutContractWithDataMember() };
            yield return new object[] { new PersonWithoutContractWithDataMember() { FullName = "John Doe" } };
            yield return new object[] { new PersonWithoutContractWithDataMember() { Age = 21 } };
            yield return new object[] { new PersonWithoutContractWithDataMember() { FullName = "John Doe", Age = 21 } };

            yield return new object[] { new PersonGetSet() };
            yield return new object[] { new PersonGetSet() { FullName = "John Doe" } };
            yield return new object[] { new PersonGetSet() { Age = 21 } };
            yield return new object[] { new PersonGetSet() { FullName = "John Doe", Age = 21 } };

            yield return new object[] { new PersonContract() };
            yield return new object[] { new PersonContract() { FullName = "John Doe" } };
            yield return new object[] { new PersonContract() { Age = 21 } };
            yield return new object[] { new PersonContract() { FullName = "John Doe", Age = 21 } };

            yield return new object[] { new PersonContractMemberGetter() };
            yield return new object[] { new PersonContractMemberGetter() { FullName = "John Doe" } };

            yield return new object[] { new PersonContractWithNonPublicMember() };
            yield return new object[] { new PersonContractWithNonPublicMember() { FullName = "John Doe" } };

            yield return new object[] { new PersonContractOverrideName() };
            yield return new object[] { new PersonContractOverrideName() { FullName = "John Doe" } };
            yield return new object[] { new PersonContractOverrideName() { Age = 21 } };
            yield return new object[] { new PersonContractOverrideName() { FullName = "John Doe", Age = 21 } };

            yield return new object[] { new PersonContractOrdered() };
            yield return new object[] { new PersonContractOrdered() { FullName = "John Doe" } };
            yield return new object[] { new PersonContractOrdered() { Age = 21 } };
            yield return new object[] { new PersonContractOrdered() { FullName = "John Doe", Age = 21 } };

            yield return new object[] { new PersonWithDictionary() };
            yield return new object[] { new PersonWithDictionary() { FullName = "John Doe" } };
            yield return new object[] { new PersonWithDictionary() { Age = 21 } };
            yield return new object[] { new PersonWithDictionary() { FullName = "John Doe", Age = 21 } };
            yield return new object[] { new PersonWithDictionary() { FullName = "John Doe", Age = 21, Dict = new Dictionary<string, string> { { "Key1", "Value1" }, { "Key2", "Value2" } } } };

            yield return new object[] { new PersonContractWithIgnore() };
            yield return new object[] { new PersonContractWithIgnore() { FullName = "John Doe" } };
            yield return new object[] { new PersonContractWithIgnore() { Age = 21 } };
            yield return new object[] { new PersonContractWithIgnore() { FullName = "John Doe", Age = 21 } };

            yield return new object[] { new PersonContractWithStruct() };
            yield return new object[] { new PersonContractWithStruct() { FullName = "John Doe" } };
            yield return new object[] { new PersonContractWithStruct() { Age = 21 } };
            yield return new object[] { new PersonContractWithStruct() { FullName = "John Doe", Age = 21, LastLogin = DateTime.UtcNow } };
            yield return new object[] { new PersonContractWithStruct() { FullName = "John Doe", Age = 21, LastLogin = DateTime.Now } };

            yield return new object[] { new PersonContractWithNullable() };
            yield return new object[] { new PersonContractWithNullable() { FullName = "John Doe" } };
            yield return new object[] { new PersonContractWithNullable() { Age = 21 } };
            yield return new object[] { new PersonContractWithNullable() { FullName = "John Doe", Age = 21, LastLogin = DateTime.UtcNow } };
            yield return new object[] { new PersonContractWithNullable() { FullName = "John Doe", Age = 21, LastLogin = DateTime.Now } };
        }

        [TestMethod]
        [DynamicData(nameof(GetTestCases), DynamicDataSourceType.Method)]
        public void Serialize_LikeNewtonsoft(object obj)
        {
            var options = new JsonSerializerOptions
            {
                TypeInfoResolver = new DataContractJsonResolver(),
                IncludeFields = true
            };

            string actualJson = JsonSerializer.Serialize(obj, options);
            string expectedJson = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            Assert.AreEqual(expectedJson, actualJson);
        }
    }
}
