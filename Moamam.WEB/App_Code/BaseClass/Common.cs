using System.Configuration;
using System.Text;
using System.Web;
using System.IO;
using System;

public class MyWebConfig
{
    static public string connectionString = ConfigurationManager.ConnectionStrings[ConfigurationManager.ConnectionStrings["ConnectionStringDefault"].ConnectionString].ToString();
    static public string uploadFolder = HttpContext.Current.Server.MapPath("~/Upload");
    static public string downloadFolder = HttpContext.Current.Server.MapPath("~/Download");
    static public Int32 displayRowCount = Convert.ToInt32(ConfigurationManager.AppSettings["DisplayRowCount"]);
    static public Int32 pagerDisplayRowCount = Convert.ToInt32(ConfigurationManager.AppSettings["PagerDisplayRowCount"]);

    static MyWebConfig()
    {
        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Download")))
        {
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Download"));
        }

        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload")))
        {
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload"));
        }
    }

    #region "[Defined Common Functions]==================================================================================="
    public static int CInt(string sValue)
    {
        try
        {
            string sValue_SV = string.Empty;
            if (sValue == null || sValue.Trim() == "") return 0;
            sValue_SV = sValue.Replace(",", "").Trim();
            if (sValue_SV.IndexOf(".") >= 0) sValue_SV = sValue_SV.Replace(sValue_SV.Substring(sValue_SV.IndexOf(".")), "");    // double형일 경우에 소수이하부분을 버린다.
            sValue_SV = string.Format("{0:##############0}", Convert.ToInt32(sValue_SV));
            return int.Parse(sValue_SV);
        }
        catch
        {
            return 0;
        }
    }
    public static double CDouble(string sValue)
    {
        try
        {
            string sValue_SV = string.Empty;
            if (sValue == null || sValue.Trim() == "") return 0;
            sValue_SV = sValue.Replace(",", "").Trim();
            sValue_SV = string.Format("{0:##############0}", Convert.ToDouble(sValue_SV));
            return double.Parse(sValue_SV);
        }
        catch
        {
            return 0;
        }
    }
    public static double CDoubleFix2(string sValue)
    {
        try
        {
            string sValue_SV = string.Empty;
            if (sValue == null || sValue.Trim() == "") return 0;
            sValue_SV = sValue.Replace(",", "").Trim();
            sValue_SV = string.Format("{0:##############0.00}", Convert.ToDouble(sValue_SV));
            return double.Parse(sValue_SV);
        }
        catch
        {
            return 0;
        }
    }
    public static double CDoubleFix3(string sValue)
    {
        try
        {
            string sValue_SV = string.Empty;
            if (sValue == null || sValue.Trim() == "") return 0;
            sValue_SV = sValue.Replace(",", "").Trim();
            sValue_SV = string.Format("{0:##############0.000}", Convert.ToDouble(sValue_SV));
            return double.Parse(sValue_SV);
        }
        catch
        {
            return 0;
        }
    }
    public static string String_Format(string szFormat, object szValue, bool FixedFormat, bool ZeroView)
    {
        int ConvertInt = 0;
        double ConvertDouble = 0;
        bool isDouble = false;
        bool isZero = false;
        string ChangeFormat = string.Empty;
        string RTNValue = string.Empty;

        try
        {
            isDouble = (szFormat.IndexOf(".") >= 0 ? true : false);
            if (isDouble == true)
            {
                ConvertDouble = MyWebConfig.CDouble(szValue.ToString());
                if (ConvertDouble <= 0) isZero = true;
            }
            else
            {
                ConvertInt = MyWebConfig.CInt(szValue.ToString());
                if (ConvertInt <= 0) isZero = true;
            }

            if (ZeroView == true && FixedFormat == true && isZero == true) return string.Format("{0:" + szFormat + "}", 0);
            else if (ZeroView == true && isZero == true) return "0";
            else if (ZeroView == false && isZero == true) return "";

            ChangeFormat = "";
            if (FixedFormat == true)
            {
                ChangeFormat = szFormat;
            }
            else
            {
                for (int i = 0; i < szFormat.Length; i++)
                {
                    ChangeFormat += (szFormat.Substring(i, 1) != "," && szFormat.Substring(i, 1) != "." ? "#" : szFormat.Substring(i, 1));
                }
            }

            RTNValue = string.Format("{0:" + ChangeFormat + "}", (isDouble == true ? ConvertDouble : ConvertInt));
            return RTNValue;
        }
        catch
        {
            return (ZeroView == true ? "0" : "");
        }
    }

    /// <summary>
    /// 날짜출력 형식
    /// [1 = yyyy-MM-dd HH:mm:ss, 2 = yyyy-MM-dd, 3 = yyyy년 MM월 dd일, default = yyyyMMdd]
    /// </summary>
    public static string DateTimeFormat(DateTime Date, string Type)
    {
        string strDate = string.Empty;

        switch (Type)
        {
            case "1":
                strDate = Date.ToString("yyyy-MM-dd HH:mm:ss");
                break;
            case "2":
                strDate = Date.ToString("yyyy-MM-dd");
                break;
            case "3":
                strDate = Date.ToString("yyyy년 MM월 dd일");
                break;
            default:
                strDate = Date.ToString("yyyyMMdd");
                break;
        }

        return strDate;
    }
    #endregion





    public static string JavascriptAlert(string str)
    {
        StringBuilder strbuild = new StringBuilder("");

        strbuild.Append("<script type=\"text/javascript\">");
        strbuild.Append("   alert(\"" + str + "\");");
        strbuild.Append("</" + "script>");

        return strbuild.ToString();
    }

    public static string JavascriptAlertClose(string str)
    {
        StringBuilder strbuild = new StringBuilder("");

        strbuild.Append("<script type=\"text/javascript\">");
        strbuild.Append("   alert(\"" + str + "\"); self.close();");
        strbuild.Append("</" + "script>");

        return strbuild.ToString();
    }




}