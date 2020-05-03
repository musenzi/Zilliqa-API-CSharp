using MusZil_Core.Crypto;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.EC;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using ECPoint = Org.BouncyCastle.Math.EC.ECPoint;

namespace MusZil_Core.Utils
{
    public class CryptoUtil
    {
        #region Mus
        public static string GenerateXmlKeyInfo(bool inculdePrivate = false)
        {
            //Generate a public/private key pair.  
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //Save the public key information to an RSAParameters structure.  
            RSAParameters rsaKeyInfo = rsa.ExportParameters(inculdePrivate);

            return  rsa.ToXmlString(true);
        }
        public static string GenerateJsonKeyInfo(bool inculdePrivate = false)
        {
            //Generate a public/private key pair.  
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //Save the public key information to an RSAParameters structure.  
            RSAParameters rsaKeyInfo = rsa.ExportParameters(inculdePrivate);

            //XElement newNode = XDocument.Parse(rsa.ToXmlString(true)).Root;
            
            string xmlContent = rsa.ToXmlString(true);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlContent);
            XmlNode newNode = doc.DocumentElement;

            var json = JsonConvert.SerializeXmlNode(newNode);
            
            return json;
        }

        public static void Encrypt(byte [] publicKey)
        {

            byte[] exponent = { 1, 0, 1 };

            //Create values to store encrypted symmetric keys.  
            byte[] encryptedSymmetricKey;
            byte[] encryptedSymmetricIV;

            //Create a new instance of the RSACryptoServiceProvider class.  
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            //Create a new instance of the RSAParameters structure.  
            RSAParameters rsaKeyInfo = new RSAParameters();
            //rsaKeyInfo.

            //Set rsaKeyInfo to the public key values.
            rsaKeyInfo.Modulus = publicKey;
            rsaKeyInfo.Exponent = exponent;

            //Import key parameters into RSA.  
            rsa.ImportParameters(rsaKeyInfo);

            //Create a new instance of the RijndaelManaged class.  
            RijndaelManaged rm = new RijndaelManaged();

            //Encrypt the symmetric key and IV.  
            encryptedSymmetricKey = rsa.Encrypt(rm.Key, false);
            encryptedSymmetricIV = rsa.Encrypt(rm.IV, false);

            rsa.ExportParameters(true);

            
        }

        public static string GenerateRfc2898(byte[] pwd, string data = "")
        {
            // Create a byte array to hold the random value.
            byte[] salt = new byte[8];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with a random value.
                rngCsp.GetBytes(salt);
            }

            //The default iteration count is 1000 so the two methods use the same iteration count.
            int iterations = 1000;
            Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(pwd, salt, iterations);
            // Encrypt the data.
            TripleDES encAlg = TripleDES.Create();
            encAlg.Key = k1.GetBytes(16);
            MemoryStream encryptionStream = new MemoryStream();
            CryptoStream encrypt = new CryptoStream(encryptionStream, encAlg.CreateEncryptor(), CryptoStreamMode.Write);
            byte[] utfD1 = new System.Text.UTF8Encoding(false).GetBytes(data);

