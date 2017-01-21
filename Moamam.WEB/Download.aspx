<%@ Page Language="C#" %>
<%@ Import Namespace="System.IO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    { 
        string downloadType = Request["type"];
        string savePath = "~/Download";
        
        if (downloadType != null)
        {
            switch (downloadType)
            {
                case "T": savePath = "~/Template"; break;
                case "D": savePath = "~/Download"; break;
            }
        }

        string vPath = Server.UrlDecode(Request["file"]);
        string realPath = Server.MapPath(Path.Combine(savePath, vPath));
        string realExtension = Path.GetExtension(vPath).Replace(".","");
        if (realExtension == "csv" || realExtension == "xls" || realExtension == "xlsx")    // 일단 이거 3가지만 다운로드 허용 2017-01-20
        {
            string strFileName = HttpUtility.UrlEncode(Path.GetFileName(vPath), new UTF8Encoding()).Replace("+", "%20");
            strFileName = strFileName.Replace("..", "").Replace("/", ""); // 경로 변경 관련 제거 2017-01-20
            Response.Clear();
            Response.BufferOutput = false;
            Response.Buffer = false;
            Response.ContentType = "Application/UnKnown";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + strFileName + "\"");
            Response.WriteFile(realPath);
            Response.Flush();
            Response.End();
        }
        else {
            Response.Write(realExtension + "은 적절한 요청이 아닙니다.");
            Response.End();
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Download</title>
    <meta http-equiv="content-type" content="text/html; charset=utf-8"/>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:center">File Downloading...</div>
    </form>
</body>
</html>
