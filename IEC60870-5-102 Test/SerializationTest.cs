using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using IEC60870_5_102.ApplicationLayer;
using IEC60870_5_102.LinkLayer;
using IEC60870_5_102.Serialization;

namespace IEC60870_5_102_Test
{
    [TestClass]
    public class SerializationTest
    {
        [TestMethod]
        public void RequestOperationalIntegratorsDeserializationTest()
        {
            byte[] dataToDecode = { 104, 21, 21, 104, 8, 1, 0, 122, 1, 7, 1, 0, 11, 1, 6, 0, 0, 1, 1, 8, 0, 0, 1, 12, 8, 189, 22 };

            using (System.IO.Stream stream = new System.IO.MemoryStream(dataToDecode))
            {
                VariableTelegram telegram = Telegram.Deserialize(stream) as VariableTelegram;

                Assert.AreNotEqual(null, telegram);
                Assert.AreEqual(1, telegram.LinkDirection);
                Assert.AreEqual(1, telegram.ASDU.ASDUDirection.MeassurePoint);
                Assert.AreEqual(RegisterDirections.HourlyTotes, telegram.ASDU.ASDUDirection.RegisterDirection);
                Assert.AreEqual(new TransmissionCause(TransmissionCauses.ActivationACK), telegram.ASDU.Cause);
                Assert.AreEqual(new DateTime(2008, 1, 1, 0, 0, 0), (telegram.ASDU as C_CI_NT_2).TimeFrom.DateTime);
                Assert.AreEqual(new DateTime(2008, 12, 1, 0, 0, 0), (telegram.ASDU as C_CI_NT_2).TimeTo.DateTime);
            }
        }
    }
}
