﻿namespace SiRFLive.Utilities
{
    using SiRFLive.Configuration;
    using SiRFLive.General;
    using SiRFLive.GUI.Commmunication;
    using SiRFLive.MessageHandling;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    public class HelperFunctions
    {
        public static string BuildCSVString(List<string> inputStringList)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string str in inputStringList)
            {
                builder.Append(str);
                builder.Append(",");
            }
            return builder.ToString().TrimEnd(new char[] { ',' });
        }

        public static string ByteToHex(byte[] input)
        {
            StringBuilder builder = new StringBuilder();
            string str = string.Empty;
            try
            {
                foreach (byte num in input)
                {
                    builder.Append(Convert.ToString(num, 0x10).PadLeft(2, '0').PadRight(3, ' '));
                }
                str = builder.ToString().ToUpper();
            }
            catch
            {
            }
            return str;
        }

        public static float FindAverageFloat(float[] inputArray)
        {
            float num = 0f;
            float num2 = 0f;
            int num3 = 0;
            foreach (float num4 in inputArray)
            {
                if (num4 != 0f)
                {
                    num2 += num4;
                    num3++;
                }
            }
            if (num3 > 0)
            {
                num = num2 / ((float) num3);
            }
            return num;
        }

        public static float FindMaxFloat(float[] inputArray)
        {
            List<float> list = new List<float>();
            float num = -9999f;
            foreach (float num2 in inputArray)
            {
                list.Add(num2);
            }
            list.Sort();
            if (list.Count > 0)
            {
                num = list[list.Count - 1];
            }
            list.Clear();
            return num;
        }

        public static string FormatDigitsToString(double inNum, int precision)
        {
            if (inNum == -9999.0)
            {
                return "N/A";
            }
            switch (precision)
            {
                case 0:
                    return string.Format("{0:F0}", inNum);

                case 1:
                    return string.Format("{0:F1}", inNum);

                case 2:
                    return string.Format("{0:F2}", inNum);

                case 3:
                    return string.Format("{0:F3}", inNum);

                case 4:
                    return string.Format("{0:F4}", inNum);

                case 5:
                    return string.Format("{0:F5}", inNum);

                case 6:
                    return string.Format("{0:F6}", inNum);
            }
            return string.Format("{0:F2}", inNum);
        }

        public static string FormatDigitsToString(int inNum, int precision)
        {
            if (inNum == -9999)
            {
                return "N/A";
            }
            return inNum.ToString();
        }

        public static float GetCalibrationAtten(float cableLoss, int level, string type)
        {
            IniHelper helper = new IniHelper(clsGlobal.InstalledDirectory + @"\scripts\stationCalValues.cfg");
            float num2 = Convert.ToSingle(helper.IniReadValue("CABLE_LOSS", "CAL_CABLE_LOSS"));
            float num3 = Convert.ToSingle(helper.IniReadValue(type.ToUpper(), level.ToString()));
            if (num2 < cableLoss)
            {
                return (num3 - (cableLoss - num2));
            }
            if (num2 > cableLoss)
            {
                return (num3 + (num2 - cableLoss));
            }
            return num3;
        }

        public static string GetTimeStampInString()
        {
            StringBuilder builder = new StringBuilder(50);
            DateTime now = DateTime.Now;
            builder.AppendFormat("{0:D2}/{1:D2}/{2:D4} {3:D2}:{4:D2}:{5:D2}.{6:D3}", new object[] { now.Month, now.Day, now.Year, now.Hour, now.Minute, now.Second, now.Millisecond });
            return builder.ToString();
        }

        public string GetTimeStampInString(DateTime timeToFormat)
        {
            StringBuilder builder = new StringBuilder(50);
            builder.AppendFormat("{0:D2}/{1:D2}/{2:D4} {3:D2}:{4:D2}:{5:D2}.{6:D3}", new object[] { timeToFormat.Month, timeToFormat.Day, timeToFormat.Year, timeToFormat.Hour, timeToFormat.Minute, timeToFormat.Second, timeToFormat.Millisecond });
            return builder.ToString();
        }

        public static byte[] HexToByte(string msg)
        {
            msg = msg.Replace(" ", "");
            byte[] buffer = new byte[msg.Length / 2];
            try
            {
                for (int i = 0; i < msg.Length; i += 2)
                {
                    buffer[i / 2] = Convert.ToByte(msg.Substring(i, 2), 0x10);
                }
            }
            catch
            {
            }
            return buffer;
        }

        public static bool IsDuplicateString(List<string> inList)
        {
            List<string> list = new List<string>();
            foreach (string str in inList)
            {
                if (list.Contains(str))
                {
                    return true;
                }
                list.Add(str);
            }
            list.Clear();
            return false;
        }

        public static int Min2Val(int a, int b)
        {
            if (a >= b)
            {
                return b;
            }
            return a;
        }

        public static List<int> ParseTestLevels(string inputString)
        {
            List<int> list = new List<int>();
            string str = inputString.Replace(" ", "");
            if (str.Length != 0)
            {
                foreach (string str2 in str.Split(new char[] { ',' }))
                {
                    string[] strArray2 = str2.Split(new char[] { ':' });
                    if (strArray2.Length == 3)
                    {
                        try
                        {
                            int num4;
                            int item = Convert.ToInt32(strArray2[0]);
                            int num2 = Convert.ToInt32(strArray2[2]);
                            int num3 = Convert.ToInt32(strArray2[1]);
                            int num5 = Math.DivRem(Math.Abs((int) (num2 - item)), Math.Abs(num3), out num4);
                            if (item <= num2)
                            {
                                if (num3 > 0)
                                {
                                    for (int i = 0; i <= num5; i++)
                                    {
                                        list.Add(item + (num3 * i));
                                    }
                                    if (num4 != 0)
                                    {
                                        list.Add(num2);
                                    }
                                }
                            }
                            else if (num3 < 0)
                            {
                                for (int j = 0; j <= num5; j++)
                                {
                                    list.Add(num2 - (num3 * j));
                                }
                                if (num4 != 0)
                                {
                                    list.Add(item);
                                }
                                list.Reverse();
                            }
                        }
                        catch
                        {
                        }
                    }
                    else if (strArray2.Length == 1)
                    {
                        try
                        {
                            list.Add(Convert.ToInt32(strArray2[0]));
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return list;
        }

        public static bool ReadCSVTruthData(string filePath, int startAtLine, ref Hashtable output)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }
            try
            {
                StreamReader reader = new StreamReader(filePath);
                string str = string.Empty;
                int num = 0;
                str = reader.ReadLine();
                while ((str != null) && (num < startAtLine))
                {
                    num++;
                    str = reader.ReadLine();
                }
                while (str != null)
                {
                    try
                    {
                        ReferenceData data = new ReferenceData();
                        string[] strArray = str.Split(new char[] { ',' });
                        if (strArray.Length >= 8)
                        {
                            data.RecordTime = Convert.ToDouble(strArray[7]);
                            data.DegreeLatitude = Convert.ToDouble(strArray[1]);
                            data.DegreeLongitude = Convert.ToDouble(strArray[2]);
                            data.MeterAltitude = Convert.ToDouble(strArray[3]);
                            data.MeterPerSecondVelocity = Convert.ToDouble(strArray[4]);
                            string key = string.Format("{0:F1}", data.RecordTime);
                            if (!output.ContainsKey(key))
                            {
                                output.Add(key, data);
                            }
                            str = reader.ReadLine();
                        }
                        continue;
                    }
                    catch
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ArrayList ReadIMUFile(string filePath, double startTow, double endTow)
        {
            ArrayList list = new ArrayList();
            try
            {
                StreamReader reader = new FileInfo(filePath).OpenText();
                double num = 0.0;
                int num2 = 0;
                int num3 = 0;
                bool flag = false;
                string str = string.Empty;
                string input = reader.ReadLine();
                while (input != null)
                {
                    if (!flag && input.Contains("SW Version"))
                    {
                        string pattern = "SW Version*(?<swVer>.*)";
                        Regex regex = new Regex(pattern, RegexOptions.Compiled);
                        if (regex.IsMatch(input))
                        {
                            str = regex.Match(input).Result("${swVer}");
                            flag = true;
                        }
                    }
                    string[] strArray = input.Split(new char[] { ',' });
                    if (strArray.Length <= 20)
                    {
                        goto Label_0302;
                    }
                    if (strArray[0] == "28")
                    {
                        num3 = Convert.ToInt16(strArray[0x13]);
                        if (num2 < num3)
                        {
                            num2 = num3;
                            num = (uint) Convert.ToDouble(strArray[4]);
                        }
                    }
                    if (!(strArray[0] == "41") || (Convert.ToInt16(strArray[2]) <= 0))
                    {
                        goto Label_0302;
                    }
                    PositionInfo.PositionStruct struct2 = new PositionInfo.PositionStruct();
                    struct2.NavType = Convert.ToUInt16(strArray[2]);
                    int num4 = 8;
                    struct2.TOW = (uint) (Convert.ToDouble(strArray[num4 / 2]) / 1000.0);
                    if (startTow > 0.0)
                    {
                        if (struct2.TOW >= startTow)
                        {
                            if ((endTow > 0.0) && (struct2.TOW > endTow))
                            {
                                return list;
                            }
                            goto Label_0190;
                        }
                        input = reader.ReadLine();
                        continue;
                    }
                    if ((endTow > 0.0) && (struct2.TOW > endTow))
                    {
                        return list;
                    }
                Label_0190:
                    struct2.RxTime_Hour = Convert.ToInt32(strArray[num4++]);
                    struct2.RxTime_Minute = Convert.ToInt32(strArray[num4++]);
                    struct2.RxTime_second = (ushort) (Convert.ToDouble(strArray[num4++]) / 1000.0);
                    struct2.SatellitesUsed = Convert.ToUInt32(strArray[num4++]);
                    struct2.Latitude = Convert.ToDouble(strArray[num4++]) / 10000000.0;
                    struct2.Longitude = Convert.ToDouble(strArray[num4++]) / 10000000.0;
                    struct2.Altitude = Convert.ToDouble(strArray[num4++]) / 100.0;
                    struct2.Speed = Convert.ToDouble(strArray[num4 + 2]) / 100.0;
                    struct2.Heading = Convert.ToDouble(strArray[num4 + 3]) / 100.0;
                    struct2.NumSVInFix = Convert.ToUInt16(strArray[num4 + 0x12]);
                    struct2.HDOP = Convert.ToDouble(strArray[num4 + 0x13]) / 5.0;
                    if (struct2.TOW == num)
                    {
                        struct2.MaxCN0 = num2;
                    }
                    else
                    {
                        struct2.MaxCN0 = 0.0;
                    }
                    num2 = 0;
                    num3 = 0;
                    struct2.SW_Version = str;
                    list.Add(struct2);
                Label_0302:
                    input = reader.ReadLine();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public ArrayList ReadPositionFile(string filePath)
        {
            ArrayList list = new ArrayList();
            try
            {
                FileInfo info = new FileInfo(filePath);
                string str = info.OpenText().ReadLine();
                if (str.Length != 0)
                {
                    str = str.Replace(" ", "").TrimEnd(new char[] { '\n' }).TrimEnd(new char[] { '\r' });
                }
                string str2 = str;
                if (str2 == null)
                {
                    return list;
                }
                if (!(str2 == "SSB"))
                {
                    if ((str2 == "NMEA") || (str2 == "OSP"))
                    {
                    }
                    return list;
                }
                list = this.ReadIMUFile(filePath, 0.0, 0.0);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public static void ResetTimeStart(bool resetCount)
        {
            foreach (string str in clsGlobal.CommWinRef.Keys)
            {
                frmCommOpen open = (frmCommOpen) clsGlobal.CommWinRef[str];
                open.comm.RxCtrl.ResetCtrl.ResetTimerStart(resetCount);
            }
        }

        public static void ResetTimeStop(bool resetCount)
        {
            foreach (string str in clsGlobal.CommWinRef.Keys)
            {
                frmCommOpen open = (frmCommOpen) clsGlobal.CommWinRef[str];
                open.comm.RxCtrl.ResetCtrl.ResetTimerStop(resetCount);
            }
        }
    }
}

