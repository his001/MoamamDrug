using System;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.UI;

using Moamam.Data.Common;
using Moamam.Data.WebControls;
 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

/// <summary>
/// UserFunction의 요약 설명입니다.
/// </summary>
public class UserFunction : BasePage
{
    public static string test() {
        return "1";
    }
    #region ==//추가 함수 모음// ====================================================
    public static string MakeHtmlInfoBase(string SendValue, string chk)
    {
        string tmp = "&nbsp;";
        SendValue = IsInjectionReplace(SendValue);//SQL INJECTION
        int rint = 0;

        if (chk == "YN")
        {
            if (!string.IsNullOrEmpty(SendValue))
            {
                if (SendValue == "Y") tmp = "Y";
                else tmp = "<font color='silver'>N</font>"; 
            }
            else
            {
                tmp = "<font color='silver'>N</font>";
            }
        }
        else if (chk == "CINT")
        {
            if (!string.IsNullOrEmpty(SendValue))
            {
                decimal d = Convert.ToDecimal(SendValue);
                rint = Convert.ToInt32(d);
                tmp = rint.ToString();
            }
            else
            {
                tmp = "";
            }
        }
        else if (chk == "CINTW")
        {
            if (!string.IsNullOrEmpty(SendValue))
            {
                decimal d = Convert.ToDecimal(SendValue);
                rint = Convert.ToInt32(d);
                tmp = rint.ToString("#,##0");
            }
            else
            {
                tmp = "";
            }
        }
        else if (chk == "LEN")
        {
            if (SendValue.Length > 9)
                SendValue = SendValue.Substring(0, 9) + "...";

            tmp = SendValue;
        }
        else if (chk == "LEN12")
        {
            if (SendValue.Length > 12)
                SendValue = SendValue.Substring(0, 11) + "...";

            tmp = SendValue;
        }
        else if (chk == "PERCENT")
        {
            if (!string.IsNullOrEmpty(SendValue))
            {
                decimal d = Convert.ToDecimal(SendValue);
                rint = Convert.ToInt32(d);
                tmp = rint.ToString() + "%";
            }
            else
            {
                tmp = "0";
            }
        }
        else if (chk == "METHOD")
        {
            if (SendValue == "A") SendValue = "<font color='red'>자동</font>";
            else if (SendValue == "M") SendValue = "<font color='blue'>수동</font>";
            else SendValue = "";
            tmp = SendValue;
        }
        else if (chk == "STATUS")
        {
            if (SendValue == "A") SendValue = "<font color='red'>승인</font>";
            else if (SendValue == "W") SendValue = "미승인";
            else if (SendValue == "C") SendValue = "<font color='blue'>전송</font>";
            else SendValue = "미승인";
            tmp = SendValue;
        }
        else if (chk == "DATE")
        {
            if (SendValue.Length == 4)
            {
                SendValue = SendValue.Substring(0, 4) + "." ;
            }
            else if (SendValue.Length == 6)
            {
                SendValue = SendValue.Substring(0, 4) + "." + SendValue.Substring(4, 2) + ".";
            }
            else if (SendValue.Length == 8)
            {
                SendValue = SendValue.Substring(0, 4) + "." + SendValue.Substring(4, 2) + "." + SendValue.Substring(6, 2);
            } 
            tmp = SendValue;
        }
        else if (chk == "TERMID")
        {
            if (SendValue == "P") SendValue = "<font color='red'>P</font>"; 
            else if (SendValue == "T") SendValue = "<font color='blue'>T</font>";
            else SendValue = "";
            tmp = SendValue;
        }
        else
        {
            if (!string.IsNullOrEmpty(SendValue))
            {
                tmp = SendValue;
            }
        }
        //
        return tmp;
    }


    public static string MakeCalsBase(string a, string b, string chk)
    {
        string tmp = string.Empty;
        if (string.IsNullOrEmpty(a)) a = "0";
        if (string.IsNullOrEmpty(b)) b = "0";

        a = IsInjectionReplace(a);//SQL INJECTION
        b = IsInjectionReplace(b);//SQL INJECTION

        a = (Convert.ToInt32(Convert.ToDecimal(a))).ToString();
        b = (Convert.ToInt32(Convert.ToDecimal(b))).ToString();
         

        if (chk == "SSSUM")
        {
            tmp = (Convert.ToInt32(a) + Convert.ToInt32(b) / 100).ToString();
        }
        else
        {
            tmp = (Convert.ToInt32(a) + Convert.ToInt32(b)).ToString();
        }

        return tmp;
    }

