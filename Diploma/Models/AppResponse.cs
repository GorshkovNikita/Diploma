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
            this.Length = path.Length;
            this.RunTime = run_time;
        }

        public double RunTime { get; private set; }
        public double Length { get; private set; }
        public List<Point> Path { get; private set; }
    }
}