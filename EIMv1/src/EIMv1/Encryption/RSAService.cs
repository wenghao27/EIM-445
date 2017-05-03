using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace EIMv1.Encryption
{
    public class RSAService
    {

        /// <summary>
        /// Generate RSA key pair.
        /// </summary>
        /// <returns>RSA key pair byte array.</returns>
        public static byte[] RSAKeyGeneration()
        {
            try
            {
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048))
                {
                    // Export key pair (public and private)
                    byte[] keyPair = RSA.ExportCspBlob(true);

                    //// Export public key only
                    //byte[] publicKey = RSA.ExportCspBlob(false);

                    //// Convert key pair into a base 64 string
                    //string keyPairString = convertToString(keyPair);

                    //// Convert public key into a base 64 string
                    //string publicKeyString = convertToString(publicKey);

                    //// Write key pair to console for demo purposes
                    //Debug.WriteLine("Key Pair: " + keyPairString);
                    //Debug.WriteLine("Public: " + publicKeyString);

                    //// BYTE TO STRING AND BACK TO BYTE TEST
                    //// BEGIN
                    //byte[] test1 = convertToBytes(keyPairString);
                    //byte[] test2 = convertToBytes(publicKeyString);

                    //string keyPairString2 = convertToString(test1);
                    //string publicKeyString2 = convertToString(test2);

                    //Debug.WriteLine("Private Test: " + keyPairString2);
                    //Debug.WriteLine("Public Test: " + publicKeyString2);

                    //if (keyPairString == keyPairString2)
                    //{
                    //    if (publicKeyString == publicKeyString2)
                    //    {
                    //        Debug.WriteLine("Key convertion from bytes to string and back is successful");
                    //    }
                    //}
                    //else
                    //{
                    //    Debug.WriteLine("Key convertion from bytes to string and back failed");
                    //}
                    //// END
                    //// BYTE TO STRING AND BACK TO BYTE TEST

                    //// ENCRYPT AND DECRYPT TEST
                    //// BEGIN
                    //string aeskeysample = "hello world";
                    //byte[] aeskeysamplebytes = encodeStringtoBytes(aeskeysample);
                    //string aeskeysamplestring = convertToString(aeskeysamplebytes);
                    //byte[] aeskeysampletoBase64bytes = convertToBytes(aeskeysamplestring);
                    //byte[] encryptedkey = RSAEncrypt(publicKey, aeskeysampletoBase64bytes);
                    //string encryptedkeystring = convertToString(encryptedkey);
                    //Debug.WriteLine("Encrypted 'hello world': " + encryptedkeystring);
                    //byte[] encryptedkeybytes = convertToBytes(encryptedkeystring);
                    //byte[] decryptedkey = RSADecrypt(keyPair, encryptedkeybytes);
                    //string decryptedkeystring = encodeBytestoString(decryptedkey);
                    //Debug.WriteLine("Decrypted 'hello world': " + decryptedkeystring);
                    //// END
                    //// ENCRYPT AND DECRYPT TEST

                    return keyPair;
                }

            }
            catch (CryptographicException e)
            {
                //Catch this exception in case the encryption did
                //not succeed.
                Console.WriteLine(e.Message);
                return null;

            }
        }

        /// <summary>
        /// Encode an input string to byte array.
        /// </summary>
        /// <param name="str">String to convert into UTF8 bytes.</param>
        /// <returns>Encoded UTF8 byte array.</returns>
        public static byte[] encodeStringtoBytes(string str)
        {
            byte[] enc;
            return enc = Encoding.UTF8.GetBytes(str);
        }

        /// <summary>
        /// Encode byte array into UTF8 string.
        /// </summary>
        /// <param name="bytes">Byte array to convert into UTF8 string.</param>
        /// <returns>UTF8 string from input bytes.</returns>
        public static string encodeBytestoString(byte[] bytes)
        {
            string enc;
            return enc = Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Convert byte array into base 64 string
        /// </summary>
        /// <param name="bytes">Byte array to convert into base 64 string.</param>
        /// <returns>Base 64 string from input bytes.</returns>
        public static string convertToString(byte[] bytes)
        {
            string str;
            return str = Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Convert string into base 64 bytes.
        /// </summary>
        /// <param name="str">String to convert into base 64 byte array.</param>
        /// <returns>Base 64 byte array from input string.</returns>
        public static byte[] convertToBytes(string str)
        {
            byte[] bytes;
            return bytes = Convert.FromBase64String(str);
        }

        /// <summary>
        /// Extract public key from RSA key pair blob.
        /// </summary>
        /// <param name="keyPair">RSA key pair blob.</param>
        /// <returns>Public key from RSA key pair.</returns>
        public static byte[] RSAPublicKeyOnly(byte[] keyPair)
        {
            try
            {
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048))
                {

                    RSA.ImportCspBlob(keyPair);
                    byte[] publicKey = RSA.ExportCspBlob(false);
                    return publicKey;
                }

            }
            catch (CryptographicException e)
            {
                //Catch this exception in case the encryption did
                //not succeed.
                Console.WriteLine(e.Message);
                return null;

            }
        }

        /// <summary>
        /// Encrypt data with the public portion of an RSA key pair.
        /// </summary>
        /// <param name="publicKey">RSA public key.</param>
        /// <param name="AESkey">Data to encrypt.</param>
        /// <returns>Encrypted byte array.</returns>
        public static byte[] RSAEncrypt(byte[] publicKey, byte[] AESkey)
        {
            try
            {
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048))
                {

                    RSA.ImportCspBlob(publicKey);
                    byte[] result = RSA.Encrypt(AESkey, true);
                    return result;
                }

            }
            catch (CryptographicException e)
            {
                //Catch this exception in case the encryption did
                //not succeed.
                Console.WriteLine(e.Message);
                return null;

            }
        }

        /// <summary>
        /// Decrypt data with the RSA key pair.
        /// </summary>
        /// <param name="keyPair">RSA key pair</param>
        /// <param name="encryptedData">Data to decrypt.</param>
        /// <returns>Decrypted byte array.</returns>
        public static byte[] RSADecrypt(byte[] keyPair, byte[] encryptedData)
        {
            try
            {
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048))
                {

                    RSA.ImportCspBlob(keyPair);
                    byte[] result = RSA.Decrypt(encryptedData, true);
                    return result;
                }

            }
            catch (CryptographicException e)
            {
                //Catch this exception in case the encryption did
                //not succeed.
                Console.WriteLine(e.Message);
                return null;

            }
        }
    }
}
