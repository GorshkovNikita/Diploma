using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diploma.Algorithms;
using Diploma.Models.GraphData;
using System.Diagnostics;
using Neo4jClient;

namespace Diploma.Models
{
    public class AppRequest
    {
        public AppRequest(List<Point> points, string route_type, string trajectory_type, string short_algorithm, string sub_short_algorithm, double ke)
        {
            _source = points.First();
            _target = points.Last();
            _points = points;
            _routeType = route_type;
            _trajectoryType = trajectory_type;
            _shortAlgorithm = short_algorithm;
            _subShortAlgorithm = sub_short_algorithm;
            _KE = ke;
            _resultPath = new Path();
            this.GetPath();
            this.Response = new AppResponse(_resultPath, _runTime);
        }

        private void GetPath()
        {
            var watch = Stopwatch.StartNew();
            if (_trajectoryType == "open")
            {
                if (_routeType == "short")
                {
                    _resultPath = this.GetShortestPath();
                }
                else
                {
                    _resultPath = this.GetSafestSportPath();
                }
            }
            else if (_trajectoryType == "close")
            {
                //_points.Add(_points[0]);
                _resultPath = Greedy.RunAlgo(_points);
            }
            watch.Stop();
            var elapsedMs = watch.Elapsed.TotalSeconds;
            _runTime = elapsedMs;
        }

        private Path GetShortestPath()
        {
            Path path = new Path();
            if (_shortAlgorithm == "dijkstra")
            {
                path = Dijkstra.RunAlgo(new GraphIterator(), _points[0].ID, _points[1].ID);
                for (int i = 1; i < _points.Count; i++)
                    path.JoinPath(Dijkstra.RunAlgo(new GraphIterator(), _points[i - 1].ID, _points[i].ID));
            }
            else
            {
                path = AStar.RunAlgo(new GraphIterator(), _points[0].ID, _points[1].ID);
                for (int i = 1; i < _points.Count; i++)
                    path.JoinPath(AStar.RunAlgo(new GraphIterator(), _points[i - 1].ID, _points[i].ID));
            }
            return path;
        }

        private Path GetSafestSportPath()
        {
            List<Path> paths = new List<Path>();
            Path path = new Path();
            if (_subShortAlgorithm == "kshort")
                paths = KShortestPaths.RunAlgo(new KShortestGraphIterator(), _points[0].ID, _points[1].ID, Convert.ToInt32(_KE), _shortAlgorithm);
            else
                paths = EClosest.RunAlgo(new EClosestIterator(), _points[0].ID, _points[1].ID, _KE, _shortAlgorithm);
            if (_routeType == "safe")
            {
                double minSafetyFactor = paths.Max(p => p.SafetyFactor);
                path = paths.Where(p => p.SafetyFactor == minSafetyFactor).First();
                path.CalculateLength();
            }
            else if (_routeType == "sport")
            {
                double maxSafetyFactor = paths.Min(p => p.SafetyFactor);
                path = paths.Where(p => p.SafetyFactor == maxSafetyFactor).First();
                path.CalculateLength();
            }
            return path;
        }

        public AppResponse Response { get; private set; }
        private Point _source;
        private Point _target;
        private List<Point> _points;
        private string _routeType;
        private string _trajectoryType;
        private string _shortAlgorithm;
        private string _subShortAlgorithm;
        private Path _resultPath;
        private double _runTime;
        private double _KE;
    }
}