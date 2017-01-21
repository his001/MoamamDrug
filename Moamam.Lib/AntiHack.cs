using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using System.Web;

namespace Moamam.Lib
{
    public class AntiHack
    {

        public static bool ChkXSS(string inputParameter)
        {
            if (string.IsNullOrEmpty(inputParameter))
                return true;

            // Following regex convers all the js events and html tags mentioned in followng links.
            //https://www.owasp.org/index.php/XSS_Filter_Evasion_Cheat_Sheet                 
            //https://msdn.microsoft.com/en-us/library/ff649310.aspx

            var pattren = new StringBuilder();

            //Checks any js events i.e. onKeyUp(), onBlur(), alerts and custom js functions etc.             
            pattren.Append(@"((alert|on\w+|function\s+\w+)\s*\(\s*(['+\d\w](,?\s*['+\d\w]*)*)*\s*\))");

            //Checks any html tags i.e. <script, <embed, <object etc.
            pattren.Append(@"|(<(script|iframe|embed|frame|frameset|object|img|applet|body|html|style|layer|link|ilayer|meta|bgsound))");

            return !Regex.IsMatch(System.Web.HttpUtility.UrlDecode(inputParameter), pattren.ToString(), RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        public static string rtnXSS(string str)
        {
            if (!ChkXSS(str))
            {
                str = HttpContext.Current.Server.HtmlEncode(str);
            }
            return str;
        }

        public static string rtnSQLInj(string strValue)
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
            //if (chk != "Q")
            //{
            //    strValue = strValue.Replace(" ", "");
            //}
            strValue = strValue.Replace("--", "");
            //strValue = strValue.Replace("'", "`");
            //strValue = strValue.Replace("1=1", "");
            //strValue = strValue.Replace(";", "");
            tmp = strValue;
            return tmp;
        }

    }

        
}
