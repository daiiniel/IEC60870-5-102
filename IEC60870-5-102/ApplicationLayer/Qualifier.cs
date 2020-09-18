using System;

using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Qualifier
    /// </summary>
    public class Qualifier
    {
        #region Properties

        /// <summary>
        /// Boolean indicating wether the meter is valid or not
        /// </summary>
        public bool IV { get; private set; }

        /// <summary>
        /// Boolean indicating if the meter was synchronized during the period
        /// </summary>
        public bool CA { get; private set; }

        /// <summary>
        /// Boolean indicating wether there was overflow within the read
        /// </summary>
        public bool CY { get; private set; }

        /// <summary>
        /// Hourly verification during the period (VH=1)
        /// </summary>
        public bool VH { get; private set; }

        /// <summary>
        /// Boolean determining if the parameteres where modified during the period (MP = 1)
        /// </summary>
        public bool MP { get; private set; }

        /// <summary>
        /// Boolean indicating if there was intrusism during the period (INT = 1)
        /// </summary>
        public bool INT { get; private set; }

        /// <summary>
        /// Period not completed due to a power supply fault during the period (AL = 1)
        /// </summary>
        public bool AL { get; private set; }

        /// <summary>
        /// Reserved bit
        /// </summary>
        public bool RES { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the class
        /// </summary>
        public Qualifier()
        {

        }

        #endregion

        #region IEncodeable

        /// <summary>
        /// Serializes the current instance of the class to the specified encoder
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public void Serialize(IEncoder encoder)
        {
            byte data =0;

            if (this.RES)
                data |= 0x01;

            if (this.AL)
                data |= 0x02;

            if (this.INT)
                data |= 0x04;

            if (this.MP)
                data |= 0x08;

            if (this.VH)
                data |= 0x10;

            if (this.CY)
                data |= 0x20;

            if (!this.CA)
                data |= 0x40;

            if (!this.IV)
                data |= 0x80;

            encoder.Write(data);
        }

        /// <summary>
        /// Deserializes the current instance of the class from the specified decoder
        /// </summary>
        /// <param name="decoder">Decoder to use</param>
        public void Deserialize(IDecoder decoder)
        {
            byte data = decoder.ReadByte();

            this.RES = ( data & 0x01 ) > 0;
            this.AL = ( data & 0x02 ) > 0;
            this.INT = ( data & 0x03 ) > 0;
            this.MP = ( data & 0x04 ) > 0;
            this.VH = ( data & 0x05 ) > 0;
            this.CY = ( data & 0x06 ) > 0;
            this.CA = ( data & 0x07 ) > 0;
            this.IV = !( ( data & 0x08 ) > 0 );
        }

        #endregion

        #region Object

        /// <summary>
        /// Returns a string representing the current qualifier instance
        /// </summary>
        /// <returns>Qualifier string</returns>
        public override string ToString()
        {
            return String.Format(
                "{0}",
                this.IV.ToString());
        }

        #endregion

    }
}
