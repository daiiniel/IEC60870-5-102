using System;

using IEC60870_5_102.Serialization;
using IEC60870_5_102.LinkLayer;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Represents an operational ingetrational response from the slave (M_IT_TK_2)
    /// </summary>
    [ASDU(11)]
    public class M_IT_TK_2 : ASDU
    {
        #region Properties
        /// <summary>
        /// Power integrator
        /// </summary>
        public PowerIntegrator Integrator { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor of the class
        /// </summary>
        public M_IT_TK_2() : base()
        {
            this.Integrator = new PowerIntegrator();
        }

        #endregion

        #region ISerializable

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

            for (int ii = 0; ii < numberOfObjects; ii++)
            {
                byte objectDirection = decoder.ReadByte();

                if (objectDirection == ( byte ) ObjectsDirections.ActiveIn)
                    this.Integrator.Active_In.Decode(decoder);
                else if (objectDirection == ( byte ) ObjectsDirections.ActiveOut)
                    this.Integrator.Active_Out.Decode(decoder);
                else if (objectDirection == ( byte ) ObjectsDirections.Reactive1)
                    this.Integrator.Reactive_1.Decode(decoder);
                else if (objectDirection == ( byte ) ObjectsDirections.Reactive2)
                    this.Integrator.Reactive_2.Decode(decoder);
                else if (objectDirection == ( byte ) ObjectsDirections.Reactive3)
                    this.Integrator.Reactive_3.Decode(decoder);
                else if (objectDirection == ( byte ) ObjectsDirections.Reactive4)
                    this.Integrator.Reactive_4.Decode(decoder);
            }

            this.Integrator.DateTime.Decode(decoder);
        }

        #endregion

    }
}
