using System;
using System.Web;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

using Moamam.Lib;

namespace Moamam.Data.Common
{
    public class SiteUser
    {
        public DataSet GetUserLoginCheck(string userId)
        {
            string strSql = @"
SELECT A.USER_ID
,      A.USER_NAME
,      B.CODE_ID AS SUB_CD
,      B.CODE_DESC AS SUB_CD_NM
FROM USERS A
    INNER JOIN
    (
    SELECT 
        CODE_GROUP
        , CODE_ID
        , CODE_DESC
    FROM CODE_MASTER
    WHERE 
        CODE_TYPE = 'C'
        AND CODE_GROUP = 9000 /* --AND CODE_ID = 1 --관리자권한 코드 */
    ) B
ON     A.USER_TYPE  = B.CODE_ID
WHERE  A.USER_ID    = '" + userId + @"'
AND    A.USE_YN     = 'Y'";

            return MssqlHelper.GetDataSet(strSql, CommandType.Text);
        }

        public DataSet GetUserLoginFirstUrl(string groupCode)
        {
            string strSql = @"
SELECT '/' + MENU_URL + '?G=' + CONVERT(VARCHAR, GROUP_CD) + '&M=' + CONVERT(VARCHAR, MENU_CD) AS MENU_URL
FROM MENU 
WHERE USE_YN = 'Y'
AND GROUP_CD IN (
  SELECT MIN(GROUP_CD)
  FROM MENU_GROUP
  WHERE USE_YN = 'Y'
  AND USER_AUTH_CD >= " + groupCode + @" /* 조회조건:로그인 사용자 권한 */
)
ORDER BY MENU_CD ASC ";

            return MssqlHelper.GetDataSet(strSql, CommandType.Text);
        }


        public DataSet GetUserUseUrl(string groupCdoe)
        {
            string strSql = @"
SELECT MENU_URL 
FROM MENU
WHERE 
GROUP_CD NOT IN 
  (
  SELECT GROUP_CD
  FROM MENU_GROUP
  WHERE USER_AUTH_CD >= " + groupCdoe + @" /*--조회조건:로그인 사용자 권한*/
  )";

            return MssqlHelper.GetDataSet(strSql, CommandType.Text);
        }


        public DataSet GetUserGroupList()
        {
            string strSql = @"
SELECT CODE_GROUP AS GROUP_CD
   , CODE_ID    AS SUB_CD
   , CODE_DESC  AS SUB_CD_NM
FROM CODE_MASTER
WHERE CODE_TYPE = 'C'
 AND CODE_GROUP = 9000
 /* --AND CODE_ID = 1 --관리자권한 코드 */
ORDER BY CODE_ID;";

            return MssqlHelper.GetDataSet(strSql, CommandType.Text);
        }

    }
}
