using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diploma.Algorithms;

namespace Diploma.Models.GraphData
{
    public abstract class AbstractGraphIterator
    {
        public AbstractGraphIterator()
        {
            this.InitGraphIterator();
        }

        /// <summary>
        /// Инициализирует итератор начальными данными
        /// </summary>
        protected abstract void InitGraphIterator();

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
        /// Назначает новый текущий узел (узел с наименьшим расстоянием от источника), определяет смежных с ним
        /// </summary>
        public abstract void SetCurrentNode();

        /// <summary>
        /// Определяет находится ли узел в закрытом списке
        /// </summary>
        /// <param name="node">ID узла</param>
        /// <returns>true/false</returns>
        public abstract bool IsClosedNodeListContainsNode(long node);

        /// <summary>
        /// Определяет находится ли узел в открытом списке
        /// </summary>
        /// <param name="node">ID узла</param>
        /// <returns>true/false</returns>
        public abstract bool IsOpenedNodeListContainsNode(long node);

        /// <summary>
        /// Удалить текущий элемент из открытого списка и добавить его в открытый
        /// </summary>
        public abstract void RemoveCurrentFromOpenedNodesAndAddInClosedNodes();

        /// <summary>
        /// Обновление длины от источника и изменение родителя, если необходимо
        /// </summary>
        /// <param name="node">Узел для обновления</param>
        public abstract void UpdateLength(NodeDist node);

        /// <summary>
        /// Создает конечный путь, состоящий только из вершин графа (без промежуточных узлов)
        /// </summary>
        /// <returns>Путь</returns>
        public abstract Path CreatePath();

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
        public NodeData Current { get; protected set; }

        /// <summary>
        /// Родитель текущего узла (ПОКА НИКАК НЕ ИСПОЛЬЗУЕТСЯ)
        /// </summary>
        public NodeData Parent { get; protected set; }

        /// <summary>
        /// Смежные с текущим узлы (ID и длина до них)
        /// </summary>
        public List<NodeDist> AdjacentNodes { get; protected set; }

        /// <summary>
        /// Ребра, которые необходимо игнорировать
        /// </summary>
        public Dictionary<long, long> DeletedEdges { get; protected set; }
    }
}