using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Data;
using System.Data.SqlClient;

using HtmlAgilityPack;
using System.Xml;

public partial class Site_Data_DrugInfo : BasePage
{
    #region ###################
    const string _keyDrug = "eVISHo10RZetWdKPlTwtbOWvvDwj7WgmuKM%2BfGxCPTxD%2FRGXMeEnrf5hgizc26pIVBrL04e8mR4DdPDCEyHazw%3D%3D"; //의약품 낱알식별정보(DB) 서비스
    const string _gPgRownum = "100";
    const string _gCurPgnum = "1";
    const string _keyDrugUrl = "http://apis.data.go.kr/1470000/MdcinGrnIdntfcInfoService/getMdcinGrnIdntfcInfoList?ServiceKey=" + _keyDrug + "&numOfRows=" + _gPgRownum + "&pageNo=" + _gCurPgnum + "&entp_name=&item_name="; //의약품 낱알식별정보(DB) 서비스
    protected string sHTML = string.Empty;
    #endregion ###################

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }

    private string getHTML(string _searchText)
    {
        //int euckrCodepage = 51949;Encoding euckr = Encoding.GetEncoding(euckrCodepage);byte[] euckrBytes = euckr.GetBytes(_searchText);_searchText = euckr.GetString(euckrBytes);

        ////_searchText = System.Uri.UnescapeDataString(_searchText);
        ////_searchText = Encoding.GetEncoding("EUC-KR").GetString(Encoding.GetEncoding("UTF-8").GetBytes(_searchText));
        ////_searchText = HttpContext.Current.Server.UrlEncode(_searchText);
        //Response.Write( HttpContext.Current.Server.UrlDecode("%EF%BF%BD%EB%B8%98%EF%BF%BD%EB%92%AA%EF%BF%BD%EB%B5%BE%E7%94%B1%EF%BF%BD") + "</br>");
        //Response.Write(HttpContext.Current.Server.UrlDecode(_searchText) + "</br>");
        //Response.Write(_searchText);
        //Response.End();
        string _param = Uri.UnescapeDataString("?drug_name=" + _searchText + "&sunb_name=&firm_name=&x=0&y=0");
        string _url = "http://www.health.kr/drug_info/basedrug/list.asp" + _param;
        //_url = Encoding.GetEncoding("UTF-8").GetString(Encoding.GetEncoding("EUC-KR").GetBytes(_url));

        string rtnString = "<table border='1'><tr><td>제품명</td><td>성분/함량</td><td>제조수입사</td><td>분류</td><td>투여경로</td><td>제형</td><td>구분</td><td>보험</td></tr>";
        string _idx = string.Empty;
        string _drugName = string.Empty;
        string _drugCode = string.Empty;
        string _drugDanPoomYN = string.Empty;
        string _drugSungBun = string.Empty;
        string _drugCompany = string.Empty;
        string _drugBunRu = string.Empty;
        string _drugToYeo = string.Empty;
        string _drugJeHyong = string.Empty;
        string _drugGubun = string.Empty;
        string _drugInsure = string.Empty;
        string _drugImage = string.Empty;

        HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
        htmlDoc.OptionFixNestedTags = true;

        string urlToLoad = _url;
        HttpWebRequest request = HttpWebRequest.Create(urlToLoad) as HttpWebRequest;
        request.Method = "GET";

        /* Sart browser signature */
        request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";
        request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
        request.Headers.Add(HttpRequestHeader.AcceptLanguage, "ko-KR,ko;q=0.8");
        /* Sart browser signature */

        //Console.WriteLine(request.RequestUri.AbsoluteUri);
        WebResponse response = request.GetResponse();

        htmlDoc.Load(response.GetResponseStream(), Encoding.GetEncoding("euc-kr"));
        if (htmlDoc.DocumentNode != null)
        {
            var findclasses = htmlDoc.DocumentNode
            .Descendants("tr")
            .Where(d =>
                d.Attributes.Contains("class")
                && d.Attributes["class"].Value.Contains("bottom_border_dot")
            );

            
            foreach (var articleNode in findclasses)
            {

                #region ########## 없는 Data Db 에 저장 ##########
                _drugName = string.Empty;
                _idx = string.Empty;
                _drugSungBun = string.Empty;
                _drugDanPoomYN = string.Empty;
                _drugCompany = string.Empty;
                _drugBunRu = string.Empty;
                _drugToYeo = string.Empty;
                _drugJeHyong = string.Empty;
                _drugGubun = string.Empty;
                _drugInsure = string.Empty;

                _drugName       = articleNode.ChildNodes[1].InnerText;
                _idx            = articleNode.ChildNodes[1].InnerHtml.Replace("<a href=\"show_detail.asp?idx=", "").Replace("\">", "").Replace("</a>", "").Replace(_drugName, "").Trim();
                _drugSungBun    = articleNode.ChildNodes[3].InnerText;
                _drugDanPoomYN  = articleNode.ChildNodes[3].InnerHtml.IndexOf("<img src=\"/images/icon/single.gif\" valign=\"middle\">") > -1 ? "Y" : "N";
                _drugCompany    = articleNode.ChildNodes[5].InnerText;
                _drugBunRu      = articleNode.ChildNodes[7].InnerHtml;
                _drugToYeo      = articleNode.ChildNodes[9].InnerHtml;
                _drugJeHyong    = articleNode.ChildNodes[11].InnerHtml;
                _drugGubun      = articleNode.ChildNodes[13].InnerHtml;
                _drugInsure     = articleNode.ChildNodes[15].InnerHtml;

                _drugName       = _drugName.Replace("\t","").Replace("\n","").Replace("\r","");
                _idx            = _idx.Replace("\t","").Replace("\n","").Replace("\r",""); 
                _drugSungBun    = _drugSungBun.Replace("\t","").Replace("\n","").Replace("\r","");
                _drugDanPoomYN  = _drugDanPoomYN.Replace("\t","").Replace("\n","").Replace("\r","");
                _drugCompany    = _drugCompany.Replace("\t","").Replace("\n","").Replace("\r","");
                _drugBunRu      = _drugBunRu.Replace("\t","").Replace("\n","").Replace("\r","");
                _drugToYeo      = _drugToYeo.Replace("\t","").Replace("\n","").Replace("\r","");
                _drugJeHyong    = _drugJeHyong.Replace("\t","").Replace("\n","").Replace("\r","");
                _drugGubun      = _drugGubun.Replace("\t","").Replace("\n","").Replace("\r","");
                _drugInsure     = _drugInsure.Replace("\t","").Replace("\n","").Replace("\r","");


                string _chkDrugCodeIdx = "<a href=\"/drug_info/basedrug/show_detail.asp?drug_code=";
                if (articleNode.InnerHtml.IndexOf(_chkDrugCodeIdx)>-1)
                {
                    _drugCode = articleNode.InnerHtml.Substring(articleNode.InnerHtml.IndexOf(_chkDrugCodeIdx) + _chkDrugCodeIdx.Length ,13);
                    _drugImage = "/drug_info/pop_sb.asp?sbcode=" + _drugCode + "01";
                }

                if (setDrugSave(_idx, _drugCode, _drugName, _drugDanPoomYN, _drugSungBun, _drugCompany, _drugBunRu, _drugToYeo, _drugJeHyong, _drugGubun, _drugInsure, _drugImage, "system") == "OK")
                {
                    //_saveCnt++;
                }
                #endregion ########## 없는 Data Db 에 저장 ##########

                //rtnString = rtnString + "<tr>" + WebUtility.HtmlDecode(articleNode.InnerHtml) + "</tr>";
                rtnString = rtnString + "<tr><td>" + _drugName + "</td><td>" + _drugSungBun + "</td><td>" 
                    + _drugCompany + "</td><td>" + _drugBunRu + "</td><td>" + _drugToYeo + "</td><td>" 
                    + _drugJeHyong + "</td><td>" + _drugGubun + "</td><td>" + _drugInsure + "</td></tr>";
            }

            rtnString = rtnString + "</table>";
        }

        return rtnString;
    }

    /// <summary>
    /// 사용자 IP 확인
    /// </summary>
    /// <returns></returns>
    private string getUserIP() { 
        #region IPv4
        string strUserIPv4 = string.Empty;
        foreach (IPAddress _IP in Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress))
        {
            if (_IP.AddressFamily.ToString() == "InterNetwork")
            {
                strUserIPv4 = _IP.ToString();
                break;
            }
        }

        if (strUserIPv4 == string.Empty)
        {
            foreach (IPAddress _IP in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (_IP.AddressFamily.ToString() == "InterNetwork")
                {
                    strUserIPv4 = _IP.ToString();
                    break;
                }
            }
        }
        #endregion
        return strUserIPv4;
    }


    /// <summary>
    /// 약학 정보원의 자료를 조사해 온다
    /// </summary>
    /// <param name="_idx"></param>
    /// <param name="_drugCode"></param>
    /// <param name="_drugName"></param>
    /// <param name="_drugDanPoomYN"></param>
    /// <param name="_drugSungBun"></param>
    /// <param name="_drugCompany"></param>
    /// <param name="_drugBunRu"></param>
    /// <param name="_drugToYeo"></param>
    /// <param name="_drugJeHyong"></param>
    /// <param name="_drugGubun"></param>
    /// <param name="_drugInsure"></param>
    /// <param name="_drugImage"></param>
    /// <param name="_userId"></param>
    /// <returns></returns>
    private string setDrugSave(string _idx ,string _drugCode, string _drugName ,string _drugDanPoomYN ,string _drugSungBun ,string _drugCompany
        ,string _drugBunRu ,string _drugToYeo ,string _drugJeHyong ,string _drugGubun ,string _drugInsure
        , string _drugImage, string _userId)
    {
        string _regip = getUserIP();

        DataSet ds = null;
        string spName = "SPM_Web_COMMON_Tbl_Drug_Info_S";
        SqlParameterCollection param = DataCommon.InitSqlParameterCollection();
        param.Add(new SqlParameter("idx", _idx));
        param.Add(new SqlParameter("drugCode", _drugCode));
        param.Add(new SqlParameter("drugName", _drugName));
        param.Add(new SqlParameter("drugDanPoomYN", _drugDanPoomYN));
        param.Add(new SqlParameter("drugSungBun", _drugSungBun));
        param.Add(new SqlParameter("drugCompany", _drugCompany));
        param.Add(new SqlParameter("drugBunRu", _drugBunRu));
        param.Add(new SqlParameter("drugToYeo", _drugToYeo));
        param.Add(new SqlParameter("drugJeHyong", _drugJeHyong));
        param.Add(new SqlParameter("drugGubun", _drugGubun));
        param.Add(new SqlParameter("drugInsure", _drugInsure));
        param.Add(new SqlParameter("drugImage", _drugImage));
        param.Add(new SqlParameter("regip", _regip));
        param.Add(new SqlParameter("userId", _userId));

        string srtMsg = "err";
        try
        {
            ds = DataCommon.CommonSpCall(spName, param);
            if (ds.Tables[0].Rows.Count > 0)
            {
                srtMsg = ds.Tables[0].Rows[0]["RESULT"].ToString();
            }
        }
        catch (Exception ex) {
            srtMsg = "err";
        }

        return srtMsg;
    }

    protected void btnSearchSvr_Click(object sender, EventArgs e)
    {
        SetNaverXmlParseing(txt_ITEM_NAME.Text);   

        ////Response.Write("%BE%C6%BD%BA%C7%C7%B8%B0" + "<br/>");
        ////Response.Write(HttpContext.Current.Server.UrlDecode("%BE%C6%BD%BA%C7%C7%B8%B0") + "<br/>");
        ////string _searchText = "아스피린";
        ////Response.Write(HttpContext.Current.Server.UrlEncode(_searchText) + "<br/>");
        ////Response.Write(HttpContext.Current.Server.UrlDecode(_searchText) + "<br/>");
        ////Response.Write(Encoding.GetEncoding("EUC-KR").GetString(Encoding.GetEncoding("UTF-8").GetBytes(_searchText)) + "<br/>");

        ////int euckrCodepage = 51949; Encoding euckr = Encoding.GetEncoding(euckrCodepage); byte[] euckrBytes = euckr.GetBytes(_searchText); string _sampleTxt = euckr.GetString(euckrBytes);
        ////Response.Write(_sampleTxt + "<br/>");
        ////Response.Write(Encoding.GetEncoding("UTF-8").GetString(Encoding.GetEncoding("EUC-KR").GetBytes(_searchText)) + "<br/>");
        ////Response.Write(txt_drug_nameSvr.Value);

        ////Response.End();
        //string _searchText = txtSerchName.Text;
        ////_searchText = Encoding.GetEncoding("UTF-8").GetString(Encoding.GetEncoding("EUC-KR").GetBytes(_searchText));

        ////UTF-8 Encoding의 문자열을 ANSI Encoding으로 변환
        //byte[] arrSource = System.Text.Encoding.Default.GetBytes(_searchText);
        //_searchText = Encoding.GetEncoding("EUC-KR").GetString(arrSource);

        //sHTML = getHTML(_searchText);
    }



    /// <summary>
    /// 정부 3.0 공공 DB 에서 의약 정보를 받아와 저장한다.
    /// </summary>
    /// <param name="strXml"></param>
    private void SetNaverXmlParseing(String _searchName)
    {

        var m_strFilePath = _keyDrugUrl + _searchName;
        string xmlStr;
        using (var wc = new WebClient())
        {
            xmlStr = wc.DownloadString(m_strFilePath);
        }
        //var xmlDoc = new XmlDocument();
        //

        #region 
        
        string strXml = @"
<response>
<header>
<resultCode>00</resultCode>
<resultMsg>NORMAL SERVICE.</resultMsg>
</header>
<body>
<numOfRows>3</numOfRows>
<pageNo>1</pageNo>
<totalCount>16925</totalCount>
<items>
<item>
<ITEM_SEQ>201502386</ITEM_SEQ>
<ITEM_NAME>덱스트론정300밀리그램(덱시부프로펜디.씨.)</ITEM_NAME>
<ENTP_SEQ>19610006</ENTP_SEQ>
<ENTP_NAME>대화제약(주)</ENTP_NAME>
<CHART>분홍색의장방형필름코팅정</CHART>
<ITEM_IMAGE>http://drug.mfds.go.kr/html/item_image_download.jsp?docId=147427049111000046</ITEM_IMAGE>
<PRINT_FRONT>D분할선T</PRINT_FRONT>
<PRINT_BACK/>
<DRUG_SHAPE>장방형</DRUG_SHAPE>
<COLOR_CLASS1>분홍</COLOR_CLASS1>
<COLOR_CLASS2/>
<LINE_FRONT>-</LINE_FRONT>
<LINE_BACK/>
<LENG_LONG>15.3</LENG_LONG>
<LENG_SHORT>7.7</LENG_SHORT>
<THICK>5.3</THICK>
<IMG_REGIST_TS>20150611</IMG_REGIST_TS>
<CLASS_NO>01140</CLASS_NO>
<CLASS_NAME>해열.진통.소염제</CLASS_NAME>
<ETC_OTC_NAME>일반의약품</ETC_OTC_NAME>
<ITEM_PERMIT_DATE>20150421</ITEM_PERMIT_DATE>
<FORM_CODE_NAME>정제, 미분류</FORM_CODE_NAME>
<MARK_CODE_FRONT_ANAL/>
<MARK_CODE_BACK_ANAL/>
<MARK_CODE_FRONT_IMG/>
<MARK_CODE_BACK_IMG/>
</item>
<item>
<ITEM_SEQ>201502393</ITEM_SEQ>
<ITEM_NAME>유덱스캡슐</ITEM_NAME>
<ENTP_SEQ>20050016</ENTP_SEQ>
<ENTP_NAME>(주)한국팜비오</ENTP_NAME>
<CHART>흰색의 분말을 함유한 상하부 흰색의 경질캡슐제</CHART>
<ITEM_IMAGE>http://drug.mfds.go.kr/html/item_image_download.jsp?docId=147427049111000058</ITEM_IMAGE>
<PRINT_FRONT>PB UD</PRINT_FRONT>
<PRINT_BACK/>
<DRUG_SHAPE>장방형</DRUG_SHAPE>
<COLOR_CLASS1>하양</COLOR_CLASS1>
<COLOR_CLASS2>하양</COLOR_CLASS2>
<LINE_FRONT/>
<LINE_BACK/>
<LENG_LONG>19.2</LENG_LONG>
<LENG_SHORT>6.7</LENG_SHORT>
<THICK>7</THICK>
<IMG_REGIST_TS>20150706</IMG_REGIST_TS>
<CLASS_NO>03910</CLASS_NO>
<CLASS_NAME>간장질환용제</CLASS_NAME>
<ETC_OTC_NAME>전문의약품</ETC_OTC_NAME>
<ITEM_PERMIT_DATE>20150422</ITEM_PERMIT_DATE>
<FORM_CODE_NAME>경질 캡슐, 산제</FORM_CODE_NAME>
<MARK_CODE_FRONT_ANAL/>
<MARK_CODE_BACK_ANAL/>
<MARK_CODE_FRONT_IMG/>
<MARK_CODE_BACK_IMG/>
</item>
<item>
<ITEM_SEQ>201502730</ITEM_SEQ>
<ITEM_NAME>알레자이정(레보세티리진염산염)</ITEM_NAME>
<ENTP_SEQ>20010024</ENTP_SEQ>
<ENTP_NAME>(주)다산메디켐</ENTP_NAME>
<CHART>흰색 또는 미황색의 타원형 필름코팅정</CHART>
<ITEM_IMAGE>http://drug.mfds.go.kr/html/item_image_download.jsp?docId=147427065184100000</ITEM_IMAGE>
<PRINT_FRONT>DM</PRINT_FRONT>
<PRINT_BACK>LC</PRINT_BACK>
<DRUG_SHAPE>타원형</DRUG_SHAPE>
<COLOR_CLASS1>하양</COLOR_CLASS1>
<COLOR_CLASS2/>
<LINE_FRONT/>
<LINE_BACK/>
<LENG_LONG>8.2</LENG_LONG>
<LENG_SHORT>4.4</LENG_SHORT>
<THICK>2.5</THICK>
<IMG_REGIST_TS>20150730</IMG_REGIST_TS>
<CLASS_NO>01410</CLASS_NO>
<CLASS_NAME>항히스타민제</CLASS_NAME>
<ETC_OTC_NAME>전문의약품</ETC_OTC_NAME>
<ITEM_PERMIT_DATE>20150501</ITEM_PERMIT_DATE>
<FORM_CODE_NAME>필름코팅정</FORM_CODE_NAME>
<MARK_CODE_FRONT_ANAL/>
<MARK_CODE_BACK_ANAL/>
<MARK_CODE_FRONT_IMG/>
<MARK_CODE_BACK_IMG/>
</item>
</items>
</body>
</response>
";
        #endregion

        var DslstDrugInfo = new List<List<string>>();

        XmlDocument xml = new XmlDocument(); // XmlDocument 생성
        xml.LoadXml(xmlStr);
        //xmlDoc.LoadXml(xmlStr);
        XmlNodeList xnList = xml.GetElementsByTagName("item"); //접근할 노드

        string _saveResult = string.Empty;
        string _ITEM_SEQ = string.Empty; string _ITEM_NAME = string.Empty; string _ENTP_SEQ = string.Empty; string _ENTP_NAME = string.Empty; string _CHART = string.Empty; string _ITEM_IMAGE = string.Empty;
        string _PRINT_FRONT = string.Empty; string _PRINT_BACK = string.Empty; string _DRUG_SHAPE = string.Empty; string _COLOR_CLASS1 = string.Empty; string _COLOR_CLASS2 = string.Empty;
        string _LINE_FRONT = string.Empty; string _LINE_BACK = string.Empty; string _LENG_LONG = string.Empty; string _LENG_SHORT = string.Empty; string _THICK = string.Empty;
        string _IMG_REGIST_TS = string.Empty; string _CLASS_NO = string.Empty; string _CLASS_NAME = string.Empty; string _ETC_OTC_NAME = string.Empty; string _ITEM_PERMIT_DATE = string.Empty;
        string _FORM_CODE_NAME = string.Empty; string _MARK_CODE_FRONT_ANAL = string.Empty; string _MARK_CODE_BACK_ANAL = string.Empty; string _MARK_CODE_FRONT_IMG = string.Empty;
        string _MARK_CODE_BACK_IMG = string.Empty; string _userId = string.Empty;
        _userId = "systerm";

        foreach (XmlNode xn in xnList)
        {
            List<string> lstDrugInfo = new List<string>();

            _ITEM_SEQ = string.Empty; _ITEM_NAME = string.Empty; _ENTP_SEQ = string.Empty; _ENTP_NAME = string.Empty; _CHART = string.Empty; _ITEM_IMAGE = string.Empty;
            _PRINT_FRONT = string.Empty; _PRINT_BACK = string.Empty; _DRUG_SHAPE = string.Empty; _COLOR_CLASS1 = string.Empty; _COLOR_CLASS2 = string.Empty;
            _LINE_FRONT = string.Empty; _LINE_BACK = string.Empty; _LENG_LONG = string.Empty; _LENG_SHORT = string.Empty; _THICK = string.Empty;
            _IMG_REGIST_TS = string.Empty; _CLASS_NO = string.Empty; _CLASS_NAME = string.Empty; _ETC_OTC_NAME = string.Empty; _ITEM_PERMIT_DATE = string.Empty;
            _FORM_CODE_NAME = string.Empty; _MARK_CODE_FRONT_ANAL = string.Empty; _MARK_CODE_BACK_ANAL = string.Empty; _MARK_CODE_FRONT_IMG = string.Empty;
            _MARK_CODE_BACK_IMG = string.Empty;


            _ITEM_SEQ = xn["ITEM_SEQ"].InnerText;
            _ITEM_NAME = xn["ITEM_NAME"].InnerText;
            _ENTP_SEQ = xn["ENTP_SEQ"].InnerText;
            _ENTP_NAME = xn["ENTP_NAME"].InnerText;
            _CHART = xn["CHART"].InnerText;
            _ITEM_IMAGE = xn["ITEM_IMAGE"].InnerText;
            _PRINT_FRONT = xn["PRINT_FRONT"].InnerText;
            _PRINT_BACK = xn["PRINT_BACK"].InnerText;
            _DRUG_SHAPE = xn["DRUG_SHAPE"].InnerText;
            _COLOR_CLASS1 = xn["COLOR_CLASS1"].InnerText;
            _COLOR_CLASS2 = xn["COLOR_CLASS2"].InnerText;
            _LINE_FRONT = xn["LINE_FRONT"].InnerText;
            _LINE_BACK = xn["LINE_BACK"].InnerText;
            _LENG_LONG = xn["LENG_LONG"].InnerText;
            _LENG_SHORT = xn["LENG_SHORT"].InnerText;
            _THICK = xn["THICK"].InnerText;
            _IMG_REGIST_TS = xn["IMG_REGIST_TS"].InnerText;
            _CLASS_NO = xn["CLASS_NO"].InnerText;
            _CLASS_NAME = xn["CLASS_NAME"].InnerText;
            _ETC_OTC_NAME = xn["ETC_OTC_NAME"].InnerText;
            _ITEM_PERMIT_DATE = xn["ITEM_PERMIT_DATE"].InnerText;
            _FORM_CODE_NAME = xn["FORM_CODE_NAME"].InnerText;
            _MARK_CODE_FRONT_ANAL = xn["MARK_CODE_FRONT_ANAL"].InnerText;
            _MARK_CODE_BACK_ANAL = xn["MARK_CODE_BACK_ANAL"].InnerText;
            _MARK_CODE_FRONT_IMG = xn["MARK_CODE_FRONT_IMG"].InnerText;
            _MARK_CODE_BACK_IMG = xn["MARK_CODE_BACK_IMG"].InnerText;


            lstDrugInfo.Add(_ITEM_SEQ);//품목일련번호
            lstDrugInfo.Add(_ITEM_NAME);//품목명
            lstDrugInfo.Add(_ENTP_SEQ);//업체일련번호
            lstDrugInfo.Add(_ENTP_NAME);//업체명
            lstDrugInfo.Add(_CHART);	//성상
            lstDrugInfo.Add(_ITEM_IMAGE);//큰제품이미지
            lstDrugInfo.Add(_PRINT_FRONT);//표시(앞)
            lstDrugInfo.Add(_PRINT_BACK);//표시(뒤)
            lstDrugInfo.Add(_DRUG_SHAPE);//의약품모양
            lstDrugInfo.Add(_COLOR_CLASS1);//색깔(앞)
            lstDrugInfo.Add(_COLOR_CLASS2);//색깔(뒤)
            lstDrugInfo.Add(_LINE_FRONT);//분할선(앞)
            lstDrugInfo.Add(_LINE_BACK);//분할선(뒤)
            lstDrugInfo.Add(_LENG_LONG);//크기(장축)
            lstDrugInfo.Add(_LENG_SHORT);//크기(단축)
            lstDrugInfo.Add(_THICK);//크기(두께)
            lstDrugInfo.Add(_IMG_REGIST_TS);//약학정보원 이미지 생성일
            lstDrugInfo.Add(_CLASS_NO);//분류번호
            lstDrugInfo.Add(_CLASS_NAME);//분류명
            lstDrugInfo.Add(_ETC_OTC_NAME);//전문/일반
            lstDrugInfo.Add(_ITEM_PERMIT_DATE);//	품목허가일자
            lstDrugInfo.Add(_FORM_CODE_NAME);//	제형코드이름
            lstDrugInfo.Add(_MARK_CODE_FRONT_ANAL);//	마크내용(앞)
            lstDrugInfo.Add(_MARK_CODE_BACK_ANAL);//	마크내용(뒤)
            lstDrugInfo.Add(_MARK_CODE_FRONT_IMG);//	마크이미지(앞)
            lstDrugInfo.Add(_MARK_CODE_BACK_IMG);	//마크이미지(뒤)
            
            DslstDrugInfo.Add(lstDrugInfo);
            
            _saveResult =  setDrugOrgSave(_ITEM_SEQ, _ITEM_NAME, _ENTP_SEQ, _ENTP_NAME, _CHART, _ITEM_IMAGE,
                _PRINT_FRONT, _PRINT_BACK, _DRUG_SHAPE, _COLOR_CLASS1, _COLOR_CLASS2,
                _LINE_FRONT, _LINE_BACK, _LENG_LONG, _LENG_SHORT, _THICK,
                _IMG_REGIST_TS, _CLASS_NO, _CLASS_NAME, _ETC_OTC_NAME, _ITEM_PERMIT_DATE,
                _FORM_CODE_NAME, _MARK_CODE_FRONT_ANAL, _MARK_CODE_BACK_ANAL, _MARK_CODE_FRONT_IMG, _MARK_CODE_BACK_IMG
                , _userId);
            if (_saveResult == "OK") { }
        }
    }

    private string setDrugOrgSave(string _ITEM_SEQ, string _ITEM_NAME, string _ENTP_SEQ, string _ENTP_NAME, string _CHART, string _ITEM_IMAGE,
        string _PRINT_FRONT, string _PRINT_BACK, string _DRUG_SHAPE, string _COLOR_CLASS1, string _COLOR_CLASS2,
        string _LINE_FRONT, string _LINE_BACK, string _LENG_LONG, string _LENG_SHORT, string _THICK,
        string _IMG_REGIST_TS, string _CLASS_NO, string _CLASS_NAME, string _ETC_OTC_NAME, string _ITEM_PERMIT_DATE,
        string _FORM_CODE_NAME, string _MARK_CODE_FRONT_ANAL, string _MARK_CODE_BACK_ANAL, string _MARK_CODE_FRONT_IMG, string _MARK_CODE_BACK_IMG
        , string _userId)
    {
        string _regip = getUserIP();

        DataSet ds = null;
        string spName = "SPM_Web_COMMON_Tbl_DrugOrg_Info_S";
        SqlParameterCollection param = DataCommon.InitSqlParameterCollection();
        param.Add(new SqlParameter("ITEM_SEQ", _ITEM_SEQ));
        param.Add(new SqlParameter("ITEM_NAME", _ITEM_NAME));						//품목명
        param.Add(new SqlParameter("ENTP_SEQ", _ENTP_SEQ));							//업체일련번호
        param.Add(new SqlParameter("ENTP_NAME", _ENTP_NAME));						//업체명
        param.Add(new SqlParameter("CHART", _CHART));								//성상
        param.Add(new SqlParameter("ITEM_IMAGE", _ITEM_IMAGE));						//큰제품이미지
        param.Add(new SqlParameter("PRINT_FRONT", _PRINT_FRONT));					//표시(앞)
        param.Add(new SqlParameter("PRINT_BACK", _PRINT_BACK));						//표시(뒤)
        param.Add(new SqlParameter("DRUG_SHAPE", _DRUG_SHAPE));						//의약품모양
        param.Add(new SqlParameter("COLOR_CLASS1", _COLOR_CLASS1));					//색깔(앞)
        param.Add(new SqlParameter("COLOR_CLASS2", _COLOR_CLASS2));					//색깔(뒤)
        param.Add(new SqlParameter("LINE_FRONT", _LINE_FRONT));						//분할선(앞)
        param.Add(new SqlParameter("LINE_BACK", _LINE_BACK));						//분할선(뒤)
        param.Add(new SqlParameter("LENG_LONG", _LENG_LONG));						//크기(장축)
        param.Add(new SqlParameter("LENG_SHORT", _LENG_SHORT));						//크기(단축)
        param.Add(new SqlParameter("THICK", _THICK));								//크기(두께)
        param.Add(new SqlParameter("IMG_REGIST_TS", _IMG_REGIST_TS));				//약학정보원 이미지 생성일
        param.Add(new SqlParameter("CLASS_NO", _CLASS_NO));							//분류번호
        param.Add(new SqlParameter("CLASS_NAME", _CLASS_NAME));						//분류명
        param.Add(new SqlParameter("ETC_OTC_NAME", _ETC_OTC_NAME));					//전문/일반
        param.Add(new SqlParameter("ITEM_PERMIT_DATE", _ITEM_PERMIT_DATE));			//품목허가일자
        param.Add(new SqlParameter("FORM_CODE_NAME", _FORM_CODE_NAME));				//제형코드이름
        param.Add(new SqlParameter("MARK_CODE_FRONT_ANAL", _MARK_CODE_FRONT_ANAL));	//마크내용(앞)
        param.Add(new SqlParameter("MARK_CODE_BACK_ANAL", _MARK_CODE_BACK_ANAL));	//마크내용(뒤)
        param.Add(new SqlParameter("MARK_CODE_FRONT_IMG", _MARK_CODE_FRONT_IMG));	//마크이미지(앞)
        param.Add(new SqlParameter("MARK_CODE_BACK_IMG", _MARK_CODE_BACK_IMG));		//마크이미지(뒤)
        param.Add(new SqlParameter("regip", _regip));
        param.Add(new SqlParameter("userId", _userId));

        string srtMsg = "err";
        try
        {
            ds = DataCommon.CommonSpCall(spName, param);
            if (ds.Tables[0].Rows.Count > 0)
            {
                srtMsg = ds.Tables[0].Rows[0]["RESULT"].ToString();
            }
        }
        catch (Exception ex)
        {
            srtMsg = "err";
        }
        return srtMsg;
    }
}
