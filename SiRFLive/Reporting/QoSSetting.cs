﻿namespace SiRFLive.Reporting
{
    using System;

    public class QoSSetting
    {
        private string _2dPositionError = "-9999";
        private string _3dPositionError = "-9999";
        private string _almReqFlag = "-9999";
        private string _icdRev = "N/A";
        private bool _init;
        private string _locMethod = "-9999";
        private string _numFixes = "-9999";
        private string _posReqType = "N/A";
        private string _respTMax = "-9999";
        private string _tAccPriority = "-9999";
        private string _tbFixes = "-9999";
        private string _verticalPositionError = "-9999";

        public string AlmReqFlag
        {
            get
            {
                return this._almReqFlag;
            }
            set
            {
                this._almReqFlag = value;
            }
        }

        public string ICDSTR
        {
            get
            {
                return this._icdRev;
            }
            set
            {
                this._icdRev = value;
            }
        }

        public bool IsInit
        {
            get
            {
                return this._init;
            }
            set
            {
                this._init = value;
            }
        }

        public string LocMethod
        {
            get
            {
                return this._locMethod;
            }
            set
            {
                this._locMethod = value;
            }
        }

        public string NumFixes
        {
            get
            {
                return this._numFixes;
            }
            set
            {
                this._numFixes = value;
            }
        }

        public string Position2DError
        {
            get
            {
                return this._2dPositionError;
            }
            set
            {
                this._2dPositionError = value;
            }
        }

        public string Position3DError
        {
            get
            {
                return this._3dPositionError;
            }
            set
            {
                this._3dPositionError = value;
            }
        }

        public string PosReqType
        {
            get
            {
                return this._posReqType;
            }
            set
            {
                this._posReqType = value;
            }
        }

        public string RespTMax
        {
            get
            {
                return this._respTMax;
            }
            set
            {
                this._respTMax = value;
            }
        }

        internal string TAccPriority
        {
            get
            {
                return this._tAccPriority;
            }
            set
            {
                this._tAccPriority = value;
            }
        }

        public string TBFixes
        {
            get
            {
                return this._tbFixes;
            }
            set
            {
                this._tbFixes = value;
            }
        }

        public string VerticalPositionError
        {
            get
            {
                return this._verticalPositionError;
            }
            set
            {
                this._verticalPositionError = value;
            }
        }
    }
}

