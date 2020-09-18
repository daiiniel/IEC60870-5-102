using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;
using System.IO;

namespace IEC60870_5_102.TransportLayer
{
    public class RS232Client : IClient
    {
        #region Properties

        /// <summary>
        /// Serial port to manage the connection
        /// </summary>
        SerialPort m_serial;
        
        bool m_isConnected;
        /// <summary>
        /// Boolean indicating whether the channel is connected or not
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return m_isConnected;
            }
        }

        /// <summary>
        /// Stream of the connection to exchange data with its endpoint
        /// </summary>
        public Stream Stream
        {
            get
            {
                if (this.m_serial == null)
                    return null;

                return m_serial.BaseStream;
            }
        }

        /// <summary>
        /// Connection endpoint
        /// </summary>
        public TransportEndpoint Endpoint { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the RS232 
        /// </summary>
        public RS232Client(TransportEndpoint endpoint)
        {
            this.Initialize();

            this.Endpoint = endpoint;

            SerialEndpoint s = this.Endpoint as SerialEndpoint;

            m_serial = new SerialPort(s.PortName);

            m_serial.BaudRate = s.BaudRate;
            m_serial.DataBits = s.DataBits;
            m_serial.Parity = s.Parity;
            m_serial.StopBits = s.StopBits;                       
        }

        /// <summary>
        /// Initializes the RS232 channel
        /// </summary>
        void Initialize()
        {

        }

        #endregion

        #region Methods

        /// <summary>
        /// Connects the channel to the given endpoint
        /// </summary>
        public virtual void Connect()
        {
            if (this.IsConnected)
                throw new Exception("The channel is already connected.");
                        
            this.m_serial.Open();
            this.m_serial.BaseStream.Flush();

            m_isConnected = true;
        }

        /// <summary>
        /// Disconnects the channel from its endpoint
        /// </summary>
        public virtual void Disconnect()
        {
            if(!this.IsConnected)
                return;

            this.m_serial.Close();

            m_isConnected = false;
        }


        #endregion

        #region IDisposable

        /// <summary>
        /// Disposes the current channel
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the memory used by the current object
        /// </summary>
        /// <param name="disposing">Release memory or not</param>
        void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(this.m_serial != null)
                {
                    this.m_serial.Dispose();
                    this.m_serial = null;
                }
            }
        }

        #endregion

    }
}
