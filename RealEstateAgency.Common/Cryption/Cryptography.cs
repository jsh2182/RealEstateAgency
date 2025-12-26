using System;
using System.IO;
using System.Security.Cryptography;

namespace RealEstateAgency
{
    public class Cryptography
    {
        public static string cipherKey = "@#$*%^et";

        /// <summary>
        /// Encrypt the argument with RC2 algorithm.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>

        public static string Encrypt(string str, string pass)
        {
            return RC2Encryption(str, pass.ToLower());
        }

        /// <summary>
        /// Encrypts the string with AES and then RC2 algorithms.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Pass"></param>
        /// <returns></returns>

        public static string FullEncrypt(string str, string pass)
        {
            pass = pass.ToLower();
            clsAES aes = new clsAES(clsAES.KeySize.Bits128, pass);
            string AESEnc = ByteArray2HexStr(aes.Encrypt(System.Text.ASCIIEncoding.UTF8.GetBytes(str)));
            return RC2Encryption(AESEnc, pass);
        }
        /*
            pass = pass.ToLower();
            clsAES aes = new clsAES(clsAES.KeySize.Bits128, pass);
            string AESEnc = ByteArray2HexStr(aes.Encrypt(System.Text.ASCIIEncoding.UTF8.GetBytes(str)));
            return RC2Encryption(AESEnc, pass);
         * */

        /// <summary>
        /// Decrypts the encrypted string with RC2 and then AES algorithms. 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pass"></param>
        /// <returns></returns>

        public static string Decrypt(string str, string pass)
        {


            pass = pass.ToLower();

            //////////////////////////
            return RC2Decryption(str, pass);
            //////////////////////////

            //clsAES aes = new clsAES(clsAES.KeySize.Bits128, pass);

            //string strEnc = System.Text.Encoding.UTF8.GetString(aes.Decrypt(HexStr2ByteArray(RC2Decryption(str, pass))));

            //// Ignore null chars.

            //strEnc = strEnc.Replace("\0", "");

            //return strEnc;
        }

        public static string RC2Encryption(string src, string strKey)
        {
            try
            {
                //   return src;

                if (strKey.Length == 0) return "";

                // Create a new instance of the RC2CryptoServiceProvider class

                RC2CryptoServiceProvider rc2CSP = new RC2CryptoServiceProvider();

                // Get the key (8 bytes) and IV (64 bytes).


                while (strKey.Length < 8) strKey += strKey;

                //byte[] key = System.Text.Encoding.ASCII.GetBytes(strKey.Substring(0, 8));
                byte[] key = System.Text.Encoding.UTF8.GetBytes(strKey.Substring(0, 8));

                string strIV = strKey;
                while (strIV.Length < 64)
                    strIV += strIV;
                strIV = strIV.Substring(0, 64);
                //byte[] IV = System.Text.Encoding.ASCII.GetBytes(strIV);
                byte[] IV = System.Text.Encoding.UTF8.GetBytes(strIV);

                // Get an encryptor.

                ICryptoTransform encryptor = rc2CSP.CreateEncryptor(key, IV);

                // Encrypt the data as an array of encrypted bytes in memory.

                MemoryStream msEncrypt = new MemoryStream();
                CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

                // Convert the data to a byte array.

                //byte[] toEncrypt = System.Text.Encoding.ASCII.GetBytes(src);
                byte[] toEncrypt = System.Text.Encoding.UTF8.GetBytes(src);

                // Write all data to the crypto stream and flush it.

                csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
                csEncrypt.FlushFinalBlock();

                // Get the encrypted array of bytes.

                byte[] encrypted = msEncrypt.ToArray();

                return Convert.ToBase64String(encrypted);
            }
            catch { return ""; }
        }

