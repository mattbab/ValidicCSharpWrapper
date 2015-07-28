using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using ValidicCSharp;
using ValidicCSharp.Interfaces;
using ValidicCSharp.Model;
using ValidicCSharp.Request;

namespace ValidicCSharpTests
{
    public class ClientTests : BaseTests
    {
        private const string UserUnderTest = "51c7dc676dedda04f9000011";
        private const string OrganizationUnderTest = "51aca5a06dedda916400002b";

        private static List<ICommandFilter> GetFilters
        {
            get
            {
                var filters = new List<ICommandFilter> {new FromDateFilter {Value = "09-01-01"}};
                return filters;
            }
        }

        [Test]
        public void EnterpriseBulkSleepDataPopulates()
        {
            var client = new Client {AccessToken = "ENTERPRISE_KEY"};
            var sleepData = Task.Run(async () => await client.GetEnterpriseSleepData(OrganizationUnderTest, GetFilters)).Result;

            Assert.IsTrue(sleepData.Object.Count > 0);
            Assert.IsTrue(sleepData.Object.First().Id != null);
        }

        [Test]
        public void EnterpriseUserDiabetesDataPopulates()
        {
            var client = new Client {AccessToken = "ENTERPRISE_KEY"};
            var diabetesData = Task.Run(async () => await client.GetEnterpriseUserDiabetesData(UserUnderTest, OrganizationUnderTest, GetFilters)).Result;

            Assert.IsTrue(diabetesData.Object.Count > 0);
            Assert.IsTrue(diabetesData.Object.First().Id != null);
        }

        [Test]
        public void GetEnterpriseFitnessData()
        {
            var client = new Client {AccessToken = "ENTERPRISE_KEY"};
            var data = Task.Run(async () => await client.GetEnterpriseFitnessData(OrganizationUnderTest, GetFilters)).Result;
            Assert.IsTrue(data.Object.Count > 0);
            Assert.IsTrue(data.Object.First().Id != null);
        }

        [Test]
        public void GetEnterpriseUserFitnessData()
        {
            var client = new Client {AccessToken = "ENTERPRISE_KEY"};
            var data = Task.Run(async () => await client.GetEnterpriseUserFitnessData(UserUnderTest, OrganizationUnderTest, GetFilters)).Result;
            Assert.IsTrue(data.Object.Count > 0);
            Assert.IsTrue(data.Object.First().Id != null);
        }

#if TEMP
        [Test]
        public void GetUserFitnessData()
        {
            var client = new Client();
            var data = client.GetUserFitnessData(UserUnderTest, GetFilters);

            Assert.IsTrue(data.Object.Count > 0);
            Assert.IsTrue(data.Object.First().Id != null);
        }
#endif

        [Test]
        public void TestForNullNutritionData()
        {
            var obj =
                "{\"_id\":\"55549ffd409efe36f60247ca\",\"timestamp\":\"2015-05-14T11:50:40+00:00\",\"utc_offset\":\"-04:00\",\"calories\": null,\"carbohydrates\": null,\"fat\": null,\"fiber\": null,\"protein\": null,\"sodium\": null,\"water\": null,\"meal\":\"Coffee: 8 fl oz\",\"source\":\"jawbone_up\",\"source_name\":\"Jawbone Up\",\"last_updated\":\"2015-05-16T06:05:10+00:00\",\"xid\":\"4RxHnh1nqGv28Fwi3gJ_XlATkP2CFlhV\",\"title\": 0,\"time_updated\":\"1431689639\",\"time_completed\":\"1431604240\",\"polyunsaturated_fat\": null,\"potassium\": null,\"carbohydrate\": null,\"saturated_fat\": null,\"num_foods\": 0,\"monounsaturated_fat\": null,\"tz\":\"America/New_York\",\"vitamin_c\": null,\"vitamin_a\": null,\"unsaturated_fat\": null,\"place_type\": null,\"sugar\": null,\"num_drinks\": 1,\"calcium\": null,\"iron\": null,\"cholesterol\": null,\"accuracy\": 100 }";

            var nutrition = JsonConvert.DeserializeObject<Nutrition>(obj);
            Assert.True(nutrition != null);
        }
    }
}