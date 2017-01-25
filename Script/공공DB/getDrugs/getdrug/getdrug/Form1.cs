using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//using System.Web;
using System.Net;
using System.Data;
using System.Data.SqlClient;

using HtmlAgilityPack;
using System.Xml;

using Moamam.Lib;

namespace getdrug
{
    public partial class FrmMain : Form
    {

        #region ###################
        const string _keyDrug = "eVISHo10RZetWdKPlTwtbOWvvDwj7WgmuKM%2BfGxCPTxD%2FRGXMeEnrf5hgizc26pIVBrL04e8mR4DdPDCEyHazw%3D%3D"; //의약품 낱알식별정보(DB) 서비스
        private string _keyDrugUrl = "http://apis.data.go.kr/1470000/MdcinGrnIdntfcInfoService/getMdcinGrnIdntfcInfoList?ServiceKey=" + _keyDrug + "&numOfRows=100&entp_name=&item_name=&pageNo="; //의약품 낱알식별정보(DB) 서비스 
        protected string sHTML = string.Empty;
        #endregion ###################

        public FrmMain()
        {
            InitializeComponent();
        }

        private void btn_Run_Click(object sender, EventArgs e)
        {
            if (lbl_Status.Text == "stop") {
                lbl_Status.Text = "run";
                btn_Run.Text = "정지";
            }

            string _newURLPage = "";
            int _page = 1;
            if (lbl_Status.Text == "run")
            {
                for (int i = 1; i < 172; i++)
                {
                    txt_page.Text = i.ToString();

                    _newURLPage = _keyDrugUrl + i.ToString();

                    SetNaverXmlParseing(_newURLPage);

                    SetScreenLog(System.DateTime.Now.ToString() + " : " + _newURLPage);
                    System.Threading.Thread.Sleep(10 * 1000); // 15초   // 5 * 60 * 1000 5분
                    //if (lbl_Status.Text != "run") { break; }
                }
                lbl_Status.Text = "stop";
                btn_Run.Text = "실행";
            }
        }

        private void SetScreenLog(string str){
            string Cur_str = txt_result.Text;
            //txt_result.AppendText(str + "\r\n");
            txt_result.AppendText("\r\n" + str);
        }

        /// <summary>
        /// 정부 3.0 공공 DB 에서 의약 정보를 받아와 저장한다.
        /// </summary>
        /// <param name="strXml"></param>
        private void SetNaverXmlParseing(String _keyDrugUrl)
        {

            var m_strFilePath = _keyDrugUrl;
            string xmlStr;
            using (var wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                xmlStr = wc.DownloadString(m_strFilePath);
            }
            //var xmlDoc = new XmlDocument();

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

                _saveResult = setDrugOrgSave(_ITEM_SEQ, _ITEM_NAME, _ENTP_SEQ, _ENTP_NAME, _CHART, _ITEM_IMAGE,
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
            string _regip = CommonNet.getUserIP();

            DataSet ds = null;
            string spName = "SPM_Web_COMMON_Tbl_DrugOrg_Info_S";
            SqlParameterCollection param = DbConStr.InitSqlParameterCollection();
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
                ds = DbConStr.CommonSpCall(spName, param);
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
}
