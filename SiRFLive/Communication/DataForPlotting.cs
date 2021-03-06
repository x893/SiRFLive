﻿namespace SiRFLive.Communication
{
    using System;

    public class DataForPlotting : IDisposable
    {
        public double[] Alt_nvplot = new double[MAX_S];
        public float[,] Avg_CNo = new float[9, 8];
        public float[,] azimuth = new float[TrackSVRec.MAX_SVT, MAX_P];
        public int[] Bin_Azimuth = new int[] { 0x2d, 90, 0x87, 180, 0xe1, 270, 0x13b, 360 };
        public int[] Bin_Elev = new int[] { 10, 20, 30, 40, 50, 60, 70, 80, 90 };
        public double[,] cnos = new double[TrackSVRec.MAX_SVT, MAX_S];
        public float[,] elevation = new float[TrackSVRec.MAX_SVT, MAX_P];
        public double[,] Idx_Avg_CNo = new double[9, 8];
        private int idx_nvplot;
        public int[] idx_P = new int[TrackSVRec.MAX_SVT];
        private int idx_S = -1;
        private bool isDisposed;
        public double[] Lat_nvplot = new double[MAX_S];
        public double[] Lon_nvplot = new double[MAX_S];
        public static int MAX_P = 0x7d0;
        public static int MAX_S = 0x3f480;
        public double RefAlt = -13.8;
        public double RefLat = 37.375062;
        public double RefLon = -121.914245;
        public TrackSVRec SVTrkr = new TrackSVRec();
        public double[] tows = new double[MAX_S];
        public double[] tows_nvplot = new double[MAX_S];

        public DataForPlotting()
        {
            for (int i = 0; i < TrackSVRec.MAX_SVT; i++)
            {
                this.idx_P[i] = 0;
            }
            for (int j = 0; j < 9; j++)
            {
                for (int m = 0; m < 8; m++)
                {
                    this.Avg_CNo[j, m] = 0f;
                    this.Idx_Avg_CNo[j, m] = 0.0;
                }
            }
            for (int k = 0; k < TrackSVRec.MAX_SVT; k++)
            {
                for (int n = 0; n < MAX_S; n++)
                {
                    this.cnos[k, n] = 0.0;
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed && disposing)
            {
                this.azimuth = null;
                this.elevation = null;
                this.SVTrkr = null;
                this.idx_P = null;
                this.Bin_Elev = null;
                this.Bin_Azimuth = null;
                this.Avg_CNo = null;
                this.Idx_Avg_CNo = null;
                this.cnos = null;
                this.tows = null;
                this.tows_nvplot = null;
                this.Lat_nvplot = null;
                this.Lon_nvplot = null;
                this.Alt_nvplot = null;
            }
            this.isDisposed = true;
        }

        ~DataForPlotting()
        {
            this.azimuth = null;
            this.elevation = null;
            this.SVTrkr = null;
            this.idx_P = null;
            this.Bin_Elev = null;
            this.Bin_Azimuth = null;
            this.Avg_CNo = null;
            this.Idx_Avg_CNo = null;
            this.cnos = null;
            this.tows = null;
            this.tows_nvplot = null;
            this.Lat_nvplot = null;
            this.Lon_nvplot = null;
            this.Alt_nvplot = null;
        }

        public int GetNumSample_nvplot()
        {
            return this.idx_nvplot;
        }

        public int GetNumSamplesTrackVsTimePlot()
        {
            return (this.idx_S + 1);
        }

        public int GetNumTrackedSVs()
        {
            return this.SVTrkr.getNumTrackedSVs();
        }

        public void InsertAlt(double alt)
        {
            this.Alt_nvplot[this.idx_nvplot] = alt;
        }

        public void InsertCNo(int prn, double cno)
        {
            int num = this.SVTrkr.getIdx_readonly(prn);
            if (num >= 0)
            {
                this.cnos[num, this.idx_S] = cno;
            }
        }

        public void InsertLat(double lat)
        {
            this.Lat_nvplot[this.idx_nvplot] = lat;
        }

        public void InsertLon(double lon)
        {
            this.Lon_nvplot[this.idx_nvplot] = lon;
        }

        public void InsertTOW(double tow)
        {
            this.updateIdx();
            this.tows[this.idx_S] = tow;
        }

        public void InsertTow_nvplot(double tow)
        {
            this.tows_nvplot[this.idx_nvplot] = tow;
        }

        public void UpdateAvgCNoTable(float elev, float azimu, float cno)
        {
            if ((elev <= 90f) && (azimu <= 360f))
            {
                int index = 0;
                while ((elev > this.Bin_Elev[index]) && (index < 8))
                {
                    index++;
                }
                int num2 = 0;
                while ((azimu > this.Bin_Azimuth[num2]) && (num2 < 7))
                {
                    num2++;
                }
                Avg_CNo[index, num2] = (float) (((this.Avg_CNo[index, num2] * this.Idx_Avg_CNo[index, num2]) + cno) / (1.0 + this.Idx_Avg_CNo[index, num2]));
                Idx_Avg_CNo[index, num2]++;
            }
        }

        private void updateIdx()
        {
            this.idx_S++;
            if ((this.idx_S < 0) || (this.idx_S >= MAX_S))
            {
                this.idx_S = 0;
            }
        }

        public void UpdateIdx_nvplot()
        {
            this.idx_nvplot++;
            if (this.idx_nvplot >= (MAX_S - 1))
            {
                this.idx_nvplot = 0;
            }
        }
    }
}

