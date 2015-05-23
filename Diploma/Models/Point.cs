using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace Diploma.Models
{
    /// <summary>
    /// Представляет точку на карте
    /// </summary>
    public class Point
    {
        public Point()
        {

        }

        public Point(Double lat, Double lon)
        {
            this.Latitude = lat;
            this.Longitude = lon;
        }

        public Point(OSMNode node)
        {
            this.ID = node.ID;
            this.Latitude = node.Latitude;
            this.Longitude = node.Longitude;
            this.AddTags(node);
        }

        public Point(string point)
        {
            string[] coords = point.Split(';');
            this.Latitude = Convert.ToDouble(coords[0], CultureInfo.InvariantCulture);
            this.Longitude = Convert.ToDouble(coords[1], CultureInfo.InvariantCulture);
        }

        public void AddTags(OSMNode node)
        {
            if (node.HasTag("bicycle"))
            {
                if (node.Tags["bicycle"] == "no")
                    Bicycle = false;
            }
            else
                Bicycle = true;
            if (node.HasTag("barrier"))
                Barrier = node.Tags["barrier"];
            else
                Barrier = "no";
        }

        public Int64 ID { get; set; }
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
        public String Barrier { get; set; }
        public bool Bicycle { get; set; }
    }
}