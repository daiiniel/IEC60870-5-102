using System;

namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Common ASDU directions
    /// </summary>
    public enum RegisterDirections : UInt16
    {
        Default = 0,

        HourlyTotes = 11,
        QuarterTotes = 12,
        DailyTotes1 = 21,
        DailyTotes2 = 22,
        DailyTotes3 = 23,

        PowerONandVoltageIncidences = 52,
        SyncAndHourlyChangeIncidences = 53,
        ParameterChangeIncidences = 54,
        InternalErrors = 55,
        IntrusismIncidences = 128,
        CommunicationIncidences = 129,
        PrivatePasswordIncidences = 130,
        Contract1Incidences = 131,
        Contract2Incidences = 132,
        Contract3Incidences = 133,

        ContractITariffInformation = 134,
        ContractIITariffInformation = 135,
        ContractIIITariffInformation = 136,

        LatentContractITariffInformation = 137,
        LatentContractIITariffInformation = 138,
        LatentContractIIITariffInformation = 139,
    }
}
