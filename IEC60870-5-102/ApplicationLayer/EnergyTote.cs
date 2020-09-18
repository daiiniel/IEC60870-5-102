using System;

using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Represents an energy tote
    /// </summary>
    public class EnergyTote : IEncodeable
    {
        #region Properties

        /// <summary>
        /// Value of the Tote
        /// </summary>
        public UInt32 Value { get; private set; }

        /// <summary>
        /// Determines whether the toet is valid or not
        /// </summary>
        public bool IV { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public EnergyTote()
        {

        }

        #endregion

        #region IEncodable

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
            UInt32 data = decoder.ReadUInt32();

            this.Value = data & 0x3FFFFFFF;

            this.IV = !( (data & 0x80000000) > 0 );
        }

        #endregion

        #region Object

        /// <summary>
        /// Gets a string representing the current instance of an energy tote
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Value.ToString();
        }

        #endregion

    }
}
