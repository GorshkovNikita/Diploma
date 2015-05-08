using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diploma.Models
{
    public class Path
    {
        public Path()
        {
            Points = new List<Point>();
        }

        public List<Point> Points { get; set; }
        public double Length { get; set; }
    }
}