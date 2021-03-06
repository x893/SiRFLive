﻿namespace SiRFLive.Analysis
{
    using System;

    public class computeTropo
    {
        public double NL_ComputeTropo(double alt, double elev)
        {
            double num;
            double num3 = Math.Sin(elev);
            if (alt > 20000.0)
            {
                return 0.0;
            }
            if (alt > -500.0)
            {
                num = Math.Exp(-alt / 6000.0);
            }
            else
            {
                num = 1.0870000123977661;
            }
            if (num3 < 0.0264055)
            {
                return (83.618958 * num);
            }
            return ((2.208 / Math.Min(num3, 1.0)) * num);
        }
    }
}

