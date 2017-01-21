using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using System.Data;
using System.Data.SqlClient;
using Moamam.Lib;

namespace Moamam.Data.Site.BaseClass
{
    /// <summary>
    /// DCCODE의 요약 설명입니다.
    /// </summary> 
    public class Dccode
    {
        public string DCCODE { get; set; }
        public string DC_NAME { get; set; }
    }


    public class ucDccode
    {
        public ucDccode()
        {
            //
            // TODO: 여기에 생성자 논리를 추가합니다.
            //
        }
        public static DataSet ucDccodeLists()
        {
            try
            {

                string strSql = "SP_WEB_Common_WH_List1_R";

                DataSet ds = MssqlHelper.GetDataSet(strSql, CommandType.Text);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
