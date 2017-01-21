using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

using Moamam.Lib;

namespace Moamam.Data.Site.Management
{
    public class MenuList
    {
        public DataSet GetMenuGroupList(int rowCnt, int pageNum)
        {
            SqlParameter[] Params = new SqlParameter[2];
            Params[0] = new SqlParameter("@rowCnt", rowCnt);
            Params[1] = new SqlParameter("@pageNum", pageNum);
            return MssqlHelper.GetDataSet("[dbo].[SP_WEB_Management_MenuList1_R]", Params, CommandType.StoredProcedure);
        }


        public string SetMenuGroupUpDown(string groupCd, string strUpDown, string updateUser)
        {
            string strMessage = string.Empty;
            SqlParameter[] Params = new SqlParameter[3];
            Params[0] = new SqlParameter("@group_cd", groupCd);
            Params[1] = new SqlParameter("@UpDown", strUpDown);
            Params[2] = new SqlParameter("@userId", updateUser);

            DataSet ds = MssqlHelper.GetDataSet("[dbo].[SP_WEB_Management_MenuOrderList1_U]", Params, CommandType.StoredProcedure);
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
            return strMessage;
        }


        public string SetPageGroupUpDown(string groupCd, string strMenuCd, string strUpDown, string updateUser)
        {
            string strMessage = string.Empty;
            SqlParameter[] Params = new SqlParameter[4];
            Params[0] = new SqlParameter("@group_cd", groupCd);
            Params[1] = new SqlParameter("@menu_cd", strMenuCd);
            Params[2] = new SqlParameter("@UpDown", strUpDown);
            Params[3] = new SqlParameter("@userId", updateUser);

            DataSet ds = MssqlHelper.GetDataSet("[dbo].[SP_WEB_Management_MenuOrderList2_U]", Params, CommandType.StoredProcedure);
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
            return strMessage;
        }


        public DataSet GetMenuList(string goupCd, int rowCnt, int pageNum)
        {
            SqlParameter[] Params = new SqlParameter[3];
            Params[0] = new SqlParameter("@goupCd", goupCd);
            Params[1] = new SqlParameter("@rowCnt", rowCnt);
            Params[2] = new SqlParameter("@pageNum", pageNum);
            return MssqlHelper.GetDataSet("[dbo].[SP_WEB_Management_MenuOrderList2_R]", Params, CommandType.StoredProcedure);
        }

        public DataSet GetddlUseParentMenu(string _groupCd)
        {

            string strSql = @"
	SELECT '사용안함' AS MENU_NM,'' AS MENU_CD, NULL AS SEQ

	UNION ALL

	SELECT MENU_NM, CONVERT(VARCHAR, MENU_CD) AS MENU_CD, SEQ
	FROM MENU TM WITH(NOLOCK)
	WHERE PARENT_CD IS NULL
	AND TM.GROUP_CD = CASE ISNULL('{0}', '') WHEN '' THEN TM.GROUP_CD ELSE '{0}' END
	ORDER BY SEQ
";

            return MssqlHelper.GetDataSet(string.Format(strSql, _groupCd), CommandType.Text);

        }


        public string SetMenuGroupUpdate(string groupCd, string groupNm, string userAuthCd, string useYn, string updateUser)
        {
            string strSql = @"
UPDATE MENU_GROUP 
SET    GROUP_NM         = '{0}'
,      USER_AUTH_CD     = {1}
,      USE_YN           = '{2}'
,      UPDATE_USER      = '{3}'
,      UPDATE_DATE      = GETDATE()
WHERE  GROUP_CD         = {4}";

            strSql = string.Format(strSql, groupNm, userAuthCd, useYn, updateUser, groupCd);
            return MssqlHelper.Execute(strSql, CommandType.Text) > 0 ? "OK" : "";
        }


        public string SetMenuUpdate(string groupCd, string menuCd, string menuNm, string menuUrl, string useYn, string createUser, string parentCd)
        {
            string strSql = @"
UPDATE MENU
SET    MENU_NM      = '{0}'
,      MENU_URL     = '{1}'
,      USE_YN       = '{2}'
,      UPDATE_USER  = '{3}'
,      PARENT_CD    = case when '{6}' ='' then null else '{6}' end 
,      UPDATE_DATE  = GETDATE()
WHERE  GROUP_CD     = {4}
AND    MENU_CD      = {5}";



            strSql = string.Format(strSql, menuNm, menuUrl, useYn, createUser, groupCd, menuCd, parentCd);
            return MssqlHelper.Execute(strSql, CommandType.Text) > 0 ? "OK" : "";
        }


        public string SetMenuGroupInsert(string groupNm, string userAuthCd, string useYn, string createUser)
        {
            string strSql;
            string strMessage;

            if (GetMenuGroupItemExist(groupNm) > 0)
            {
                strMessage = "이미 등록된 메뉴입니다.";
            }
            else
            {
                strSql = @"
INSERT INTO MENU_GROUP
(
    GROUP_CD
,   GROUP_NM
,   USER_AUTH_CD
,   SEQ
,   USE_YN
,   CREATE_USER
,   CREATE_DATE
)
VALUES
(
     {0}
,   '{1}'
,    {2}
,   (SELECT ISNULL(MAX(SEQ)+1 ,1) FROM MENU_GROUP WITH(NOLOCK) )
,   '{3}'
,   '{4}'
,   GETDATE()
)";
                strSql = string.Format(strSql, GetMenuGroupMaxCd().ToString(), groupNm, userAuthCd, useYn, createUser);
                strMessage = MssqlHelper.Execute(strSql, CommandType.Text) > 0 ? "OK" : "";
            }

            return strMessage;
        }