    #region MakeStyle
    public static string MakeStyle(NButton ClientID, string chk)
    {
        string tmp = string.Empty;
        if (chk == "cursor")
        {
            ClientID.Style.Add("cursor", "pointer");
        }
        else if (chk == "blur")
        {
            ClientID.Attributes.Add("onfocus", "blur()");
        }
        else
        {
            ClientID.Style.Add("cursor", "default");
        }
        return tmp;
    }
    public static string MakeStyle(System.Web.UI.WebControls.Button ClientID, string chk)
    {
        string tmp = string.Empty;
        if (chk == "cursor")
        {
            ClientID.Style.Add("cursor", "pointer");
        }
        else
        {
            ClientID.Style.Add("cursor", "default");
        }
        return tmp;
    }
    public static string MakeStyle(System.Web.UI.WebControls.ImageButton ClientID, string chk)
    {
        string tmp = string.Empty;
        if (chk == "cursor")
        {
            ClientID.Style.Add("cursor", "pointer");
        }
        else
        {
            ClientID.Style.Add("cursor", "default");
        }
        return tmp;
    }
    #endregion

    #region base.MakeButtonToEnabledOrVisible
    public static string MakeButtonToEnabledOrVisible(NButton ClientID, string chk)
    {
        string tmp = string.Empty;
        if (chk == "Visible")
        {
            if (!ClientID.Enabled) ClientID.Visible = false;
        }
        else
        {
            ClientID.Visible = true;
            ClientID.Enabled = true;
        }
        return tmp;
    }
    public static string MakeButtonToEnabledOrVisible(System.Web.UI.WebControls.Button ClientID, string chk)
    {
        string tmp = string.Empty;
        if (chk == "Visible")
        {
            if (!ClientID.Enabled) ClientID.Visible = false;
        }
        else
        {
            ClientID.Visible = true;
            ClientID.Enabled = true;
        }
        return tmp;
    }
    public static string MakeButtonToEnabledOrVisible(System.Web.UI.WebControls.ImageButton ClientID, string chk)
    {
        string tmp = string.Empty;
        if (chk == "Visible")
        {
            if (!ClientID.Enabled) ClientID.Visible = false;
        }
        else
        {
            ClientID.Visible = true;
            ClientID.Enabled = true;
        }
        return tmp;
    }
    #endregion

    #region 아이템 코드 자리수 매김(왼쪽에 "0"으로 체움)
    public static string MakeItemLandgth(string item) {
        string tmp = string.Empty;
        item = IsInjectionReplace(item.ToString(), "");//SQL인젝션 20161219추가
        item = item.ToUpper().ToString() == "NULL" ? "" : item.ToString();//소문자 NULL일경우 20161219추가
        int num = item.ToString().Length;
        for (int i = 0; i < 9 - num; i++) {
            tmp += "0";
        }
        tmp = tmp + item;
            return tmp;

    }
    #endregion



    public static string IsNullOrEmpty(string Item,string ReplaceValue)
    {
        string tmp = string.Empty;
        Item = IsInjectionReplace(Item.ToString(), "");//SQL인젝션 20161219추가
        Item = Item.ToUpper().ToString() == "NULL" ? ReplaceValue : Item.ToString();//소문자 NULL일경우 20161219추가
        tmp = string.IsNullOrEmpty(Item) ? ReplaceValue : Item;
        return tmp;
    }
    #endregion

    public static string IsInjectionReplace(string strValue, string chk = "")
    {
        #region SQL Injection 특수문자 필터링
        /* 
            '필수 필터링 문자 리스트
            strSearch(0)="'"
            strSearch(1)=""""
            strSearch(2)="\"
            strSearch(3)=null
            strSearch(4)="#"
            strSearch(5)="--"
            strSearch(6)=";"

            '변환될 필터 문자	
            strReplace(0)="''"
            strReplace(1)=""""""
            strReplace(2)="\\"
            strReplace(3)="\"&null
            strReplace(4)="\#"
            strReplace(5)="\--"
            strReplace(6)="\;"
         */
        #endregion

        string tmp;
        if (chk != "Q")
        {
            strValue = strValue.Replace(" ", "");
        }
        strValue = strValue.Replace("--", "");
        strValue = strValue.Replace("'", "`");
        strValue = strValue.Replace("1=1", "");
        strValue = strValue.Replace(";", "");
        tmp = strValue;
        return tmp;
    }

    public static class HTML
    {
        /// <summary>
        /// 해당 URL의 리턴 값을 string 형태로 반환한다.
        /// </summary>
        /// <param name="pURL"></param>
        /// <returns></returns>
        public static string GetHtmlInfo(string pURL)
        {
            WebClient webClient = new WebClient();
            byte[] arrByte = webClient.DownloadData(pURL);

            return Encoding.UTF8.GetString(arrByte);
        }

    }

}