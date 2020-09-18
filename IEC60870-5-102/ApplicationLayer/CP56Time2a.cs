using System;

using IEC60870_5_102.Serialization;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// 56 bits datetime
    /// </summary>
    public class CP56Time2a : IEncodeable
    {
        #region Static

        /// <summary>
        /// Converts a CP40Time2a to a DateTime
        /// </summary>
        /// <param name="value">CP40Time2a to convert</param>
        public static implicit operator DateTime(CP56Time2a value)
        {
            return value.DateTime;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Boolean indicating whether the read is valid or not
        /// </summary>
        public bool IV { get; set; }

        /// <summary>
        /// Date time 
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

        #endregion

        #region Constructors

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public CP56Time2a()
        {

        }

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="dateTime"></param>
        public CP56Time2a(DateTime dateTime)
        {
            this.DateTime = dateTime;
            this.IV = true;
        }

        #endregion

        #region IEncodeable

        /// <summary>
        /// Serializes the current instance of the class to the specified encoder
        /// </summary>
        /// <param name="encoder">Encoder to use</param>
        public void Encode(IEncoder encoder)
        {
            encoder.Write(( UInt16 ) (this.DateTime.Second * 1000 + this.DateTime.Millisecond));

            byte data = 0x00;

            if (!this.IV)
                data |= 0x80;

            if (this.TIS)
                data |= 0x40;

            data |= (byte) (this.DateTime.Minute & 0x3F);

            encoder.Write(data);

            data = 0x00;

            data |= (byte) ( this.DateTime.Hour & 0x1F );

            encoder.Write(data);

            data = 0x0;

            data |= ( byte ) ( this.DateTime.Day & 0x1F );
            data |= ( byte ) ( ((( byte ) this.DateTime.DayOfWeek) << 5) & 0xE0 );

            encoder.Write(data);

            data = 0x00;

            data |= ( byte ) ( ( byte ) ( ( byte ) this.PTI << 6 ) & 0xC0 );
            data |= ( byte ) ( ( byte ) ( ( byte ) this.ETI << 4 ) & 0x30 );
            data |= ( byte ) ( this.DateTime.Month & 0x0F );

            encoder.Write(data);

            data = 0x00;

            encoder.Write(( byte ) (this.DateTime.Year % 1000));
        }

        /// <summary>
        /// Deserializes the current instance of the class from the specified decoder
        /// </summary>
        /// <param name="decoder">Decoder to use</param>
        public void Decode(IDecoder decoder)
        {
            UInt16 miliseconds = decoder.ReadUInt16();

            byte data = decoder.ReadByte(); //0

            this.IV = !( ( data & 0x80 ) > 0 );
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

            this.DateTime = new DateTime(year, month, day, hour, minute, miliseconds / 1000, miliseconds % 1000);
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
