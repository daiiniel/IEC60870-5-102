using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEC60870_5_102.TransportLayer
{
    /// <summary>
    /// Modem endpoint
    /// </summary>
    public class ModemEndpoint : SerialEndpoint
    {

        #region Properties

        /// <summary>
        /// Telephone number to use
        /// </summary>
        public string TelephoneNumber { get; private set; }

        /// <summary>
        /// PIN code of the modem
        /// </summary>
        public string PIN { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="telephoneNumber">Telephone number of the endpoint</param>
        /// <param name="portName">Serial port to use</param>
        /// <param name="baudRate">Speed to use</param>
        /// <param name="parity">Parity to use</param>
        /// <param name="dataBits">Data bits</param>
        /// <param name="stopBits">Stop bits</param>
        public ModemEndpoint(string telephoneNumber, string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits) : base(portName, baudRate, parity, dataBits, stopBits, SuportedConnectionsTypes.modem)
        {
            this.TelephoneNumber = telephoneNumber;
            this.PIN = String.Empty;
        }

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="telephoneNumber">Telephone number of the endpoint</param>
        /// <param name="portName">Serial port to use</param>
        /// <param name="baudRate">Speed to use</param>
        /// <param name="parity">Parity to use</param>
        /// <param name="dataBits">Data bits</param>
        /// <param name="stopBits">Stop bits</param>
        /// <param name="PIN">PIN code of the modem</param>
        public ModemEndpoint(string telephoneNumber, string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits, string PIN) : base(portName, baudRate, parity, dataBits, stopBits, SuportedConnectionsTypes.modem)
        {
            this.TelephoneNumber = telephoneNumber;
            this.PIN = PIN;
        }


        #endregion
        
    }
}
