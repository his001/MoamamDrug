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
    public class SafetyStockItem
    {
        #region 주석
        //public DataSet GetSafetyStockItemList(string serctionFrom, string sectionTo, string item, int rowCnt, int pageNum)
        //{
        //    SqlParameter[] Params = new SqlParameter[5];
        //    Params[0] = new SqlParameter("@SECTIONFROM", serctionFrom);
        //    Params[1] = new SqlParameter("@SECTIONTO", sectionTo);
        //    Params[2] = new SqlParameter("@ROWCNT", rowCnt);
        //    Params[3] = new SqlParameter("@PAGENUM", pageNum);
        //    Params[4] = new SqlParameter("@ITEM", item);
        //    return MssqlHelper.GetDataSet("[dbo].[SP_WEB_SAFETYSTCOKITEMLIST]", Params, CommandType.StoredProcedure);
        //}
        //public DataSet GetSafetyStockItemList_Excel(string serctionFrom, string sectionTo, string item, int rowCnt, int pageNum)
        //{
        //    SqlParameter[] Params = new SqlParameter[5];
        //    Params[0] = new SqlParameter("@SECTIONFROM", serctionFrom);
        //    Params[1] = new SqlParameter("@SECTIONTO", sectionTo);
        //    Params[2] = new SqlParameter("@ROWCNT", rowCnt);
        //    Params[3] = new SqlParameter("@PAGENUM", pageNum);
        //    Params[4] = new SqlParameter("@ITEM", item);
        //    return MssqlHelper.GetDataSet("[dbo].[SP_WEB_SAFETYSTCOKITEMLIST_Excel]", Params, CommandType.StoredProcedure);
        //}
        #endregion

        public DataSet GetSafetyStockItemList(string serctionFrom, string sectionTo, string item, int rowCnt, int pageNum, string smod, string suppCode, string rudterm)
        {
            SqlParameter[] Params = new SqlParameter[8];
            Params[0] = new SqlParameter("@SECTIONFROM", serctionFrom);
            Params[1] = new SqlParameter("@SECTIONTO", sectionTo);
            Params[2] = new SqlParameter("@ROWCNT", rowCnt);
            Params[3] = new SqlParameter("@PAGENUM", pageNum);
            Params[4] = new SqlParameter("@SMODE", smod);
            Params[5] = new SqlParameter("@ITEM", item);
            Params[6] = new SqlParameter("@SUPPCODE", suppCode);
            Params[7] = new SqlParameter("@RUDTERM", rudterm);
            return MssqlHelper.GetDataSet("[dbo].[SP_WEB_SAFETYSTCOKITEMLIST_SUPP_R]", Params, CommandType.StoredProcedure);
        }
        public string SetExecludeCrud(SafetyStockInsert proi)
        {
            //string strSql;
            string strMessage = string.Empty;
            SqlParameter[] Params = null;

            if (proi.CMDCRUD == "UPDATE")
            {
                Params = new SqlParameter[9];
                Params[0] = new SqlParameter("@ITEM", proi.ITEM);
                Params[1] = new SqlParameter("@WH", proi.WH);
                Params[2] = new SqlParameter("@SFS_FIXED_VALUE", proi.SFS_FIXED_VALUE);
                Params[3] = new SqlParameter("@SFS_RATE", proi.SFS_RATE);
                Params[4] = new SqlParameter("@SFS_TERM_ID", proi.SFS_TERM_ID);
                Params[5] = new SqlParameter("@SFS_START_DATE", proi.SFS_START_DATE);
                Params[6] = new SqlParameter("@SFS_END_DATE", proi.SFS_END_DATE);
                Params[7] = new SqlParameter("@CMDCRUD", proi.CMDCRUD);
                Params[8] = new SqlParameter("@USERID", proi.UserId);


                DataSet ds = MssqlHelper.GetDataSet("[dbo].[SP_WEB_SAFETYSTCOKSAVE_U]", Params, CommandType.StoredProcedure);
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
