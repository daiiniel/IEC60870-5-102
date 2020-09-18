using System;

using IEC60870_5_102.LinkLayer;
using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// ASDU to receive the configuration from the RM
    /// </summary>
    [ASDU(142)]
    public class M_RM_NA_2 : ASDU
    {
        #region Properties

        /// <summary>
        /// Configuration of the point
        /// </summary>
        public MeassurePointConfiguration Configuration { get; private set; }

        #endregion

        #region Constructors

        public M_RM_NA_2() : base()
        {
            this.Configuration = new MeassurePointConfiguration();
        }

        #endregion

        #region IEncodeable

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
            byte nObjects = decoder.ReadByte();

            this.Cause.Decode(decoder);
            this.ASDUDirection.Decode(decoder);

            this.Configuration.Deserialize(decoder);
        }

        #endregion
    }
}
