using System;
using System.IO;

using System.Diagnostics;
using IEC60870_5_102.Utils;

namespace IEC60870_5_102.TransportLayer
{
    class Transport : IDisposable
    {
        #region Properties


        IClient m_channel;
        /// <summary>
        /// Transport client
        /// </summary>
        IClient Channel
        {
            get
            {
                return m_channel;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new transport layer
        /// </summary>
        /// <param name="endpoint">Endpoint description</param>
        public Transport(TransportEndpoint endpoint)
        {
            this.CreateChannel(endpoint);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the channel of the master
        /// </summary>
        /// <param name="endpoint">Endpoint of the slave</param>
        void CreateChannel(TransportEndpoint endpoint)
        {
            if (this.Channel != null)
                this.Channel.Dispose();

            switch (endpoint.ConnectionType)
            {
                case SuportedConnectionsTypes.serial:
                    m_channel = new RS232Client(endpoint as SerialEndpoint);

                    break;
                case SuportedConnectionsTypes.modem:
                    m_channel = new ModemClient(endpoint as ModemEndpoint);

                    break;
                case SuportedConnectionsTypes.tcp:
                    m_channel = new TcpClient(endpoint as TcpEndpoint);

                    break;
                default:

                    throw new NotSupportedException("The given connection type is not supported by the current version.");
            }
        }

        /// <summary>
        /// Connects the transport to the endpoint
        /// </summary>
        public void Connect()
        {
            this.Channel.Connect();
        }

        /// <summary>
        /// Disconnects the transport layer from the endpoint
        /// </summary>
        public void Disconnect()
        {
            this.Channel.Disconnect();
        }

        /// <summary>
        /// Receives the next packet from the channel
        /// </summary>
        /// <returns></returns>
        public MessagePacket Receive()
        {
            MessagePacket messagePacket = new MessagePacket(this.Channel.Stream);

            messagePacket.Read();

            string msg = "Message read: " + Helpers.ByteArrayToHexString(messagePacket.Message);

            Debug.WriteLine(msg);
            Console.WriteLine(msg);

            this.Log(msg);

            return messagePacket;
        }

        public void Send(byte[] packet)
        {
            MessagePacket messagePacket = new MessagePacket(this.Channel.Stream, packet);

            messagePacket.Write();

            String msg = "Message written: " + Helpers.ByteArrayToHexString(messagePacket.Message);

            Debug.WriteLine(msg);
            Console.WriteLine(msg);

            this.Log(msg);
        }

        void Log(string msg)
        {
            using (StreamWriter w = new StreamWriter("log.txt", true))
            {
                w.WriteLine(msg);
            }
        }
        /// <summary>
        /// Releases the memory of the current instance
        /// </summary>
        public void Dispose()
        {
            this.Channel.Dispose();

            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
