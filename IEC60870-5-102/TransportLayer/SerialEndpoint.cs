using System.IO.Ports;

namespace IEC60870_5_102.TransportLayer
{
    public class SerialEndpoint : TransportEndpoint
    {
        #region Properties

        /// <summary>
        /// Name of the serial port
        /// </summary>
        public string PortName { get; private set; }

        /// <summary>
        /// Baud rate speed
        /// </summary>
        public int BaudRate { get; private set; }

        /// <summary>
        /// Connection parity
        /// </summary>
        public Parity Parity { get; private set; }

        /// <summary>
        /// Data bits
        /// </summary>
        public int DataBits { get; private set; }

        /// <summary>
        /// Stop bits
        /// </summary>
        public StopBits StopBits { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// RS232 endpoint constructor
        /// </summary>
        /// <param name="portName">Name of the port</param>
        /// <param name="baudRate">Rate in bauds</param>
        /// <param name="parity">Connection parity</param>
        /// <param name="dataBits">Data bits</param>
        /// <param name="stopBits">Stop bits of the connection</param>
        public SerialEndpoint(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits) : base(SuportedConnectionsTypes.serial)
        {
            this.PortName = portName;
            this.BaudRate = baudRate;
            this.Parity = parity;
            this.DataBits = dataBits;
            this.StopBits = stopBits;
        }

        /// <summary>
        /// Constructor for base classess
        /// </summary>
        /// <param name="portName">Name of the port</param>
        /// <param name="baudRate">Rate in bauds</param>
        /// <param name="parity">Connection parity</param>
        /// <param name="dataBits">Data bits</param>
        /// <param name="stopBits">Stop bits of the connection</param>
        /// <param name="type">Connection type</param>
        protected SerialEndpoint(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits, SuportedConnectionsTypes type) : base(type)
        {
            this.PortName = portName;
            this.BaudRate = baudRate;
            this.Parity = parity;
            this.DataBits = dataBits;
            this.StopBits = stopBits;
        }

        #endregion
    }
}
