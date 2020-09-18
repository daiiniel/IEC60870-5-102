using System;

using IEC60870_5_102.LinkLayer;
using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Instant values request
    /// </summary>
    [ASDU(162)]
    public class C_IV_RQ : ASDU
    {
        #region Constructors

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public C_IV_RQ() : base()
        {

        }
        
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="meassurePoint">Slave meassure point</param>
        public C_IV_RQ(
            UInt16 meassurePoint) : base(
                TransmissionCauses.Request,
                meassurePoint,
                RegisterDirections.Default)
        {

        }

        #endregion

        #region ISerializable

        /// <summary>
        /// Serializes the current instance of the class to the specified encoder
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public override void Encode(IEncoder encoder)
        {            
            encoder.Write(( byte ) 3);

            this.Cause.Encode(encoder);
            this.ASDUDirection.Encode(encoder);

            encoder.Write(( byte ) ObjectsDirections.EnergyTotes);
            encoder.Write(( byte ) ObjectsDirections.ActivePowers);
            encoder.Write(( byte ) ObjectsDirections.V_I);
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
        }

        #endregion
    }
}
