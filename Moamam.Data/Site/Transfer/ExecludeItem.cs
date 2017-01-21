using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

using Moamam.Lib;

namespace Moamam.Data.Site.Transfer
{
    public class ExecludeItem
    {
        public DataSet GetExecludeItem(string item, int rowCnt, int pageNum)
        {
            string strSql = @"
SELECT T.*
FROM (         
    SELECT FLOOR((ROW_NUMBER() OVER (ORDER BY A.ITEM) - 1) / {0}  + 1) PAGE
            , COUNT(*) OVER() AS TOTAL_COUNT
            , A.ACTION_TYPE
            , A.ITEM
            , B.ITEM_DESC
            , A.CREATE_USER
            , A.CREATE_DATE
        FROM PVS_TRF_EXC_ITEM A
            , PVS_ITEM_MASTER B
        WHERE A.ITEM = B.ITEM
        AND A.ITEM = CASE ISNULL('{1}','') WHEN '' THEN A.ITEM ELSE '{1}' END
    ) T
WHERE T.PAGE = {2}";

            strSql = string.Format(strSql, rowCnt, item, pageNum);
            return MssqlHelper.GetDataSet(strSql, CommandType.Text);
        }


        public string SetExecludeCrud(string cmdCrud, string item, string createUser)
        {
            string strSql;
            string strMessage = string.Empty;

            if (cmdCrud == "CREATE")
            {
                if (GetItemMasterExist(item) <= 0)
                    return strMessage = "시스템에 등록되어 있지 않은 아이템입니다.";

                if (GetTrfExcItemExist(item) > 0)
                    return strMessage = "이미 등록된 아이템입니다.";

                strSql = @"
INSERT INTO PVS_TRF_EXC_ITEM
        (ITEM, ACTION_TYPE, CREATE_USER, CREATE_DATE)
VALUES  ('{0}', 'A', '{1}',GETDATE())";

                strSql = string.Format(strSql, item, createUser);
                strMessage = MssqlHelper.Execute(strSql, CommandType.Text) > 0 ? "OK" : "";
            }

            if (cmdCrud == "DELETE")
            {
                strSql = "DELETE FROM PVS_TRF_EXC_ITEM WHERE ITEM = '" + item + "'";
                strMessage = MssqlHelper.Execute(strSql, CommandType.Text) > 0 ? "OK" : "";
            }

            return strMessage;
        }


        public int GetItemMasterExist(string item)
        {
            string strSql = "select count(*) from pvs_item_master where item = '" + item + "'";
            return Convert.ToInt32(MssqlHelper.GetDataScalar(strSql, CommandType.Text));
        }

        public int GetTrfExcItemExist(string item)
        {
            string strSql = "select count(*) from pvs_trf_exc_item where item = '" + item + "'";
            return Convert.ToInt32(MssqlHelper.GetDataScalar(strSql, CommandType.Text));
        }

    }
}
