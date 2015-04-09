using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestLeaflet.Extended_Classes;

namespace TestLeaflet.Models
{
    public class Line
    {
        public Line()
        {
            Points = new List<Point>();
        }

        public Line(List<Point> points)
        {
            Points = new List<Point>(points.Count);
            Points = points;
        }

        public void AddPoint(Point point)
        {
            Points.Add(point);
        }

        public double Length
        {
            get
            {
                Length = 0;
                for (int i = 0; i < Points.Count - 1; i++)
                {
                    Length += Distance.Calc(Points[i], Points[i + 1]);
                }
                return Length;
            }
            private set { Length = value; }
        }

        public List<Point> Points { get; private set; }
        public String RoadType { get; set; }
    }
}