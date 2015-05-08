using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diploma.Extended_Classes
{
    public class SpeedTable
    {
        public static int GetKmh(string implicitlySpeed)
        {
            implicitlySpeed = implicitlySpeed.Remove(0, 3);
            if (implicitlySpeed == "urban")
                return 60;
            else if (implicitlySpeed == "motorway")
                return 110;
            else if (implicitlySpeed == "living_street")
                return 20;
            else if (implicitlySpeed == "rural")
                return 90;
            else
                return 0;
        }

        public static int GetKmh(int mph)
        {
            switch (mph)
            {
                case 5:
                    return 8;
                case 10:
                    return 16;
                case 15:
                    return 24;
                case 20:
                    return 32;
                case 25:
                    return 40;
                case 30:
                    return 48;
                case 35:
                    return 56;
                case 40:
                    return 64;
                case 45:
                    return 72;
                case 50:
                    return 80;
                case 55:
                    return 88;
                case 60:
                    return 96;
                case 65:
                    return 104;
                case 70:
                    return 112;
                case 75:
                    return 120;
                case 80:
                    return 128;
                default:
                    return 0;
            }
        }
    }
}