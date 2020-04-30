using MusZil_Core;
using MusZil_Core.Utils;
using NUnit.Framework;
using NUnitTestMusZIL.IntegrationTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace NUnitTestMusZIL.UtilityTests
{
    public class UtilTests
    {
        #region Encode/Decode
        [Test]
        public void AddressEncode()
        {
            var address = new Address("0x551AA8653Aa7b75D9fDD75f5D4D41d0647F734E8");
            var bech = MusBech32.Base16ToBech32Address(address.Base16);

            Assert.IsTrue(bech.StartsWith("zil"));

            Assert.AreEqual("zil125d2sef657m4m87awh6af4qaqerlwd8gv6fglj", bech);
        }
        [Test]
        public void AddressEncode2()
        {
            var address = new Address("0x551AA8653Aa7b75D9fDD75f5D4D41d0647F734E8");
            address.ToBech32Address();
            Assert.AreEqual("zil125d2sef657m4m87awh6af4qaqerlwd8gv6fglj", address.Bech32.ToString());
        }

        [Test]
        public void AddressDecode()
        {
            var encStr = "zil1fs6jhg4axvj9ekscq6v7ddwxxd9tthpxl7820q";

            var dec = MusBech32.Bech32ToBase16Address(encStr);

            Assert.AreEqual("0x" + "4C352ba2Bd33245CDA180699e6B5c6334AB5dC26".ToUpper(), dec);
        }
        [Test]
        public void AddressDecodeEncode()
        {
            var encStr = "zil1fs6jhg4axvj9ekscq6v7ddwxxd9tthpxl7820q";
            var length = encStr.Length;
            var dec = MusBech32.Decode(encStr);
            var enc = MusBech32.Encode(dec);
            var test = enc.Substring(0, length);
            Assert.AreEqual("zil1fs6jhg4axvj9ekscq6v7ddwxxd9tthpxl7820q", test);
        }

        #endregion

        #region ByteUtil

        [Test]
        public void ConvertStringToHex()
        {
            var t = ByteUtil.LongToBaseN(15, 16);
            Assert.AreEqual("F", t);

        }
        [Test]
        public void ConvertStringToB32()
        {
            var t = ByteUtil.LongToBaseN(64, 32);
            Assert.AreEqual("20", t);
        }

        [Test]
        public void ConverToBinary()
        {
            var t = ByteUtil.LongToBaseN(8, 2);
            Assert.AreEqual("1000", t);
        }

        [Test]
        public void GetHexVal()
        {
            var t = ByteUtil.GetHexVal('F');
            Assert.AreEqual(t, 15);
        }
        [Test]
        public void GetHexString()
        {
            var s = ByteUtil.HexStringToByteArray("FF");
            var t = ByteUtil.ConvertByteArrToString(s);
            Assert.AreEqual(t, "11111111");
        }

        #endregion

        #region CryptoUtil

        [Test]
        public void KeyNotEmptyXML()
        {
            var info = CryptoUtil.GenerateXmlKeyInfo(true);

            Assert.IsFalse(String.IsNullOrWhiteSpace(info));
        }
        [Test]
        public void KeyNotEmptyJson()
        {
            var info = CryptoUtil.GenerateJsonKeyInfo(true);

            Assert.IsFalse(String.IsNullOrWhiteSpace(info));
        }

        #endregion

    }
}
