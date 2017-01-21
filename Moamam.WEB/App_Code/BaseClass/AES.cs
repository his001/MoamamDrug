using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace AESWeb
{
    public class AES
    {
        public static string Reverse(string strIP)
        {
            if (strIP.Length == 0) return "";

            String strRet = "";
            for (int i = strIP.Length - 1; i >= 0; i--)
            {
                strRet += strIP.Substring(i, 1);
            }

            for (int i = strIP.Length; i < 32; i++)
            {
                strRet += "a";
            }
            return strRet;
        }
        public static string getAESEncryptData(string value, string encryptionKey)
        {
            if (value.Length == 0) return "";

            try
            {
                var key = Encoding.UTF8.GetBytes(encryptionKey); //must be 16/24/32 chars
                var rijndael = new RijndaelManaged();
                rijndael.Key = key;
                rijndael.Mode = CipherMode.ECB;
                rijndael.Padding = PaddingMode.PKCS7;

                var transform = rijndael.CreateEncryptor();
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, transform, CryptoStreamMode.Write))
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes(value);

                        cs.Write(buffer, 0, buffer.Length);
                        cs.FlushFinalBlock();
                        cs.Close();
                    }
                    ms.Close();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string getAESDecryptData(string value, string encryptionKey)
        {
            if (value.Length == 0) return "";

            try
            {
                var key = Encoding.UTF8.GetBytes(encryptionKey); //must be 16/24/32 chars
                var rijndael = new RijndaelManaged();
                rijndael.Key = key;
                rijndael.Mode = CipherMode.ECB;
                rijndael.Padding = PaddingMode.PKCS7;

                var buffer = Convert.FromBase64String(value);
                var transform = rijndael.CreateDecryptor();
                string decrypted;
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, transform, CryptoStreamMode.Write))
                    {
                        cs.Write(buffer, 0, buffer.Length);
                        cs.FlushFinalBlock();
                        decrypted = Encoding.UTF8.GetString(ms.ToArray());
                        cs.Close();
                    }
                    ms.Close();
                }

                return decrypted;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static bool isBase64(string base64String)
        {
            if (base64String.Length == 0) return false;

            if (base64String.Replace(" ", "").Length % 4 != 0)
            {
                return false;
            }

            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch (FormatException ex)
            {
                // Handle the exception
            }
            return false;
        }
    }
}
