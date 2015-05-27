using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diploma.Models;

namespace Diploma.Extended_Classes
{
    public class CurrentConfig
    {
        static CurrentConfig()
        {
            MarkersNumber = 0;
            PointsCount = 0;
            PointStartID = 0;
            Path = null;
        }

        public static int PointsCount;
        public static int MarkersNumber;
        public static long PointStartID;
        public static Path Path;
    }
}