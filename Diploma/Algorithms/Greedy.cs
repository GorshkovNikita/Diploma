using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diploma.Models;
using Diploma.Extended_Classes;
using Diploma.Models.GraphData;

namespace Diploma.Algorithms
{
    public class Greedy
    {
        public static Path RunAlgo(List<Point> points)
        {
            Path path = new Path();
            Point source = points[0];
            long last = 0;
            path.Points.Add(points[0]);
            for (int i = 0; i < points.Count - 1; i++)
            {
                Dictionary<long, double> lengths = new Dictionary<long, double>();
                for (int j = i + 1; j < points.Count; j++)
                {
                    lengths.Add(points[j].ID, Distance.Calc(points[i], points[j]));
                }
                long minID = lengths.OrderBy(kvp => kvp.Value).First().Key;
                path.JoinPath(AStar.RunAlgo(new GraphIterator(), points[i].ID, minID));
                last = minID;
                if (points[i + 1].ID != last)
                {
                    Point pnt = points.Where(p => p.ID == last).First();
                    int idx = points.IndexOf(pnt);
                    points.RemoveAt(idx);
                    points.Insert(i + 1, pnt);
                }
            }
            path.JoinPath(AStar.RunAlgo(new GraphIterator(), last, source.ID));
            return path;
        }
    }
}