        public string SetMenuInsert(string groupCd, string menuNm, string menuUrl, string useYn, string createUser, string parentCd)
        {
            string strSql;
            string strMessage;

            if (string.IsNullOrEmpty(parentCd)) parentCd = "NULL";

            if (GetMenuItemExist(groupCd, menuNm, menuUrl) > 0)
            {
                strMessage = "이미 등록된 메뉴입니다.";
            }
            else
            {
                strSql = @"
INSERT INTO MENU
(
    GROUP_CD
,   MENU_CD
,   PARENT_CD
,   MENU_NM
,   MENU_URL
,   SEQ
,   USE_YN
,   CREATE_USER
,   CREATE_DATE    
)
VALUES
(
     {0}
,    {1}
,   {6}
,   '{2}'
,   '{3}'
, (SELECT ISNULL(MAX(SEQ)+1,1) FROM MENU WITH(NOLOCK) WHERE GROUP_CD={0})
,   '{4}'
,   '{5}'
,   GETDATE()
)";

                strSql = string.Format(strSql, groupCd, GetMenuMaxCd().ToString(), menuNm, menuUrl, useYn, createUser, parentCd);
                strMessage = MssqlHelper.Execute(strSql, CommandType.Text) > 0 ? "OK" : "";
            }

            return strMessage;
        }

        public string SetMenuGroupDelete(string groupCd)
        {
            int intVal = 0;

            using (TransactionScope tran = new TransactionScope())
            {
                intVal = MssqlHelper.Execute("DELETE FROM MENU_GROUP WHERE GROUP_CD = " + groupCd, CommandType.Text);
                MssqlHelper.Execute("DELETE FROM MENU WHERE GROUP_CD = " + groupCd, CommandType.Text);
                MssqlHelper.Execute("DELETE FROM MENU_GRANT WHERE GROUP_CD=" + groupCd, CommandType.Text);
                if (intVal > 0) tran.Complete();
            }

            return intVal > 0 ? "OK" : "";
        }

        public string SetMenuDelete(string groupCd, string menuCd)
        {
            string strSql = "DELETE FROM MENU WHERE GROUP_CD = {0} AND MENU_CD = {1};DELETE FROM menu_grant WHERE GROUP_CD = {0} AND MENU_CD = {1};";
            return MssqlHelper.Execute(string.Format(strSql, groupCd, menuCd), CommandType.Text) > 0 ? "OK" : "";
        }

        public int GetMenuGroupItemExist(string groupNm)
        {
            string strSql = "select count(*) from menu_group where group_nm = '" + groupNm + "'";
            return Convert.ToInt32(MssqlHelper.GetDataScalar(strSql, CommandType.Text));
        }

        public int GetMenuGroupMaxCd()
        {
            string strSql = "select isnull(max(group_cd),0)+1 from menu_group";
            return Convert.ToInt32(MssqlHelper.GetDataScalar(strSql, CommandType.Text));
        }

        public int GetMenuItemExist(string groupCd, string menuNm, string menuUrl)
        {
            string strSql = "select count(*) from menu where group_cd={0} and menu_nm='{1}' and menu_url='{2}'";
            return Convert.ToInt32(MssqlHelper.GetDataScalar(string.Format(strSql, groupCd, menuNm, menuUrl), CommandType.Text));
        }

        public int GetMenuMaxCd()
        {
            string strSql = "select isnull(max(menu_cd),0)+1 from menu";
            return Convert.ToInt32(MssqlHelper.GetDataScalar(strSql, CommandType.Text));
        }



        public DataSet GetMenuList3(string goupCd, string menuCd, int rowCnt, int pageNum)
        {
            SqlParameter[] Params = new SqlParameter[4];
            Params[0] = new SqlParameter("@goupCd", goupCd);
            Params[1] = new SqlParameter("@menuCd", menuCd);
            Params[2] = new SqlParameter("@rowCnt", rowCnt);
            Params[3] = new SqlParameter("@pageNum", pageNum);
            return MssqlHelper.GetDataSet("[dbo].[SP_WEB_Management_MenuOrderList3_R]", Params, CommandType.StoredProcedure);
        }


        public string SetAuthSave(string groupCd, string menuCd, string usergroup, string grant_v, string grant_i, string grant_s, string grant_r, string grant_a, string createUser)
        {

            string strMessage = string.Empty;
            SqlParameter[] Params = new SqlParameter[9];
            Params[0] = new SqlParameter("@group_cd", groupCd);
            Params[1] = new SqlParameter("@menuCd", menuCd);
            Params[2] = new SqlParameter("@usergroup", usergroup);
            Params[3] = new SqlParameter("@grant_v", grant_v);
            Params[4] = new SqlParameter("@grant_i", grant_i);
            Params[5] = new SqlParameter("@grant_s", grant_s);
            Params[6] = new SqlParameter("@grant_r", grant_r);
            Params[7] = new SqlParameter("@grant_a", grant_a);
            Params[8] = new SqlParameter("@userId", createUser);

            DataSet ds = MssqlHelper.GetDataSet("[dbo].[SP_WEB_Management_MenuOrderList3_U]", Params, CommandType.StoredProcedure);
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
            return strMessage;
        }


        public string setExistschkAuthData(string groupCd, string menuCd)
        {
            string strMessage = string.Empty;
            SqlParameter[] Params = new SqlParameter[2];
            Params[0] = new SqlParameter("@group_cd", groupCd);
            Params[1] = new SqlParameter("@menuCd", menuCd);

            DataSet ds = MssqlHelper.GetDataSet("[dbo].[SP_WEB_Management_MenuOrderAuthchkList3_U]", Params, CommandType.StoredProcedure);
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
            return strMessage;
        }


    }

}
