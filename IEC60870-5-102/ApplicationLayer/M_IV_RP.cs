using System;

using IEC60870_5_102.LinkLayer;
using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Reprresents a M_IV_RP to read the isntant values from an slave
    /// </summary>
    [ASDU(163)]
    public class M_IV_RP : ASDU
    {
        #region Properties

        /// <summary>
        /// Instant energy totalizers
        /// </summary>
        public InstantEnergyTotes InstantEnergyTotes { get; private set; }

        /// <summary>
        /// Instant power values
        /// </summary>
        public InstantPowers InstantPowers { get; private set; }

        /// <summary>
        /// Instant voltage and current values
        /// </summary>
        public V_I InstantVI { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public M_IV_RP()
        {
            this.InstantEnergyTotes = new InstantEnergyTotes();
            this.InstantPowers = new InstantPowers();
            this.InstantVI = new V_I();
        }

        #endregion

        #region IEncodeable

        /// <summary>
        /// Serializes the current instance of the class to the specified encoder
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public override void Encode(IEncoder encoder)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserializes the current instance of the class from the specified decoder
        /// </summary>
        /// <param name="decoder">Decoder to use</param>
        public override void Decode(IDecoder decoder)
        {
            int numberOfObjects = decoder.ReadByte();

            this.Cause.Decode(decoder);
            this.ASDUDirection.Decode(decoder);

            for(int ii = 0; ii < numberOfObjects; ii++)
            {
                byte objectDirection = decoder.ReadByte();

                if (objectDirection == ( byte ) ObjectsDirections.EnergyTotes)
                    this.InstantEnergyTotes.Deserialize(decoder);
                else if (objectDirection == ( byte ) ObjectsDirections.ActivePowers)
                    this.InstantPowers.Deserialize(decoder);
                else if (objectDirection == ( byte ) ObjectsDirections.V_I)
                    this.InstantVI.Decode(decoder);
            }
        }

        #endregion
    }
}
