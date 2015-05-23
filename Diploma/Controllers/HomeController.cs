using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Diploma.Extended_Classes;
using System.Globalization;
using Diploma.Models;
using Diploma.Models.GraphData;
using Diploma.Algorithms;
using Neo4jClient;
using System.Web.Script.Serialization;
using System.Diagnostics;

namespace Diploma.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int index = 0)
        {
            //List<Point> allTrafficSignals = XmlHelper.GetAllTrafficSignals();
            /*Line street = XmlHelper.FindStreet("МКАД");
            for (Int16 i = 0; i < street.Points.Count; i++)
            {
                ViewData["x" + i] = Convert.ToString(street.Points[i].Latitude, CultureInfo.InvariantCulture);
                ViewData["y" + i] = Convert.ToString(street.Points[i].Longitude, CultureInfo.InvariantCulture);
            }*/
            //XmlHelper.CreateNewXmlWithNodes();
            //XmlHelper.CreateNewXmlWithWays();
            //XmlHelper.AddNodesInDBFromXml();
            //XmlHelper.AddWaysInDBFromXml();
            //double dist = Distance.Calc(new Point(55.590775, 37.599965), new Point(55.629260, 37.618345));
            //OSMWay way = OSMWay.Create(4870889);
            //OSMNode node = OSMNode.Create(26609007);
            //LineData r = graph.GetLineDataBetweenNodes(1857876172, 259794533);
            //LineData lineData = graph.GetLineDataBetweenNodes(259791092, 467150376);
            //List<long> lst = DBConnection.GetNodesInWayBetween(23964689, 259794497, 1741243719);
            //Graph.BuildTestGraphFromWiki();
            //Path path = DijkstraAlgorithm.RunAlgo(new GraphIterator(), 1939502615, 259791149);
            //Path path = DijkstraAlgorithm.RunAlgo(new GraphIterator(), 1939502615, 2086140685).GetFullPath();
            //List<Path> lst = KShortestPathsAlgorithm.RunAlgo(new KShortestGraphIterator(), 1939502615, 2086140685, 5);
            //List<NodeDist> node = graph.GetAllAdjacentNodesInfo(2195315963);
            //graph.BuildTestGraph();
            //Graph.BuildGraph();
            //List<long> nodes = graph.GetAllNodesOfIntersectionsOfWay(DBConnection.GetAllIntersectionsOfWay(316207298));
            //List<Line> lst = graph.GetAllLinesFromWay(316207298);
            //List<AllGraphNode> lst = DBConnection.GetAllIntersectionsOfWay(316207298);
            //Point node = graph.GetPoint(27717690);
            //graph.CreateIndex();
            //graph.CreateUniqueConstraint();
            //List<long> lst = DBConnection.GetAllIntersectedWayID();
            //Graph.BuildTestGraph();
            //Graph.CreateRelationshipsOfWay(188511699);
            //Graph.CreateRelationshipsOfWay(23964689);
            //Graph.CreateRelationshipsOfWay(188511699);
            //Graph.CreateRelationshipsOfWay(23964689);
            //Graph.CreateRelationshipsOfWay(23964689);
            //Graph.CreateRelationshipsOfWay(188511699);
            //Graph.CreateRelationshipsOfWay(23964689);
            //long i = Graph.GetCountNodes();
            //Path path = EClosest.RunAlgo(new EClosestIterator(), 1, 5, 6);
            CurrentConfig.MarkersNumber = 0;
            CurrentConfig.Path = null;
            return View();
        }

        public string Nearest(string lat, string lon)
        {
            if (Request.IsAjaxRequest())
            {
                Point point = Graph.GetNearest(new Point(Convert.ToDouble(lat, CultureInfo.InvariantCulture), Convert.ToDouble(lon, CultureInfo.InvariantCulture)));
                CurrentConfig.MarkersNumber++;
                if (CurrentConfig.MarkersNumber == 1)
                    point.ID = 1;
                else if (CurrentConfig.MarkersNumber == 2)
                {
                    point.ID = 2;
                    CurrentConfig.MarkersNumber = 0;
                }
                return new JavaScriptSerializer().Serialize(point);
                /*if (point != null)
                {
                    CurrentConfig.MarkersNumber++;
                    var jsonPoint = new JavaScriptSerializer().Serialize(point);
                    if (CurrentConfig.MarkersNumber == 2)
                    {
                        CurrentConfig.MarkersNumber = 0;
                        try
                        {
                            var watch = Stopwatch.StartNew();
                            //CurrentConfig.Path = EClosest.RunAlgo(new EClosestIterator(), CurrentConfig.PointStartID, point.ID, 15);
                            //List<Path> lst = KShortestPaths.RunAlgo(new KShortestGraphIterator(), CurrentConfig.PointStartID, point.ID, 15);
                            // Min - безопасный
                            // Max - гоночный
                            //double minSafetyFactor = lst.Max(p => p.SafetyFactor);
                            //CurrentConfig.Path = lst.Where(p => p.SafetyFactor == minSafetyFactor).First();
                            //CurrentConfig.Path = Dijkstra.RunAlgo(new GraphIterator(), CurrentConfig.PointStartID, point.ID);
                            CurrentConfig.Path = AStar.RunAlgo(new GraphIterator(), CurrentConfig.PointStartID, point.ID);
                            CurrentConfig.PointStartID = 0;
                            watch.Stop();
                            var elapsedMs = watch.Elapsed.TotalSeconds;
                            CurrentConfig.Path.RunTime = Math.Round(elapsedMs, 4);
                        }
                        catch
                        {
                            CurrentConfig.Path = null;
                            CurrentConfig.MarkersNumber = 0;
                            return null;
                        }
                    }
                    else if (CurrentConfig.MarkersNumber == 1)
                    {
                        CurrentConfig.Path = null;
                        CurrentConfig.PointStartID = point.ID;
                    }
                    return jsonPoint;
                }
                return null;*/
            }
            else
                return null;
        }

        public string GetFullPath()
        {
            if (CurrentConfig.Path != null)
            {
                var jsonPath = new JavaScriptSerializer().Serialize(CurrentConfig.Path.GetFullPath());
                return jsonPath;
            }
            else
                return null;
        }

        [HttpPost]
        public string RouteRequest(string source_str, string target_str, string route_type, string short_algorithm, string sub_short_algorithm, string ke)
        {
            if (Request.IsAjaxRequest())
            {
                Point source, target;
                int KE;
                try
                {
                    source = new Point(source_str);
                    source.ID = Graph.GetIdByLatLng(source);
                    target = new Point(target_str);
                    target.ID = Graph.GetIdByLatLng(target);
                    KE = Convert.ToInt32(ke);
                }
                catch
                {
                    return "Неправильно введены данные";
                }
                try
                {
                    AppRequest req = new AppRequest(source, target, route_type, short_algorithm, sub_short_algorithm, KE);
                    AppResponse res = req.Response;
                    var jsonPath = new JavaScriptSerializer().Serialize(res);
                    return jsonPath;
                }
                catch
                {
                    return "Невозможно построить маршрут";
                }
            }
            return null;
        }
    }
}
