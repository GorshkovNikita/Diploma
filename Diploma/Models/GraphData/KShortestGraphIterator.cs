using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diploma.Models.GraphData
{
    public class KShortestGraphIterator
    {
        public KShortestGraphIterator()
        {
            FoundedPaths = new List<Path>();
            PossiblePaths = new List<Path>();
        }

        /// <summary>
        /// Проверка на совпадение подпути с хотя бы одним подпутем из найденных (FoundedPaths)
        /// </summary>
        /// <param name="subpath"></param>
        /// <returns></returns>
        public bool CheckSubpath(Path subpath)
        {
            for (int i = 0; i < this.FoundedPaths.Count; i++)
            {
                if (subpath.Equals(this.FoundedPaths[i].Points.Take(subpath.Points.Count)))
                {
                    LastSubpathIndex = i;
                    return true;
                }
            }
            return true;
        }

        /// <summary>
        /// Список А, найденные пути от A1(кратчайший) до A(k-1)
        /// </summary>
        public List<Path> FoundedPaths { get; set; }
        /// <summary>
        /// Список Б, кандидаты
        /// </summary>
        public List<Path> PossiblePaths { get; set; }
        /// <summary>
        /// Список удаленных ребер (действует на одной итерации, потом очищается)
        /// </summary>
        public Dictionary<long, long> DeletedEdges { get; set; }
        /// <summary>
        /// Номер последнего совпадающего подпути
        /// </summary>
        public int LastSubpathIndex { get; set; }
    }
}