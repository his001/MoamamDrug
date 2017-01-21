using System;
using System.Collections.Generic;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Linq;

namespace Moamam.Lib
{
    public class RsaHelper
    {
        // RSA 암호화 
        public static string RSAEncrypt(string getValue, string pubKey)
        {
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(); //암호화 
            rsa.FromXmlString(pubKey);

            //암호화할 문자열을 UFT8인코딩 
            byte[] inbuf = (new UTF8Encoding()).GetBytes(getValue);

            //암호화 
            byte[] encbuf = rsa.Encrypt(inbuf, false);

            //암호화된 문자열 Base64인코딩 
            return Convert.ToBase64String(encbuf);
        }

        // RSA 복호화 
        public static string RSADecrypt(string getValue, string priKey)
        {
            //RSA객체생성 
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(); //복호화 
            rsa.FromXmlString(priKey);

            //sValue문자열을 바이트배열로 변환 
            byte[] srcbuf = Convert.FromBase64String(getValue);

            //바이트배열 복호화 
            byte[] decbuf = rsa.Decrypt(srcbuf, false);

            //복호화 바이트배열을 문자열로 변환 
            string sDec = (new UTF8Encoding()).GetString(decbuf, 0, decbuf.Length);
            return sDec;
        }

        public static string RSADecrypt(byte[] getValue, string priKey)
        {
            //RSA객체생성 
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(); //복호화 
            rsa.FromXmlString(priKey);

            //sValue문자열을 바이트배열로 변환 
            byte[] srcbuf = getValue;

            //바이트배열 복호화 
            byte[] decbuf = rsa.Decrypt(srcbuf, false);

            //복호화 바이트배열을 문자열로 변환 
            string sDec = (new UTF8Encoding()).GetString(decbuf, 0, decbuf.Length);
            return sDec;
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }
    }
}
