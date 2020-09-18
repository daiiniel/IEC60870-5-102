using System;

namespace IEC60870_5_102.TransportLayer
{    
    /// <summary>
    /// Represents a tcp transport endpoint
    /// </summary>
    public class TcpEndpoint : TransportEndpoint
    {
        #region Properties

        string m_Host;
        /// <summary>
        /// Host endpoint
        /// </summary>
        public string Host
        {
            get
            {
                return m_Host;
            }
            set
            {
                m_Host = value;
            }
        }

        UInt32 m_Port;
        /// <summary>
        /// Connection endpoint port
        /// </summary>
        public UInt32 Port
        {
            get
            {
                return m_Port;
            }
            set
            {
                m_Port = value;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance of the TcpEndpoint class
        /// </summary>
        /// <param name="host">Host endpoint</param>
        /// <param name="port">Connection endpoint port</param>
        public TcpEndpoint(string host, UInt32 port) : base(SuportedConnectionsTypes.tcp)
        {
            this.Host = host;
            this.Port = port;
        }

        #endregion

        #region Public methods

        #region Object

        /// <summary>
        /// Returns a string representing the current instance of the tcp endpoint
        /// </summary>
        /// <returns>String representing the current instance</returns>
        public override string ToString()
        {
            return String.Format("{0}:{1}", this.Host, this.Port);
        }

        #endregion

        #endregion
    }
}
