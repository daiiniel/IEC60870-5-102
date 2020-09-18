

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Suported transmission causses
    /// </summary>
    public enum TransmissionCauses : byte
    {
        Initialized = 4,

        Request = 5,

        Activation = 6,

        ActivationACK = 7,

        Deactivation = 8,

        DeactivationACK = 9,

        ActivationEnd = 10,

        DataRegisterNotAvailable = 13,

        ASDUNotAvailable = 14,

        ASDUNumberUnknown = 15,

        ASDUUnknown = 16,

        InformationObjectNotAvailable = 17,

        IntegrationPeriodNotAvailable = 18,
    }
}
