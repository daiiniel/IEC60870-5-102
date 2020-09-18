using System;

using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Represents the parameters of a meassure point
    /// </summary>
    public class MeassurePointParameters : IEncodeable
    {
        #region Properties

        /// <summary>
        /// Link direction
        /// </summary>
        public UInt16 LinkDirection { get; private set; }

        /// <summary>
        /// Number of points
        /// </summary>
        public byte NumberOfPoints { get; private set; }

        /// <summary>
        /// Meassure point
        /// </summary>
        public UInt16 MeassurePoint { get; private set; }

        /// <summary>
        /// Password 
        /// </summary>
        public UInt32 Password { get; private set; }

        /// <summary>
        /// Integration period
        /// </summary>
        public byte IntegrationPeriod { get; private set; }

        /// <summary>
        /// Register depth
        /// </summary>
        public UInt16 RegisterDepth { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor of the class
        /// </summary>
        public MeassurePointParameters()
        {

        }

        #endregion

        #region IEncodeable

        /// <summary>
        /// Serializes the current instance to the specified encoder
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public void Encode(IEncoder encoder)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserializes a C_AC_NA_2 object
        /// </summary>
        /// <param name="decoder">Decodes to use</param>
        public void Decode(IDecoder decoder)
        {
            this.LinkDirection = decoder.ReadUInt16();
            this.NumberOfPoints = decoder.ReadByte();
            this.MeassurePoint = decoder.ReadUInt16();

            this.Password = decoder.ReadUInt32();
            this.IntegrationPeriod = decoder.ReadByte();
            this.RegisterDepth = decoder.ReadUInt16();   

            for(int ii = 0; ii < 234; ii++)
            {
                decoder.ReadByte();
            }
        }

        #endregion
    }
}
