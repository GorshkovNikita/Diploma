using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Diploma.Models;

namespace DiplomaTest
{
    [TestFixture]
    public class OSMNodeTest
    {
        [Test, TestCaseSource("CreateCases")]
        public void CreateTest(Int64 id, object[] expectedData)
        {
            OSMNode node = OSMNode.Create(id);
            Assert.AreEqual(expectedData[0], id, "IDs are not equal!");
            Assert.AreEqual(expectedData[1], node.Type, "Types are not equal!");
            Assert.AreEqual(expectedData[2], node.Latitude, "Latitudes are not equal!");
            Assert.AreEqual(expectedData[3], node.Longitude, "Longitudes are not equal!");
            Assert.AreEqual(expectedData[4], node.Tags, "Tags are not equal!");
        }

        public IEnumerable<TestCaseData> CreateCases()
        {
            yield return new TestCaseData(
                27717690,
                new object[]
                {
                    27717690,
                    OSMType.NODE,
                    55.8906808,
                    37.4838778,
                    new Dictionary<string, string>()
                    { 
                        { "highway", "traffic_signals" } 
                    }
                }
            );
        }
    }
}
