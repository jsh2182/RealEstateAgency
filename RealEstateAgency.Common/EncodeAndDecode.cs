
using RealEstateAgency.Models.Schema.Coockie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Common
{
    public class EncodeAndDecode
    {
        public string Encode(string strEncode)
        {
            if (!string.IsNullOrEmpty(strEncode))
            {
                byte[] keyArray;
                byte[] toEncryptArray = Encoding.UTF8.GetBytes(strEncode);
                string key = new SecurityCode().CodeDigit;

                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF32.GetBytes(key));
                hashmd5.Clear();

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;

                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] resultArray =
                  cTransform.TransformFinalBlock(toEncryptArray, 0,
                  toEncryptArray.Length);
                tdes.Clear();

                return Convert.ToBase64String(resultArray, 0, resultArray.Length).Replace("=", "___");

            }
            else
                return null;
        }

        public string Decode(string strDecode)
        {
            if (!string.IsNullOrEmpty(strDecode))
            {
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(strDecode.Replace("___", "="));
                string key = new SecurityCode().CodeDigit;

                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(ASCIIEncoding.UTF32.GetBytes(key));
                hashmd5.Clear();

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(
                                     toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();

                return ASCIIEncoding.UTF8.GetString(resultArray);
            }
            else
                return null;
        }
    }
}
