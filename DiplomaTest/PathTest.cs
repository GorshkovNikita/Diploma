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
    class PathTest
    {
        [Test, TestCaseSource("EqualsCases")]
        public void EqualsTest(Path path1, Path path2, bool expectedResult)
        {
            bool actualResult = path1.Equals(path2);

            Assert.AreEqual(expectedResult, actualResult);
        }

        public IEnumerable<TestCaseData> EqualsCases()
        {
            yield return new TestCaseData(
                new object[]
                {
                    new Path()
                    {
                        Points = new List<Point>()
                        {
                            { 
                                new Point()
                                {
                                    ID = 1,
                                    Latitude = 1,
                                    Longitude = 1
                                }
                            },
                            {
                                new Point()
                                {
                                    ID = 2,
                                    Latitude = 1,
                                    Longitude = 2
                                }
                            }
                        },
                        Length = 10
                    },
                    new Path()
                    {
                        Points = new List<Point>()
                        {
                            { 
                                new Point()
                                {
                                    ID = 1,
                                    Latitude = 1,
                                    Longitude = 1
                                }
                            },
                            {
                                new Point()
                                {
                                    ID = 2,
                                    Latitude = 1,
                                    Longitude = 2
                                }
                            }
                        },
                        Length = 10
                    },
                    true
                }
            );
            yield return new TestCaseData(
                new object[]
                {
                    new Path()
                    {
                        Points = new List<Point>()
                        {
                            { 
                                new Point()
                                {
                                    ID = 1,
                                    Latitude = 1,
                                    Longitude = 1
                                }
                            },
                            {
                                new Point()
                                {
                                    ID = 2,
                                    Latitude = 1,
                                    Longitude = 2
                                }
                            }
                        },
                        Length = 10
                    },
                    new Path()
                    {
                        Points = new List<Point>()
                        {
                            { 
                                new Point()
                                {
                                    ID = 1,
                                    Latitude = 1,
                                    Longitude = 1
                                }
                            },
                            {
                                new Point()
                                {
                                    ID = 2,
                                    Latitude = 1,
                                    Longitude = 2
                                }
                            },
                            {
                                new Point()
                                {
                                    ID = 2,
                                    Latitude = 1,
                                    Longitude = 2
                                }
                            }
                        },
                        Length = 10
                    },
                    false
                }
            );
            yield return new TestCaseData(
                new object[]
                {
                    new Path()
                    {
                        Points = new List<Point>()
                        {
                            { 
                                new Point()
                                {
                                    ID = 1,
                                    Latitude = 1,
                                    Longitude = 1
                                }
                            },
                            {
                                new Point()
                                {
                                    ID = 2,
                                    Latitude = 1,
                                    Longitude = 2
                                }
                            }
                        },
                        Length = 10
                    },
                    new Path()
                    {
                        Points = new List<Point>()
                        {
                            { 
                                new Point()
                                {
                                    ID = 1,
                                    Latitude = 1,
                                    Longitude = 1
                                }
                            },
                            {
                                new Point()
                                {
                                    ID = 1,
                                    Latitude = 1,
                                    Longitude = 2
                                }
                            }
                        },
                        Length = 10
                    },
                    false
                }
            );
        }
    }
}
