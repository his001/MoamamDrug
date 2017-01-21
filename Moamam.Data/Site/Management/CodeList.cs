using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

using Moamam.Lib;

namespace Moamam.Data.Site.Management
{
    public class CodeList
    {
        public DataSet GetCodeGroupList(int pageNum, int rowCnt)
        {
            SqlParameter[] Params = new SqlParameter[2];
            Params[0] = new SqlParameter("@rowCnt", rowCnt);
            Params[1] = new SqlParameter("@pageNum", pageNum);
            return MssqlHelper.GetDataSet("[dbo].[SP_WEB_Management_CodeList1_R]", Params, CommandType.StoredProcedure);
        }


        public DataSet GetCodeSubList(string codeGroup, int pageNum, int rowCnt)
        {
            SqlParameter[] Params = new SqlParameter[3];
            Params[0] = new SqlParameter("@codeGroup", codeGroup);
            Params[1] = new SqlParameter("@rowCnt", rowCnt);
            Params[2] = new SqlParameter("@pageNum", pageNum);
            return MssqlHelper.GetDataSet("[dbo].[SP_WEB_Management_CodeList2_R]", Params, CommandType.StoredProcedure);
        }


        public string SetCodeGroupUpdate(string codeGroupOld, string codeGroupNew, string codeIdOld, string codeIdNew, string codeDesc)
        {
            string strSql;
            int intVal = 0;

            using (TransactionScope tran = new TransactionScope())
            {
                strSql = @"
UPDATE CODE_MASTER
SET    CODE_GROUP = {0}
,      CODE_ID    = {1}
,      CODE_DESC  = '{2}'
WHERE  CODE_GROUP = {3}
AND    CODE_ID = {4}
AND    CODE_TYPE = 'G' ";

                strSql = string.Format(strSql, codeGroupNew, codeIdNew, codeDesc, codeGroupOld, codeIdOld);
                intVal =+ MssqlHelper.Execute(strSql, CommandType.Text);

                if(codeIdNew != codeIdOld)
                {
                    strSql = @"
UPDATE CODE_MASTER
SET    CODE_GROUP = {0}
WHERE  CODE_GROUP = {1}
AND    CODE_TYPE = 'C' ";

                    strSql = string.Format(strSql, codeIdNew, codeIdOld);
                    MssqlHelper.Execute(strSql, CommandType.Text);

                    if (intVal > 0) tran.Complete();
                }
                else
                {
                    if (intVal > 0) tran.Complete();
                }

            }

            return intVal > 0 ? "OK" : "";
        }



        public string SetCodeSubUpdate(string subCodeGroup, string subCodeIdOld, string subCodeIdNew, string subCodeDesc, string subCodeRef1, string subCodeRef2, string subCodeRef3, string subCodeRef4)
        {
            string strSql = @"
UPDATE CODE_MASTER
SET    CODE_GROUP = {0}
,      CODE_ID    = {1}
,      CODE_DESC  = '{2}'
,      CODE_REF_1 = {3}
,      CODE_REF_2 = {4}
,      CODE_REF_3 = '{5}'
,      CODE_REF_4 = '{6}'                        
WHERE  CODE_GROUP = {0}
AND    CODE_ID = {7}
AND    CODE_TYPE = 'C' ";

            strSql = string.Format(strSql, subCodeGroup, subCodeIdNew, subCodeDesc,
                string.IsNullOrEmpty(subCodeRef1) ? "NULL" : subCodeRef1,
                string.IsNullOrEmpty(subCodeRef2) ? "NULL" : subCodeRef2,
                subCodeRef3, subCodeRef4, subCodeIdOld);

            return MssqlHelper.Execute(strSql, CommandType.Text) > 0 ? "OK" : "";
        }


        public string SetCodeGroupInsert(string codeGroup, string codeId, string codeDesc)
        {
            string strSql;
            string strMessage;

            if (GetCodeGroupExist(codeGroup, codeId) > 0)
            {
                strMessage = "이미 등록된 마스터코드 입니다.";
            }
            else
            {
                strSql = @"
INSERT INTO CODE_MASTER
(
    CODE_GROUP
,   CODE_ID
,   CODE_DESC
,   CODE_TYPE
)
VALUES
(
    {0}
,   {1}
,   '{2}'
,   'G'
)";
                strSql = string.Format(strSql, codeGroup, codeId, codeDesc);
                strMessage = MssqlHelper.Execute(strSql, CommandType.Text) > 0 ? "OK" : "";
            }

            return strMessage;
        }


        public string SetCodeSubInsert(string subCodeGroup, string subCodeId, string subCodeDesc, string subCodeRef1, string subCodeRef2, string subCodeRef3, string subCodeRef4)
        {
            string strSql;
            string strMessage;

            if (GetCodeSubExist(subCodeGroup, subCodeId) > 0)
            {
                strMessage = "이미 등록된 서브코드 입니다.";
            }
            else
            {
                strSql = @"
INSERT INTO CODE_MASTER
(
    CODE_GROUP
,   CODE_ID
,   CODE_REF_1
,   CODE_REF_2
,   CODE_REF_3
,   CODE_REF_4
,   CODE_DESC
,   CODE_TYPE
)
VALUES
(
    {0}
,   {1}
,   {2}
,   {3}
,   '{4}'
,   '{5}'
,   '{6}'
,   'C'
)";
                strSql = string.Format(strSql, subCodeGroup, subCodeId,
                    string.IsNullOrEmpty(subCodeRef1) ? "NULL" : subCodeRef1, 
                    string.IsNullOrEmpty(subCodeRef2) ? "NULL" : subCodeRef2, 
                    subCodeRef3, subCodeRef4, subCodeDesc);

                strMessage = MssqlHelper.Execute(strSql, CommandType.Text) > 0 ? "OK" : "";
            }

            return strMessage;
        }


        public string SetCodeGroupDelete(string codeGroup, string codeId)
        {
            string strSql;
            int intVal = 0;

            using (TransactionScope tran = new TransactionScope())
            {
                strSql = "DELETE FROM CODE_MASTER WHERE CODE_GROUP = {0} AND CODE_ID = {1} AND CODE_TYPE = 'G'";
                intVal =+ MssqlHelper.Execute(string.Format(strSql, codeGroup, codeId), CommandType.Text);

                strSql = "DELETE FROM CODE_MASTER WHERE CODE_GROUP = {0} AND CODE_TYPE = 'C'";
                MssqlHelper.Execute(string.Format(strSql, codeId), CommandType.Text);

                if (intVal > 0) tran.Complete();
            }

            return intVal > 0 ? "OK" : "";
        }


        public string SetCodeSubDelete(string subCodeGroup, string subCodeId)
        {
            string strSql = "DELETE FROM CODE_MASTER WHERE CODE_GROUP = {0} AND CODE_ID = {1} AND CODE_TYPE = 'C' ";
            return MssqlHelper.Execute(string.Format(strSql, subCodeGroup, subCodeId), CommandType.Text) > 0 ? "OK" : "";
        }


        public int GetCodeSubExist(string codeGroup, string codeId)
        {
            string strSql = "select count(*) from code_master where code_group={0} and code_id='{1}' and code_type='C'";
            return Convert.ToInt32(MssqlHelper.GetDataScalar(string.Format(strSql, codeGroup, codeId), CommandType.Text));
        }

        public int GetCodeGroupExist(string codeGroup, string codeId)
        {
            string strSql = "select count(*) from code_master where code_group={0} and code_id='{1}' and code_type='G'";
            return Convert.ToInt32(MssqlHelper.GetDataScalar(string.Format(strSql, codeGroup, codeId), CommandType.Text));
        }


    }
}
