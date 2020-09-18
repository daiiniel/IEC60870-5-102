using System;

using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Represents an incidence
    /// </summary>
    public class Incidence : IEncodeable
    {
        #region Properties

        public byte SPA { get; private set; }

        public byte SPQ { get; private set; }

        public bool SPI { get; private set; }

        public CP56Time2a Datetime { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the class
        /// </summary>
        public Incidence()
        {
            this.Datetime = new CP56Time2a();
        }

        #endregion

        #region ISerializable

        /// <summary>
        /// Serializes the current instance to the specified encoder
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public void Encode(IEncoder encoder)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserializes a C_AC_NA_2 object
        /// </summary>
        /// <param name="decoder">Decodes to use</param>
        public void Decode(IDecoder decoder)
        {
            this.SPA = decoder.ReadByte();

            byte data = decoder.ReadByte();

            this.SPQ = (byte) ((data >> 1));
            this.SPI = ( data & 0x01 ) > 0;

            this.Datetime.Decode(decoder);
        }

        #endregion

        #region Object

        /// <summary>
        /// Gets the text representation of the incidence
        /// </summary>
        /// <returns>String representing the incidence</returns>
        public override string ToString()
        {
            switch (this.SPA)
            {
                case 1:
                    switch (this.SPQ)
                    {
                        case 1:
                            return "System reboot. Previous reboot data lost.";
                        case 2:
                            return "System reboot. After power supply fault. Date, parameters and data are stored.";
                    }

                    break;
                case 3:
                    switch (this.SPQ)
                    {
                        case 0:
                            return "Power supply fault. Voltage under limits.";
                        case 1:
                            return "Voltage meassure fault on phase I";
                        case 2:
                            return "Voltage meassure fault on phase II";
                        case 3:
                            return "Voltage meassure fault on phase III";
                    }
                    break;
                case 7:
                    switch (this.SPQ)
                    {
                        case 2:
                            return "Slave not in sync. ";
                        case 9:
                            return "Date and time changed. Old value.";
                        case 11:
                            return "Date and time changed. New value.";
                        case 21:
                            return "Facturation period of contract I close by command";
                        case 22:
                            return "Facturation period of contract II close by command";
                        case 23:
                            return "Facturation period of contract III close by command";
                    }
                    break;
                case 15:
                    switch (this.SPQ)
                    {
                        case 0:
                            return "Slave parameters changed.";
                        case 1:
                            return "Serial port configuration changed";
                        case 24:
                            return "Contract I power changed.";
                        case 25:
                            return "Contract II power changed.";
                        case 26:
                            return "Contract III power changed.";
                        case 27:
                            return "Contract I festivity days changed";
                        case 28:
                            return "Contract II festivity days changed";
                        case 29:
                            return "Contract III festivity days changed";
                    }
                    break;
                case 16:
                    switch (this.SPQ)
                    {
                        case 0:
                            return "Slave password changed.";
                        case 1:
                            return "Communication port configuration changed";
                        case 21:
                            return "Contract I parameters changed";
                        case 22:
                            return "Contract II parameters changed";
                        case 23:
                            return "Contract III parameters changed";
                    }
                    break;
                case 18:
                    switch (this.SPQ)
                    {
                        case 1:
                            return "Intrusism detected";
                        case 2:
                            return "Communication stablished with a master";
                        case 3:
                            return "Communication stablished with TPL.";
                        case 4:
                            return "Communication with the GPS lost and restored.";

                        case 21:
                            return "Communications stablished to send Contract I information";
                        case 22:
                            return "Communications stablished to send Contract II information";
                        case 23:
                            return "Communications stablished to send Contract III information";
                    }
                    break;
            }

            return "Incidence not implemented";
        }

        #endregion
    }
}