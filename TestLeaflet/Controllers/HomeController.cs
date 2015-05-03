using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestLeaflet.Extended_Classes;
using System.Globalization;
using TestLeaflet.Models;

namespace TestLeaflet.Controllers
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
            //Point node = graph.GetPoint(27717690);
            //graph.CreateIndex();
            //graph.CreateUniqueConstraint();
            return View();
        }
    }
}
