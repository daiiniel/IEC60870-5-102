using System;

using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Energy tariff information
    /// </summary>
    public enum ETI : byte
    {
        Zero,
        One,
        Two
    }

    /// <summary>
    /// Power tariff information
    /// </summary>
    public enum PTI : byte
    {
        Zero,
        One, 
        Two
    }

    /// <summary>
    /// Represents a type A time according to the IEC 60870
    /// </summary>
    public class CP40Time2a : IEncodeable
    {
        #region Static

        /// <summary>
        /// Converts a DateTime to CP40Time2a
        /// </summary>
        /// <param name="value">Datetime to convert</param>
        public static implicit operator CP40Time2a (DateTime value)
        {
            return new CP40Time2a(value);
        }

        /// <summary>
        /// Converts a CP40Time2a to a DateTime
        /// </summary>
        /// <param name="value">CP40Time2a to convert</param>
        public static implicit operator DateTime (CP40Time2a value)
        {
            return value.DateTime;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Represents the date time
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Power tariff information
        /// </summary>
        public PTI PTI { get; set; }

        /// <summary>
        /// Energy tariff information
        /// </summary>
        public ETI ETI { get; set; }

        /// <summary>
        /// Tariff information
        /// </summary>
        public bool TIS { get; set; }

        /// <summary>
        /// Boolean indicating if the datetime is valid or not
        /// </summary>
        public bool Valid { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public CP40Time2a()
        {

        }

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="datetime">Date time</param>
        public CP40Time2a(DateTime datetime)
        {
            this.DateTime = datetime;
            this.PTI = PTI.Zero;
            this.ETI = ETI.Zero;
            this.TIS = false;
            this.Valid = true;
        }

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="dateTime">Date time</param>
        /// <param name="powerTariffInformation">Power tariff information</param>
        /// <param name="energyTariffInformation">Energy tariff information</param>
        /// <param name="TIS">Tariff information</param>
        public CP40Time2a(DateTime dateTime, PTI powerTariffInformation, ETI energyTariffInformation, bool TIS)
        {
            this.DateTime = dateTime;
            this.PTI = powerTariffInformation;
            this.ETI = energyTariffInformation;
            this.TIS = TIS;
        }

        #endregion

        #region IEncodeable

        /// <summary>
        /// Serializes the current instance of the class to the specified encoder
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public void Encode(IEncoder encoder)
        {
            byte[] data = new byte[5];

            if (!this.Valid)
                data[0] |= 0x80;

            if (this.TIS)
                data[0] |= 0x40;

            data[0] |= ( byte ) ( this.DateTime.Minute & 0x3F );

            if (this.DateTime.IsDaylightSavingTime())
                data[1] |= 0x80;

            data[1] |= ( byte ) ( this.DateTime.Hour & 0x1F );

            data[2] |= ( byte ) ( ( byte ) ( ( byte ) this.DateTime.DayOfWeek << 5) & 0xE0 );
            data[2] |= ( byte ) ( this.DateTime.Day & 0x1F );

            data[3] |= ( byte ) ( ( byte ) ( ( byte ) this.PTI << 6) & 0xC0 );
            data[3] |= ( byte ) ( ( byte ) ( ( byte ) this.ETI << 4 ) & 0x30 );
            data[3] |= ( byte ) ( this.DateTime.Month & 0X0F );

            data[4] |= ( byte ) (( this.DateTime.Year % 100 ) & 0x7F);
                                                                      
            encoder.Write(data);
        } 

        /// <summary>
        /// Deserializes the current instance of the class from the specified decoder
        /// </summary>
        /// <param name="decoder">Decoder to use</param>
        public void Decode(IDecoder decoder)
        {
            byte data = decoder.ReadByte(); //0

            this.Valid = !( ( data & 0x80 ) > 0 );
            this.TIS = ( ( data & 0x40 ) > 0 );

            int minute = data & 0x3F;

            data = decoder.ReadByte(); //1

            int hour = data & 0x1F;

            data = decoder.ReadByte();

            int day = data & 0x1F;

            data = decoder.ReadByte(); //3

            this.PTI = ( PTI ) ( data & 0xC0 );
            this.ETI = ( ETI ) ( data & 0x30 );
            int month = data & 0x0F;

            data = decoder.ReadByte(); //4

            int year = 2000 + (data & 0x7F);

            this.DateTime = new DateTime(year, month, day, hour, minute, 00);                                                
        }

        #endregion

        #region Object

        /// <summary>
        /// Gets a string representing the current instance of the type A date time
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.DateTime.ToString();
        }

        #endregion
    }
}
