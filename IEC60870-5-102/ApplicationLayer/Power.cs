using System;

using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Represents a power value
    /// </summary>
    public class Power : IEncodeable
    {
        #region Properties

        public UInt32 Active { get; private set; }

        public UInt32 Reactive { get; private set; }

        public float PowerFactor { get; private set; }

        public bool Imported { get; private set; }

        public bool Inductive { get; private set; }

        public bool IV { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Power()
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
            UInt64 data = decoder.ReadUInt64();

            this.Active = (UInt32) data & 0xFFFFFF;
            this.Reactive = (UInt32) (( data >> 24 ) & 0xFFFFFF);

            this.PowerFactor = (( float ) ( ( data >> 48 ) & 0x3FF )) / 1000 ;

            this.Imported = !( ( ( data >> 58 ) & 0x1 ) > 0 );
            this.Inductive = !( ( ( data >> 59 ) & 0x01 ) > 0 );

            this.IV = !( ( ( data >> 63 ) & 0x01 ) > 0 );
        }

        #endregion

    }
}