        public static string RC2Decryption(string enc, string strKey)
        {
            try
            {
                // return enc;

                if (strKey.Length == 0) return "";

                // Get the key (8 bytes) and IV (64 bytes).


                while (strKey.Length < 8) strKey += strKey;

                //byte[] key = System.Text.Encoding.ASCII.GetBytes(strKey.Substring(0, 8));
                byte[] key = System.Text.Encoding.UTF8.GetBytes(strKey.Substring(0, 8));

                string strIV = strKey;
                while (strIV.Length < 64)
                    strIV += strIV;
                strIV = strIV.Substring(0, 64);
                //byte[] IV = System.Text.Encoding.ASCII.GetBytes(strIV);
                byte[] IV = System.Text.Encoding.UTF8.GetBytes(strIV);

                //Get a decryptor.

                RC2CryptoServiceProvider rc2CSP = new RC2CryptoServiceProvider();
                ICryptoTransform decryptor = rc2CSP.CreateDecryptor(key, IV);

                // Now decrypt the encrypted data.

                int encLength = Convert.FromBase64String(enc).Length;

                MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(enc));
                CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

                byte[] fromEncrypt = new byte[encLength];

                // Read the data out of the crypto stream.

                csDecrypt.Read(fromEncrypt, 0, encLength);

                // Convert the byte array back into a string.

                //return System.Text.Encoding.ASCII.GetString(fromEncrypt);

                return System.Text.Encoding.UTF8.GetString(fromEncrypt).Replace("\0", "");
            }
            catch { return ""; }
        }

        public static byte[] SHA1Encryption(string src)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(src);
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            return sha1.ComputeHash(data);
        }

        public static byte[] HexStr2ByteArray(string hexStr)
        {
            if (hexStr.Length % 2 != 0) // must have even length 
                return null;
            if (hexStr.IndexOf("0x") == 0 || hexStr.IndexOf("0X") == 0)
                hexStr = hexStr.Substring(2);
            byte[] byteArray = new Byte[hexStr.Length / 2];
            for (int i = 0; i < hexStr.Length; i += 2)
                byteArray[i / 2] = Convert.ToByte(HexToInt(hexStr.Substring(i, 2)));

            return byteArray;
        }

        private static int HexToInt(string hex)
        {
            return (HexDigitValue(hex[0]) * 16 + HexDigitValue(hex[1]));
        }

        private static int HexDigitValue(char hexDigit)
        {
            int retValue = 0;

            switch (hexDigit.ToString().ToLower())
            {
                case "0": retValue = 0; break;
                case "1": retValue = 1; break;
                case "2": retValue = 2; break;
                case "3": retValue = 3; break;
                case "4": retValue = 4; break;
                case "5": retValue = 5; break;
                case "6": retValue = 6; break;
                case "7": retValue = 7; break;
                case "8": retValue = 8; break;
                case "9": retValue = 9; break;
                case "a": retValue = 10; break;
                case "b": retValue = 11; break;
                case "c": retValue = 12; break;
                case "d": retValue = 13; break;
                case "e": retValue = 14; break;
                case "f": retValue = 15; break;
            }
            return retValue;
        }

        public static string ByteArray2HexStr(byte[] bytes)
        {
            string hexStr = "";

            for (int i = 0; i < bytes.Length; i++)
                hexStr += IntToHexStr(Convert.ToInt32(bytes[i]));

            return hexStr;
        }

        private static string IntToHexStr(int digit)
        {
            return HexDigit(digit / 16) + HexDigit(digit % 16);
        }

        private static string HexDigit(int digit)
        {
            string retValue = "0";

            switch (digit)
            {
                case 0: retValue = "0"; break;
                case 1: retValue = "1"; break;
                case 2: retValue = "2"; break;
                case 3: retValue = "3"; break;
                case 4: retValue = "4"; break;
                case 5: retValue = "5"; break;
                case 6: retValue = "6"; break;
                case 7: retValue = "7"; break;
                case 8: retValue = "8"; break;
                case 9: retValue = "9"; break;
                case 10: retValue = "a"; break;
                case 11: retValue = "b"; break;
                case 12: retValue = "c"; break;
                case 13: retValue = "d"; break;
                case 14: retValue = "e"; break;
                case 15: retValue = "f"; break;
            }
            return retValue;
        }
    }
}