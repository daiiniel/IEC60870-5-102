

namespace IEC60870_5_102.TransportLayer
{
    /// <summary>
    /// Implemented conenction types
    /// </summary>
    public enum SuportedConnectionsTypes
    {
        /// <summary>
        /// Direct connection through a RS232 interface
        /// </summary>
        serial,
        /// <summary>
        /// Remote connection made by a data call with a modem
        /// </summary>
        modem,
        /// <summary>
        /// Remote connection using tcp sochets
        /// </summary>
        tcp
    }

    /// <summary>
    /// Represents an endpoint 
    /// </summary>
    public abstract class TransportEndpoint
    {
        #region PROPERTIES

        /// <summary>
        /// Connection type
        /// </summary>
        protected SuportedConnectionsTypes m_connectionType;

        /// <summary>
        /// Connection type
        /// </summary>
        public SuportedConnectionsTypes ConnectionType
        {
            get
            {
                return m_connectionType;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for the Transport endpoint
        /// </summary>
        /// <param name="connectionType">Connection type of the endpoint</param>
        protected TransportEndpoint(SuportedConnectionsTypes connectionType)
        {
            m_connectionType = connectionType;
        }

        #endregion
    }
}
