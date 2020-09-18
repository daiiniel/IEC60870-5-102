using System;

using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Represents the instant energy totes read from the slave
    /// </summary>
    public class InstantEnergyTotes
    {
        #region Properties

        /// <summary>
        /// Active input energy
        /// </summary>
        public EnergyTote Active_In { get; private set; }

        /// <summary>
        /// Active output energy
        /// </summary>
        public EnergyTote Active_Out { get; private set; }

        /// <summary>
        /// Reactive energy within the first quadrant
        /// </summary>
        public EnergyTote Reactive_1 { get; private set; }

        /// <summary>
        /// Reactive energy within the second quadrant
        /// </summary>
        public EnergyTote Reactive_2 { get; private set; }

        /// <summary>
        /// Reactive energy within the third quadrant
        /// </summary>
        public EnergyTote Reactive_3 { get; private set; }

        /// <summary>
        /// Reactive energy within the fourth quadrant
        /// </summary>
        public EnergyTote Reactive_4 { get; private set; }

        /// <summary>
        /// Date time
        /// </summary>
        public CP40Time2a DateTime { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public InstantEnergyTotes()
        {
            this.Active_In = new EnergyTote();
            this.Active_Out = new EnergyTote();
            this.Reactive_1 = new EnergyTote();
            this.Reactive_2 = new EnergyTote();
            this.Reactive_3 = new EnergyTote();
            this.Reactive_4 = new EnergyTote();

            this.DateTime = new CP40Time2a();
        }

        #endregion

        #region IEncodable

        /// <summary>
        /// Serializes the current instance of the class to the specified encoder
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public void Serialize(IEncoder encoder)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserializes the current instance of the class from the specified decoder
        /// </summary>
        /// <param name="decoder">Decoder to use</param>
        public void Deserialize(IDecoder decoder)
        {
            this.Active_In.Decode(decoder);
            this.Active_Out.Decode(decoder);

            this.Reactive_1.Decode(decoder);
            this.Reactive_2.Decode(decoder);
            this.Reactive_3.Decode(decoder);
            this.Reactive_4.Decode(decoder);

            this.DateTime.Decode(decoder);
        }

        #endregion
    }
}
