using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace IEC60870_5_102.TransportLayer
{
    /// <summary>
    /// Represents a new tcp client
    /// </summary>
    public class TcpClient : IClient
    {
        #region Properties

        Socket m_socket;

        public Stream Stream
        {
            get
            {
                if (!this.IsConnected)
                    return null;

                return new NetworkStream(m_socket);
            }
        }

        /// <summary>
        /// Gets a boolean that indicates whether the client is connected
        /// </summary>
        public bool IsConnected
        {
            get
            {
                if (m_socket == null)
                    return false;

                return m_socket.Connected;
            }
        } 

        /// <summary>
        /// Connection endpoint details
        /// </summary>
        public TransportEndpoint Endpoint { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the Tcp transport client
        /// </summary>
        /// <param name="endpoint">Endpoint information</param>
        public TcpClient(TransportEndpoint endpoint)
        {
            if (!(endpoint is TcpEndpoint))
                throw new ArgumentException("Given endpoint must be of type TcpEndpoing.");

            this.Endpoint = endpoint;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Connects the client
        /// </summary>
        public void Connect()
        {
            if (this.IsConnected)
                throw new InvalidOperationException("Client is already connected.");

            if (m_socket != null)
                m_socket.Dispose();

            TcpEndpoint e = this.Endpoint as TcpEndpoint;

            m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            m_socket.Connect(IPAddress.Parse(e.Host), (int) e.Port);
        }

        /// <summary>
        /// Disconnects the client from the endpoint
        /// </summary>
        public void Disconnect()
        {
            if (!this.IsConnected && m_socket == null)
                return;

            m_socket.Disconnect(false);
            m_socket.Dispose();

            m_socket = null;
        }

        public void Dispose()
        {
            this.Disconnect();

            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
