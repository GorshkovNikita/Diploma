using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Diploma.Extended_Classes;
using System.Globalization;
using Diploma.Models;
using Diploma.Models.Graph;
using Diploma.Algorithms;
using Neo4jClient;

namespace Diploma.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
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
            Graph graph = new Graph();
            //LineData r = graph.GetLineDataBetweenNodes(1857876172, 259794533);
            //LineData lineData = graph.GetLineDataBetweenNodes(259791092, 467150376);
            //List<long> lst = DBConnection.GetNodesInWayBetween(23964689, 259794497, 1741243719);
            //graph.BuildTestGraphFromWiki();
            DijkstraAlgorithm.RunAlgo(new GraphIterator(graph), 1939502615, 259791149);
            //List<NodeDist> node = graph.GetAllAdjacentNodesInfo(2195315963);
            //graph.BuildTestGraph();
            //graph.BuildGraph();
            //List<long> nodes = graph.GetAllNodesOfIntersectionsOfWay(DBConnection.GetAllIntersectionsOfWay(316207298));
            //List<Line> lst = graph.GetAllLinesFromWay(316207298);
            //List<AllGraphNode> lst = DBConnection.GetAllIntersectionsOfWay(316207298);
            //Point node = graph.GetPoint(27717690);
            //graph.CreateIndex();
            //graph.CreateUniqueConstraint();
            //List<long> lst = DBConnection.GetAllIntersectedWayID();
            return View();
        }
    }
}
