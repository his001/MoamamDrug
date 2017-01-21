using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

using Moamam.Lib;

namespace Moamam.Data.Site.Management
{
    public class UserList
    {
        public DataSet GetUserList(string userId, string userName, string userType, int rowCnt, int pageNum)
        {
            SqlParameter[] Params = new SqlParameter[5];
            Params[0] = new SqlParameter("@rowCnt", rowCnt);
            Params[1] = new SqlParameter("@userId", userId);
            Params[2] = new SqlParameter("@userName", userName);
            Params[3] = new SqlParameter("@userType", userType);
            Params[4] = new SqlParameter("@pageNum", pageNum);
            return MssqlHelper.GetDataSet("[dbo].[SP_WEB_Management_UserList1_R]", Params, CommandType.StoredProcedure);
        }


        public string SetUserUpdate(string userId, string UserType, string useYn)
        {
            string strMessage = string.Empty;

            SqlParameter[] Params = new SqlParameter[3];
            Params[0] = new SqlParameter("@userType", UserType);
            Params[1] = new SqlParameter("@useYn", useYn);
            Params[2] = new SqlParameter("@userId", userId);

            DataSet ds = MssqlHelper.GetDataSet("[dbo].[SP_WEB_Management_UserList1_U]", Params, CommandType.StoredProcedure);
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


        public string SetUserInsert(string userId, string userName, string userType, string useYn)
        {
            string strMessage;
            if (GetUserItemExist(userId) > 0)
            {
                strMessage = "이미 등록된 아이디 입니다.";
            }
            else
            {
                SqlParameter[] Params = new SqlParameter[4];
                Params[0] = new SqlParameter("@userId", userId);
                Params[1] = new SqlParameter("@userName", userName);
                Params[2] = new SqlParameter("@userType", userType);
                Params[3] = new SqlParameter("@useYn", useYn);

                DataSet ds = MssqlHelper.GetDataSet("[dbo].[SP_WEB_Management_UserList1_I]", Params, CommandType.StoredProcedure);
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

        
        public string SetUserDelete(string userId)
        {
            string strSql = "DELETE FROM USERS WHERE USER_ID = '" + userId + "'";
            return MssqlHelper.Execute(strSql, CommandType.Text) > 0 ? "OK" : "";
        }


        public int GetUserItemExist(string userId)
        {
            string strSql = "select count(*) from users where user_id = '" + userId + "'";
            return Convert.ToInt32(MssqlHelper.GetDataScalar(strSql, CommandType.Text));
        }

    }
}
