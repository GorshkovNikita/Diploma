﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diploma.Algorithms;

namespace Diploma.Models.GraphData
{
    public class EClosestIterator : AbstractGraphIterator
    {
        public EClosestIterator() : base()
        { }

        protected override void InitGraphIterator()
        {
            this.ClosedNodes = new Dictionary<long, List<NodeData>>();
            this.OpenedNodes = new Dictionary<long, List<NodeData>>();
            this.Current = null;
            this.Parent = null;
            this.AdjacentNodes = null;
        }

        public override void SetCurrentNode()
        {
            double minLength = this.OpenedNodes.Values.Min(v => v[0].LengthFromSource);
            this.Current = this.OpenedNodes.Where(nd => nd.Value[0].LengthFromSource == minLength).First().Value[0];
            this.AdjacentNodes = Graph.GetAllAdjacentNodesInfo(this.Current.ID);
        }

        public override bool IsClosedNodeListContainsNode(long node)
        {
            if (this.ClosedNodes.ContainsKey(node))
                return true;
            else
                return false;
        }

        public override bool IsOpenedNodeListContainsNode(long node)
        {
            if (this.OpenedNodes.ContainsKey(node))
                return true;
            else
                return false;
        }

        public override void RemoveCurrentFromOpenedNodesAndAddInClosedNodes()
        {
            KeyValuePair<long, List<NodeData>> pair = this.OpenedNodes.Where(n => n.Key == this.Current.ID).First();
            this.OpenedNodes.Remove(this.Current.ID);
            this.ClosedNodes.Add(pair.Key, pair.Value);
        }

        public override void UpdateLength(NodeDist node)
        {
            NodeData nd = this.OpenedNodes.Where(n => n.Key == node.ID).First().Value[0];
            if (nd.LengthFromSource > (this.Current.LengthFromSource + node.Length))
            {
                this.OpenedNodes[node.ID][0].LengthFromSource = this.Current.LengthFromSource + node.Length;
                this.OpenedNodes[node.ID][0].ParentID = this.Current.ID;
            }
        }

        public override Path CreatePath()
        {
            Path path = new Path();
            path.Length = Math.Round(this.ClosedNodes[this.Target][0].LengthFromSource, 4);
            NodeData nodeData = this.ClosedNodes[this.Target][0];
            List<NodeData> l = this.ClosedNodes[this.Target];
            path.Points.Insert(0, Graph.GetPoint(nodeData.ID));
            while (nodeData.ParentID != 0)
            {
                path.Points.Insert(0, Graph.GetPoint(nodeData.ParentID));
                nodeData = this.ClosedNodes.Where(n => n.Key == nodeData.ParentID).First().Value[0];
            }
            //path.CalculateLength();
            return path;
        }

        /// <summary>
        /// Список закрытых узлов для E близких
        /// </summary>
        public Dictionary<long, List<NodeData>> ClosedNodes { get; set; }

        /// <summary>
        /// Список открытых узлов для E близких
        /// </summary>
        public Dictionary<long, List<NodeData>> OpenedNodes { get; set; }

        public long Source { get; set; }
        public long Target { get; set; }
    }
}