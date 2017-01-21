using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Moamam.Lib;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
 
using Moamam.Data.Site.MasterMain;
public partial class Site_PopupPage_ProductItemUpload : BasePage
{
    static readonly int _chunkVolume = 700;
    StreamReader _sr = null;

    #region Page Events **********************************************************************************************
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initExcelDown();
            InitVariables();
        }
    }


    /// <summary>
    /// POSTBACK이 발생되거나 처음 로딩시 엑셀다운로드 가능하게 함
    /// </summary>
    private void initExcelDown()
    {
        //업데이트패널에서 다운로드
        //ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnExcel);
    }

    /// <summary>
    /// 페이지 유효성 검사를 수행합니다.
    /// 페이지 시작시 가장 처음에 위치합니다.
    /// </summary>
    private void InitVariables()
    { 

        //페이지에 대한 권한을 검사
        //Response.Write(Session["menuCd"].ToString() + "/");
        if (!SecurityChecked(GetParams(), SecurityInfo)) this.RedirectToSecurityError(); 

        //커서모양 셋팅 
        UserFunction.MakeStyle(btnUpload, "cursor");
        UserFunction.MakeStyle(btnSave, "cursor");

        ////버튼 권한에 따른 처리 - Client 스크립트가 있어서 버튼 자체 안보이게 해야 합니다.
        //UserFunction.MakeButtonToEnabledOrVisible(btnUpload, "Visible");//정합성체크 시작
        //UserFunction.MakeButtonToEnabledOrVisible(btnSave, "Visible");// 데이터 반영 

    }

    /// <summary>
    /// NButton 권한 설정 파라미터
    /// </summary>
    private Control[] GetParams()
    {
        return new Control[]
            { 
                btnUpload,
                btnSave
            };
    }
     
    #endregion Page Events


    #region LoadData
    private void LoadData()
    {
        try
        {
            DataTable dt = new DataTable();

            rptUpload.DataSource = dt;
            rptUpload.DataBind();
            DataSet ds = null; 


            ds = (new ProductItemUpload()).GetExecludeStage(SessionAuth.GetUserID().ToString());

            if (ds != null)
            {
                rptUpload.DataSource = ds.Tables[0];
                rptUpload.DataBind();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblCount.Text = string.Format("[ 정상 {0}건 / 에러 {1}건]", ds.Tables[0].Rows[0]["OK_CNT"].ToString(), ds.Tables[0].Rows[0]["NG_CNT"].ToString());
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["NG_CNT"].ToString()) < 1)
                    {
                        hdnDataExists.Value = "에러난 항목이 없습니다.";
                    }
                    else
                    {
                        hdnDataExists.Value = "";
                    }
                }
                else
                {
                    lblCount.Text = "";
                    hdnDataExists.Value = "에러난 항목이 없습니다.";
                }
            }
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }

    }
    #endregion

    #region upload
    private bool Upload()
    {
        bool result = false;
        string filePath = "";
        if (FileUpload1.HasFile)
        {
            //Upload 파일 경로
            string uploadFolders = MyWebConfig.uploadFolder;
            //allowedExtensions 파일 확장자
            string[] allowedExtensions = { ".csv" };

            filePath = FileUploader.FileUpload(FileUpload1, uploadFolders, "", "_" + Session.SessionID, allowedExtensions, true); //file upload
            #region ############ DRM 체크 로직 추가 2017-01-02 S ############
            //try
            //{
            //    if (fasoo.GetFileType(filePath) == "NeedDec")
            //    {
            //        string DecDerName = Path.GetDirectoryName(filePath);
            //        string DecFineName = filePath.Replace(DecDerName, "").Replace("\\", "");
            //        if (fasoo.SetDecFasoo(DecDerName, DecFineName) == "성공")
            //        {
            //            //filePath = fasoo.DecFolders + "\\" + FileUpload1.FileName;
            //        }
            //        else
            //        {
            //            result = false;
            //            base.ShowMessage("암호화 모듈 해제중 문제가 발생 하였습니다.");
            //            return result;
            //        }
            //    }
            //}
            //catch (Exception ex) { }
            #endregion ############ DRM 체크 로직 추가 2017-01-02 E ############
            _sr = new StreamReader(filePath, Encoding.Default);

            DataTable dt = new DataTable();
            //Header 설정 ITEM,WH,ORDER_GROUP,ADO_START_DATE,PALLET_SIZE,LAYER_ SIZE,ACTION_TYPE
            dt.Columns.Add("ITEM", typeof(string));
            dt.Columns.Add("WH", typeof(int));
            dt.Columns.Add("ORDER_GROUP", typeof(string));
            dt.Columns.Add("ADO_START_DATE", typeof(string));
            dt.Columns.Add("PALLET_SIZE", typeof(double));//20161219 int에서 double 변경
            dt.Columns.Add("LAYER_SIZE", typeof(double));//20161219 int에서 double 변경ㄴ
            dt.Columns.Add("ACTION_TYPE", typeof(string));
            dt.Columns.Add("ERROR_MSG", typeof(string));
            dt.Columns.Add("CREATE_USER", typeof(string));
            dt.Columns.Add("CREATE_DATE", typeof(string));

            try
            { 
                List<ProductExecludItemData> dataPac = new List<ProductExecludItemData>();

                int rownum = 0;
                dt.Rows.Clear();
                while (true)
                {
                    //Data Chunk
                    string[] chunkData = GetNextChunk();

                    if (chunkData == null)
                        break; 

                    foreach (string csvRow in chunkData)
                    {
                        if (rownum >= 1)
                        {
                            DataRow row = dt.NewRow();
                            string[] itemArray = csvRow.Split(',');

                            for (int i = 0; i < itemArray.Length + 3; i++)
                            {
                                if (i == 0) row["ITEM"]         = UserFunction.MakeItemLandgth(itemArray[0].ToString().Trim());
                                if (i == 1) row["WH"]           = UserFunction.IsNullOrEmpty(itemArray[1].ToString().Trim(), "0");//0값이 기본값
                                if (i == 2) row["ORDER_GROUP"]  = UserFunction.IsNullOrEmpty(itemArray[2].ToString().Trim(), "0");//0값이 기본값
                                if (i == 3) row["ADO_START_DATE"] = UserFunction.IsNullOrEmpty(itemArray[3].ToString().Trim(), DateTime.Today.ToString("yyyyMMdd"));
                                if (i == 4) row["PALLET_SIZE"]  = UserFunction.IsNullOrEmpty(itemArray[4].ToString().Trim(), "0");//0값이 기본값
                                if (i == 5) row["LAYER_SIZE"]   = UserFunction.IsNullOrEmpty(itemArray[5].ToString().Trim(), "0");//0값이 기본값
                                if (i == 6) row["ACTION_TYPE"]  = UserFunction.IsNullOrEmpty(itemArray[6].ToString().Trim(), null);//NULL값이 기본값
                                if (i == 7) row["ERROR_MSG"]    = null;
                                if (i == 8) row["CREATE_USER"]  = SessionAuth.GetUserID();
                                if (i == 9) row["CREATE_DATE"]  = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
                            }
                            // chunk 단위로 DataSet에 Data Add
                            dt.Rows.Add(row);
                        }
                        rownum++;
                    } 
                }
                string d = rownum.ToString();                
                string strMsg = (new ProductItemUpload()).SetExecludeBulkUpload(dt, SessionAuth.GetUserID(),"ADO_UNIT_SIZE_UPLOAD");
                if (strMsg == "OK")
                {
                    result = true;
                }
                else
                {
                    result = false;
                    base.ShowMessage("파일업로드중 에러가 발생되었습니다.\\n\\n확인방법\\n1. 상품마스터 파일인지 검토후 다시 올리시길 바랍니다.\\n2. 해당파일의 내용이 맞게 되었는지 확인바랍니다.\\n3.파일확인후에도 안될 시 관리자에게 문의 바랍니다..\\n\\n(메시지 : " + strMsg + ")");
                }

            }
            catch (Exception ex)
            {
                result = false;
                //base.ShowMessage(ex.Message);
                base.ShowMessage("파일업로드중 에러가 발생되었습니다.\\n\\n확인방법\\n1. 상품마스터 파일인지 검토후 다시 올리시길 바랍니다.\\n2. 해당파일의 내용이 맞게 되었는지 확인바랍니다.\\n3.파일확인후에도 안될 시 관리자에게 문의 바랍니다.\\n\\n(메시지 : " + ex.Message + ")");
            }
            finally
            {
                dt.Clear();
            }
        }
        _sr.Close();
        _sr.Dispose();
        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        catch { }

        return result;
    }

    public string[] GetNextChunk()
    {
        List<string> buffer = new List<string>();
        int i = 0;
        while (true)
        {
            if (!_sr.EndOfStream && _sr.Peek() > 0)
            {
                string str = _sr.ReadLine().Trim();
                buffer.Add(str);
                i++;
            }
            else
                break;

            if (buffer.Count >= _chunkVolume)
                break;
        }
        string d = i.ToString();
        if (buffer.Count > 0)
            return (string[])buffer.ToArray();
        else
            return null;
    }
    #endregion

    #region SaveData
    private void SaveData()
    {
        try
        {
            string strMsg = (new ProductItemUpload()).SetExecludeConfirm(SessionAuth.GetUserID());
            if (strMsg == "OK")
            {
                //base.ShowMessage("반영되었습니다"); 
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ajaxMessageScript", "try{parent.jsfn_Search();}catch(e){};window.close();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ajaxMessageScript", "alert('반영되었습니다');parent.jsfn_Search();parent.jsfn_UploadPopExit();", true);
            }
            else
            {
                base.ShowMessage(strMsg);
            }
        }
        catch (Exception ex)
        {
            base.ShowMessage(ex.Message);
        }
    }

    //Error Message Check
    private bool SaveDataVaildation()
    {
        //오류 항목 체크
        bool IResult = true;

        foreach (RepeaterItem ri in rptUpload.Items)
        {
            HtmlInputHidden hidStatus = ri.FindControl("hidStatus") as HtmlInputHidden;

            // Status = ('E', 오류) ('S', 성공)
            if (hidStatus.Value == "E")
            {
                IResult = false;
                break;
            }
        }

        return IResult;
    }
    #endregion

    #region Event
    /// <summary>
    /// 정합성 체크 버튼
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (Upload()) //True 이면 Validation Check
        {
            base.ShowMessage("Upload 하였습니다.");
            LoadData();
        }
        else //false
        {
            base.ShowMessage("Upload 실패하였습니다.");
        }
    }

    //Save Button
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveData();
    }

    protected void rptUpload_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            Label lblErrorMessage = e.Item.FindControl("lblerrorMessage") as Label;
            HtmlTableRow hidTr = e.Item.FindControl("hidTr") as HtmlTableRow;

            //오류 항목 Font색 변경
            if (row["STATUS"].ToString() == "E")
            {
                lblErrorMessage.ForeColor = System.Drawing.Color.OrangeRed;
                hidTr.Visible = true;
            }
            else
            {
                hidTr.Visible = false;
            }
        }
    }
    #endregion


    #region "[UserDefined Funcitions]==============================================================================" 
    /// <summary>
    /// MakeHtmlInfo
    /// </summary> 
    protected string MakeHtmlInfo(string SendValue, string chk)
    {
        string tmp = "&nbsp;";
        tmp = UserFunction.MakeHtmlInfoBase(SendValue, chk);
        return tmp;
    }
    #endregion
}
