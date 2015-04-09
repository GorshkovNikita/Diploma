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
        public Point(Double lat, Double lon)
        {
            this.Latitude = lat;
            this.Longitude = lon;
        }

        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
    }
}