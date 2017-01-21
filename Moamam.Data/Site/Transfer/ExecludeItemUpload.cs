using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Moamam.Lib;

namespace Moamam.Data.Site.Transfer
{
    public class ExecludeItemUpload
    {
        public DataSet GetExecludeStage()
        {
            string strSql = @"
SELECT A.ACTION_TYPE
     , CASE ISNULL(A.ERR_MSG,'') WHEN '' THEN 'S' ELSE 'E' END AS STATUS /*--('E', 오류) ('S', 성공)*/
     , A.ITEM
     , B.ITEM_DESC
     , A.ERR_MSG AS ERROR_MESSAGE
     , A.CREATE_USER
     , A.CREATE_DATE
     , (SELECT COUNT(*) FROM PVS_TRF_EXC_ITEM_STAGE WHERE ISNULL(ERR_MSG,'') = '')  AS OK_CNT
     , (SELECT COUNT(*) FROM PVS_TRF_EXC_ITEM_STAGE WHERE ISNULL(ERR_MSG,'') <> '') AS NG_CNT
FROM PVS_TRF_EXC_ITEM_STAGE A 
     LEFT JOIN PVS_ITEM_MASTER B ON A.ITEM = B.ITEM ";

            return MssqlHelper.GetDataSet(strSql, CommandType.Text);
        }

        // --> Bulk Copy 로 구현
        public string SetExecludeUpload(List<ExecludItemData> dataPac)
        {
            string strMsg;
            int intVal = 0;

            MssqlHelper.Execute("TRUNCATE TABLE PVS_TRF_EXC_ITEM_STAGE", CommandType.Text);

            foreach(ExecludItemData data in dataPac)
            {
                string strSql = string.Empty;
                string strErr = string.Empty;

                if (data.ACTION_TYPE != "A" && data.ACTION_TYPE != "C" && data.ACTION_TYPE != "D")
                    strErr = "ACTION_TYPE은 A or C or D 만 입력 가능합니다.";

                if (GetItemMasterExist(data.ITEM) <= 0)
                    strErr = "[ITEM_MASTER]테이블에 등록된 아이템이 없습니다.";

                strSql = @"
INSERT
INTO PVS_TRF_EXC_ITEM_STAGE 
    (ITEM, ACTION_TYPE, ERR_MSG, CREATE_USER, CREATE_DATE)
VALUES ('{0}', '{1}', '{2}', '{3}', GETDATE())";

                strSql = string.Format(strSql, data.ITEM, data.ACTION_TYPE, strErr, data.CREATE_USER);
                intVal =+ MssqlHelper.Execute(strSql, CommandType.Text);
            }

            strMsg = intVal > 0 ? "OK" : "";

            return strMsg;
        }


        public string SetExecludeConfirm()
        {
            string strSql = @"
INSERT INTO PVS_TRF_EXC_ITEM 
  (ITEM, ACTION_TYPE, CREATE_USER, CREATE_DATE)
SELECT A.ITEM
     , A.ACTION_TYPE
     , A.CREATE_USER
     , A.CREATE_DATE
FROM PVS_TRF_EXC_ITEM_STAGE A
WHERE 
   ISNULL(A.ERR_MSG,'') = ''
   AND NOT EXISTS (SELECT 1 FROM PVS_TRF_EXC_ITEM B WHERE B.ITEM = A.ITEM)";

            MssqlHelper.Execute(strSql, CommandType.Text);
            MssqlHelper.Execute("DELETE FROM PVS_TRF_EXC_ITEM WHERE ACTION_TYPE = 'D'", CommandType.Text);

            return "OK";
        }


        public int GetItemMasterExist(string item)
        {
            string strSql = "select count(*) from pvs_item_master where item = '" + item + "'";
            return Convert.ToInt32(MssqlHelper.GetDataScalar(strSql, CommandType.Text));
        }

    }
}
