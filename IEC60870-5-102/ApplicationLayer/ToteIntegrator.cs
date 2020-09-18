using System;

using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Represents a tote integrator according to the IEC 60870
    /// </summary>
    public class ToteIntegrator : IEncodeable
    {

        #region Properties

        /// <summary>
        /// Qualifier
        /// </summary>
        public Qualifier Qualifier { get; private set; }

        /// <summary>
        /// Value of the tote integrator
        /// </summary>
        public UInt32 Value { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor of the class
        /// </summary>
        public ToteIntegrator()
        {
            this.Qualifier = new Qualifier();
        }

        #endregion

        #region IEncodeable

        /// <summary>
        /// Serializes the current instance of the class to the specified encoder
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public void Encode(IEncoder encoder)
        {
            encoder.Write(this.Value);
            this.Qualifier.Serialize(encoder);
        }

        /// <summary>
        /// Deserializes the current instance of the class from the specified decoder
        /// </summary>
        /// <param name="decoder">Decoder to use</param>
        public void Decode(IDecoder decoder)
        {
            this.Value = decoder.ReadUInt32();
            this.Qualifier.Deserialize(decoder);                
        }

        #endregion

        #region Objects

        /// <summary>
        /// Gets a string representing the current instance of a tote integrator
        /// </summary>
        /// <returns>String representing the current instance</returns>
        public override string ToString()
        {
            return this.Value.ToString();
        }

        #endregion

    }
}
