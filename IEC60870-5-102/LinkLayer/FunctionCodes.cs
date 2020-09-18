

namespace IEC60870_5_102.LinkLayer
{
    /// <summary>
    /// Enumeration of the function codes from the master to the slave
    /// </summary>
    public enum FunctionCodesPRM1 : byte
    {

        /// <summary>
        /// Resets the remote link 
        /// </summary>
        LinkReset = 0,

        /// <summary>
        /// Send user data
        /// </summary>
        UserData = 3,

        /// <summary>
        /// Link status request
        /// </summary>
        LinkRequest = 9,

        /// <summary>
        /// Send class 2 user data
        /// </summary>
        Class2UserData = 11

    }

    /// <summary>
    /// Function codes for control fields messages from slave to master
    /// </summary>
    public enum FunctionCodesPRM0 : byte
    {
        /// <summary>
        /// Positive acknowledgment
        /// </summary>
        ACK = 0,

        /// <summary>
        /// Negative acknowledgment
        /// </summary>
        NACK = 1,

        /// <summary>
        /// User data
        /// </summary>
        UserData = 8,

        /// <summary>
        /// Data user not available
        /// </summary>
        NACK_UserData = 9,

        /// <summary>
        /// Link status or access request
        /// </summary>
        LinkStatus = 11
    }

}
