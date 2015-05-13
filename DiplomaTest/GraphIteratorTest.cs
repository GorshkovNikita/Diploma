using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Diploma.Models.GraphData;

namespace DiplomaTest
{
    [TestFixture]
    class GraphIteratorTest
    {
        [Test, TestCaseSource("IsDeletedEdgeContainsEdgeCases")]
        public void IsDeletedEdgeContainsEdgeTest(Dictionary<long, long> deleted, long source, long target, bool expectedResult)
        {
            GraphIterator graph = new GraphIterator();
            graph.DeletedEdges = deleted;
            bool actualResult = graph.IsDeletedEdgeContainsEdge(source, target);

            Assert.AreEqual(expectedResult, actualResult);
        }

        public IEnumerable<TestCaseData> IsDeletedEdgeContainsEdgeCases()
        {
            yield return new TestCaseData(
                new Dictionary<long, long>()
                {
                    { 2107326314, 2107326323}
                },
                2107326314,
                2107326323,
                true
            );
            yield return new TestCaseData(
                new Dictionary<long, long>()
                {
                    { 2107326314, 2107326323}
                },
                2107326323,
                2107326323,
                false
            );
            yield return new TestCaseData(
                new Dictionary<long, long>()
                {
                    { 2107326314, 2107326323}
                },
                2107326314,
                2107326314,
                false
            );
        }
    }
}
