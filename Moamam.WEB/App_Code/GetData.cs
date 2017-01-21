using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.Web.Script.Serialization;
using Moamam.Lib;


/// <summary>
/// GetData의 요약 설명입니다.
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// ASP.NET AJAX를 사용하여 스크립트에서 이 웹 서비스를 호출하려면 다음 줄의 주석 처리를 제거합니다. 
 [System.Web.Script.Services.ScriptService]
public class GetData : System.Web.Services.WebService {

    public GetData () {

        //디자인된 구성 요소를 사용하는 경우 다음 줄의 주석 처리를 제거합니다. 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    /// <summary>
    /// 아이템 코드입력하면 이름 나오게 하는 함수
    /// </summary>
    [WebMethod]
    public string ItemCodeToName(string item)
    {
        string url = string.Format("/Pages/Mbr/GetData.aspx?tcode={0}", item);
        string strHttpResponse = UserFunction.HTML.GetHtmlInfo(url);
        return strHttpResponse.ToString();
    }
    
}
