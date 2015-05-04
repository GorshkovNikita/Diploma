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

        public void AddTags(OSMWay way)
        {
            this.CalcLength();
            RoadType = way.Tags["highway"];
            if (way.HasTag("lanes"))
                Lanes = Convert.ToInt32(way.Tags["lanes"]);
            if (way.HasTag("name"))
                Name = way.Tags["name"];
            if (way.HasTag("oneway"))
            {
                if (way.Tags["oneway"] == "yes")
                    Oneway = true;
                else
                    Oneway = false;
            }
        }

        public void CalcLength()
        {
            Length = 0;
            for (int i = 0; i < Points.Count - 1; i++)
            {
                Length += Distance.Calc(Points[i], Points[i + 1]);
            }
        }

        public double Length { get; private set; }
        public List<Point> Points { get; private set; }
        public String RoadType { get; set; }
        public Int32 Lanes { get; set; }
        public Boolean Oneway { get; set; }
        public String Name { get; set; }
    }
}