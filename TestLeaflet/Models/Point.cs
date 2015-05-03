using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestLeaflet.Models
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
        }

        public Int64 ID { get; set; }
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
    }
}