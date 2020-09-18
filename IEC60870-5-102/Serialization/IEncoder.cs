using System;
using System.IO;

namespace IEC60870_5_102.Serialization
{
    /// <summary>
    /// Interface for encoders
    /// </summary>
    public interface IEncoder : IDisposable
    {
        #region Properties

        /// <summary>
        /// Gets the length of the encoded data
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Current stream of the encoder
        /// </summary>
        Stream Stream { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the encoded data as an array of bytes
        /// </summary>
        byte[] ToArray();

        /// <summary>
        /// Writes a byte to the stream
        /// </summary>
        /// <param name="value">Byte to serialize</param>
        void Write(Byte value);

        /// <summary>
        /// Writes an UInt32 to the stream
        /// </summary>
        /// <param name="value">UInt16 to serialize</param>
        void Write(UInt16 value);
        
        /// <summary>
        /// Writes an Int16 to the stream
        /// </summary>
        /// <param name="value">Int16 to serialize</param>
        void Write(Int16 value);
        
        /// <summary>
        /// Writes an UInt32 to the stream
        /// </summary>
        /// <param name="value">UInt32 to serialize</param>
        void Write(UInt32 value);

        /// <summary>
        /// Writes an Int32 to the stream
        /// </summary>
        /// <param name="value">Int32 to serialize</param>
        void Write(Int32 value);

        /// <summary>
        /// Writes an UInt64 to the stream
        /// </summary>
        /// <param name="value">UInt64 to serialize</param>
        void Write(UInt64 value);

        /// <summary>
        /// Writes an Int64 to the stream
        /// </summary>
        /// <param name="value">Int64 to serialize</param>
        void Write(Int64 value);

        /// <summary>
        /// Writes an array of bytes to the stream
        /// </summary>
        /// <param name="values">Array of bytes to write</param>
        void Write(byte[] values);

        /// <summary>
        /// Writes an array of bytes at the start of the stream
        /// </summary>
        /// <param name="values">Array of bytes to write at the begining of the stream</param>
        void Prepend(byte[] values);

        #endregion

    }
}
