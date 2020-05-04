using MusZil_Core.Crypto;
using MusZil_Core.Utils;
using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusZil_Core.Accounts
{
    public class Account
    {
        public Balance Balance { get; set; }
        public Address Address { get; set; }
        public ECKeyPair KeyPair { get; set; }
        public Account(string privateKey)
        {
            if (String.IsNullOrWhiteSpace(privateKey))
                throw new ArgumentException("Private key is empty");

            InitializeAccount(privateKey);
        }
        public Account(string file, string passphrase)
        {
            string privateKey = CryptoUtil.DecryptPrivateKey(file, passphrase);
            InitializeAccount(privateKey);
        }
        
        public string ToJsonFile(string privateKey, string passphrase, KDFType type)
        {
            return CryptoUtil.EncryptPrivateKey(privateKey, passphrase, type);
        }

        public string GetPublicKey()
        {
            return ByteUtil.ByteArrayToHexString(KeyPair.PublicKey.ToByteArray());
        }

        public string GetPrivateKey()
        {
            return ByteUtil.ByteArrayToHexString(KeyPair.PrivateKey.ToByteArray());
        }

        public static string ToCheckSumAddress(string address)
        {
            if (!address.StartsWith("0x"))
            {
                throw new Exception("not a valid base 16 address");
            }

            address = address.ToLower().Replace("0x", "");
            string hash = ByteUtil.ByteArrayToHexString(HashUtil.CalculateSha256Hash(ByteUtil.HexStringToByteArray(address)));
            StringBuilder ret = new StringBuilder("0x");
            byte[] x = ByteUtil.HexStringToByteArray(hash);
            BigInteger v = new BigInteger(ByteUtil.HexStringToByteArray(hash));
            for (int i = 0; i < address.Length; i++)
            {
                if ("1234567890".IndexOf(address.ToCharArray()[i]) != -1)
                {
                    ret.Append(address.ToCharArray()[i]);
                }
                else
                {
                    BigInteger checker = v.And(BigInteger.ValueOf(2).Pow(255 - 6 * i));
                    ret.Append(checker.CompareTo(BigInteger.ValueOf(1)) < 0 ? address.ToCharArray()[i].ToString().ToLower() : address.ToCharArray()[i].ToString().ToUpper());
                }
            }
            return ret.ToString();
        }

        private void InitializeAccount(string pk)
        {
            Address = new Address();
            Balance = new Balance();
            var pub = CryptoUtil.GetPublicKeyFromPrivateKey(pk, true);
            Address.Raw = "0x" + CryptoUtil.GetAddressFromPublicKey(pub);
            KeyPair = new ECKeyPair(new BigInteger(pk, 16), new BigInteger(pub, 16));
        }
    }
}
