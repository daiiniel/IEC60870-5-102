using System;

using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.TransportLayer
{
    /// <summary>
    /// Modem channel for the connection
    /// </summary>
    class ModemClient : RS232Client
    {        
        #region Constructor

        /// <summary>
        /// Creates an instance of the ModemChannel 
        /// </summary>
        /// <param name="endpoint">Endpoint of the connection</param>
        public ModemClient(TransportEndpoint endpoint) : base(endpoint)
        {
            base.Connect();

            string ate0 = this.SendCommand("ATE0");

            //if (!this.SendCommand("AT").Contains("OK"))
            //    throw new Exception(
            //        String.Format(
            //            "Modem not recognized in port {0}",
            //            (endpoint as ModemEndpoint).PortName));

            //if (this.SendCommand("AT+CPIN=?").Contains("OK"))
            //    if (!this.SendCommand("AT+CPIN?").Contains("READY"))
            //        this.SendCommand(
            //            String.Format(
            //                "AT+CPIN={0}",
            //                (endpoint as ModemEndpoint).PIN));

            base.Disconnect();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Connects the channel to the given endpoint
        /// </summary>
        public override void Connect()
        {
            base.Connect();

            string pinRes = this.SendCommand("AT+CPIN?");

            if(pinRes.Contains("SIM PIN"))
            {
                pinRes = this.SendCommand($"AT+CPIN={(Endpoint as ModemEndpoint).PIN}");
            }

            string res = this.SendCommand($"ATD{(this.Endpoint as ModemEndpoint).TelephoneNumber}");

            if (!res.Contains("CONNECT"))
            {
                throw new Exception("Could not connect to the given endpoint.");
            }
        }

        /// <summary>
        /// Disconnects from the endpoint
        /// </summary>
        public override void Disconnect()
        {
            this.SendCommand("ATH", false);

            base.Disconnect();
        }

        /// <summary>
        /// Sends a command to the modem
        /// </summary>
        /// <param name="command">String command to send</param>
        /// <returns>Modem response</returns>
        String SendCommand(String command, bool shouldReturn = true)
        {
            if (!this.IsConnected)
                throw new InvalidOperationException("The serial port is not opened.");

            BinaryEncoder encoder = new BinaryEncoder(this.Stream);

            System.Threading.Thread.Sleep(200);

            encoder.Write(
                String.Format(
                    "{0}{1}",
                    command,
                    Convert.ToChar((byte) 0x0D)));

            if (!shouldReturn)
                return null;

            BinaryDecoder decoder = new BinaryDecoder(this.Stream);

            string response = decoder.ReadString((byte)0x0D);
            
            return response;
        }

        #endregion
    }
}
