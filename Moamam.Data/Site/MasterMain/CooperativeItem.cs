using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using Moamam.Lib;

namespace Moamam.Data.Site.MasterMain
{
    public class CooperativeItem
    {
        #region 주석
        //public DataSet GetCooperativeItemList(string dccode, string ordergroup, int rowCnt, int pageNum)
        //{
        //    SqlParameter[] Params = new SqlParameter[4];
        //    Params[0] = new SqlParameter("@DCCODE", dccode);
        //    Params[1] = new SqlParameter("@ORDERGROUP", ordergroup);
        //    Params[2] = new SqlParameter("@ROWCNT", rowCnt);
        //    Params[3] = new SqlParameter("@PAGENUM", pageNum);
        //    return MssqlHelper.GetDataSet("[dbo].[SP_WEB_COOPERATIVELIST]", Params, CommandType.StoredProcedure);
        //}
        //public DataSet GetCooperativeItemListToSupp(string dccode, string ordergroup,string supp, int rowCnt, int pageNum)
        //{
        //    SqlParameter[] Params = new SqlParameter[5];
        //    Params[0] = new SqlParameter("@DCCODE", dccode);
        //    Params[1] = new SqlParameter("@ORDERGROUP", ordergroup);
        //    Params[2] = new SqlParameter("@ROWCNT", rowCnt);
        //    Params[3] = new SqlParameter("@PAGENUM", pageNum);
        //    Params[4] = new SqlParameter("@SUPP", supp);
        //    return MssqlHelper.GetDataSet("[dbo].[SP_WEB_COOPERATIVELIST]", Params, CommandType.StoredProcedure);
        //}
        //public DataSet GetCooperativeItemList_Excel(string dccode, string ordergroup, int rowCnt, int pageNum)
        //{
        //    SqlParameter[] Params = new SqlParameter[4];
        //    Params[0] = new SqlParameter("@DCCODE", dccode);
        //    Params[1] = new SqlParameter("@ORDERGROUP", ordergroup);
        //    Params[2] = new SqlParameter("@ROWCNT", rowCnt);
        //    Params[3] = new SqlParameter("@PAGENUM", pageNum);
        //    return MssqlHelper.GetDataSet("[dbo].[SP_WEB_COOPERATIVELIST_Excel]", Params, CommandType.StoredProcedure);
        //}

        //public DataSet GetCooperativeItemList_ExcelToSupp(string dccode, string ordergroup, string supp, int rowCnt, int pageNum)
        //{
        //    SqlParameter[] Params = new SqlParameter[5];
        //    Params[0] = new SqlParameter("@DCCODE", dccode);
        //    Params[1] = new SqlParameter("@ORDERGROUP", ordergroup);
        //    Params[2] = new SqlParameter("@ROWCNT", rowCnt);
        //    Params[3] = new SqlParameter("@PAGENUM", pageNum);
        //    Params[4] = new SqlParameter("@SUPP", supp);
        //    return MssqlHelper.GetDataSet("[dbo].[SP_WEB_COOPERATIVELIST_Excel]", Params, CommandType.StoredProcedure);
        //}
        #endregion

        public DataSet GetCooperativeItemNameToList(string ITEMNAME, int rowCnt, int pageNum, string ddlName)
        {
            SqlParameter[] Params = new SqlParameter[4];
            Params[0] = new SqlParameter("@ITEMNAME", ITEMNAME);
            Params[1] = new SqlParameter("@ROWCNT", rowCnt);
            Params[2] = new SqlParameter("@PAGENUM", pageNum);
            Params[3] = new SqlParameter("@NAME", ddlName);
            return MssqlHelper.GetDataSet("[dbo].[SP_WEB_SUPPLIERNAMETOLIST]", Params, CommandType.StoredProcedure);
        }

        public DataSet GetCooperativeItemListToSupp(string dccode, string ordergroup, string supp, int rowCnt, int pageNum, string smode, string suppChecked)
        {
            SqlParameter[] Params = new SqlParameter[7];
            Params[0] = new SqlParameter("@DCCODE", dccode);
            Params[1] = new SqlParameter("@ORDERGROUP", ordergroup);
            Params[2] = new SqlParameter("@ROWCNT", rowCnt);
            Params[3] = new SqlParameter("@PAGENUM", pageNum);
            Params[4] = new SqlParameter("@SMODE", smode);
            Params[5] = new SqlParameter("@SUPP", supp);
            Params[6] = new SqlParameter("@SUPPCHECKED", suppChecked);
            return MssqlHelper.GetDataSet("[dbo].[SP_WEB_COOPERATIVELIST_SUPP_R]", Params, CommandType.StoredProcedure);
        }

        public string SetExecludeCrud(CooperativeInsert proi)
        {
            //string strSql;
            string strMessage = string.Empty;
            SqlParameter[] Params = null;

            if (proi.CMDCRUD == "UPDATE")
            {
                Params = new SqlParameter[15];
                Params[0] = new SqlParameter("@SUPPLIER", proi.SUPPLIER);
                Params[1] = new SqlParameter("@WH", proi.WH);
                Params[2] = new SqlParameter("@SUP_START_DATE", proi.SUP_START_DATE);
                Params[3] = new SqlParameter("@SUP_END_DATE", proi.SUP_END_DATE);
                Params[4] = new SqlParameter("@ORDER_GROUP", proi.ORDER_GROUP);
                Params[5] = new SqlParameter("@SUP_TERM_ID", proi.SUP_TERM_ID);
                Params[6] = new SqlParameter("@W_MON", proi.W_MON);
                Params[7] = new SqlParameter("@W_TUE", proi.W_TUE);
                Params[8] = new SqlParameter("@W_WED", proi.W_WED);
                Params[9] = new SqlParameter("@W_THU", proi.W_THU);
                Params[10] = new SqlParameter("@W_FRI", proi.W_FRI);
                Params[11] = new SqlParameter("@W_SAT", proi.W_SAT);
                Params[12] = new SqlParameter("@W_SUN", proi.W_SUN);
                Params[13] = new SqlParameter("@CMDCRUD", proi.CMDCRUD);
                Params[14] = new SqlParameter("@USERID", proi.UserId);


                DataSet ds = MssqlHelper.GetDataSet("[dbo].[SP_WEB_COOPERATIVESAVE_U]", Params, CommandType.StoredProcedure);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        strMessage = ds.Tables[0].Rows[0]["RESULT"].ToString();
                    }
                    else
                    {
                        strMessage = "저장중 에러가 발생되었습니다.";
                    }
                }
                else
                {
                    strMessage = "저장중 에러가 발생되었습니다.";
                } 
            } 
            return strMessage;
        }
    }
}
