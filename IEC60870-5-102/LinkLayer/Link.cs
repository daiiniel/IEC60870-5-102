using System;
using System.IO;

using IEC60870_5_102.TransportLayer;

namespace IEC60870_5_102.LinkLayer
{
    /// <summary>
    /// Link layer
    /// </summary>
    public class Link : IDisposable
    {
        #region Properties

        bool m_lastFCB;
        /// <summary>
        /// Last FCB sent to the channel
        /// </summary>
        bool LastFCB
        {
            get
            {
                return m_lastFCB;
            }
            set
            {
                m_lastFCB = value;
            }
        }

        /// <summary>
        /// Represents the transport layer
        /// </summary>
        Transport Transport { get; set; }

        #endregion

        #region Constructors

         /// <summary>
         /// Constructor of the class
         /// </summary>
         /// <param name="channel">Connection endpoint</param>
        public Link(TransportEndpoint endpoint)
        {
            this.Transport = new Transport(endpoint);
        }

        #endregion

        #region Methods
                
        /// <summary>
        /// Connects the master to the slave to start the comunication exchange
        /// </summary>
        public void Connect()
        {
            this.Transport.Connect();
        }

        /// <summary>
        /// Disconnects the master from the slave fishing the comunication exchange
        /// </summary>
        public void Disconnect()
        {
            this.Transport.Disconnect();
        }

        public FunctionCodesPRM0 Send(UInt16 linkDirection, ASDU request)
        {
            return (FunctionCodesPRM0) this.Send(linkDirection,
                new VariableTelegram(
                linkDirection,
                request)).ControlField.Code;
        }

        /// <summary>
        /// Gets a response by sending a telegram to the channel 
        /// </summary>
        /// <param name="linkDirecion">Direction of the slave that will receive the telegram</param>
        /// <param name="request">Request telegram</param>
        /// <returns>Telegram response</returns>
        public Telegram Send(UInt16 linkDirection, Telegram request)
        {
            request.LinkDirection = linkDirection;

            Telegram response = null;

            while (response == null || response.LinkDirection != request.LinkDirection)
            {
                try
                {
                    this.LastFCB = request.ControlField.Update(this.LastFCB);


                    using (MemoryStream ms = new MemoryStream(300))
                    {
                        // Write
                        Telegram.Serialize(ms, request);

                        this.Transport.Send(ms.ToArray());
                    }

                    
                    // Read
                    MessagePacket packet = this.Transport.Receive();

                    using (MemoryStream ms = new MemoryStream(packet.Message))
                    {
                        response = Telegram.Deserialize(ms);
                    }
                   
                }
                catch (Exception e)
                {
                    e.ToString();
                    throw;
                }
            }

            return response;
        }

        /// <summary>
        /// Request the link status of the slave
        /// </summary>
        internal void RequestLinkStatus(ushort linkDirection)
        {
            FixedTelegram request = new FixedTelegram(
                linkDirection,
                FunctionCodesPRM1.LinkRequest);

            Telegram response = this.Send(linkDirection, request);

            if (response.ControlField.Code != ( byte ) FunctionCodesPRM0.LinkStatus)
                throw new Exception(
                    String.Format(
                        "Status request not available. {0} received.",
                        System.Enum.GetName(typeof(FunctionCodesPRM0), response.ControlField.Code)));
        }

        /// <summary>
        /// Resets the remote link within the slave
        /// </summary>
        internal void ResetLink(ushort linkDirection)
        {
            FixedTelegram request = new FixedTelegram(
                linkDirection,
                FunctionCodesPRM1.LinkReset);

            Telegram response = this.Send(linkDirection, request);

            if (response.ControlField.Code != ( byte ) FunctionCodesPRM0.ACK)
                throw new Exception(
                    String.Format(
                        "Could not reset the remote link. {0} code received",
                        Enum.GetName(
                            typeof(FunctionCodesPRM0),
                            response.ControlField.Code)));
        }

        /// <summary>
        /// Request data from the slave
        /// </summary>
        /// <returns>Telegram response from the slave</returns>
        internal ASDU RequestData(ushort linkDirection)
        {
            Telegram request = new FixedTelegram(
                linkDirection,
                FunctionCodesPRM1.Class2UserData);

            Telegram response = this.Send(linkDirection, request);

            if (response.ControlField.Code != ( byte ) FunctionCodesPRM0.UserData)
                throw new Exception(
                    String.Format(
                        "Could not get request data. {0} code received.",
                        Enum.GetName(
                            typeof(FunctionCodesPRM0),
                            response.ControlField.Code)));

            if (response is VariableTelegram)
                return ( response as VariableTelegram ).ASDU;

            //TODO: Handle not receiving data
            throw new Exception("Could not receive data");
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Disposes the current link
        /// </summary>
        public void Dispose()
        {
            this.Transport.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
