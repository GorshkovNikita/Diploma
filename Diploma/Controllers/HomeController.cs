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
            Graph.Connect();
            CurrentConfig.MarkersNumber = 0;
            CurrentConfig.PointsCount = 0;
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
        public string RouteRequest(string[] points, string route_type, string short_algorithm, string sub_short_algorithm, string ke)
        {
            if (Request.IsAjaxRequest())
            {
                List<Point> way_points = new List<Point>();
                double KE;
                try
                {
                    for (int i = 0; i < points.Count(); i++)
                    {
                        Point pnt = new Point(points[i]);
                        pnt.ID = Graph.GetIdByLatLng(pnt);
                        way_points.Add(pnt);
                    }
                    KE = Convert.ToDouble(ke);
                }
                catch
                {
                    return "Неправильно введены данные";
                }
                try
                {
                    AppRequest req = new AppRequest(way_points, route_type, short_algorithm, sub_short_algorithm, KE);
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

        public void IncPointsCount()
        {
            if (Request.IsAjaxRequest())
            {
                CurrentConfig.PointsCount++;
            }
        }

        public void DecPointsCount()
        {
            if (Request.IsAjaxRequest())
            {
                CurrentConfig.PointsCount--;
            }
        }

        public void ResetCurrentConfig()
        {
            if (Request.IsAjaxRequest())
            {
                CurrentConfig.PointsCount = 0;
                CurrentConfig.MarkersNumber = 0;
            }
        }
    }
}
