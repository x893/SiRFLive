﻿namespace SiRFLive.TruthData
{
    using System;

    public class TunnelTimestampFile : TimestampFile
    {
        public TunnelInfo[] m_TunnelInfoArray;

        public TunnelTimestampFile(string filename) : base(filename)
        {
            this.m_TunnelInfoArray = new TunnelInfo[100];
            this.ParseTunnelInfo();
        }

        public int GetTimeSinceEnteringTunnel(int gpsTOW)
        {
            foreach (TunnelInfo info in this.m_TunnelInfoArray)
            {
                if ((info != null) && info.InsideTunnel(gpsTOW))
                {
                    return ((gpsTOW - info.enter_tow) + 1);
                }
            }
            return 0;
        }

        public int GetTimeSinceExitingTunnel(int gpsTOW, int maxTime)
        {
            foreach (TunnelInfo info in this.m_TunnelInfoArray)
            {
                if ((info != null) && info.RecentTunnelExit(gpsTOW, maxTime))
                {
                    return ((gpsTOW - info.exit_tow) + 1);
                }
            }
            return 0;
        }

        public bool InsideTunnel(int gpsTOW)
        {
            foreach (TunnelInfo info in this.m_TunnelInfoArray)
            {
                if ((info != null) && info.InsideTunnel(gpsTOW))
                {
                    return true;
                }
            }
            return false;
        }

        public int InsideTunnelNum(int gpsTOW)
        {
            int num = 0;
            foreach (TunnelInfo info in this.m_TunnelInfoArray)
            {
                if ((info != null) && info.InsideTunnel(gpsTOW))
                {
                    return num;
                }
                num++;
            }
            return 0;
        }

        private string[] ParseTimestampName(string inStr)
        {
            return inStr.Split(new char[] { '_' });
        }

        private void ParseTunnelInfo()
        {
            foreach (Timestamp timestamp in base.m_Timestamps)
            {
                string[] strArray = this.ParseTimestampName(timestamp.name);
                if ((strArray.Length == 3) && (strArray[0] == "Tunnel"))
                {
                    int index = int.Parse(strArray[1]);
                    int length = this.m_TunnelInfoArray.Length;
                    if (this.m_TunnelInfoArray[index] == null)
                    {
                        this.m_TunnelInfoArray[index] = new TunnelInfo();
                    }
                    if (strArray[2] == "Enter")
                    {
                        int num2 = Convert.ToInt32(timestamp.gps_tow);
                        this.m_TunnelInfoArray[index].enter_tow = num2;
                    }
                    else if (strArray[2] == "Exit")
                    {
                        int num3 = Convert.ToInt32(timestamp.gps_tow);
                        this.m_TunnelInfoArray[index].exit_tow = num3;
                    }
                }
            }
        }

        public bool RecentTunnelExit(int gpsTOW, int maxTimeSinceExit)
        {
            foreach (TunnelInfo info in this.m_TunnelInfoArray)
            {
                if ((info != null) && info.RecentTunnelExit(gpsTOW, maxTimeSinceExit))
                {
                    return true;
                }
            }
            return false;
        }

        public int RecentTunnelExitNum(int gpsTOW, int maxTimeSinceExit)
        {
            int num = 0;
            foreach (TunnelInfo info in this.m_TunnelInfoArray)
            {
                if ((info != null) && info.RecentTunnelExit(gpsTOW, maxTimeSinceExit))
                {
                    return num;
                }
                num++;
            }
            return 0;
        }
    }
}

