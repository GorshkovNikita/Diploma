using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diploma.Extended_Classes;

namespace Diploma.Models
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
            WayID = way.ID;
            this.CalcLength();
            RoadType = way.Tags["highway"];
            if (way.HasTag("lanes"))
                Lanes = Convert.ToInt32(way.Tags["lanes"]);
            else
                Lanes = 0;
            if (way.HasTag("name"))
                Name = way.Tags["name"];
            else
                Name = "";
            if (way.HasTag("oneway"))
            {
                if (way.Tags["oneway"] == "yes")
                    Oneway = true;
            }
            else
                Oneway = false;
            if (way.HasTag("maxspeed"))
                MaxSpeed = this.ParseMaxSpeed(way.Tags["maxspeed"]);
            else
                MaxSpeed = 200;
            if (way.HasTag("surface"))
                Surface = way.Tags["surface"];
            else
                Surface = "";
        }

        public void CalcLength()
        {
            Length = 0;
            for (int i = 0; i < Points.Count - 1; i++)
            {
                Length += Distance.Calc(Points[i], Points[i + 1]);
            }
        }

        public int ParseMaxSpeed(string maxSpeed)
        {
            if (maxSpeed.Contains(" mph"))
                return SpeedTable.GetKmh(Convert.ToInt32(maxSpeed.Remove(maxSpeed.Length - 4, 4)));
            else if (maxSpeed.Contains("mph"))
                return SpeedTable.GetKmh(Convert.ToInt32(maxSpeed.Remove(maxSpeed.Length - 3, 3)));
            else if (maxSpeed.Contains("ru:") || maxSpeed.Contains("RU:"))
                return SpeedTable.GetKmh(maxSpeed);
            else
                return Convert.ToInt32(maxSpeed);
        }

        public long WayID { get; private set; }
        public double Length { get; private set; }
        public List<Point> Points { get; private set; }
        public String RoadType { get; private set; }
        public Int32 Lanes { get; private set; }
        public Boolean Oneway { get; private set; }
        public String Name { get; private set; }
        public int MaxSpeed { get; private set; }
        public string Surface { get; private set; }
    }
}