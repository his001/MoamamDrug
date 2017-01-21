using System;
using System.Web;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

using Moamam.Lib;

namespace Moamam.Data.Common
{

    /// <summary>
    /// User 세션 정보를 저장 타입
    /// </summary>
    [Serializable]
    public class UserInfo
    {
        public string UserID;
        public string UserName;
        public string UserGroupCode;
        public string UserGroupName;
    }

    /// <summary>
    //User 메서드에서 사용할 열거형 값
    /// <summary>
    public enum UserInfoType
    {
        UserID,
        UserName,
        UserGroupCode,
        UserGroupName
    }


    public class SiteGrant
    {
        public DataSet SelectGrant(string menuCd, string userGroup)
        {
            SqlParameter[] Params = new SqlParameter[2];
            Params[0] = new SqlParameter("@groupcd", string.IsNullOrEmpty(userGroup) ? "0" : userGroup);
            Params[1] = new SqlParameter("@menucd", menuCd);
            return MssqlHelper.GetDataSet("[dbo].[SP_WEB_DAO_Common_SiteGrant1_R]", Params, CommandType.StoredProcedure);

            //            string strSql = @"
//select  b.user_group, 
//        c.group_cd,
//        a.menu_cd,
//        a.menu_url, 
//        isnull(b.grant_v, 0) 'view',
//        isnull(b.grant_i, 0) 'inquiry', 
//        isnull(b.grant_s, 0) 'save', 
//        isnull(b.grant_r, 0) 'report', 
//        isnull(b.grant_a, 0) 'approval'
//from menu a with(nolock)
//     join menu_group c with(nolock) on a.group_cd= c.group_cd
//     left outer join menu_grant b with(nolock) on a.menu_cd = b.menu_cd                                          
//where a.use_yn = 'Y'
//and a.menu_cd = {0}
//and b.user_group = {1}
//";

//            strSql = string.Format(strSql, menuCd, string.IsNullOrEmpty(userGroup) ? "0" : userGroup);
//            DataSet ds = MssqlHelper.GetDataSet(strSql, CommandType.Text);

//            return ds;
        }

        public int SetUserLog(string userId, string menuCd, string useType)
        {
            string strSql = @" insert into users_log (log_time, user_id, group_cd, menu_cd, use_type, connect_ip) values (getdate(),'{0}',{1},{2},'{3}','{4}')";
            strSql = string.Format(strSql, userId, GetMenuGroupCd(menuCd), menuCd, useType, HttpContext.Current.Request.UserHostAddress);
            strSql = AntiHack.rtnSQLInj(strSql);
            return MssqlHelper.Execute(strSql, CommandType.Text);
        }

        public string GetMenuGroupCd(string menuCd)
        {
            string strSql = "select isnull(min(group_cd),0) from menu where menu_cd=" + menuCd;
            return MssqlHelper.GetDataScalar(strSql, CommandType.Text).ToString();
        }

    }

}
