using System;

using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Represents the instant powers values read from an slave
    /// </summary>
    public class InstantPowers
    {

        #region Properties

        /// <summary>
        /// Total power of the three phasess
        /// </summary>
        public Power Total { get; private set; }

        /// <summary>
        /// Instant power of the phase one
        /// </summary>
        public Power Fase_I { get; private set; }

        /// <summary>
        /// Instant power of the phase two
        /// </summary>
        public Power Fase_II { get; private set; }

        /// <summary>
        /// Instant power of the phase three
        /// </summary>
        public Power Fase_III { get; private set; }

        /// <summary>
        /// Date time of the values
        /// </summary>
        public CP40Time2a Datetime { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public InstantPowers()
        {
            this.Total = new Power();
            this.Fase_I = new Power();
            this.Fase_II = new Power();
            this.Fase_III = new Power();

            this.Datetime = new CP40Time2a();
        }

        #endregion

        #region IEncodeable

        /// <summary>
        /// Serializes the current instance of the class to the specified encoder
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public void Serialize(IEncoder encoder)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserializes the current instance of the class from the specified decoder
        /// </summary>
        /// <param name="decoder">Decoder to use</param>
        public void Deserialize(IDecoder decoder)
        {
            this.Total.Decode(decoder);
            this.Fase_I.Decode(decoder);
            this.Fase_II.Decode(decoder);
            this.Fase_III.Decode(decoder);

            this.Datetime.Decode(decoder);
        }

        #endregion

    }
}
