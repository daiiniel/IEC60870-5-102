using System;

using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Configuration of the meassure point
    /// </summary>
    public class MeassurePointConfiguration
    {
        #region Properties

        /// <summary>
        /// Code of the manufacter
        /// </summary>
        public byte ManufacterCode { get; private set; }

        /// <summary>
        /// Register model (meter)
        /// </summary>
        public string RegisterModel { get; private set; }

        /// <summary>
        /// Firmware version
        /// </summary>
        public byte FirmwareVersion { get; private set; }

        /// <summary>
        /// Serial number of the meter
        /// </summary>
        public UInt32 SerialNumber { get; private set; }

        /// <summary>
        /// Date of the standart
        /// </summary>
        public byte StandartDate { get; private set; }

        /// <summary>
        /// Date of the version implementation
        /// </summary>
        public CP40Time2a ProtocolVersionDate { get; private set; }

        /// <summary>
        /// Percentage of the battery status (0-100)
        /// </summary>
        public byte BatteryStatus { get; private set; }

        /// <summary>
        /// Serial port 1 configuration
        /// </summary>
        public SerialPortConfiguration SerialPort1 { get; private set; }

        /// <summary>
        /// Serial port 2 configuration
        /// </summary>
        public SerialPortConfiguration SerialPort2 { get; private set; }

        /// <summary>
        /// Voltage at the primary
        /// </summary>
        public UInt32 PrimaryVoltage { get; private set; }

        /// <summary>
        /// Voltage at the secondary
        /// </summary>
        public UInt32 SecondaryVoltage { get; private set; }

        /// <summary>
        /// Current at the primary
        /// </summary>
        public UInt32 PrimaryCurrent { get; private set; }

        /// <summary>
        /// Current at the secondary
        /// </summary>
        public UInt32 SecondaryCurrent { get; private set; }

        /// <summary>
        /// Integration period 1 (Register direction 11)
        /// </summary>
        public byte IntegrationPeriodI { get; private set; }

        /// <summary>
        /// Integration period 2 (Register direction 12)
        /// </summary>
        public byte IntegrationPeriodII { get; private set; }

        /// <summary>
        /// Integration period 3 (Register direction 13)
        /// </summary>
        public byte IntegrationPeriodIII { get; private set; }

        /// <summary>
        /// Status of the first contract
        /// </summary>
        public ContractStatus ContractI { get; private set; }

        /// <summary>
        /// Status of the second contract
        /// </summary>
        public ContractStatus ContractII { get; private set; }

        /// <summary>
        /// Status of the third contract
        /// </summary>
        public ContractStatus ContractIII { get; private set; }

        #endregion

        #region Constructor

        public MeassurePointConfiguration()
        {
            this.ProtocolVersionDate = new CP40Time2a();
            this.SerialPort1 = new SerialPortConfiguration();
            this.SerialPort2 = new SerialPortConfiguration();
        }

        #endregion

        #region IEncodeable

        /// <summary>
        /// Serializes the current instance to the specified encoder
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public void Serialize(IEncoder encoder)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserializes the current instance
        /// </summary>
        /// <param name="decoder">Decodes to use</param>
        public void Deserialize(IDecoder decoder)
        {
            this.ManufacterCode = decoder.ReadByte();
            this.RegisterModel = decoder.ReadString(2);
            this.FirmwareVersion = decoder.ReadByte();
            this.SerialNumber = decoder.ReadUInt32();
            this.StandartDate = decoder.ReadByte();
            this.ProtocolVersionDate.Decode(decoder);
            this.BatteryStatus = decoder.ReadByte();

            this.SerialPort1.Decode(decoder);
            this.SerialPort2.Decode(decoder);

            this.PrimaryVoltage = decoder.ReadUInt32();
            this.SecondaryVoltage = decoder.ReadUInt32();

            this.PrimaryCurrent = decoder.ReadUInt32();
            this.PrimaryCurrent = decoder.ReadUInt32();

            this.IntegrationPeriodI = decoder.ReadByte();
            this.IntegrationPeriodII = decoder.ReadByte();
            this.IntegrationPeriodIII = decoder.ReadByte();

            byte data = decoder.ReadByte();

            this.ContractI = (ContractStatus) (data & 0x03);
            this.ContractII = ( ContractStatus ) ( ( data >> 2 ) & 0x03 );
            this.ContractIII = ( ContractStatus ) ( ( data >> 4 ) & 0x03 );

            decoder.ReadBuffer(165);
        }

        #endregion

    }
}
