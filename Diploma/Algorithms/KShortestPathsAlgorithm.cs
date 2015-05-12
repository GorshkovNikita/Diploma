using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diploma.Models;
using Diploma.Models.GraphData;

namespace Diploma.Algorithms
{
    public class KShortestPathsAlgorithm
    {
        public static List<Path> RunAlgo(KShortestGraphIterator graph, long source, long target, int K)
        {
            // находим кратчайший
            Path shortestPath = DijkstraAlgorithm.RunAlgo(new GraphIterator(), source, target);
            // добавляем егов  список найденных
            graph.FoundedPaths.Add(shortestPath);
            // цикл по k, то есть пока не найдем K субоптимальных путей
            for (int k = 2; k < K; k++)
            {
                // цикл по всем вершинам k-1 пути
                for (int i = 0; i < graph.FoundedPaths[k - 1].Points.Count - 1; i++)
                {
                    // нахождение подпути до iой вершины, который в дальнейшем станет корнем
                    Path subpath = new Path();
                    for (int j = 0; j < i + 1; j++)
                    {
                        subpath.Points.Add(graph.FoundedPaths[k - 1].Points[j]);
                    }
                    // если подпуть совпадает с любым из найденных, то принимаем ребро i - i+1 = infinity
                    if (graph.CheckSubpath(subpath))
                    {
                        // удаляем ребро
                        graph.DeletedEdges.Add(subpath.Points[i].ID, graph.FoundedPaths[graph.LastSubpathIndex].Points[i + 1].ID);
                    }
                }

                // очищаем удаленные ребра
                graph.DeletedEdges.Clear();
            }
            return graph.PossiblePaths;
        }
    }
}