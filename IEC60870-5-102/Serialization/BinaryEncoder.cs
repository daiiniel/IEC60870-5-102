using System;
using System.Text;
using System.IO;

namespace IEC60870_5_102.Serialization
{
    /// <summary>
    /// Binary encoder for IEC60870-5-102 types
    /// </summary>
    public class BinaryEncoder : Coder, IEncoder
    {
        #region PROPERTIES

        /// <summary>
        /// Gets the length of the encoded data
        /// </summary>
        public int Length
        {
            get
            {
                return (int) this.Writer.BaseStream.Length;
            }
        }

        BinaryWriter m_Writer;
        /// <summary>
        /// Binary writer for binary serialization
        /// </summary>
        BinaryWriter Writer
        {
            get
            {
                return m_Writer;
            }
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Constructor for encoders
        /// </summary>
        public BinaryEncoder() : base()
        {
            m_Writer = new BinaryWriter(base.Stream);
        }

        /// <summary>
        /// Constructor for encoders
        /// </summary>
        /// <param name="stream">Stream to use</param>
        public BinaryEncoder(Stream stream) : base()
        {
            m_Writer = new BinaryWriter(stream);
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Gets the encoded data as an array of bytes
        /// </summary>
        /// <returns>Array of bytes within the stream</returns>
        public byte[] ToArray()
        {
            Stream stream = this.Writer.BaseStream;

            long position = stream.Position;

            stream.Position = 0;

            int length = (int) stream.Length;
            byte[] res = new byte[length];

            int count = 0;

            while (count < length)
                count += stream.Read(res, count, length - count);

            stream.Position = position;

            return res;
        }

        /// <summary>
        /// Writes a byte to the stream
        /// </summary>
        /// <param name="value">Byte to serialize</param>
        public void Write(Byte value)
        {
            this.Writer.Write(value);
        }

        // <summary>
        /// Writes an UInt32 to the stream
        /// </summary>
        /// <param name="value">UInt16 to serialize</param>
        public void Write(UInt16 value)
        {
            this.Writer.Write(value);
        }

        /// <summary>
        /// Writes an Int16 to the stream
        /// </summary>
        /// <param name="value">Int16 to serialize</param>
        public void Write(Int16 value)
        {
            this.Writer.Write(value);
        }

        /// <summary>
        /// Writes an UInt32 to the stream
        /// </summary>
        /// <param name="value">UInt32 to serialize</param>
        public void Write(UInt32 value)
        {
            this.Writer.Write(value);
        }

        /// <summary>
        /// Writes an Int32 to the stream
        /// </summary>
        /// <param name="value">Int32 to serialize</param>
        public void Write(Int32 value)
        {
            this.Writer.Write(value);
        }

        /// <summary>
        /// Writes an UInt64 to the stream
        /// </summary>
        /// <param name="value">UInt64 to serialize</param>
        public void Write(UInt64 value)
        {
            this.Writer.Write(value);
        }

        /// <summary>
        /// Writes an Int64 to the stream
        /// </summary>
        /// <param name="value">Int64 to serialize</param>
        public void Write(Int64 value)
        {
            this.Writer.Write(value);
        }

        /// <summary>
        /// Writes an array of bytes to the stream
        /// </summary>
        /// <param name="values">Array of bytes to write</param>
        public void Write(byte[] values)
        {
            this.Writer.Write(values);
        }

        /// <summary>
        /// Writes a string to the stream
        /// </summary>
        /// <param name="value">String to write</param>
        public void Write(String value)
        {
            this.Write(ASCIIEncoding.UTF8.GetBytes(value));
        }

        /// <summary>
        /// Writes an array of bytes at the start of the stream
        /// </summary>
        /// <param name="values">Array of bytes to write at the begining of the stream</param>
        public void Prepend(byte[] values)
        {
            using (MemoryStream sTemp = new MemoryStream(this.Length))
            {
                this.Stream.CopyTo(sTemp, this.Length);

                this.Stream.Position = 0;

                this.Writer.Write(values);
                this.Writer.Write(sTemp.ToArray());
            }
        }

        #endregion

    }
}
