using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

using Moamam.Lib;

namespace Moamam.Data.Common
{

    public partial class SiteMenu
    {
        public DataSet GetMenuGroup(string groupCdoe)
        {
            string strSql = string.Empty;

            if(groupCdoe == "1")
            {
                strSql = @"SELECT MG.GROUP_CD MENU_GROUP_CD, MG.GROUP_NM MENU_GROUP_NM FROM MENU_GROUP MG WITH(NOLOCK) WHERE MG.USE_YN = 'Y' ORDER BY MG.SEQ";
            }
            else
            {
                strSql = @" SELECT MG.GROUP_CD MENU_GROUP_CD, MG.GROUP_NM MENU_GROUP_NM FROM MENU_GROUP MG WITH(NOLOCK) WHERE MG.USER_AUTH_CD >= " + groupCdoe + @" AND MG.USE_YN = 'Y' ORDER BY MG.SEQ";
            }

            return MssqlHelper.GetDataSet(strSql, CommandType.Text);
        }


        public DataSet GetSubMenu(string groupCode)
        {
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@groupCode", groupCode);
            return MssqlHelper.GetDataSet("[dbo].[SP_WEB_DAO_Common_SiteMenu1_R]", Params, CommandType.StoredProcedure);
        }
                
        public DataSet GetUrlMenu(string menuUrl)
        {

            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@menuUrl", menuUrl);
            return MssqlHelper.GetDataSet("[dbo].[SP_WEB_DAO_Common_SiteMenu2_R]", Params, CommandType.StoredProcedure);
        }


        public DataSet GetPraMenu(string groupCd, string menuCd)
        {
            SqlParameter[] Params = new SqlParameter[2];
            Params[0] = new SqlParameter("@groupcd", groupCd);
            Params[1] = new SqlParameter("@menucd", menuCd);
            return MssqlHelper.GetDataSet("[dbo].[SP_WEB_DAO_Common_SiteMenu3_R]", Params, CommandType.StoredProcedure);
        }

    }
}
