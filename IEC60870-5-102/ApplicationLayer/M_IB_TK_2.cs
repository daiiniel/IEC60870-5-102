using System;
using System.Collections.Generic;

using IEC60870_5_102.LinkLayer;
using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Represents a M_IB_TK_2 telegram according to IEC60870-5-102 to receive operational integrators blocks
    /// </summary>
    [ASDU(140)]
    public class M_IB_TK_2 : ASDU
    {

        #region Properties

        /// <summary>
        /// Integrator read from the slave
        /// </summary>
        public List<PowerIntegrator> Integrators { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor of the class
        /// </summary>
        public M_IB_TK_2() : base()
        {
            this.Integrators = new List<PowerIntegrator>();
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

                if (objectDirection == ( byte ) ObjectsDirections.Totes1To6)
                {
                    PowerIntegrator integrator = new PowerIntegrator();

                    integrator.Active_In.Decode(decoder);
                    integrator.Active_Out.Decode(decoder);
                    integrator.Reactive_1.Decode(decoder);
                    integrator.Reactive_2.Decode(decoder);
                    integrator.Reactive_3.Decode(decoder);
                    integrator.Reactive_4.Decode(decoder);

                    integrator.DateTime.Decode(decoder);

                    this.Integrators.Add(integrator);
                }
            }
        }

        #endregion

    }
}
