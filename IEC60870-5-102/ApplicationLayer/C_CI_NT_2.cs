using System;

using IEC60870_5_102.LinkLayer;
using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Operational interation totes
    /// </summary>
    [ASDU(122)]
    public class C_CI_NT_2 : ASDU
    {
        #region Properties

        /// <summary>
        /// Time from
        /// </summary>
        public CP40Time2a TimeFrom { get; set; }

        /// <summary>
        /// Time to
        /// </summary>
        public CP40Time2a TimeTo { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public C_CI_NT_2() : base()
        {
            this.TimeFrom = new CP40Time2a();
            this.TimeTo = new CP40Time2a();
        }

        /// <summary>
        /// Initializes an instance of the C_CI_NT_2 telegram
        /// </summary>
        /// <param name="meassurePoint">Meassure point</param>
        /// <param name="timeFrom">Time from</param>
        /// <param name="timeTo">Time to</param>
        /// <param name="registerDirection">Register direction</param>
        public C_CI_NT_2(
            UInt16 meassurePoint,
            CP40Time2a timeFrom,
            CP40Time2a timeTo,
            RegisterDirections registerDirection) : base(
                TransmissionCauses.Activation,
                meassurePoint,
                registerDirection)
        {
            this.TimeFrom = timeFrom;
            this.TimeTo = timeTo;
        }

        #endregion

        #region Methods

        #endregion

        #region ISerializable

        /// <summary>
        /// Serializes the current instance 
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public override void Encode(IEncoder encoder)
        {            
            encoder.Write((byte) 1);

            this.Cause.Encode(encoder);
            this.ASDUDirection.Encode(encoder);

            encoder.Write((byte) ObjectsDirections.ActiveIn);
            encoder.Write(( byte ) ObjectsDirections.Reactive4);

            this.TimeFrom.Encode(encoder);
            this.TimeTo.Encode(encoder);
        }

        /// <summary>
        /// Deserializes the data specified within the given decoder to the current instance
        /// </summary>
        /// <param name="decoder">Decoder to be used</param>
        public override void Decode(IDecoder decoder)
        {
            int numberOfObjects = decoder.ReadByte();

            this.Cause.Decode(decoder);

            this.ASDUDirection.Decode(decoder);

            ObjectsDirections firstObject = (ObjectsDirections) decoder.ReadByte();
            ObjectsDirections lastObject = (ObjectsDirections) decoder.ReadByte();

            this.TimeFrom.Decode(decoder);
            this.TimeTo.Decode(decoder);
        }

        #endregion
    }
}
