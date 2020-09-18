using System;

using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Supported serial speeds in bauds
    /// </summary>
    public enum SupportedSerialSpeeds : byte
    {
        PortNotAvailable = 0,
        v300 = 1,
        v600 = 2,
        v1200 = 3,
        v2400 = 4,
        v4800 = 5,
        v9600 = 6,
        v14400 = 7,
        v19200 = 8,
        v28800 = 9,
        v38400 = 10,
        v57600 = 11,
        Unknow = 255
    }

    /// <summary>
    /// Enumeration of the supported configurations for serial ports
    /// </summary>
    public enum SupportedSerialConfigurations : byte
    {
        PortNotAvailable = 0,
        v7N1 = 1,
        v7E1 = 2,
        v7O1 =3,
        v7N2 = 4,
        v7E2 = 5,
        v7O2 = 6,
        v8N1 = 7,
        v8E1 = 8,
        v8O1 = 9,
        v8N2 = 10,
        v8E2 = 11,
        v8O2 = 12,
    }

    /// <summary>
    /// Configuration of a serial port
    /// </summary>
    public class SerialPortConfiguration : IEncodeable
    {
        #region Properties

        /// <summary>
        /// Code of the port speed
        /// </summary>
        public SupportedSerialSpeeds SpeedCode { get; private set; }

        /// <summary>
        /// Code of the serial port configuration: data bits, parity and stop bits (eg. 9N1)
        /// </summary>
        public SupportedSerialConfigurations ConfigCode { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public byte InitCharMode { get; private set; }

        /// <summary>
        /// String to send 
        /// </summary>
        public string InitString { get; private set; }

        #endregion

        #region Constructor

        #endregion

        #region IEncodeable

        /// <summary>
        /// Serializes the current instance to the specified encoder
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public void Encode(IEncoder encoder)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserializes the current instance
        /// </summary>
        /// <param name="decoder">Decodes to use</param>
        public void Decode(IDecoder decoder)
        {
            this.SpeedCode = (SupportedSerialSpeeds) decoder.ReadByte();
            this.ConfigCode = (SupportedSerialConfigurations) decoder.ReadByte();

            this.InitCharMode = decoder.ReadByte();
            this.InitString = decoder.ReadString(20);
        }

        #endregion

    }
}
