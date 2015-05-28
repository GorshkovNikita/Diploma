using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diploma.Extended_Classes
{
    public class HighwayFactor
    {
        public static double GetFactor(string highwayTagName)
        {
            if (highwayTagName == "trunk")
                return Trunk;
            else if (highwayTagName == "primary")
                return Primary;
            else if (highwayTagName == "secondary")
                return Secondary;
            else if (highwayTagName == "tertiary")
                return Teritary;
            else if (highwayTagName == "unclassified")
                return Unclassified;
            else if (highwayTagName == "residental")
                return Residential;
            else if (highwayTagName == "service")
                return Service;
            else if (highwayTagName == "motorway_link")
                return MotorwayLink;
            else if (highwayTagName == "trunk_link")
                return TrunkLink;
            else if (highwayTagName == "primary_link")
                return PrimaryLink;
            else if (highwayTagName == "secondary_link")
                return SecondaryLink;
            else if (highwayTagName == "teritary_link")
                return TeritaryLink;
            else if (highwayTagName == "pedestrian")
                return Pedestrian;
            else if (highwayTagName == "living_street")
                return Track;
            else if (highwayTagName == "footway")
                return FootWay;
            else if (highwayTagName == "cycleway")
                return Cycleway;
            else return 1;
        }

        public const double Trunk = 0.2;
        public const double Primary = 0.3;
        public const double Secondary = 0.4;
        public const double Teritary = 0.5;
        public const double Unclassified = 0.6;
        public const double Residential = 0.7;
        public const double Service = 0.8;
        public const double MotorwayLink = 0.2;
        public const double TrunkLink = 0.3;
        public const double PrimaryLink = 0.35;
        public const double SecondaryLink = 0.4;
        public const double TeritaryLink = 0.45;
        public const double Pedestrian = 0.9;
        public const double Track = 0.9;
        public const double LivingStreet = 0.9;
        public const double FootWay = 1;
        public const double Cycleway = 1;
    }
}