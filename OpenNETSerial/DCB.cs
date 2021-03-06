﻿namespace OpenNETCF.IO.Serial
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.InteropServices;
    using System.Text;

    [StructLayout(LayoutKind.Sequential)]
    internal class DCB
    {
        private uint DCBlength;
        public uint BaudRate;
        private BitVector32 Control;
        internal ushort wReserved;
        public ushort XonLim;
        public ushort XoffLim;
        public byte ByteSize;
        public byte Parity;
        public byte StopBits;
        public sbyte XonChar;
        public sbyte XoffChar;
        public sbyte ErrorChar;
        public sbyte EofChar;
        public sbyte EvtChar;
        internal ushort wReserved1;
        private readonly BitVector32.Section sect1;
        private readonly BitVector32.Section DTRsect;
        private readonly BitVector32.Section sect2;
        private readonly BitVector32.Section RTSsect;
        public DCB()
        {
            this.DCBlength = (uint) Marshal.SizeOf(this);
            this.Control = new BitVector32(0);
            this.sect1 = BitVector32.CreateSection(15);
            this.DTRsect = BitVector32.CreateSection(3, this.sect1);
            this.sect2 = BitVector32.CreateSection(0x3f, this.DTRsect);
            this.RTSsect = BitVector32.CreateSection(3, this.sect2);
        }

        internal void _SuppressCompilerWarnings()
        {
            this.wReserved = this.wReserved;
            this.wReserved1 = this.wReserved1;
        }

        public bool fBinary
        {
            get
            {
                return this.Control[1];
            }
            set
            {
                this.Control[1] = value;
            }
        }
        public bool fParity
        {
            get
            {
                return this.Control[2];
            }
            set
            {
                this.Control[2] = value;
            }
        }
        public bool fOutxCtsFlow
        {
            get
            {
                return this.Control[4];
            }
            set
            {
                this.Control[4] = value;
            }
        }
        public bool fOutxDsrFlow
        {
            get
            {
                return this.Control[8];
            }
            set
            {
                this.Control[8] = value;
            }
        }
        public byte fDtrControl
        {
            get
            {
                return (byte) this.Control[this.DTRsect];
            }
            set
            {
                this.Control[this.DTRsect] = value;
            }
        }
        public bool fDsrSensitivity
        {
            get
            {
                return this.Control[0x40];
            }
            set
            {
                this.Control[0x40] = value;
            }
        }
        public bool fTXContinueOnXoff
        {
            get
            {
                return this.Control[0x80];
            }
            set
            {
                this.Control[0x80] = value;
            }
        }
        public bool fOutX
        {
            get
            {
                return this.Control[0x100];
            }
            set
            {
                this.Control[0x100] = value;
            }
        }
        public bool fInX
        {
            get
            {
                return this.Control[0x200];
            }
            set
            {
                this.Control[0x200] = value;
            }
        }
        public bool fErrorChar
        {
            get
            {
                return this.Control[0x400];
            }
            set
            {
                this.Control[0x400] = value;
            }
        }
        public bool fNull
        {
            get
            {
                return this.Control[0x800];
            }
            set
            {
                this.Control[0x800] = value;
            }
        }
        public byte fRtsControl
        {
            get
            {
                return (byte) this.Control[this.RTSsect];
            }
            set
            {
                this.Control[this.RTSsect] = value;
            }
        }
        public bool fAbortOnError
        {
            get
            {
                return this.Control[0x4000];
            }
            set
            {
                this.Control[0x4000] = value;
            }
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DCB:\r\n");
            builder.AppendFormat(null, "  BaudRate:     {0}\r\n", new object[] { this.BaudRate });
            builder.AppendFormat(null, "  Control:      0x{0:x}\r\n", new object[] { this.Control.Data });
            builder.AppendFormat(null, "    fBinary:           {0}\r\n", new object[] { this.fBinary });
            builder.AppendFormat(null, "    fParity:           {0}\r\n", new object[] { this.fParity });
            builder.AppendFormat(null, "    fOutxCtsFlow:      {0}\r\n", new object[] { this.fOutxCtsFlow });
            builder.AppendFormat(null, "    fOutxDsrFlow:      {0}\r\n", new object[] { this.fOutxDsrFlow });
            builder.AppendFormat(null, "    fDtrControl:       {0}\r\n", new object[] { this.fDtrControl });
            builder.AppendFormat(null, "    fDsrSensitivity:   {0}\r\n", new object[] { this.fDsrSensitivity });
            builder.AppendFormat(null, "    fTXContinueOnXoff: {0}\r\n", new object[] { this.fTXContinueOnXoff });
            builder.AppendFormat(null, "    fOutX:             {0}\r\n", new object[] { this.fOutX });
            builder.AppendFormat(null, "    fInX:              {0}\r\n", new object[] { this.fInX });
            builder.AppendFormat(null, "    fNull:             {0}\r\n", new object[] { this.fNull });
            builder.AppendFormat(null, "    fRtsControl:       {0}\r\n", new object[] { this.fRtsControl });
            builder.AppendFormat(null, "    fAbortOnError:     {0}\r\n", new object[] { this.fAbortOnError });
            builder.AppendFormat(null, "  XonLim:       {0}\r\n", new object[] { this.XonLim });
            builder.AppendFormat(null, "  XoffLim:      {0}\r\n", new object[] { this.XoffLim });
            builder.AppendFormat(null, "  ByteSize:     {0}\r\n", new object[] { this.ByteSize });
            builder.AppendFormat(null, "  Parity:       {0}\r\n", new object[] { this.Parity });
            builder.AppendFormat(null, "  StopBits:     {0}\r\n", new object[] { this.StopBits });
            builder.AppendFormat(null, "  XonChar:      {0}\r\n", new object[] { this.XonChar });
            builder.AppendFormat(null, "  XoffChar:     {0}\r\n", new object[] { this.XoffChar });
            builder.AppendFormat(null, "  ErrorChar:    {0}\r\n", new object[] { this.ErrorChar });
            builder.AppendFormat(null, "  EofChar:      {0}\r\n", new object[] { this.EofChar });
            builder.AppendFormat(null, "  EvtChar:      {0}\r\n", new object[] { this.EvtChar });
            return builder.ToString();
        }
        [Flags]
        private enum ctrlBit
        {
            fAbortOnErrorMask = 0x4000,
            fBinaryMask = 1,
            fDsrSensitivityMask = 0x40,
            fDtrControlMask = 0x30,
            fErrorCharMask = 0x400,
            fInXMask = 0x200,
            fNullMask = 0x800,
            fOutxCtsFlowMask = 4,
            fOutxDsrFlowMask = 8,
            fOutXMask = 0x100,
            fParityMask = 2,
            fRtsControlMask = 0x3000,
            fTXContinueOnXoffMask = 0x80
        }

        public enum DtrControlFlags
        {
            Disable,
            Enable,
            Handshake
        }

        public enum RtsControlFlags
        {
            Disable,
            Enable,
            Handshake,
            Toggle
        }
    }
}

