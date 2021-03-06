﻿namespace SiRFLive.MessageHandling
{
    using SiRFLive.Communication;
    using SiRFLive.General;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml.XPath;

    public class utils_AutoReply
    {
        internal static string ByteArrayToHexString(byte[] Bytes)
        {
            string str = "0123456789ABCDEF";
            StringBuilder builder = new StringBuilder();
            foreach (byte num in Bytes)
            {
                builder.Append(str[num >> 4]);
                builder.Append(str[num & 15]);
            }
            return builder.ToString();
        }

        internal static byte EncodeCTAccuracy(int accuracy)
        {
            if (accuracy > 0xefff)
            {
                return 0xff;
            }
            if ((accuracy > 0xe7ff) && (accuracy < 0xf000))
            {
                return 0xfe;
            }
            if (accuracy <= 1)
            {
                return 0;
            }
            int num2 = 0;
            while (num2 < 15)
            {
                if ((((int) 1) << num2) >= accuracy)
                {
                    break;
                }
                num2++;
            }
            if (num2 > 0)
            {
                num2--;
            }
            int num3 = 0;
            while (num3 <= 15)
            {
                if (((1.0 * (1 + (num3 / 0x10))) * (((int) 1) << num2)) > accuracy)
                {
                    break;
                }
                num3++;
            }
            num3 = max(num3, 15);
            if ((((1.0 * (((int) 1) << num2)) * (1 + (num3 / 0x10))) <= accuracy) && (num2 < 15))
            {
                num2++;
            }
            return (byte) ((num2 << 4) | num3);
        }

        internal static byte EncodePTAccuracy(double accuracy)
        {
            if (accuracy >= 7680.0)
            {
                return 0xff;
            }
            if ((accuracy >= 7424.0) && (accuracy < 7680.0))
            {
                return 0xfe;
            }
            if (accuracy <= 0.125)
            {
                return 0;
            }
            int num2 = 0;
            while (num2 < 15)
            {
                if ((((int) 1) << num2) >= ((int) accuracy))
                {
                    break;
                }
                num2++;
            }
            if (num2 > 0)
            {
                num2--;
            }
            int num3 = 0;
            while (num3 <= 15)
            {
                if (((0.125 * (1 + (num3 / 0x10))) * (((int) 1) << num2)) > ((int) accuracy))
                {
                    break;
                }
                num3++;
            }
            num3 = max(num3, 15);
            if ((((0.125 * (((int) 1) << num2)) * (1 + (num3 / 0x10))) <= ((int) accuracy)) && (num2 < 15))
            {
                num2++;
            }
            return (byte) ((num2 << 4) | num3);
        }

        internal static byte EncodeTimeAccuracy(double acc, int ttType)
        {
            if (ttType == 0)
            {
                return EncodeCTAccuracy((int) acc);
            }
            if (ttType == 1)
            {
                return EncodePTAccuracy(acc);
            }
            return 0;
        }

        internal static string FieldList_to_HexString(bool isSLCRx, ArrayList fieldList, byte channelType)
        {
            string message = "";
            int num = 0;
            if (fieldList.Count == 0)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(((SLCMsgStructure) fieldList[0]).defaultValue);
            for (int i = 1; i < fieldList.Count; i++)
            {
                if (string.IsNullOrEmpty(((SLCMsgStructure) fieldList[i]).defaultValue))
                {
                    builder.Append(",");
                    builder.Append("0");
                }
                else
                {
                    builder.Append(",");
                    builder.Append(((SLCMsgStructure) fieldList[i]).defaultValue);
                }
            }
            string[] strArray = builder.ToString().Split(new char[] { ',' });
            StringBuilder builder2 = new StringBuilder();
            for (int j = 0; j < fieldList.Count; j++)
            {
                builder2.Append(MsgFactory.ConvertDecimalToHex(strArray[j], ((SLCMsgStructure) fieldList[j]).datatype, ((SLCMsgStructure) fieldList[j]).scale));
                num += ((SLCMsgStructure) fieldList[j]).bytes;
            }
            message = builder2.ToString();
            return ("A0A2" + num.ToString("X").PadLeft(4, '0') + message + GetChecksum(message, isSLCRx) + "B0B3");
        }

        internal static byte get_REL_FREQ_ACC(double fAccuracy)
        {
            if (fAccuracy > 239.0)
            {
                return 0xff;
            }
            if ((fAccuracy > 231.0) && (fAccuracy < 240.0))
            {
                return 0xfe;
            }
            if (fAccuracy < 0.00390625)
            {
                return 0;
            }
            if ((fAccuracy > 0.00390625) && (fAccuracy < 0.004150390625))
            {
                return 1;
            }
            int num2 = 0;
            while (num2 < 15)
            {
                if ((0.00390625 * (((int) 1) << num2)) >= fAccuracy)
                {
                    break;
                }
                num2++;
            }
            if (num2 > 0)
            {
                num2--;
            }
            int num3 = 0;
            while (num3 <= 15)
            {
                if (((0.00390625 * (((int) 1) << num2)) * (1.0 + (((double) num3) / 16.0))) >= fAccuracy)
                {
                    break;
                }
                num3++;
            }
            num3 = min(num3, 15);
            if (((0.00390625 * (((int) 1) << num2)) * (1.0 + (((double) num3) / 16.0))) >= fAccuracy)
            {
                return (byte) ((num2 << 4) | num3);
            }
            if (num2 < 15)
            {
                num2++;
            }
            return (byte) (num2 << 4);
        }

        internal static string get2HoursEphFromFile(string eph_Path)
        {
            if (!File.Exists(eph_Path))
            {
                return "";
            }
            StreamReader reader = File.OpenText(eph_Path);
            string str = "";
            string input = "";
            new Regex("[0-9]+");
            Regex regex = new Regex("[0-9]+,");
            bool flag = false;
            StringBuilder builder = new StringBuilder();
            while ((((input = reader.ReadLine()) != null) && !input.Contains("End of File")) && !flag)
            {
                if (input.Contains("Ephemeris Data at GPS time"))
                {
                    while (((input = reader.ReadLine()) != null) && (regex.Match(input).ToString() == ""))
                    {
                    }
                    builder.Append("1, ");
                    builder.Append(input);
                    while (((input = reader.ReadLine()) != null) && (regex.Match(input).ToString() != ""))
                    {
                        if (!input.StartsWith("0,"))
                        {
                            builder.Append("1,");
                            builder.Append(input);
                        }
                    }
                    flag = true;
                }
            }
            string str3 = "";
            str = builder.ToString();
            if (str.Length >= 3)
            {
                str3 = str.Replace(" ", "").TrimEnd(new char[] { '\n' }).TrimEnd(new char[] { '\r' }).TrimEnd(new char[] { ',' });
            }
            reader.Close();
            return str3;
        }

        internal static string getAcqAssistDataFromFile(string acqAssistFile_Path, double gpsTowNow)
        {
            string str = string.Empty;
            if (!File.Exists(acqAssistFile_Path))
            {
                return str;
            }
            StreamReader reader = File.OpenText(acqAssistFile_Path);
            string str2 = string.Empty;
            string str3 = string.Empty;
            double num = 0.0;
            double num2 = 0.0;
            reader.ReadLine();
            bool flag = true;
            while (flag)
            {
                str = reader.ReadLine();
                if (str != null)
                {
                    str3 = str;
                    str = reader.ReadLine();
                    if (str == null)
                    {
                        continue;
                    }
                    string[] strArray = str.Split(new char[] { ',' });
                    if (strArray.Length <= 0)
                    {
                        continue;
                    }
                    num2 = Convert.ToDouble(strArray[0]);
                    if (gpsTowNow == num2)
                    {
                        str2 = str;
                    }
                    else
                    {
                        if (gpsTowNow > num2)
                        {
                            continue;
                        }
                        string[] strArray2 = str3.Split(new char[] { ',' });
                        if (strArray2.Length > 0)
                        {
                            num = Convert.ToDouble(strArray2[0]);
                        }
                        if (gpsTowNow > num)
                        {
                            continue;
                        }
                        str2 = str3;
                    }
                    break;
                }
                if (str3.Length != 0)
                {
                    string[] strArray3 = str3.Split(new char[] { ',' });
                    if (strArray3.Length > 0)
                    {
                        num = Convert.ToDouble(strArray3[0]);
                    }
                    if (gpsTowNow <= num)
                    {
                        str2 = str3;
                        break;
                    }
                    str2 = string.Empty;
                    flag = false;
                }
            }
            return str2.Replace(" ", "").TrimEnd(new char[] { '\n' }).TrimEnd(new char[] { '\r' }).TrimEnd(new char[] { ',' });
        }

        internal static string getAlmFromFileForSet(string alm_Path)
        {
            if (!File.Exists(alm_Path))
            {
                return "";
            }
            string inputMsgFile = ConfigurationManager.AppSettings["InstalledDirectory"] + @"\protocols\Protocols.xml";
            ArrayList fieldList = new ArrayList();
            fieldList = GetMessageStructure(inputMsgFile, CommunicationManager.ReceiverType.GSW, 130, -1, "SSB", "1.6");
            StreamReader reader = File.OpenText(alm_Path);
            string str2 = "";
            new Regex("[0-9]+,");
            for (int i = 0; i < 5; i++)
            {
                str2 = reader.ReadLine();
            }
            string str3 = "130,";
            while (((str2 = reader.ReadLine()) != null) && !str2.Contains("End of File"))
            {
                str3 = str3 + str2 + ",";
            }
            string[] strArray = str3.Split(new char[] { ',' });
            for (int j = 0; j < strArray.GetLength(0); j++)
            {
                if (strArray[j] != string.Empty)
                {
                    SLCMsgStructure structure = (SLCMsgStructure) fieldList[j];
                    structure.defaultValue = strArray[j];
                    fieldList[j] = structure;
                }
            }
            string str4 = FieldList_to_HexString(false, fieldList, 0);
            reader.Close();
            return str4;
        }

        internal static string getAutoReplySummary(CommunicationManager target)
        {
            StringBuilder builder = new StringBuilder();
            try
            {
                builder.Append("\n");
                builder.Append('-', 3);
                builder.Append(" ");
                builder.Append(target.PortName);
                builder.Append(" ");
                builder.Append('-', 3);
                builder.Append("\n");
                builder.Append(gettext_HWCfg(target));
                builder.Append(gettext_ApproxPosTransfer(target));
                builder.Append(gettext_TimeTransfer(target));
                builder.Append(gettext_FreqTransfer(target));
                builder.Append(gettext_PosReq(target));
                builder.Append(gettext_EphAid(target));
                builder.Append(gettext_AcqAssistAid(target));
                builder.Append(gettext_AlmAid(target));
                builder.Append(gettext_NavBit(target));
            }
            catch
            {
                builder.Append("ERROR!");
            }
            return builder.ToString();
        }

        internal static string GetChecksum(string message, bool slcRx)
        {
            int num = 0;
            string str = message.Replace(" ", "");
            for (int i = 0; i < str.Length; i += 2)
            {
                num += Convert.ToByte(str.Substring(i, 2), 0x10);
                num &= 0x7fff;
            }
            if (slcRx)
            {
                num = (num & 0x7fff) | 0x8000;
            }
            return num.ToString("X2").PadLeft(4, '0');
        }

        internal static string getEphFromFile(string eph_Path, string gpsTimeStr)
        {
            if (!File.Exists(eph_Path))
            {
                return "";
            }
            StreamReader reader = File.OpenText(eph_Path);
            string str = "";
            string input = "";
            Regex regex = new Regex("[0-9]+");
            Regex regex2 = new Regex("[0-9]+,");
            int num = 0;
            int num2 = Convert.ToInt32(gpsTimeStr);
            bool flag = false;
            StringBuilder builder = new StringBuilder();
            while ((((input = reader.ReadLine()) != null) && !input.Contains("End of File")) && !flag)
            {
                if (input.Contains("Ephemeris Data at GPS time"))
                {
                    Match match = regex.Match(input);
                    if (match.ToString() != "")
                    {
                        num = Convert.ToInt32(match.Value);
                        if ((num2 >= num) && (num2 < (num + 0x1c20)))
                        {
                            while (((input = reader.ReadLine()) != null) && (regex2.Match(input).ToString() == ""))
                            {
                            }
                            builder.Append("1, ");
                            builder.Append(input);
                            while (((input = reader.ReadLine()) != null) && (regex2.Match(input).ToString() != ""))
                            {
                                if (!input.StartsWith("0,"))
                                {
                                    builder.Append("1,");
                                    builder.Append(input);
                                }
                            }
                            flag = true;
                        }
                    }
                }
            }
            string str3 = "";
            str = builder.ToString();
            if (str.Length >= 3)
            {
                str3 = str.Replace(" ", "").TrimEnd(new char[] { '\n' }).TrimEnd(new char[] { '\r' }).TrimEnd(new char[] { ',' });
            }
            reader.Close();
            return str3;
        }

        internal static string getEphFromFileForSet(string eph_Path)
        {
            if (!File.Exists(eph_Path))
            {
                return "";
            }
            string inputMsgFile = ConfigurationManager.AppSettings["InstalledDirectory"] + @"\protocols\Protocols.xml";
            ArrayList fieldList = new ArrayList();
            fieldList = GetMessageStructure(inputMsgFile, CommunicationManager.ReceiverType.GSW, 0x95, -1, "SSB", "1.6");
            StreamReader reader = File.OpenText(eph_Path);
            string str2 = "";
            new Regex("[0-9]+,");
            for (int i = 0; i < 5; i++)
            {
                str2 = reader.ReadLine();
            }
            StringBuilder builder = new StringBuilder();
            while (((str2 = reader.ReadLine()) != null) && !str2.Contains("End of File"))
            {
                if (str2.Substring(0, 1) != "0")
                {
                    string[] strArray = ("149," + str2).Split(new char[] { ',' });
                    for (int j = 0; j < strArray.GetLength(0); j++)
                    {
                        SLCMsgStructure structure = (SLCMsgStructure) fieldList[j];
                        structure.defaultValue = strArray[j];
                        fieldList[j] = structure;
                    }
                    builder.Append(FieldList_to_HexString(false, fieldList, 0) + ",");
                }
            }
            reader.Close();
            return builder.ToString();
        }

        internal static ArrayList GetMessageStructure(string inputMsgFile, CommunicationManager.ReceiverType RxType, int mid, int sid, string protocol, string version)
        {
            XPathNavigator navigator = new XPathDocument(inputMsgFile).CreateNavigator();
            int num = 2;
            int num2 = 2;
            int num3 = 0;
            if (RxType == CommunicationManager.ReceiverType.SLC)
            {
                num3 = 1;
            }
            ArrayList list = new ArrayList();
            SLCMsgStructure structure = new SLCMsgStructure();
            int num4 = 0;
            XPathExpression expr = navigator.Compile(string.Concat(new object[] { "/protocols/protocol[@name='", protocol, "'][@version='", version, "']/input/message[@mid='", mid, "'][@subid = '", sid, "']/field" }));
            XPathNodeIterator iterator = navigator.Select(expr);
            if (iterator.Count == 0)
            {
                expr = navigator.Compile(string.Concat(new object[] { "/protocols/protocol[@name='", protocol, "'][@version='", version, "']/input/message[@mid='", mid, "'][@subid = '']/field" }));
                iterator = navigator.Select(expr);
            }
            try
            {
                int num5 = 0;
                int num6 = 0;
                while (iterator.MoveNext())
                {
                    XPathNavigator navigator2 = iterator.Current.Clone();
                    if (navigator2.GetAttribute("name", "").Contains("LOOP"))
                    {
                        num5 = int.Parse(navigator2.GetAttribute("numloops", ""));
                        num6 = int.Parse(navigator2.GetAttribute("numlines", ""));
                        for (int i = 0; i < num5; i++)
                        {
                            for (int j = 0; j < num6; j++)
                            {
                                iterator.MoveNext();
                                navigator2 = iterator.Current.Clone();
                                structure.fieldNumber = iterator.CurrentPosition;
                                structure.fieldName = navigator2.GetAttribute("name", "");
                                structure.bytes = int.Parse(navigator2.GetAttribute("bytes", ""));
                                structure.datatype = navigator2.GetAttribute("datatype", "");
                                structure.units = navigator2.GetAttribute("units", "");
                                if (navigator2.GetAttribute("scale", "") == "")
                                {
                                    structure.scale = 1.0;
                                }
                                else
                                {
                                    structure.scale = double.Parse(navigator2.GetAttribute("scale", ""));
                                }
                                structure.startByte = (((num + num2) + num3) + 1) + num4;
                                structure.endByte = (structure.startByte + structure.bytes) - 1;
                                structure.defaultValue = navigator2.GetAttribute("default", "");
                                num4 += structure.bytes;
                                list.Add(structure);
                            }
                        }
                    }
                    else
                    {
                        structure.fieldNumber = iterator.CurrentPosition;
                        structure.fieldName = navigator2.GetAttribute("name", "");
                        structure.bytes = int.Parse(navigator2.GetAttribute("bytes", ""));
                        structure.datatype = navigator2.GetAttribute("datatype", "");
                        structure.units = navigator2.GetAttribute("units", "");
                        if (navigator2.GetAttribute("scale", "") == "")
                        {
                            structure.scale = 1.0;
                        }
                        else
                        {
                            structure.scale = double.Parse(navigator2.GetAttribute("scale", ""));
                        }
                        structure.startByte = (((num + num2) + num3) + 1) + num4;
                        structure.endByte = (structure.startByte + structure.bytes) - 1;
                        structure.defaultValue = navigator2.GetAttribute("default", "");
                        num4 += structure.bytes;
                        list.Add(structure);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        internal static short getScaleFreqOffset(int rfDefaultFreqIndex, double acc, double freq, uint lastClockDrift, double lastAidingSessionClockDrift, bool specifiedRefClk, int freqAidingType)
        {
            uint num = clsGlobal.DEFAULT_RF_FREQ[rfDefaultFreqIndex];
            if (specifiedRefClk)
            {
                if (freqAidingType == 1)
                {
                    return (short) (num - freq);
                }
                return (short) freq;
            }
            lastAidingSessionClockDrift = 0.0;
            if (lastAidingSessionClockDrift != 0.0)
            {
                long num3 = (long) (lastAidingSessionClockDrift * 1575.42);
                return (short) (num - num3);
            }
            if (lastClockDrift == 0)
            {
                if (freqAidingType == 1)
                {
                    return (short) (num - freq);
                }
                return (short) freq;
            }
            if (freqAidingType == 1)
            {
                return (short) (num - lastClockDrift);
            }
            return (short) lastClockDrift;
        }

        internal static string gettext_AcqAssistAid(CommunicationManager target)
        {
            if (target.AutoReplyCtrl.AutoReplyParams.AutoAid_AcqData_fromTTB)
            {
                return "Accquisition aiding data from TTB\n\n";
            }
            if (target.AutoReplyCtrl.AutoReplyParams.AutoAid_AcqData_fromFile)
            {
                return "Accquisition aiding data from file\n\n";
            }
            return "No accquisition data ading\n\n";
        }

        internal static string gettext_AlmAid(CommunicationManager target)
        {
            if (target.AutoReplyCtrl.AutoReplyParams.AutoAid_Alm)
            {
                return "Almanac data aiding from TTB\n\n";
            }
            return "No almanac data aiding\n\n";
        }

        internal static string gettext_ApproxPosTransfer(CommunicationManager target)
        {
            if (!target.AutoReplyCtrl.ApproxPositionCtrl.Reply)
            {
                return "Approximate Position aiding = Ignore\n\n";
            }
            if (target.AutoReplyCtrl.ApproxPositionCtrl.Reject)
            {
                return "Approximate Position aiding = Reject\n\n";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("Approximate Position Aiding:\n");
            builder.Append("\tLatitude|deg = ");
            builder.Append(target.AutoReplyCtrl.ApproxPositionCtrl.Lat);
            builder.Append("\n");
            builder.Append("\tLongitude|deg = ");
            builder.Append(target.AutoReplyCtrl.ApproxPositionCtrl.Lon);
            builder.Append("\n");
            builder.Append("\tAltitude|m = ");
            builder.Append(target.AutoReplyCtrl.ApproxPositionCtrl.Alt);
            builder.Append("\n");
            builder.Append("\tEstimate Horizontal Error|m = ");
            builder.Append(target.AutoReplyCtrl.ApproxPositionCtrl.EstHorrErr);
            builder.Append("\n");
            builder.Append("\tEstimate Vertical Error|m = ");
            builder.Append(target.AutoReplyCtrl.ApproxPositionCtrl.EstVertiErr);
            builder.Append("\n\n");
            return builder.ToString();
        }

        internal static string gettext_EphAid(CommunicationManager target)
        {
            if (target.AutoReplyCtrl.AutoReplyParams.AutoAid_Eph_fromTTB)
            {
                return "Ephemeris aiding data from TTB\n\n";
            }
            if (target.AutoReplyCtrl.AutoReplyParams.AutoAid_Eph_fromFile)
            {
                return "Ephemeris aiding data from file\n\n";
            }
            return "No ephemeris data aiding\n\n";
        }

        internal static string gettext_FreqTransfer(CommunicationManager target)
        {
            if (!target.AutoReplyCtrl.FreqTransferCtrl.Reply)
            {
                return "Frequency Aiding= Ignore\n\n";
            }
            if (!target.AutoReplyCtrl.FreqTransferCtrl.SpecifiedRefFreq && !target.AutoReplyCtrl.FreqTransferCtrl.SLCReportFreqGuiIndex)
            {
                return "Frequency Aiding= Reject\n\n";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("Frequency Aiding: ");
            if (target.AutoReplyCtrl.AutoReplyParams.UseTTB_ForFreqAid)
            {
                builder.Append("Use TTB\n");
            }
            else
            {
                builder.Append("\n");
            }
            if (target.AutoReplyCtrl.FreqTransferCtrl.SLCReportFreqGuiIndex)
            {
                builder.Append("\tUse RX Reported Freq Offset -- Offset|Hz = ");
                builder.Append(target.AutoReplyCtrl.FreqTransferCtrl.FreqOffsetFromRxGui);
                builder.Append("\n");
                builder.Append("\tAccuracy|ppm = ");
                builder.Append(target.AutoReplyCtrl.FreqTransferCtrl.FreqAccFromRxGui);
                builder.Append("\n");
            }
            else if (target.AutoReplyCtrl.FreqTransferCtrl.SpecifiedRefFreq)
            {
                builder.Append("\tUse Specified Freq Params -- Offset|Hz = ");
                builder.Append(target.AutoReplyCtrl.FreqTransferCtrl.FreqOffsetUserSpecifiedGui);
                builder.Append("\n");
                builder.Append("\tAccuracy|ppm = ");
                builder.Append(target.AutoReplyCtrl.FreqTransferCtrl.FreqAccUserSpecifiedGui);
                builder.Append("\n");
            }
            if (target.AutoReplyCtrl.FreqTransferCtrl.FreqAidingMethod == 1)
            {
                builder.Append("\tFreq Aiding Type = Non-counter");
                builder.Append("\n");
            }
            else
            {
                builder.Append("\tFreq. Aiding Type = Counter");
                builder.Append("\n");
                builder.Append("\tReference Clock = ");
                builder.Append((target.AutoReplyCtrl.FreqTransferCtrl.RefClockOnOffGuiIndex == 1) ? "Off" : "On");
                builder.Append("\n");
                builder.Append("\tReference Clock Request = ");
                builder.Append((target.AutoReplyCtrl.FreqTransferCtrl.RefClockRequestGuiIndex == 1) ? "Turn off" : "None");
                builder.Append("\n");
                builder.Append("\tInclude Norminal Frequency = ");
                builder.Append(target.AutoReplyCtrl.FreqTransferCtrl.IncludeNormFreq ? "Yes" : "No");
                builder.Append("\n");
                if (target.AutoReplyCtrl.FreqTransferCtrl.IncludeNormFreq)
                {
                    builder.Append("\tExt Clk Freq|Hz = ");
                    builder.Append(target.AutoReplyCtrl.FreqTransferCtrl.NomFreq);
                    builder.Append("\n");
                    builder.Append("\tSkew = ");
                    builder.Append(target.AutoReplyCtrl.FreqTransferCtrl.ExtClkSkewppm);
                    builder.Append("\n");
                }
            }
            builder.Append("\tTime Tag = ");
            builder.Append((target.AutoReplyCtrl.FreqTransferCtrl.TimeTag == 1) ? "Invalid" : "Valid fwd");
            builder.Append("\n\n");
            return builder.ToString();
        }

        internal static string gettext_HWCfg(CommunicationManager target)
        {
            if (!target.AutoReplyCtrl.HWCfgCtrl.Reply)
            {
                return "Hardware Configuration= Ignore\n\n";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("Hardware Configuration:\n");
            builder.Append("\tPrecise = ");
            builder.Append((target.AutoReplyCtrl.HWCfgCtrl.PreciseTimeEnabled == 1) ? " Yes\n" : "No\n");
            builder.Append("\tDirection = ");
            builder.Append((target.AutoReplyCtrl.HWCfgCtrl.PreciseTimeDirection == 1) ? "CP<-SLC\n" : "CP->SLC\n");
            builder.Append("\tFrequency Aiding = ");
            builder.Append((target.AutoReplyCtrl.HWCfgCtrl.FreqAidEnabled == 1) ? "Yes" : "No");
            if (target.AutoReplyCtrl.HWCfgCtrl.FreqAidEnabled == 1)
            {
                builder.Append((target.AutoReplyCtrl.HWCfgCtrl.FreqAidMethod == 1) ? " -- Non-counter\n" : " -- Counter\n");
            }
            builder.Append("\tRTC = ");
            builder.Append((target.AutoReplyCtrl.HWCfgCtrl.RTCAvailabe == 1) ? "Yes" : "No");
            builder.Append((target.AutoReplyCtrl.HWCfgCtrl.RTCSource == 1) ? " -- Internal to GPS\n" : " -- External to GPS\n");
            builder.Append("\tCoarse = ");
            builder.Append((target.AutoReplyCtrl.HWCfgCtrl.CoarseTimeEnabled == 1) ? "Yes\n" : "No\n");
            if ((target.AutoReplyCtrl.HWCfgCtrl.FreqAidEnabled == 1) && (target.AutoReplyCtrl.HWCfgCtrl.FreqAidMethod == 0))
            {
                builder.Append("\tReference Clock = ");
                builder.Append((target.AutoReplyCtrl.HWCfgCtrl.RefClkEnabled == 0) ? "On\n" : "Off\n");
                builder.Append("\tNominal Frequency|Hz = ");
                builder.Append(target.AutoReplyCtrl.HWCfgCtrl.NorminalFreqHz);
                builder.Append("\n");
            }
            builder.Append("\tEnhanced Network Type = ");
            builder.Append(target.AutoReplyCtrl.HWCfgCtrl.NetworkEnhanceType);
            builder.Append("\n\n");
            return builder.ToString();
        }

        internal static string gettext_NavBit(CommunicationManager target)
        {
            if (target.AutoReplyCtrl.AutoReplyParams.AutoAid_NavBit)
            {
                return "NavBit Aiding from TTB\n\n";
            }
            return "No NavBit Aiding\n\n";
        }

        internal static string gettext_PosReq(CommunicationManager target)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Position Request:\n");
            switch (target.AutoReplyCtrl.PositionRequestCtrl.LocMethod)
            {
                case 0:
                    builder.Append("\tRequest Type = MS Assisted\n");
                    break;

                case 1:
                    builder.Append("\tRequest Type = MS Based\n");
                    break;

                case 2:
                    builder.Append("\tRequest Type = MS Based Preferred\n");
                    break;

                case 3:
                    builder.Append("\tRequest Type = MS Assist Preferred\n");
                    break;

                case 4:
                    builder.Append("\tRequest Type = Simul. MSB & MSA\n");
                    break;

                default:
                    builder.Append("\tRequest Type = Unknown\n");
                    break;
            }
            builder.Append("\tNum Fix = ");
            builder.Append(target.AutoReplyCtrl.PositionRequestCtrl.NumFixes);
            builder.Append("\n");
            builder.Append("\tTime Between Fix|s = ");
            builder.Append(target.AutoReplyCtrl.PositionRequestCtrl.TimeBtwFixes);
            builder.Append("\n");
            builder.Append("\tHorrizontal Error Max|m = ");
            if (target.AutoReplyCtrl.PositionRequestCtrl.HorrErrMax == 0)
            {
                builder.Append("No Max\n");
            }
            else
            {
                builder.Append(target.AutoReplyCtrl.PositionRequestCtrl.HorrErrMax);
                builder.Append("\n");
            }
            builder.Append("\tVertical Error Max|m = ");
            switch (target.AutoReplyCtrl.PositionRequestCtrl.VertErrMax)
            {
                case 0:
                    builder.Append(" < 1m\n");
                    break;

                case 1:
                    builder.Append(" < 5m\n");
                    break;

                case 2:
                    builder.Append(" < 10m\n");
                    break;

                case 3:
                    builder.Append(" < 20m\n");
                    break;

                case 4:
                    builder.Append(" < 40m\n");
                    break;

                case 5:
                    builder.Append(" < 80m\n");
                    break;

                case 6:
                    builder.Append("\t< 160m\n");
                    break;

                case 7:
                    builder.Append(" No Max\n");
                    break;

                default:
                    builder.Append(" Unknown\n");
                    break;
            }
            builder.Append("\tResponse Time Max|s = ");
            builder.Append(target.AutoReplyCtrl.PositionRequestCtrl.RespTimeMax);
            builder.Append("\n");
            if (target.AutoReplyCtrl.PositionRequestCtrl.TimeAccPriority == 0)
            {
                builder.Append("\tPriority = No\n\n");
            }
            else if (target.AutoReplyCtrl.PositionRequestCtrl.TimeAccPriority == 1)
            {
                builder.Append("\tPriority = Response Time\n\n");
            }
            else if (target.AutoReplyCtrl.PositionRequestCtrl.TimeAccPriority == 2)
            {
                builder.Append("\tPriority = Position Error\n\n");
            }
            else if (target.AutoReplyCtrl.PositionRequestCtrl.TimeAccPriority == 3)
            {
                builder.Append("\tPriority = Use Entire Response Time\n\n");
            }
            else
            {
                builder.Append("\tPriority = Unknown\n\n");
            }
            return builder.ToString();
        }

        internal static string gettext_TimeTransfer(CommunicationManager target)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Time Aiding: ");
            if (target.AutoReplyCtrl.AutoReplyParams.UseTTB_ForTimeAid)
            {
                builder.Append("Use TTB\n");
                if (target.AutoReplyCtrl.TTBTimeAidingParams.Type == 1)
                {
                    builder.Append("\tType = Coarse\n");
                    builder.Append("\tAccuracy|s = ");
                    builder.Append((uint) (target.AutoReplyCtrl.TTBTimeAidingParams.Accuracy / 0x3e8));
                    builder.Append("\n");
                    builder.Append("\tSkew|ms = ");
                    builder.Append((uint) (target.AutoReplyCtrl.TTBTimeAidingParams.Skew / 0x3e8));
                    builder.Append("\n\n");
                }
                else
                {
                    builder.Append("\n");
                    builder.Append("\tType = Precise\n");
                    builder.Append("\tAccuracy|us = ");
                    builder.Append((uint) (target.AutoReplyCtrl.TTBTimeAidingParams.Accuracy / 0x3e8));
                    builder.Append("\n");
                    builder.Append("\tSkew|ns = ");
                    builder.Append(target.AutoReplyCtrl.TTBTimeAidingParams.Skew);
                    builder.Append("\n\n");
                }
            }
            else
            {
                if (!target.AutoReplyCtrl.TimeTransferCtrl.Reply)
                {
                    return "Time Aiding: Ignore\n\n";
                }
                if (target.AutoReplyCtrl.TimeTransferCtrl.Reject)
                {
                    return "Time Aiding: Reject\n\n";
                }
                if (target.AutoReplyCtrl.AutoReplyParams.UseDOS_ForTimeAid)
                {
                    builder.Append("Use Dos Time Aiding\n");
                    builder.Append("\tAccuracy|s = ");
                    builder.Append(target.AutoReplyCtrl.TimeTransferCtrl.Accuracy);
                    builder.Append("\n");
                    builder.Append("\tSkew|ms = ");
                    builder.Append(target.AutoReplyCtrl.TimeTransferCtrl.Skew);
                    builder.Append("\n");
                    builder.Append("\tUTC_Offset|s = ");
                    builder.Append(target.RxCtrl.UTCOffset);
                    builder.Append("\n\n");
                }
            }
            return builder.ToString();
        }

        internal static byte[] HexStringToByteArray(string Hex)
        {
            byte[] buffer = new byte[Hex.Length / 2];
            int[] numArray = new int[] { 
                0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 0, 0, 0, 0, 0, 
                0, 10, 11, 12, 13, 14, 15
             };
            int index = 0;
            int num2 = 0;
            while (num2 < Hex.Length)
            {
                buffer[index] = (byte) ((numArray[char.ToUpper(Hex[num2]) - 0x30] << 4) | numArray[char.ToUpper(Hex[num2 + 1]) - '0']);
                num2 += 2;
                index++;
            }
            return buffer;
        }

        internal static int max(int x1, int x2)
        {
            if (x1 > x2)
            {
                return x1;
            }
            return x2;
        }

        internal static byte metersToICDHorzErr(double meters)
        {
            ulong num2 = (ulong) meters;
            if (num2 >= 0x168000L)
            {
                return 0xff;
            }
            if ((num2 >= 0x15c000L) && (num2 < 0x168000L))
            {
                return 0xfe;
            }
            if (num2 < 0x18L)
            {
                return 0;
            }
            if ((num2 >= 0x18L) && (num2 < 25.5))
            {
                return 1;
            }
            int num3 = 0;
            while (num3 < 15)
            {
				if ((ulong)(24 * (1 << num3)) >= num2)
                {
                    break;
                }
                num3++;
            }
            if (num3 > 0)
            {
                num3--;
            }
            int num4 = 0;
            while (num4 <= 15)
            {
                if (((ulong) ((0x18 * (((int) 1) << num3)) * (1.0 + (((double) num4) / 16.0)))) > num2)
                {
                    break;
                }
                num4++;
            }
            if (num4 > 0)
            {
                num4--;
            }
            num4 = min(num4, 15);
            return (byte) ((num3 << 4) | num4);
        }

        internal static int min(int x1, int x2)
        {
            if (x1 < x2)
            {
                return x1;
            }
            return x2;
        }

        internal static List<double> skewLatLon(double lat, double lon, double dSkew, double headingSkew)
        {
            List<double> list = new List<double>();
            double num = dSkew * Math.Cos((headingSkew * 3.1415926535897931) / 180.0);
            double num2 = dSkew * Math.Sin((headingSkew * 3.1415926535897931) / 180.0);
            num = (num / clsGlobal.NLMAJA) * 57.295779513082323;
            num2 = (num2 / (clsGlobal.NLMAJA * Math.Cos((lat * 3.1415926535897931) / 180.0))) * 57.295779513082323;
            double item = lat - num;
            double num4 = lon - num2;
            list.Add(item);
            list.Add(num4);
            return list;
        }
    }
}

