using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Moamam.Lib;


namespace Moamam.Data.Site.MasterMain
{
    public class SafetyStockItemUpload
    {
        public DataSet GetExecludeStage(string userid)
        {

            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@USERID", userid);
            return MssqlHelper.GetDataSet("[dbo].[SP_WEB_Site_PopupPage_SafetyStockUpload_List_R]", Params, CommandType.StoredProcedure);
        }


        // --> Bulk Copy 로 구현
        public string SetExecludeBulkUpload(DataTable dt, string userid, string tablename)
        {

            string strMsg;
            int intVal = 0;
            try
            {
                //테이블 데이타 삭제
                SqlParameter[] Params = new SqlParameter[2];
                Params[0] = new SqlParameter("@USERID", userid);
                Params[1] = new SqlParameter("@chk", "SAFETYSTOCK");

                DataSet ds = MssqlHelper.GetDataSet("[dbo].[SP_WEB_Site_PopupPage_Bulkupload_Data_D]", Params, CommandType.StoredProcedure);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        strMsg = ds.Tables[0].Rows[0]["RESULT"].ToString();
                    }
                    else
                    {
                        strMsg = "";
                    }
                }
                else
                {
                    strMsg = "";
                }

                if (strMsg == "OK")
                {
                    //Bulk Copy
                    intVal = MssqlHelper.SqlBulkCopy(string.Empty, tablename, dt);

                    //VALIDATION
                    Params = new SqlParameter[1];
                    Params[0] = new SqlParameter("@USERID", userid);

                    ds = MssqlHelper.GetDataSet("[dbo].[SP_WEB_Site_PopupPage_SafetyStockUpload_Validate_S]", Params, CommandType.StoredProcedure);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strMsg = ds.Tables[0].Rows[0]["RESULT"].ToString();
                        }
                        else
                        {
                            strMsg = "업로드 후 유효성 검사중 에러가 발생되었습니다.";
                        }
                    }
                    else
                    {
                        strMsg = "업로드 후 유효성 검사중 에러가 발생되었습니다.";
                    }
                    //strMsg = "OK";
                }
                else
                {
                    strMsg = "업로드 테이블 삭제중 Rollback 되었습니다.";
                }
            }
            catch (Exception e)
            {
                strMsg = e.Message.ToString();
            }

            return strMsg;

        }
        public string SetExecludeConfirm(string userid)
        {
            string strMsg = "";
            //최종 저장 및 ACTION_TYPE = 'D' 제거
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@USERID", userid);

            DataSet ds = MssqlHelper.GetDataSet("[dbo].[SP_WEB_Site_PopupPage_SafetyStockUpload_Confirm_S]", Params, CommandType.StoredProcedure);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    strMsg = ds.Tables[0].Rows[0]["RESULT"].ToString();
                }
                else
                {
                    strMsg = "최종 저장중 에러가 발생되었습니다.";
                }
            }
            else
            {
                strMsg = "최종 저장중 에러가 발생되었습니다.";
            }
            return strMsg;
        }
    }
}

