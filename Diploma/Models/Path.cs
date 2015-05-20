using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diploma.Models;
using Diploma.Models.GraphData;
using Diploma.Extended_Classes;

namespace Diploma.Models
{
    public class Path //: IEquatable<Path>
    {
        public Path()
        {
            this.InitPath();
        }

        /// <summary>
        /// Конструктор пути из существующих точек
        /// </summary>
        /// <param name="points"></param>
        public Path(List<Point> points)
        {
            this.InitPath();
            for (int i = 0; i < points.Count; i++)
            {
                this.Points.Add(points[i]);
            }
            this.CalculateLength();
        }

        /// <summary>
        /// Инициализирует путь начальными данными
        /// </summary>
        private void InitPath()
        {
            this.Points = new List<Point>();
            this.Length = 0;
            this.IsFull = false;
        }

        /// <summary>
        /// Соединение 2 путей
        /// </summary>
        /// <param name="path">Присоединяемый путь</param>
        /// <returns>Соединенный путь или null</returns>
        public Path JoinPath(Path path)
        {
            if (this.Points.Last().ID == path.Points.First().ID)
            {
                this.Length = 0;
                path.Points.RemoveAt(0);
                this.Points.AddRange(path.Points);
                this.CalculateLength();
                return this;
            }
            return null;
        }

        /// <summary>
        /// Считает длину пути
        /// </summary>
        public void CalculateLength()
        {
            if (Length == 0)
            {
                for (int i = 1; i < this.Points.Count; i++)
                {
                    Length += Graph.GetLineDataBetweenNodes(this.Points[i - 1].ID, this.Points[i].ID).Length;
                }
                Length = Math.Round(Length, 4);
            }
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
        /// Получает коэффициент безопасности маршрута
        /// </summary>
        /// <returns></returns>
        public void CalculateFactors()
        {
            this.SafetyFactor = 1;
            for (int i = 1; i < this.Points.Count; i++)
            {
                LineData line = Graph.GetLineDataBetweenNodes(this.Points[i - 1].ID, this.Points[i].ID);
                this.SafetyFactor *= Math.Pow(HighwayFactor.GetFactor(line.RoadType), line.Length / 0.1);
            }
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
        public double RunTime { get; set; }
        public double SafetyFactor { get; set; }
    }
}