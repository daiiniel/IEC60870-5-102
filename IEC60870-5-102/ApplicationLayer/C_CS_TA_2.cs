using System;

using IEC60870_5_102.LinkLayer;
using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Represents a C_CS_TA_2 to send the Date and time to the slave
    /// </summary>
    [ASDU(181)]
    public class C_CS_TA_2 : ASDU
    {
        #region MyRegion

        /// <summary>
        /// Date time 
        /// </summary>
        CP56Time2a DateTime { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public C_CS_TA_2() : base()
        {
            this.DateTime = new CP56Time2a();
        }
        
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="linkDirection">Link direction</param>
        /// <param name="meassurePoint">Meassure point</param>
        /// <param name="dateTime">Datetime to set</param>
        /// <param name="lastFBC">Last FBC used</param>
        public C_CS_TA_2(
            UInt16 meassurePoint,
            DateTime dateTime) : base
            (
                TransmissionCauses.Activation,
                meassurePoint,
                RegisterDirections.Default)
        {
            this.DateTime = new CP56Time2a(dateTime);
        }

        #endregion
        
        #region IEncodeable

        /// <summary>
        /// Serializes the current instance of the class to the specified encoder
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public override void Encode(IEncoder encoder)
        {
            encoder.Write(( byte ) 1);

            this.Cause.Encode(encoder);
            this.ASDUDirection.Encode(encoder);

            this.DateTime.Encode(encoder);
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
