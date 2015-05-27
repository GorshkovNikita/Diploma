using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diploma.Models
{
    public class AppResponse
    {
        public AppResponse(Path path, double run_time)
        {
            this.Path = path.GetFullPath().Points;
            this.Length = Math.Round(path.Length, 4);
            this.RunTime = Math.Round(run_time, 4);
            this.SafetyFactor = path.SafetyFactor;
        }

        public double RunTime { get; private set; }
        public double Length { get; private set; }
        public List<Point> Path { get; private set; }
        public double SafetyFactor { get; private set; }
    }
}