using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neo4jClient;
using Diploma.Algorithms;

namespace Diploma.Models.GraphData
{
    public class GraphIterator
    {
        public GraphIterator()
        {
            this.InitGraphIterator();
        }

        /// <summary>
        /// Конструктор, добавляющий в закрытый список игнорируемые точки и создающий словарь с игнорируемыми ребрами
        /// </summary>
        /// <param name="closedNodes">Игнорируемые точки</param>
        /// <param name="deletedEdges">Игнорируемые ребра</param>
        public GraphIterator(List<NodeData> closedNodes, Dictionary<long, long> deletedEdges)
        {
            this.InitGraphIterator();
            for (int i = 0; i < closedNodes.Count; i++)
            {
                this.ClosedNodes.Add(closedNodes[i]);
            }
            this.DeletedEdges = deletedEdges;
        }

        /// <summary>
        /// Инициализирует итератор начальными данными
        /// </summary>
        private void InitGraphIterator()
        {
            this.Current = null;
            this.Parent = null;
            this.AdjacentNodes = null;
            this.OpenedNodes = new List<NodeData>();
            this.ClosedNodes = new List<NodeData>();
            this.DeletedEdges = new Dictionary<long, long>();
        }

        /// <summary>
        /// Назначает новый текущий узел (узел с наименьшим расстоянием от источника), определяет смежных с ним
        /// </summary>
        public void SetCurrentNode()
        {
            double minLength = this.OpenedNodes.Min(n => n.LengthFromSource);
            this.Current = this.OpenedNodes.Where(n => n.LengthFromSource == minLength).First();
            this.AdjacentNodes = Graph.GetAllAdjacentNodesInfo(Current.ID);
        }

        /// <summary>
        /// Для алгоритма A*. Назначает новый текущий узел (узел с наименьшим расстоянием от источника), определяет смежных с ним
        /// </summary>
        public void SetCurrentNodeForAstar()
        {
            double minLength = this.OpenedNodes.Min(n => (n.LengthFromSource + n.LengthToTarget));
            this.Current = this.OpenedNodes.Where(n => (n.LengthFromSource + n.LengthToTarget) == minLength).First();
            this.AdjacentNodes = Graph.GetAllAdjacentNodesInfo(Current.ID);
        }

        /// <summary>
        /// Назначает новый текущий узел (по ID), определяет смежных с ним
        /// </summary>
        /// <param name="id"></param>
        public void SetCurrentNode(long id)
        {
            this.Current = new NodeData
            {
                ID = Graph.GetPoint(id).ID,
                ParentID = 0,
                LengthFromSource = 0
            };
            this.AdjacentNodes = Graph.GetAllAdjacentNodesInfo(id);
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
        /// Создает конечный путь, состоящий только из вершин графа (без промежуточных узлов)
        /// </summary>
        /// <returns>Путь</returns>
        public Path CreatePath()
        {
            Path path = new Path();
            path.Length = Math.Round(ClosedNodes.Last().LengthFromSource, 4);
            NodeData nodeData = ClosedNodes.Last();
            path.Points.Insert(0, Graph.GetPoint(nodeData.ID));
            while (nodeData.ParentID != 0)
            {
                path.Points.Insert(0, Graph.GetPoint(nodeData.ParentID));
                nodeData = ClosedNodes.Where(n => n.ID == nodeData.ParentID).First();
            }
            //path.CalculateLength();
            return path;
        }

        /// <summary>
        /// Проверяет находится ли заданное ребро в словаре удаленных ребер
        /// </summary>
        /// <param name="source">Вершина-источник</param>
        /// <param name="target">Конечная вершина</param>
        /// <returns>Находится ли? true/false</returns>
        public bool IsDeletedEdgeContainsEdge(long source, long target)
        {
            try
            {
                if (this.DeletedEdges[source] == target)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Текущий узел
        /// </summary>
        public NodeData Current { get; private set; }
        /// <summary>
        /// Родитель текущего узла (ПОКА НИКАК НЕ ИСПОЛЬЗУЕТСЯ)
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
        /// Ребра, которые необходимо игнорировать
        /// </summary>
        public Dictionary<long, long> DeletedEdges { get; set; }
    }
}