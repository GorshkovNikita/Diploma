using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diploma.Models;
using Diploma.Models.GraphData;

namespace Diploma.Models
{
    public class Path
    {
        public Path()
        {
            Points = new List<Point>();
            IsFull = false;
        }

        /// <summary>
        /// Получает все точки пути
        /// </summary>
        /// <returns></returns>
        public Path GetFullPath()
        {
            if (!this.IsFull)
            {
                List<Point> tmpPoints = new List<Point>(this.Points);
                for (int i = 1; i < tmpPoints.Count; i++)
                {
                    LineData lineData = Graph.GetLineDataBetweenNodes(tmpPoints[i - 1].ID, tmpPoints[i].ID);
                    List<long> ls = DBConnection.GetNodesInWayBetween(lineData.WayID, tmpPoints[i - 1].ID, tmpPoints[i].ID);
                    for (int j = ls.Count - 1; j >= 0; j--)
                    {
                        int idx = this.Points.FindIndex(p => p.ID == tmpPoints[i - 1].ID);
                        this.Points.Insert(idx + 1, new Point(OSMNode.Create(ls[j])));
                    }
                }
                this.IsFull = true;
                return this;
            }
            return this;
        }

        /// <summary>
        /// Проверка на эквивалентность двух путей
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool Equals(Path path)
        {
            if (this.Points.Count == path.Points.Count)
            {
                for (int i = 0; i < this.Points.Count; i++)
                {
                    if (this.Points[i].ID != path.Points[i].ID)
                        return false;
                }
                return true;
            }
            return false;
        }

        public List<Point> Points { get; set; }
        public double Length { get; set; }
        public bool IsFull { get; set; }
    }
}