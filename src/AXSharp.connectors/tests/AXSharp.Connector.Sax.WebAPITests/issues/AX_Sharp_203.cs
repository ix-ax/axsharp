using AXSharp.Connector.S71500.WebApi;
using AXSharp.Connector.S71500.WebAPITests.Primitives;
using AXSharp.Connector.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace AXSharp.Connector.S71500.WebAPITests.issues
{
    public class AX_Sharp_203
    {
        private readonly ITestOutputHelper report;
        protected static WebApiConnector Connector { get; } = TestConnector.TestApiConnector as WebApiConnector;
        public AX_Sharp_203(ITestOutputHelper output)
        {
            report = output;
            TestConnector.TestApiConnector.ReadWriteCycleDelay = 2;
        }

        [Fact()]
        //if we have a date in leap year after february, not correct date is provided from webapi
        public async Task reproduction_leap_year()
        {
            var expected = new DateOnly(2000, 3, 20);
            var baseTypeMember = new WebApiDate(Connector, "", $"myLeapDATE");
            var readDate = await baseTypeMember.GetAsync();


            report.WriteLine(readDate.ToString());

            Assert.Equal(expected, readDate);
        }


        [Fact()]
        //if we have a date after leap year before february, not correct date is provided from webapi
        public async Task reproduction_after_leap_year()
        {
            var expected = new DateOnly(2001,2, 10);
            var baseTypeMember = new WebApiDate(Connector, "", $"myAfterLeapDATE");
            var readDate = await baseTypeMember.GetAsync();


            report.WriteLine(readDate.ToString());
            Assert.Equal(expected, readDate);
        }

        [Fact()]
        //dates should be different, but are same
        public async Task reproduction_leap_year_edge_case()
        {
            var leap1 = new WebApiDate(Connector, "", $"myEdgeCaseLeapDATE1");
            var leap2 = new WebApiDate(Connector, "", $"myEdgeCaseLeapDATE2");
            var readLeap1 = await leap1.GetAsync();
            var readLeap2 = await leap2.GetAsync();

            report.WriteLine($"Expected : 02-27-2001 Actual: {readLeap1.ToString()}");
            report.WriteLine($"Expected : 02-28-2001 Actual: {readLeap2.ToString()}");

            // are same, but should be different
            Assert.Equal(readLeap1, readLeap2);
        }

        [Fact()]
        //last date in december is within leap year read as a new year
        public async Task reproduction_leap_end_year()
        {
            var expected = new DateOnly(2000, 12, 31);
            var leapEnd = new WebApiDate(Connector, "", $"myLeapEndDATE");
            var readLeap = await leapEnd.GetAsync();

            report.WriteLine($"Expected : 12-31-2000 Actual: {readLeap.ToString()}");
            Assert.Equal(expected, readLeap);
        }
    }
}
