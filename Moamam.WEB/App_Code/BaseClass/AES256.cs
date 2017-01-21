using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// AESEnDecryption의 요약 설명입니다.
/// </summary>
public class AES256
{
    public static string saltKey = "WeMoamamAdoPasswordKeyMakeParkHS"; // AES256 256bit 암호화 / 복호화

    public AES256()
	{
		//
		// TODO: 여기에 생성자 논리를 추가합니다.
		//
	}

    #region ●●●●●●●●●●●●●● AES256  256bit 암호화 / 복호화  ●●●●●●●●●●●●●●
    //AES_256 암호화
    public static String AESEncrypt256(String Input)
    {
        RijndaelManaged aes = new RijndaelManaged();
        aes.KeySize = 256;
        aes.BlockSize = 128;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = Encoding.UTF8.GetBytes(saltKey);
        aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
        byte[] xBuff = null;
        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
            {
                byte[] xXml = Encoding.UTF8.GetBytes(Input);
                cs.Write(xXml, 0, xXml.Length);
            }

            xBuff = ms.ToArray();
        }

        String Output = Convert.ToBase64String(xBuff);
        return Output;
    }


    //AES_256 복호화
    public static String AESDecrypt256(String Input)
    {
        RijndaelManaged aes = new RijndaelManaged();
        aes.KeySize = 256;
        aes.BlockSize = 128;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = Encoding.UTF8.GetBytes(saltKey);
        aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        var decrypt = aes.CreateDecryptor();
        byte[] xBuff = null;
        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
            {
                byte[] xXml = Convert.FromBase64String(Input);
                cs.Write(xXml, 0, xXml.Length);
            }

            xBuff = ms.ToArray();
        }

        String Output = Encoding.UTF8.GetString(xBuff);
        return Output;
    }
    #endregion ●●●●●●●●●●●●●● AES256  256bit 암호화 / 복호화  ●●●●●●●●●●●●●●
}