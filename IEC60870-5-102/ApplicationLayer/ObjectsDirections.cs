
namespace IEC60870_5_102.ApplicationLayer
{
    /// <summary>
    /// Direction of the objects
    /// </summary>
    public enum ObjectsDirections : byte
    {
        ActiveIn = 1,
        ActiveOut = 2,
        Reactive1 = 3,
        Reactive2 = 4,
        Reactive3 = 5,
        Reactive4 = 6,
        Res1 = 7,
        Res2 = 8,
        Totes1To8 = 9,
        Totes1To6 =10,
        Totes1_3_6 = 11,

        TariffInformationTote = 20,
        TariffInformationPeriodI = 21,
        TariffInformationPeriodII = 22,
        TariffInformationPeriodIII = 23,
        TariffInformationPeriodIV = 24,
        TariffInformationPeriodV = 25,
        TariffInformationPeriodVI = 26,
        TariffInformationPeriodVII = 27,
        TariffInformationPeriodVIII = 28,
        TariffInformationPeriodIX = 29,

        EnergyTotes = 192,
        ActivePowers = 193,
        V_I = 194
    }
}
