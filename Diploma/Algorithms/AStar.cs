﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diploma.Models;
using Diploma.Models.GraphData;
using Diploma.Extended_Classes;

namespace Diploma.Algorithms
{
    public class AStar : Algorithm
    {
        public static new Path RunAlgo(GraphIterator graph, long source, long target)
        {
            graph.SetCurrentNode(source);
            graph.OpenedNodes.Add(
                new NodeData
                {
                    ID = graph.Current.ID,
                    LengthFromSource = 0,
                    ParentID = 0
                });
            while (graph.Current.ID != target)
            {
                for (int i = 0; i < graph.AdjacentNodes.Count; i++)
                {
                    if (!graph.IsDeletedEdgeContainsEdge(graph.Current.ID, graph.AdjacentNodes[i].ID))
                    {
                        if (!graph.IsClosedNodeListContainsNode(graph.AdjacentNodes[i].ID))
                        {
                            if (!graph.IsOpenedNodeListContainsNode(graph.AdjacentNodes[i].ID))
                            {
                                graph.OpenedNodes.Add(
                                    new NodeData
                                    {
                                        ID = graph.AdjacentNodes[i].ID,
                                        LengthFromSource = graph.AdjacentNodes[i].Length + graph.Current.LengthFromSource,
                                        LengthToTarget = Distance.Calc(Graph.GetPoint(graph.AdjacentNodes[i].ID), Graph.GetPoint(target)),
                                        ParentID = graph.Current.ID
                                    });
                            }
                            else
                            {
                                graph.UpdateLength(graph.AdjacentNodes[i]);
                            }
                        }
                    }
                }
                graph.RemoveCurrentFromOpenedNodesAndAddInClosedNodes();
                graph.SetCurrentNodeForAstar();
            }
            graph.RemoveCurrentFromOpenedNodesAndAddInClosedNodes();
            //return graph.CreateFullPath();
            return graph.CreatePath();//.GetFullPath();
        }
    }
}