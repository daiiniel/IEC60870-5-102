using System;

using IEC60870_5_102.LinkLayer;
using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Reads the information event by time interval
    /// </summary>
    [ASDU(102)]
    public class C_SP_NB_2 : ASDU
    {

        #region Properties

        /// <summary>
        /// Date from
        /// </summary>
        public CP40Time2a From { get; private set; }

        /// <summary>
        /// Date to
        /// </summary>
        public CP40Time2a To { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public C_SP_NB_2() : base()
        {
            this.From = new CP40Time2a();
            this.To = new CP40Time2a();
        }

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="meassurePoint">Meassure point of the slave</param>
        /// <param name="from">Date from</param>
        /// <param name="to">Date to</param>
        /// <param name="objectDirection">Direction of the object to read</param>
        public C_SP_NB_2(
            UInt16 meassurePoint,
            RegisterDirections objectDirection,
            DateTime from,
            DateTime to) : base(
                TransmissionCauses.Activation,
                meassurePoint,
                objectDirection)
        {
            this.From = new CP40Time2a(from);
            this.To = new CP40Time2a(to);
        }

        #endregion

        #region ISerializable

        /// <summary>
        /// Serializes the current instance to the specified encoder
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public override void Encode(IEncoder encoder)
        {
            encoder.Write((byte) 0x00);

            this.Cause.Encode(encoder);
            this.ASDUDirection.Encode(encoder);

            this.From.Encode(encoder);
            this.To.Encode(encoder);
        }

        /// <summary>
        /// Deserializes a C_AC_NA_2 object
        /// </summary>
        /// <param name="decoder">Decodes to use</param>
        public override void Decode(IDecoder decoder)
        {
            int numberOfObjects = decoder.ReadByte();

            this.Cause.Decode(decoder);
            this.ASDUDirection.Decode(decoder);

            this.From.Decode(decoder);
            this.To.Decode(decoder);
        }

        #endregion

    }
}
