using System;
using System.IO;

namespace IEC60870_5_102.TransportLayer
{
    /// <summary>
    /// Connection interface for transport layers
    /// </summary>
    interface IClient : IDisposable
    {

        #region Properties

        /// <summary>
        /// Connection stream to receive and send information
        /// </summary>
        Stream Stream { get; }

        /// <summary>
        /// Boolean indicating whether the connection is stablished or not
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Transport endpoint
        /// </summary>
        TransportEndpoint Endpoint { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Connects the transport layer
        /// </summary>
        void Connect();

        /// <summary>
        /// Disconnects the transport layers
        /// </summary>
        void Disconnect();

        #endregion

    }
}
