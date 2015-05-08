using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diploma.Models;

namespace Diploma.Extended_Classes
{
    public class Distance
    {
        public static double Calc(Point from, Point to)
        {
            double fFrom = ToRadian(from.Latitude);
            double fTo = ToRadian(to.Latitude);
            double lFrom = ToRadian(from.Longitude);
            double lTo = ToRadian(to.Longitude);
            double distRad = Math.Acos(Math.Sin(fFrom) * Math.Sin(fTo) + Math.Cos(fFrom) * Math.Cos(fTo) * Math.Cos(lFrom - lTo));
            return distRad * 6371;
        }

        static double ToRadian(double val)
        {
            return (Math.PI / 180) * val;
        }
    }
}