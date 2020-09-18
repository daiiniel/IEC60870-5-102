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
        public void DateTimeToCP40Time2aSerializationTest()
        {
            DateTime dateTime = new DateTime(2016, 11, 29, 15, 26, 0);
            byte[] dataToCheck = { 0x1A, 0x0F, 0x5D, 0x0B, 0x10};

            CP40Time2a CP40DateTime = new CP40Time2a(dateTime);

            using (BinaryEncoder encoder = new BinaryEncoder())
            {
                CP40DateTime.Serialize(encoder);

                byte[] encodedData = encoder.ToArray();

                Assert.AreEqual(encodedData.Length, dataToCheck.Length);

                for (int ii = 0; ii < encodedData.Length; ii++)
                {
                    Assert.AreEqual(encodedData[ii], dataToCheck[ii]);
                }
            }
        }

        [TestMethod]
        public void CP40Time2aToDateTimeDeserializationTest()
        {
            DateTime dateToCheck = new DateTime(2016, 11, 29, 15, 26, 0);
            byte[] dataToDeserialize = { 0x1A, 0x0F, 0x1D, 0x0B, 0x10};

            using (BinaryDecoder decoder = new BinaryDecoder(dataToDeserialize))
            {
                CP40Time2a decodedTime = new CP40Time2a();
                decodedTime.Deserialize(decoder);

                Assert.AreEqual(( DateTime ) decodedTime, dateToCheck);
            }
        }

        [TestMethod]
        public void DateTimeToCP56Time2aSerializationTest()
        {
            DateTime dateTime = new DateTime(2016, 11, 23, 20, 09, 24, 576);
            byte[] dataToCheck = { 0x00, 0x60, 0x09, 0x14, 0x77, 0x0B, 0x10 };

            CP56Time2a CP56DateTime = new CP56Time2a(dateTime);

            using (BinaryEncoder encoder = new BinaryEncoder())
            {

                CP56DateTime.Serialize(encoder);

                byte[] encodedData = encoder.ToArray();

                Assert.AreEqual(encodedData.Length, dataToCheck.Length);

                for (int ii = 0; ii < encodedData.Length; ii++)
                {
                    Assert.AreEqual(encodedData[ii], dataToCheck[ii]);
                }
            }
        }

        [TestMethod]
        public void CP56Time2aToDateTimeDeserializationTest()
        {
            DateTime dateToCheck = new DateTime(2016, 11, 23, 20, 09, 24, 576);
            byte[] dataToDeserialize = { 0x00, 0x60, 0x09, 0x14, 0x77, 0x0B, 0x10 };

            using (BinaryDecoder decoder = new BinaryDecoder(dataToDeserialize))
            {
                CP56Time2a decodedTime = new CP56Time2a();
                decodedTime.Deserialize(decoder);

                Assert.AreEqual(( DateTime ) decodedTime, dateToCheck);
            }
        }

        [TestMethod]
        public void PasswordTelegramSerializationTest()
        {
            byte[] dataToCheck = {0x68, 0x0D, 0x0D, 0x68, 0x73, 0x01, 0x00, 0xB7, 0x01, 0x06, 0x01, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x34, 0x16 };

            C_AC_NA_2 passwordTelegram = new C_AC_NA_2(
                1,
                1,
                1);

            using (BinaryEncoder encoder = new BinaryEncoder())
            {
                passwordTelegram.Serialize(encoder);

                byte[] encodedData = encoder.ToArray();

                for (int ii = 0; ii < encodedData.Length; ii++)
                {
                    Assert.AreEqual(encodedData[ii], dataToCheck[ii]);
                }
            }
        }

        [TestMethod]
        public void RequestOperationalIntegratorsSerializationTest()
        {
            byte[] dataToCheck = { 0x68, 0x15, 0x15, 0x68, 0x73, 0x01, 0x00, 0x7A, 0x01, 0x06, 0x01, 0x00, 0x0b, 0x01, 0x06, 0x00, 0x00, 0x41, 0x01, 0x08, 0x00, 0x00, 0x21, 0x0C, 0x08, 0x87, 0x16 };

            C_CI_NT_2 request = new C_CI_NT_2(
                1,
                1,
                new CP40Time2a(
                    new DateTime(2008, 1, 1, 0, 0, 0)),
                new CP40Time2a(
                    new DateTime(2008, 12, 1, 0, 0, 0)),
                RegisterDirections.HourlyTotes);

            using (BinaryEncoder encoder = new BinaryEncoder())
            {
                request.Serialize(encoder);

                byte[] encodedData = encoder.ToArray();

                for (int ii = 0; ii < encodedData.Length; ii++)
                {
                    Assert.AreEqual(encodedData[ii], dataToCheck[ii]);
                }
            }
        }

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
                Assert.AreEqual(new DateTime(2008, 1, 1, 0, 0, 0), (telegram.ASDU as C_CI_NT_2 ).TimeFrom.DateTime);
                Assert.AreEqual(new DateTime(2008, 12, 1, 0, 0, 0), ( telegram.ASDU as C_CI_NT_2 ).TimeTo.DateTime);
            }
        }

        [TestMethod]
        public void OperationalIntegratorDeserializationTest()
        {
            byte[] dataToDecode = { 104, 21, 21, 104, 8, 1, 0, 122, 1, 7, 1, 0, 11, 1, 6, 0, 0, 1, 1, 8, 0, 0, 1, 12 };

            using (System.IO.Stream stream = new System.IO.MemoryStream(dataToDecode))
            {
                C_CI_NT_2 telegram = Telegram.Deserialize(stream) as C_CI_NT_2;

                Assert.AreNotEqual(null, telegram);
                Assert.AreEqual(1, telegram.LinkDirection);
                Assert.AreEqual(1, telegram.ASDUDirection.MeassurePoint);
                Assert.AreEqual(RegisterDirections.HourlyTotes, telegram.ASDUDirection.RegisterDirection);
                Assert.AreEqual(new TransmissionCause(TransmissionCauses.ActivationACK), telegram.Cause);
                Assert.AreEqual(new DateTime(2008, 1, 1, 0, 0, 0), telegram.TimeFrom.DateTime);
                Assert.AreEqual(new DateTime(2008, 12, 1, 0, 0, 0), telegram.TimeTo.DateTime);
            }
        }

    }
}
