using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using Moamam.Lib;
using Moamam.Data.Site.Transfer;
/*
 업로드 사용자컨트룰이 추가되어 공유해 드립니다.
사용방법은 업로드할 페이지에 사용자 컨트룰을 추가하시고,
OnSelEvent 를 정의하시고 CS 단에서 사용하시면 됩니다.
## ASPX
<%@ Register Src="~/UserControls/ucUploadCsv.ascx" TagName="ucUploadCsv" TagPrefix="uc" %>
<uc:ucUploadCsv ID="ucUpload" runat="server" OnSelEvent="ucUpload_SelEvent" DownloadForm="SectionUsable양식.csv" />
여기서 DownloadForm 값은 /Download 폴더에 있는 양식파일이름 입니다. 
## CS
protected void ucUpload_SelEvent(object sender, EventArgs e)
{
    DataTable dt = ucUpload.DatatableCvs;
}
 */

public partial class UserControls_ucUploadCsv : System.Web.UI.UserControl
{
    #region Fields & Properties **********************************************************************************************


    public event EventHandler SelEvent;
    public AjaxControlToolkit.ModalPopupExtender modal;
    public DataTable dataTableCvs;

    StreamReader _sr = null;

    public string DownloadForm
    {
        get { return hdnDownloadForm.Value; }
        set { hdnDownloadForm.Value = value; }
    }

    public DataTable DatatableCvs
    {
        get { return dataTableCvs; }
        set { dataTableCvs = value; }
    }


    #endregion Field & Properties
    #region Page, PostBack Events **********************************************************************************************



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dataTableCvs = null;
        }

        modal = ModalPopExtUpload;
    }


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        Upload();
        SelEvent(this.SelEvent, null);
    }


    #endregion Page Events
    #region Data Routine **********************************************************************************************


    private bool Upload()
    {
        bool result = false;
        DataTable dt = null;
        

        if (fileCvs.HasFile)
        {
            string uploadFolders = MyWebConfig.uploadFolder;    //업로드 파일경로
            string[] allowedExtensions = { ".csv" };            //파일 확장자

            string filePath = FileUploader.FileUpload(fileCvs, uploadFolders, "", "_" + Session.SessionID, allowedExtensions, true); //file upload
            _sr = new StreamReader(filePath, Encoding.Default);

            dt = new DataTable();

            try
            {
                string[] chunkData = GetNextChunk(); //Data Chunk

                foreach (string title in chunkData[0].Split(','))
                    dt.Columns.Add(title, typeof(string));

                if (chunkData != null)
                {
                    dt.Rows.Clear();

                    int rownum = 0;
                    foreach (string csvRow in chunkData)
                    {
                        if (rownum >= 1)
                        {
                            DataRow row = dt.NewRow();
                            string[] itemArray = csvRow.Split(',');

                            for (int i = 0; i < itemArray.Length; i++)
                                row[i] = itemArray[i];

                            dt.Rows.Add(row);
                        }
                        rownum++;
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            _sr.Close();
            _sr.Dispose();
        }

        DatatableCvs = dt;
        return result;
    }

    public string[] GetNextChunk()
    {
        int _chunkVolume = 700;
        List<string> buffer = new List<string>();

        while (true)
        {
            if (!_sr.EndOfStream && _sr.Peek() > 0)
                buffer.Add(_sr.ReadLine());
            else
                break;

            if (buffer.Count >= _chunkVolume)
                break;
        }

        if (buffer.Count > 0)
            return (string[])buffer.ToArray();
        else
            return null;
    }


    #endregion Data Routine


}