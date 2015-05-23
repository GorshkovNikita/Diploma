using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diploma.Models;
using Diploma.Models.GraphData;

namespace Diploma.Algorithms
{
    public class KShortestPaths
    {
        public static List<Path> RunAlgo(KShortestGraphIterator graph, long source, long target, int K, string short_algo)
        {
            Path shortestPath;
            // находим кратчайший
            if (short_algo == "dijkstra")
                shortestPath = Dijkstra.RunAlgo(new GraphIterator(), source, target);
            else
                shortestPath = AStar.RunAlgo(new GraphIterator(), source, target);
            // добавляем его в список найденных
            shortestPath.CalculateFactors();
            graph.FoundedPaths.Add(shortestPath);
            // цикл по k, то есть пока не найдем K субоптимальных путей
            for (int k = 1; k < K; k++)
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
                    // если подпуть совпадает с любым из найденных, то принимаем ребро (i, i+1) = infinity
                    if (graph.CheckSubpath(subpath))
                    {
                        // удаляем ребро
                        graph.DeletedEdges.Add(subpath.Points[i].ID, graph.FoundedPaths[graph.LastSubpathIndex].Points[i + 1].ID);
                    }
                    graph.GetRootNodes(subpath);
                    Path s;
                    try
                    {
                        if (short_algo == "dijkstra")
                            s = Dijkstra.RunAlgo(new GraphIterator(graph.RootNodes, graph.DeletedEdges), subpath.Points[i].ID, target);
                        else
                            s = AStar.RunAlgo(new GraphIterator(graph.RootNodes, graph.DeletedEdges), subpath.Points[i].ID, target);
                    }
                    catch
                    {
                        graph.DeletedEdges.Clear();
                        graph.RootNodes.Clear();
                        continue;
                    }
                    Path possiblePath = subpath.JoinPath(s);
                    if ((!graph.PossiblePaths.Any(p => p.Equals(possiblePath))) && (!graph.FoundedPaths.Any(p => p.Equals(possiblePath))))
                    {
                        graph.PossiblePaths.Add(possiblePath);
                    }
                    // очищаем удаленные ребра
                    graph.DeletedEdges.Clear();
                    graph.RootNodes.Clear();
                }
                graph.MoveFromPossibleToFounded();
            }
            return graph.FoundedPaths;
        }
    }
}