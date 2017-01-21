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
    /// SectionList의 요약 설명입니다.
    /// </summary> 
    public class Section
    {
        public string SECTION { get; set; }
        public string SECTION_NAME { get; set; }
    }


    public class SectionList
    {
        public SectionList()
        {
            //
            // TODO: 여기에 생성자 논리를 추가합니다.
            //
        }
        public static DataSet SectionLists()
        {
            try
            {

                string strSql = "dbo.SP_WEB_Common_DEPT_List1_R";

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
