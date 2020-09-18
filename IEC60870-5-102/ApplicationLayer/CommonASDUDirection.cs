using System;

using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Common ASDU direction
    /// </summary>
    public class CommonASDUDirection : IEncodeable
    {
        #region Properties

        /// <summary>
        /// Meassure point direction
        /// </summary>
        public UInt16 MeassurePoint { get; set; }

        /// <summary>
        /// Direction of the register
        /// </summary>
        public RegisterDirections RegisterDirection { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public CommonASDUDirection()
        {

        }

        /// <summary>
        /// Initializes a new instance of common ASDU direction
        /// </summary>
        /// <param name="meassurePoint">Meassure point direction</param>
        /// <param name="registerDirection">Direction of the register</param>
        public CommonASDUDirection(UInt16 meassurePoint, RegisterDirections registerDirection)
        {
            this.MeassurePoint = meassurePoint;
            this.RegisterDirection = registerDirection;
        }

        #endregion

        #region ISerializable

        /// <summary>
        /// Serializes the current instance
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public void Encode(IEncoder encoder)
        {
            encoder.Write(this.MeassurePoint);
            encoder.Write(( byte ) this.RegisterDirection);
        }

        /// <summary>
        /// Deserializes the data within the specified decoder
        /// </summary>
        /// <param name="decoder">Decoder with the data to deserialize</param>
        public void Decode(IDecoder decoder)
        {
            this.MeassurePoint = decoder.ReadUInt16();
            this.RegisterDirection = ( RegisterDirections ) decoder.ReadByte();
        }

        #endregion

    }
}
