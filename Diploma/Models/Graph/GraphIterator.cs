using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neo4jClient;
using Diploma.Algorithms;

namespace Diploma.Models.Graph
{
    public class GraphIterator
    {
        public GraphIterator(Graph graph)
        {
            _graph = graph;
            Current = null;
            Parent = null;
            AdjacentNodes = null;
            OpenedNodes = new List<NodeData>();
            ClosedNodes = new List<NodeData>();
        }

        /// <summary>
        /// Назначает новый текущий узел (узел с наименьшим расстоянием от источника), определяет смежных с ним
        /// </summary>
        public void SetCurrentNode()
        {
            double minLength = this.OpenedNodes.Min(n => n.LengthFromSource);
            this.Current = this.OpenedNodes.Where(n => n.LengthFromSource == minLength).First();
            this.AdjacentNodes = _graph.GetAllAdjacentNodesInfo(Current.ID);
        }

        /// <summary>
        /// Назначает новый текущий узел (по ID), определяет смежных с ним
        /// </summary>
        /// <param name="id"></param>
        public void SetCurrentNode(long id)
        {
            this.Current = new NodeData
            {
                ID = _graph.GetNode(id).Data.ID,
                ParentID = 0,
                LengthFromSource = 0
            };
            this.AdjacentNodes = _graph.GetAllAdjacentNodesInfo(id);
        }

        /// <summary>
        /// Определяет находится ли узел в закрытом списке
        /// </summary>
        /// <param name="node">ID узла</param>
        /// <returns>true/false</returns>
        public bool IsClosedNodeListContainsNode(long node)
        {
            int index = this.ClosedNodes.FindIndex(n => n.ID == node);
            if (index >= 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Определяет находится ли узел в открытом списке
        /// </summary>
        /// <param name="node">ID узла</param>
        /// <returns>true/false</returns>
        public bool IsOpenedNodeListContainsNode(long node)
        {
            int index = this.OpenedNodes.FindIndex(n => n.ID == node);
            if (index >= 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Удалить текущий элемент из открытого списка и добавить его в открытый
        /// </summary>
        public void RemoveCurrentFromOpenedNodesAndAddInClosedNodes()
        {
            NodeData current = this.OpenedNodes.Where(n => n.ID == Current.ID).First();
            this.ClosedNodes.Add(current);
            int index = this.OpenedNodes.FindIndex(n => n.ID == Current.ID);
            this.OpenedNodes.RemoveAt(index);
        }

        /// <summary>
        /// Обновление длины от источника и изменение родителя, если необходимо
        /// </summary>
        /// <param name="node">Узел для обновления</param>
        public void UpdateLength(NodeDist node)
        {
            NodeData nd = this.OpenedNodes.Where(n => n.ID == node.ID).First();
            if (nd.LengthFromSource > (node.Length + this.Current.LengthFromSource))
            {
                int index = this.OpenedNodes.FindIndex(n => n.ID == node.ID);
                this.OpenedNodes[index].LengthFromSource = node.Length + this.Current.LengthFromSource;
                this.OpenedNodes[index].ParentID = this.Current.ID;
            }
        }

        /// <summary>
        /// Создает конечный путь
        /// </summary>
        /// <returns>Путь</returns>
        public Path CreatePath()
        {
            Path path = new Path();
            path.Length = ClosedNodes.Last().LengthFromSource;
            NodeData nodeData = ClosedNodes.Last();
            path.Points.Insert(0, _graph.GetPoint(nodeData.ID));
            while (nodeData.ParentID != 0)
            {
                LineData lineData = _graph.GetLineDataBetweenNodes(nodeData.ParentID, nodeData.ID);
                List<long> ls = DBConnection.GetNodesInWayBetween(lineData.WayID, nodeData.ParentID, nodeData.ID);
                for (int i = ls.Count - 1; i >= 0; i--)
                {
                    path.Points.Insert(0, new Point(OSMNode.Create(ls[i])));
                }
                path.Points.Insert(0, _graph.GetPoint(nodeData.ParentID));
                nodeData = ClosedNodes.Where(n => n.ID == nodeData.ParentID).First();
            }
            return path;
        }

        /// <summary>
        /// Текущий узел
        /// </summary>
        public NodeData Current { get; private set; }
        /// <summary>
        /// Родитель текущего узла
        /// </summary>
        public NodeData Parent { get; private set; }
        /// <summary>
        /// Смежные с текущим узлы (ID и длина до них)
        /// </summary>
        public List<NodeDist> AdjacentNodes { get; private set; }
        /// <summary>
        /// Открытый списк узлов (еще не пройденные)
        /// </summary>
        public List<NodeData> OpenedNodes { get; set; }
        /// <summary>
        /// Закрытый список узлов (уже пройденные)
        /// </summary>
        public List<NodeData> ClosedNodes { get; set; }
        /// <summary>
        /// Граф
        /// </summary>
        private Graph _graph;
    }
}