using System;

using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Tariff information object
    /// </summary>
    public class TariffInformationObject
    {
        #region Properties

        /// <summary>
        /// Contract whose information is wihtin the current instance
        /// </summary>
        public ObjectsDirections Object { get; private set; }

        /// <summary>
        /// Represents the active absolut active energy (A+)
        /// </summary>
        public UInt32 ActiveAbsolutEnergy { get; private set; }

        /// <summary>
        /// Represents the active energy
        /// </summary>
        public UInt32 ActiveEnergy { get; private set; }

        /// <summary>
        /// Active energy qualifier
        /// </summary>
        public Qualifier ActiveEnergyQualifier { get; private set; }

        /// <summary>
        /// Inductive absolut active energy (A+)
        /// </summary>
        public UInt32 InductiveAbsolutEnergy { get; private set; }

        /// <summary>
        /// Inductive energy
        /// </summary>
        public UInt32 InductiveEnergy { get; private set; }

        /// <summary>
        /// Inductive energy qualifier
        /// </summary>
        public Qualifier InductiveEnergyQualifier { get; private set; }

        /// <summary>
        /// Capacitive absolut active energy (A+)
        /// </summary>
        public UInt32 CapacitiveAbsolutEnergy { get; private set; }

        /// <summary>
        /// Capacitive energy
        /// </summary>
        public UInt32 CapacitiveEnergy { get; private set; }

        /// <summary>
        /// Capacitive energy qualifier
        /// </summary>
        public Qualifier CapacitiveEnergyQualifier { get; private set; }

        /// <summary>
        /// Maximum power within the tariff period
        /// </summary>
        public UInt32 MaxPower { get; private set; }

        /// <summary>
        /// Date and time of the max power register within the tariff period
        /// </summary>
        public CP40Time2a MaxPowerDateTime { get; private set; }

        /// <summary>
        /// Maximum power qualifier
        /// </summary>
        public Qualifier MaxPowerQualifier { get; private set; }

        /// <summary>
        /// Power excess
        /// </summary>
        public UInt32 PowerExcess { get; private set; }

        /// <summary>
        /// Power excess qualifier
        /// </summary>
        public Qualifier PowerExcessQualifier { get; private set; }

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
        /// Constructor of the tariff information object
        /// </summary>
        /// <param name="contract">Contract whose information is within the object</param>
        public TariffInformationObject()
        {
            this.ActiveEnergyQualifier = new Qualifier();
            this.InductiveEnergyQualifier = new Qualifier();
            this.CapacitiveEnergyQualifier = new Qualifier();

            this.MaxPowerDateTime = new CP40Time2a();
            this.MaxPowerQualifier = new Qualifier();

            this.PowerExcessQualifier = new Qualifier();

            this.From = new CP40Time2a();
            this.To = new CP40Time2a();
        }

        #endregion

        #region ISerializable

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
            this.Object = ( ObjectsDirections ) decoder.ReadByte();

            if (( byte ) this.Object < 20 || ( byte ) this.Object > 29)
                throw new Exception("Unknown direction object");

            this.ActiveAbsolutEnergy = decoder.ReadUInt32();
            this.ActiveEnergy = decoder.ReadUInt32();
            this.ActiveEnergyQualifier.Deserialize(decoder);

            this.InductiveAbsolutEnergy = decoder.ReadUInt32();
            this.InductiveEnergy = decoder.ReadUInt32();
            this.InductiveEnergyQualifier.Deserialize(decoder);

            this.CapacitiveAbsolutEnergy = decoder.ReadUInt32();
            this.CapacitiveEnergy = decoder.ReadUInt32();
            this.CapacitiveEnergyQualifier.Deserialize(decoder);

            decoder.ReadUInt32();
            decoder.ReadByte();

            decoder.ReadUInt32();
            decoder.ReadByte();

            this.MaxPower = decoder.ReadUInt32();
            this.MaxPowerDateTime.Decode(decoder);
            this.MaxPowerQualifier.Deserialize(decoder);

            this.PowerExcess = decoder.ReadUInt32();
            this.PowerExcessQualifier.Deserialize(decoder);

            this.From.Decode(decoder);
            this.To.Decode(decoder);
        }

        #endregion
    }
}
