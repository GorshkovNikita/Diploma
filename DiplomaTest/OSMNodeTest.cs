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
            Assert.AreEqual(id, expectedData[0], "IDs are not equal!");
            Assert.AreEqual(node.Type, expectedData[1], "Types are not equal!");
            Assert.AreEqual(node.Latitude, expectedData[2], "Latitudes are not equal!");
            Assert.AreEqual(node.Longitude, expectedData[3], "Longitudes are not equal!");
            Assert.AreEqual(node.Tags, expectedData[4], "Tags are not equal!");
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
