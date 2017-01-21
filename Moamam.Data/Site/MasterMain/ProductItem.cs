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
    public class ProductItem
    {

        #region 주석
        //public DataSet GetProductItemToList(string serctionFrom, string sectionTo, string txtItem, int rowCnt, int pageNum)
        //{
        //    SqlParameter[] Params = new SqlParameter[5];
        //    Params[0] = new SqlParameter("@SECTIONFROM", serctionFrom);
        //    Params[1] = new SqlParameter("@SECTIONTO", sectionTo);
        //    Params[2] = new SqlParameter("@ITEM", txtItem);
        //    Params[3] = new SqlParameter("@ROWCNT", rowCnt);
        //    Params[4] = new SqlParameter("@PAGENUM", pageNum);
        //    return MssqlHelper.GetDataSet("[dbo].[SP_WEB_PRODUCTITEMTOLIST]", Params, CommandType.StoredProcedure);
        //}
        //public DataSet GetProductItem(string serctionFrom, string sectionTo, int rowCnt, int pageNum)
        //{
        //    SqlParameter[] Params = new SqlParameter[4];
        //    Params[0] = new SqlParameter("@SECTIONFROM", serctionFrom);
        //    Params[1] = new SqlParameter("@SECTIONTO", sectionTo);
        //    Params[2] = new SqlParameter("@ROWCNT", rowCnt);
        //    Params[3] = new SqlParameter("@PAGENUM", pageNum);
        //    return MssqlHelper.GetDataSet("[dbo].[SP_WEB_PRODUCTLIST]", Params, CommandType.StoredProcedure);
        //}

        //public DataSet GetProductItemToList_Excel(string serctionFrom, string sectionTo, string txtItem, int rowCnt, int pageNum)
        //{
        //    SqlParameter[] Params = new SqlParameter[5];
        //    Params[0] = new SqlParameter("@SECTIONFROM", serctionFrom);
        //    Params[1] = new SqlParameter("@SECTIONTO", sectionTo);
        //    Params[2] = new SqlParameter("@ITEM", txtItem);
        //    Params[3] = new SqlParameter("@ROWCNT", rowCnt);
        //    Params[4] = new SqlParameter("@PAGENUM", pageNum);
        //    return MssqlHelper.GetDataSet("[dbo].[SP_WEB_PRODUCTITEMTOLIST_Excel]", Params, CommandType.StoredProcedure);
        //}
        //public DataSet GetProductItem_Excel(string serctionFrom, string sectionTo, int rowCnt, int pageNum)
        //{
        //    SqlParameter[] Params = new SqlParameter[4];
        //    Params[0] = new SqlParameter("@SECTIONFROM", serctionFrom);
        //    Params[1] = new SqlParameter("@SECTIONTO", sectionTo);
        //    Params[2] = new SqlParameter("@ROWCNT", rowCnt);
        //    Params[3] = new SqlParameter("@PAGENUM", pageNum);
        //    return MssqlHelper.GetDataSet("[dbo].[SP_WEB_PRODUCTLIST_Excel]", Params, CommandType.StoredProcedure);
        //}
        #endregion


        public DataSet GetProductItemNameToList(string ITEMNAME, int rowCnt, int pageNum, string ddlName)
        {
            SqlParameter[] Params = new SqlParameter[4];
            Params[0] = new SqlParameter("@ITEMNAME", ITEMNAME);
            Params[1] = new SqlParameter("@ROWCNT", rowCnt);
            Params[2] = new SqlParameter("@PAGENUM", pageNum);
            Params[3] = new SqlParameter("@NAME", ddlName);
            return MssqlHelper.GetDataSet("[dbo].[SP_WEB_PRODUCTITEMNAMETOLIST]", Params, CommandType.StoredProcedure);
        }

        public DataSet GetProductItemToList(string serctionFrom, string sectionTo, string Item, int rowCnt, int pageNum, string smod, string suppCode, string rudterm)
        {
            SqlParameter[] Params = new SqlParameter[8];
            Params[0] = new SqlParameter("@SECTIONFROM", serctionFrom);
            Params[1] = new SqlParameter("@SECTIONTO", sectionTo);
            Params[2] = new SqlParameter("@ROWCNT", rowCnt);
            Params[3] = new SqlParameter("@PAGENUM", pageNum);
            Params[4] = new SqlParameter("@SMODE", smod);
            Params[5] = new SqlParameter("@ITEM", Item);
            Params[6] = new SqlParameter("@SUPPCODE", suppCode);
            Params[7] = new SqlParameter("@RUDTERM", rudterm);
            return MssqlHelper.GetDataSet("[dbo].[SP_WEB_PRODUCTITEMTOLIST_SUPP_R]", Params, CommandType.StoredProcedure);
            //return MssqlHelper.GetDataSet("[dbo].[SP_WEB_PRODUCTITEMTOLIST_R]", Params, CommandType.StoredProcedure); 
        }

        public string SetExecludeCrud(ProductInsert proi)
        { 
            string strMessage = string.Empty;
            SqlParameter[] Params = null; 

            if (proi.CMDCRUD == "UPDATE")
            {
                Params = new SqlParameter[7];
                Params[0] = new SqlParameter("@ITEM", proi.ProductCode);
                Params[1] = new SqlParameter("@WH", proi.Wh);
                Params[2] = new SqlParameter("@ORDERGROUP", proi.OrderGroup);
                Params[3] = new SqlParameter("@LAYERSIZE", proi.Layersize);
                Params[4] = new SqlParameter("@PALLETSIZE", proi.Palletsize);
                Params[5] = new SqlParameter("@CMDCRUD", proi.CMDCRUD);
                Params[6] = new SqlParameter("@USERID", proi.UserId);
                DataSet ds = MssqlHelper.GetDataSet("[dbo].[SP_WEB_PRODUCTSAVE_U]", Params, CommandType.StoredProcedure);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        strMessage = ds.Tables[0].Rows[0]["RESULT"].ToString();
                    }
                    else {
                        strMessage = "저장중 에러가 발생되었습니다.";
                    }
                }
                else
                {
                    strMessage = "저장중 에러가 발생되었습니다.";
                }
                //strMessage = MssqlHelper.Execute("[dbo].[SP_WEB_PRODUCTSAVE]", Params, CommandType.StoredProcedure) > 0 ? "OK" : ""; 
            } 
            return strMessage;
        } 
    }
}
