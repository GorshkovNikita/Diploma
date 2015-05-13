using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diploma.Algorithms;

namespace Diploma.Models.GraphData
{
    public class KShortestGraphIterator
    {
        public KShortestGraphIterator()
        {
            this.FoundedPaths = new List<Path>();
            this.PossiblePaths = new List<Path>();
            this.DeletedEdges = new Dictionary<long, long>();
            this.RootNodes = new List<NodeData>();
        }

        /// <summary>
        /// Проверка на совпадение подпути с хотя бы одним подпутем из найденных (FoundedPaths)
        /// </summary>
        /// <param name="subpath">Подпуть</param>
        /// <returns>Совпадает? true/false</returns>
        public bool CheckSubpath(Path subpath)
        {
            for (int i = 0; i < this.FoundedPaths.Count; i++)
            {
                if (subpath.Equals(new Path(this.FoundedPaths[i].Points.Take(subpath.Points.Count).ToList())))
                {
                    LastSubpathIndex = i;
                    return true;
                }
            }
            return true;
        }

        /// <summary>
        /// Получает из списка Б (кандидаты на субоптимальные) путь с наименьшей длиной
        /// </summary>
        /// <returns>Путь с наименьшей длиной</returns>
        public Path GetPathWithMinLengthFromPossible()
        {
            double minLength = this.PossiblePaths.Min(p => p.Length);
            return this.PossiblePaths.Where(p => p.Length == minLength).First();
        }

        /// <summary>
        /// Переместить из списка А в список Б
        /// </summary>
        public void MoveFromPossibleToFounded()
        {
            Path pathWithMinLength = this.GetPathWithMinLengthFromPossible();
            this.FoundedPaths.Add(pathWithMinLength);
            this.PossiblePaths.Remove(pathWithMinLength);
        }


        /// <summary>
        /// Получает все элементы корня. которые нужно игнорировать
        /// </summary>
        /// <param name="subpath"></param>
        public void GetRootNodes(Path subpath)
        {
            for (int i = 0; i < subpath.Points.Count; i++)
            {
                RootNodes.Add(new NodeData { ID = subpath.Points[i].ID });
            }
        }

        /// <summary>
        /// Список А, найденные пути от A0(кратчайший) до A(k-1)
        /// </summary>
        public List<Path> FoundedPaths { get; set; }
        /// <summary>
        /// Список Б, кандидаты На субоптимальные пути
        /// </summary>
        public List<Path> PossiblePaths { get; set; }
        /// <summary>
        /// Список удаленных ребер (действует на одной итерации, потом очищается)
        /// </summary>
        public Dictionary<long, long> DeletedEdges { get; set; }
        /// <summary>
        /// Элементы корня, которые необходимо игнорировать при Дейкстре
        /// </summary>
        public List<NodeData> RootNodes { get; set; }
        /// <summary>
        /// Номер последнего совпадающего подпути
        /// </summary>
        public int LastSubpathIndex { get; set; }
    }
}