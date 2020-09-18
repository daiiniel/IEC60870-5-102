using System;
using System.IO;

namespace IEC60870_5_102.Serialization
{
    /// <summary>
    /// Base abstract class for encoders a decoders
    /// </summary>
    public abstract class Coder
    {
        #region PROPERTIES

        Stream m_stream;
        /// <summary>
        /// Memory stream to store the serialized data
        /// </summary>
        public Stream Stream
        {
            get
            {
                return m_stream;
            }
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Constructor for encoders
        /// </summary>
        internal Coder()
        {
            m_stream = new MemoryStream();
        }

        /// <summary>
        /// Constructor for decoders
        /// </summary>
        /// <param name="serializedData">Serialized data to deccode</param>
        internal Coder(byte[] serializedData)
        {
            m_stream = new MemoryStream(serializedData, false);
        }

        /// <summary>
        /// Constructor for decoders
        /// </summary>
        /// <param name="stream">Stream with the data to deserialize</param>
        internal Coder(Stream stream)
        {
            m_stream = stream;
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Releases the memory used by the coder
        /// </summary>
        public void Dispose()
        {
            this.Stream.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
