﻿namespace CommMgrClassLibrary
{
    using AardvarkI2CClassLibrary;
    using CommonClassLibrary;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;
    using System.Windows.Forms;

    public class CommMgrClass
    {
        private NetworkStream _ClientStream;
        private CommonClass.MyRichTextBox _displayWindow;
        private CommonClass.TransmissionType _rxTransType;
        private ReceiverType _rxType = ReceiverType.SLC;
        private CommonClass.TransmissionType _txTransType;
        public TCPIPClient HostAppClient = new TCPIPClient();
        public I2CSlave HostAppI2CSlave;
        public TCPIPServer HostAppServer;

        public CommMgrClass()
        {
            this.HostAppClient.TxCurrentTransmissionType = CommonClass.TransmissionType.GP2;
            this.HostAppServer = new TCPIPServer();
            this.HostAppServer.TxCurrentTransmissionType = CommonClass.TransmissionType.GP2;
            this.HostAppI2CSlave = new I2CSlave();
            this.HostAppI2CSlave.TxCurrentTransmissionType = CommonClass.TransmissionType.GP2;
        }

        public NetworkStream ClientStream
        {
            get
            {
                return this._ClientStream;
            }
            set
            {
                this._ClientStream = value;
            }
        }

        public CommonClass.MyRichTextBox displayWindow
        {
            get
            {
                return this._displayWindow;
            }
            set
            {
                this._displayWindow = value;
            }
        }

        public CommonClass.TransmissionType RxCurrentTransmissionType
        {
            get
            {
                return this._rxTransType;
            }
            set
            {
                this._rxTransType = value;
            }
        }

        public ReceiverType RxType
        {
            get
            {
                return this._rxType;
            }
            set
            {
                this._rxType = value;
            }
        }

        public CommonClass.TransmissionType TxCurrentTransmissionType
        {
            get
            {
                return this._txTransType;
            }
            set
            {
                this._txTransType = value;
            }
        }

        public class I2CSlave : CommMgrClass.SourceDevice
        {
            private CommonClass.MyRichTextBox _displayWindow;
            private int _I2CDevicePortNum;
            private int _I2CDevicePortNumMaster;
            private int _I2CHandle = -1;
            private int _I2CHandleMaster = -1;
            private byte _I2CMasterAddress;
            private byte _I2CSlaveAddress;
            private CommonClass.TransmissionType _rxTransType;
            private CommMgrClass.ReceiverType _rxType = CommMgrClass.ReceiverType.SLC;
            private CommonClass.TransmissionType _txTransType;
            public I2CCommMode I2CTalkMode;
            public const int SLAVE_RESP_SIZE = 0x1a;

            public override void Close()
            {
                try
                {
                    AardvarkApi.aa_i2c_slave_disable(this._I2CHandle);
                    AardvarkApi.aa_close(this._I2CHandle);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Exception in I2C Read Close: " + exception.Message);
                }
                this._I2CHandle = -1;
                try
                {
                    AardvarkApi.aa_close(this._I2CHandleMaster);
                }
                catch (Exception exception2)
                {
                    MessageBox.Show("Exception in I2C Write Close: " + exception2.Message);
                }
                this._I2CHandleMaster = -1;
            }

            public void init()
            {
                byte[] buffer = new byte[0x1a];
                try
                {
                    AardvarkApi.aa_configure(this._I2CHandle, AardvarkConfig.AA_CONFIG_SPI_I2C);
                    AardvarkApi.aa_target_power(this._I2CHandle, 3);
                    AardvarkApi.aa_i2c_pullup(this._I2CHandle, 3);
                    AardvarkApi.aa_i2c_bitrate(this._I2CHandle, 800);
                    for (int i = 0; i < 0x1a; i++)
                    {
                        buffer[i] = (byte) (0x41 + i);
                    }
                    AardvarkApi.aa_i2c_slave_set_response(this._I2CHandle, 0x1a, buffer);
                    AardvarkApi.aa_i2c_slave_enable(this._I2CHandle, this._I2CSlaveAddress, 0, 0);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Exception in I2C Read Init: " + exception.Message);
                }
            }

            public override bool IsOpen()
            {
                if (this._I2CHandle < 0)
                {
                    return false;
                }
                if (this._I2CHandleMaster < 0)
                {
                    return false;
                }
                return true;
            }

            public override bool Open()
            {
                base.ConnectErrorString = string.Empty;
                try
                {
                    this._I2CHandle = AardvarkApi.aa_open(this._I2CDevicePortNum);
                }
                catch
                {
                    base.ConnectErrorString = string.Format("Unable to open Aardvark device on port {0}, error: {1}", this._I2CDevicePortNum, AardvarkApi.aa_status_string(this._I2CHandle));
                    return false;
                }
                if (this._I2CHandle <= 0)
                {
                    base.ConnectErrorString = string.Format("Unable to open Aardvark device on port {0}, Error : {1}", this._I2CDevicePortNum, AardvarkApi.aa_status_string(this._I2CHandle));
                    return false;
                }
                try
                {
                    this._I2CHandleMaster = AardvarkApi.aa_open(this._I2CDevicePortNumMaster);
                }
                catch
                {
                    base.ConnectErrorString = string.Format("Unable to open Aardvark device on port {0}, error: {1}", this._I2CDevicePortNumMaster, AardvarkApi.aa_status_string(this._I2CHandleMaster));
                    return false;
                }
                if (this._I2CHandleMaster <= 0)
                {
                    base.ConnectErrorString = string.Format("Unable to open Aardvark device on port {0}, Error : {1}", this._I2CDevicePortNumMaster, AardvarkApi.aa_status_string(this._I2CHandleMaster));
                    return false;
                }
                return true;
            }

            public override bool WriteData(byte[] msg)
            {
                bool flag = false;
                if (!this.IsOpen())
                {
                    return flag;
                }
                try
                {
                    AardvarkApi.aa_i2c_write(this._I2CHandleMaster, this._I2CMasterAddress, AardvarkI2cFlags.AA_I2C_NO_FLAGS, (short) msg.Length, msg);
                    return true;
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Exception in I2C Write: " + exception.Message);
                    return false;
                }
            }

            public CommonClass.MyRichTextBox displayWindow
            {
                get
                {
                    return this._displayWindow;
                }
                set
                {
                    this._displayWindow = value;
                }
            }

            public int I2CDevicePortNum
            {
                get
                {
                    return this._I2CDevicePortNum;
                }
                set
                {
                    this._I2CDevicePortNum = value;
                }
            }

            public int I2CDevicePortNumMaster
            {
                get
                {
                    return this._I2CDevicePortNumMaster;
                }
                set
                {
                    this._I2CDevicePortNumMaster = value;
                }
            }

            public int I2CHandle
            {
                get
                {
                    return this._I2CHandle;
                }
                set
                {
                    this._I2CHandle = value;
                }
            }

            public int I2CHandleMaster
            {
                get
                {
                    return this._I2CHandleMaster;
                }
                set
                {
                    this._I2CHandleMaster = value;
                }
            }

            public byte I2CMasterAddress
            {
                get
                {
                    return this._I2CMasterAddress;
                }
                set
                {
                    this._I2CMasterAddress = value;
                }
            }

            public byte I2CSlaveAddress
            {
                get
                {
                    return this._I2CSlaveAddress;
                }
                set
                {
                    this._I2CSlaveAddress = value;
                }
            }

            public CommonClass.TransmissionType RxCurrentTransmissionType
            {
                get
                {
                    return this._rxTransType;
                }
                set
                {
                    this._rxTransType = value;
                }
            }

            public CommMgrClass.ReceiverType RxType
            {
                get
                {
                    return this._rxType;
                }
                set
                {
                    this._rxType = value;
                }
            }

            public CommonClass.TransmissionType TxCurrentTransmissionType
            {
                get
                {
                    return this._txTransType;
                }
                set
                {
                    this._txTransType = value;
                }
            }

            public enum I2CCommMode
            {
                COMM_MODE_I2C_MULTI_MASTER,
                COMM_MODE_I2C_SLAVE
            }
        }

        public enum ReceiverType
        {
            GSW,
            SLC,
            NMEA,
            OSP,
            TTB
        }

        public class SourceDevice
        {
            public string ConnectErrorString = string.Empty;

            public virtual void Close()
            {
            }

            public virtual void Connect()
            {
            }

            public virtual bool IsOpen()
            {
                return false;
            }

            public virtual void ListenForClientToConnect()
            {
            }

            public virtual bool Open()
            {
                return true;
            }

            public virtual bool WriteData(byte[] msg)
            {
                return false;
            }

            public virtual void WriteData(CommonClass.MyRichTextBox display, string msg)
            {
            }
        }

        public class TCPIPClient : CommMgrClass.SourceDevice
        {
            private CommonClass.MyRichTextBox _displayWindow;
            private CommonClass.TransmissionType _rxTransType;
            private CommMgrClass.ReceiverType _rxType = CommMgrClass.ReceiverType.SLC;
            private TcpClient _TCPClient;
            private string _TCPClientHostName;
            private int _TCPClientPortNum;
            private NetworkStream _TCPClientStream;
            private TcpClient _TcpServerClient;
            private CommonClass.TransmissionType _txTransType;

            public override void Close()
            {
                this._TCPClientStream.Close();
                this._TCPClient.Close();
            }

            public override void Connect()
            {
            }

            public override bool IsOpen()
            {
                bool flag = false;
                if ((this._TCPClient != null) && this._TCPClient.Connected)
                {
                    flag = true;
                }
                return flag;
            }

            public override bool Open()
            {
                base.ConnectErrorString = string.Empty;
                try
                {
                    this._TCPClient = new TcpClient(this._TCPClientHostName, this._TCPClientPortNum);
                    this._TCPClientStream = this._TCPClient.GetStream();
                }
                catch (Exception exception)
                {
                    base.ConnectErrorString = string.Format("TCP/IP open error:\n{0}\n Check host app and restart if necessary", exception.Message);
                    return false;
                }
                return true;
            }

            public override bool WriteData(byte[] msg)
            {
                bool flag = false;
                if (((this._TCPClientStream == null) || !this._TCPClientStream.CanWrite) || (msg == null))
                {
                    return flag;
                }
                try
                {
                    this._TCPClientStream.Write(msg, 0, msg.Length);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public CommonClass.MyRichTextBox displayWindow
            {
                get
                {
                    return this._displayWindow;
                }
                set
                {
                    this._displayWindow = value;
                }
            }

            public CommonClass.TransmissionType RxCurrentTransmissionType
            {
                get
                {
                    return this._rxTransType;
                }
                set
                {
                    this._rxTransType = value;
                }
            }

            public CommMgrClass.ReceiverType RxType
            {
                get
                {
                    return this._rxType;
                }
                set
                {
                    this._rxType = value;
                }
            }

            public TcpClient TCPClient
            {
                get
                {
                    return this._TCPClient;
                }
                set
                {
                    this._TCPClient = value;
                }
            }

            public string TCPClientHostName
            {
                get
                {
                    return this._TCPClientHostName;
                }
                set
                {
                    this._TCPClientHostName = value;
                }
            }

            public int TCPClientPortNum
            {
                get
                {
                    return this._TCPClientPortNum;
                }
                set
                {
                    this._TCPClientPortNum = value;
                }
            }

            public NetworkStream TCPClientStream
            {
                get
                {
                    return this._TCPClientStream;
                }
                set
                {
                    this._TCPClientStream = value;
                }
            }

            public TcpClient TcpServerClient
            {
                get
                {
                    return this._TcpServerClient;
                }
                set
                {
                    this._TcpServerClient = value;
                }
            }

            public CommonClass.TransmissionType TxCurrentTransmissionType
            {
                get
                {
                    return this._txTransType;
                }
                set
                {
                    this._txTransType = value;
                }
            }
        }

        public class TCPIPServer : CommMgrClass.SourceDevice
        {
            private CommonClass.MyRichTextBox _displayWindow;
            private bool _IsTCPServerConnected;
            private CommonClass.TransmissionType _rxTransType;
            private CommMgrClass.ReceiverType _rxType = CommMgrClass.ReceiverType.SLC;
            private string _TCPServerHostName;
            private IPAddress _TCPServerIPAddress;
            private Thread _TCPServerListenerThread;
            private TcpListener _TCPServerListner;
            private int _TCPServerPortNum;
            private NetworkStream _TCPServerStream;
            private CommonClass.TransmissionType _txTransType;

            public override void Close()
            {
                this._TCPServerStream.Close();
            }

            public override bool IsOpen()
            {
                bool flag = false;
                if ((this._TCPServerStream != null) && this._TCPServerStream.CanRead)
                {
                    flag = true;
                }
                return flag;
            }

            public override bool Open()
            {
                try
                {
                    this._TCPServerListner = new TcpListener(this._TCPServerIPAddress, this._TCPServerPortNum);
                }
                catch
                {
                    MessageBox.Show(string.Format("TCP/IP open error: \nPlease Restart The Host.", new object[0]));
                    return false;
                }
                return true;
            }

            public override bool WriteData(byte[] msg)
            {
                bool flag = false;
                if (((this._TCPServerStream == null) || !this._TCPServerStream.CanWrite) || (msg == null))
                {
                    return flag;
                }
                try
                {
                    this._TCPServerStream.Write(msg, 0, msg.Length);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public CommonClass.MyRichTextBox displayWindow
            {
                get
                {
                    return this._displayWindow;
                }
                set
                {
                    this._displayWindow = value;
                }
            }

            public bool IsTCPServerConnected
            {
                get
                {
                    return this._IsTCPServerConnected;
                }
                set
                {
                    this._IsTCPServerConnected = value;
                }
            }

            public CommonClass.TransmissionType RxCurrentTransmissionType
            {
                get
                {
                    return this._rxTransType;
                }
                set
                {
                    this._rxTransType = value;
                }
            }

            public CommMgrClass.ReceiverType RxType
            {
                get
                {
                    return this._rxType;
                }
                set
                {
                    this._rxType = value;
                }
            }

            public string TCPServerHostName
            {
                get
                {
                    return this._TCPServerHostName;
                }
                set
                {
                    this._TCPServerHostName = value;
                }
            }

            public IPAddress TCPServerIPAddress
            {
                get
                {
                    return this._TCPServerIPAddress;
                }
                set
                {
                    this._TCPServerIPAddress = value;
                }
            }

            public Thread TCPServerListenerThread
            {
                get
                {
                    return this._TCPServerListenerThread;
                }
                set
                {
                    this._TCPServerListenerThread = value;
                }
            }

            public TcpListener TCPServerListner
            {
                get
                {
                    return this._TCPServerListner;
                }
                set
                {
                    this._TCPServerListner = value;
                }
            }

            public int TCPServerPortNum
            {
                get
                {
                    return this._TCPServerPortNum;
                }
                set
                {
                    this._TCPServerPortNum = value;
                }
            }

            public NetworkStream TCPServerStream
            {
                get
                {
                    return this._TCPServerStream;
                }
                set
                {
                    this._TCPServerStream = value;
                }
            }

            public CommonClass.TransmissionType TxCurrentTransmissionType
            {
                get
                {
                    return this._txTransType;
                }
                set
                {
                    this._txTransType = value;
                }
            }
        }
    }
}

