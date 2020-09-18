using System;

using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Class to store instant phase values such as current and voltage
    /// </summary>
    public class Phase : IEncodeable
    {
        #region Properties

        /// <summary>
        /// Instant current of the phase
        /// </summary>
        public float Current { get; private set; }

        /// <summary>
        /// Instant voltage of the phase
        /// </summary>
        public float Voltage { get; private set; }

        /// <summary>
        /// Boolean determining wether the current values are valid or not
        /// </summary>
        public bool IV { get; private set; }

        #endregion

        #region MyRegion

        public Phase()
        {

        }

        #endregion

        #region IEncodeable

        /// <summary>
        /// Serializes the current instance of the class to the specified encoder
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public void Encode(IEncoder encoder)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserializes the current instance of the class from the specified decoder
        /// </summary>
        /// <param name="decoder">Decoder to use</param>
        public void Decode(IDecoder decoder)
        {
            byte data1, data2, data3;

            data1 = decoder.ReadByte();
            data2 = decoder.ReadByte();
            data3 = decoder.ReadByte();

            this.Current = ( (UInt32) data3 << 16 ) | ((UInt32) data2 << 8 ) | (UInt32) data1;
            this.Current /= 10;

            UInt32 data = decoder.ReadUInt32();

            this.Voltage = (data & ( UInt32 ) 0x7FFFFF);
            this.Voltage /= 10;

            this.IV = !((data & ( (UInt32) 0x80 << 24 )) > 0);
        }

        #endregion

    }
}