            encrypt.Write(utfD1, 0, utfD1.Length);
            encrypt.FlushFinalBlock();
            encrypt.Close();
            return encryptionStream.ToString();
            
        }

        public static void GenerateAES(byte[] data)
        {
            using (Aes myAes = Aes.Create())
            {
                /*
                // 16 bytes long key for AES-128 bit encryption
                byte[] key = { 50, 199, 10, 159, 132, 55, 236, 189, 51, 243, 244, 91, 17, 136, 39, 230 };
                // optional 16 byte long initialization vector
                byte[] IV = { 150, 9, 112, 39, 32, 5, 136, 289, 251, 43, 44, 191, 217, 236, 3, 106 };
                */
                

                // Decrypt the bytes to a string.
                string roundtrip = DecryptStringFromBytes_Aes(data, myAes.Key, myAes.IV);
            }
            

           

        }
        public static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        public static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
        public string GetJsonExport(string address, string pk,string passphrase)
        {
            var algo = "aes-128-ctr";
            var pkBytes = ByteUtil.HexStringToByteArray(pk);
            var iv = "";
            var cipherText = "";
            var kdf = "pbkdf2";
            var kdfParams = new KDFParams()
            {
                n = 8192,
                c = 262144,
                r = 8,
                p = 1,
                dklen = 32
            };
            var mac = "";
            var uuid = "";
            var version = 3;

            using (var aes = Aes.Create())
            {
                // Encrypt the string to an array of bytes.
                byte[] encrypted = EncryptStringToBytes_Aes(pk, aes.Key, aes.IV);
                iv = ByteUtil.ByteArrayToHexString(aes.IV);
                aes.GetHashCode();
                cipherText = System.Text.Encoding.UTF8.GetString(encrypted);
            }
            
            

            return jsonFormat
                .Replace("<address>",address)
                .Replace("<algo>",algo)
                .Replace("<iv>", iv)
                .Replace("<cipherText>", cipherText)
                .Replace("<kdfParamas>", JsonConvert.SerializeObject(kdfParams))
                .Replace("<mac>", mac)
                .Replace("<uuid>", uuid)
                .Replace("<version>", version.ToString());
        }
        public void EncryptPrivateKey(string pk, string passphrase,string kdf = "pbkdf2")
        {
            
            var pkBytes = ByteUtil.HexStringToByteArray(pk);
            
            using (var aes = Aes.Create())
            {
                // Encrypt the string to an array of bytes.
                byte[] encrypted = EncryptStringToBytes_Aes(pk, aes.Key, aes.IV);
                aes.GetHashCode();
            }

            var cipherText = "";

            var kdfParams = new KDFParams();
            var mac = "";
            var uuid = "";
            var version = 3;
        }

        public string ToJsonFormat(RSACryptoServiceProvider rsa)
        {
            string xmlContent = rsa.ToXmlString(true);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlContent);
            XmlNode newNode = doc.DocumentElement;

            var json = JsonConvert.SerializeXmlNode(newNode);

            return json;
        }
        private string jsonFormat = @"{
    ""address"":""<address>"",
    ""crypto"": {
      ""cipher"": ""<algo>"",
      ""cipherparams"": {
        ""iv"": "" < iv>"",
      },
      ""ciphertext"": ""<cipherText>"",
      ""<kdf>"",
      ""kdfparams"" :{<kdfParams>},
      ""mac"": ""<mac>"",
    },
    ""id"": ""<uuid>"",
    ""version"": <version>,
  }";
        #endregion

        #region Keystore utils
        /**
         * The parameters of the secp256k1 curve that Bitcoin uses.
         */
        public static ECDomainParameters CURVE = new ECDomainParameters(CURVE_PARAMS.Curve, CURVE_PARAMS.G, CURVE_PARAMS.N, CURVE_PARAMS.H);
        private static X9ECParameters CURVE_PARAMS = CustomNamedCurves.GetByName("secp256k1");
        private static KeyStore keystore = new KeyStore();
        private static string pattern = "^(0x)?[0-9a-f]";


        public static string GeneratePrivateKey()
        {
            ECKeyPair keys = Schnorr.GenerateKeyPair();
            return keys.PrivateKey.ToString(8);
        }

        public static string GetAddressFromPrivateKey(string privateKey)
        {
            string publicKey = GetPublicKeyFromPrivateKey(privateKey, true);
            return GetAddressFromPublicKey(publicKey);
        }

        public static bool IsBytestring(string address)
        {
            System.Console.WriteLine(address);

            MatchCollection matchs = Regex.Matches(address, pattern);
            foreach (Match match in matchs)
            {
                System.Console.WriteLine(match.Groups);
            }
            return true;
        }

        /**
         * @param privateKey hex string without 0x
         * @return
         */
        public static string GetPublicKeyFromPrivateKey(string privateKey, bool compressed)
        {
            BigInteger bigInteger = new BigInteger(privateKey, 16);
            ECPoint point = GetPublicPointFromPrivate(bigInteger);
            return ByteUtil.ByteArrayToHexString(point.GetEncoded(compressed));
        }

        public static string GetAddressFromPublicKey(string publicKey)
        {
            SHA256 s = new SHA256Managed();
            byte[] address = s.ComputeHash(ByteUtil.HexStringToByteArray(publicKey));
            //byte[] address = GetAddressFromPublicKey);
            return ByteUtil.ByteArrayToHexString(address).Substring(24);
        }

        public static byte[] GenerateRandomBytes(int size)
        {
            byte[] bytes = new byte[size];
            new SecureRandom().NextBytes(bytes);
            return bytes;
        }

        private static ECPoint GetPublicPointFromPrivate(BigInteger privateKeyPoint)
        {
            if (privateKeyPoint.BitLength > CURVE.N.BitLength)
            {
                privateKeyPoint = privateKeyPoint.Mod(CURVE.N);
            }
            return new FixedPointCombMultiplier().Multiply(CURVE.G, privateKeyPoint);
        }

        public static string DecryptPrivateKey(string file, string passphrase)
        {
            return keystore.DecryptPrivateKey(file, passphrase);
        }

        public static string EencryptPrivateKey(string privateKey, string passphrase, KDFType type)
        {
            return keystore.EncryptPrivateKey(privateKey, passphrase, type);
        }
        
        #endregion
    }
}
