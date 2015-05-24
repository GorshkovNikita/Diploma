using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diploma.Algorithms;
using Diploma.Models.GraphData;
using System.Diagnostics;

namespace Diploma.Models
{
    public class AppRequest
    {
        public AppRequest(Point source, Point target, string route_type, string short_algorithm, string sub_short_algorithm, int ke)
        {
            _source = source;
            _target = target;
            _routeType = route_type;
            _shortAlgorithm = short_algorithm;
            _subShortAlgorithm = sub_short_algorithm;
            _KE = ke;
            _resultPath = null;
            this.GetPath();
            this.Response = new AppResponse(_resultPath, _runTime);
        }

        private void GetPath()
        {
            var watch = Stopwatch.StartNew();
            if (_routeType == "short")
            {
                if (_shortAlgorithm == "dijkstra")
                    _resultPath = Dijkstra.RunAlgo(new GraphIterator(), _source.ID, _target.ID);
                else if (_shortAlgorithm == "astar")
                    _resultPath = AStar.RunAlgo(new GraphIterator(), _source.ID, _target.ID);
            }
            else
            {
                List<Path> paths;
                if (_subShortAlgorithm == "kshort")
                    paths = KShortestPaths.RunAlgo(new KShortestGraphIterator(), _source.ID, _target.ID, _KE, _shortAlgorithm);
                else
                    _resultPath = EClosest.RunAlgo(new EClosestIterator(), _source.ID, _target.ID, _KE);
                paths = new List<Path>();
                //else if (_subShortAlgorithm == "eclosest")
                /*if (_routeType == "safe")
                {
                    double minSafetyFactor = paths.Min(p => p.SafetyFactor);
                    _resultPath = paths.Where(p => p.SafetyFactor == minSafetyFactor).First();
                    _resultPath.CalculateLength();
                }
                else if (_routeType == "sport")
                {
                    double maxSafetyFactor = paths.Max(p => p.SafetyFactor);
                    _resultPath = paths.Where(p => p.SafetyFactor == maxSafetyFactor).First();
                    _resultPath.CalculateLength();
                }*/
            }
            watch.Stop();
            var elapsedMs = watch.Elapsed.TotalSeconds;
            _runTime = elapsedMs;
        }

        public AppResponse Response { get; private set; }
        private Point _source;
        private Point _target;
        private string _routeType;
        private string _shortAlgorithm;
        private string _subShortAlgorithm;
        private Path _resultPath;
        private double _runTime;
        private int _KE;
    }
}