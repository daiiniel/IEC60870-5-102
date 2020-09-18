using System;

using IEC60870_5_102.LinkLayer;
using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Represents a M_TI_TA_2 telegram according to the IEC60870-5-102 to get the date and time
    /// </summary>
    [ASDU(72)]
    public class M_TI_TA_2 : ASDU
    {
        #region Properties

        public CP56Time2a DateTime { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Paramaterless constructor
        /// </summary>
        public M_TI_TA_2()
        {
            this.DateTime = new CP56Time2a();
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

            this.DateTime.Decode(decoder);
        }

        #endregion
    }
}
