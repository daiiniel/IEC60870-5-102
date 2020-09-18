using System;

using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Represents the voltage and current instant values
    /// </summary>
    public class V_I : IEncodeable
    {
        #region Properties

        /// <summary>
        /// Instant values of the first phase
        /// </summary>
        public Phase PhaseI { get; private set; }

        /// <summary>
        /// Instant values of the second values
        /// </summary>
        public Phase PhaseII { get; private set; }

        /// <summary>
        /// Instant values of the third phase
        /// </summary>
        public Phase PhaseIII { get; private set; }

        /// <summary>
        /// Datetime of the values
        /// </summary>
        public CP40Time2a DateTime { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Parameterless constructor for deserialization
        /// </summary>
        public V_I()
        {
            this.PhaseI = new Phase();
            this.PhaseII = new Phase();
            this.PhaseIII = new Phase();

            this.DateTime = new CP40Time2a();
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
            this.PhaseI.Decode(decoder);
            this.PhaseII.Decode(decoder);
            this.PhaseIII.Decode(decoder);
            this.DateTime.Decode(decoder);
        }

        #endregion
    }
}
