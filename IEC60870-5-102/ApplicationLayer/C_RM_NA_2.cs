using System;

using IEC60870_5_102.LinkLayer;
using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// ASDU to read the slave parameters configuration
    /// </summary>
    [ASDU(141)]
    public class C_RM_NA_2 : ASDU
    {
        #region Properties

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the class
        /// </summary>
        public C_RM_NA_2() : base()
        {

        }

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="meassurePoint">Meassure point</param>
        public C_RM_NA_2(
            UInt16 meassurePoint) : base(
                TransmissionCauses.Request,
                meassurePoint,
                RegisterDirections.Default)
        {

        }

        #endregion

        #region ISerializable

        /// <summary>
        /// Serializes the current instance to the specified encoder
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public override void Encode(IEncoder encoder)
        {
            encoder.Write(( byte ) 1);

            this.Cause.Encode(encoder);
            this.ASDUDirection.Encode(encoder);
        }

        /// <summary>
        /// Deserializes a C_AC_NA_2 object
        /// </summary>
        /// <param name="decoder">Decodes to use</param>
        public override void Decode(IDecoder decoder)
        {
            byte nObjects = decoder.ReadByte();

            this.Cause.Decode(decoder);
            this.ASDUDirection.Decode(decoder);
        }

        #endregion

    }
}
