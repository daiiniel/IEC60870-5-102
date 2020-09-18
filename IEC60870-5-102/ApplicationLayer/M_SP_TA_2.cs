using System;
using System.Collections.Generic;

using IEC60870_5_102.LinkLayer;
using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Represents a single information event
    /// </summary>
    [ASDU(1)]
    public class M_SP_TA_2 : ASDU
    {
        #region Properties

        /// <summary>
        /// Incidences of the slave
        /// </summary>
        public List<Incidence> Incidences { get; private set; }

        #endregion

        #region Constructors

        public M_SP_TA_2() : base()
        {
            this.Incidences = new List<Incidence>();
        }

        #endregion

        #region ISerializable

        /// <summary>
        /// Serializes the current instance to the specified encoder
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public override void Encode(IEncoder encoder)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserializes a C_AC_NA_2 object
        /// </summary>
        /// <param name="decoder">Decodes to use</param>
        public override void Decode(IDecoder decoder)
        {
            byte nIncidences = decoder.ReadByte();

            this.Cause.Decode(decoder);
            this.ASDUDirection.Decode(decoder);

            for(int ii = 0; ii < nIncidences; ii++)
            {
                Incidence incidence = new Incidence();
                incidence.Decode(decoder);

                this.Incidences.Add(incidence);
            }
        }

        #endregion
    }
}
