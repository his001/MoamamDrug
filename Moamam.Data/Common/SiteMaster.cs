using System;
using System.Web;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

using Moamam.Lib;

namespace Moamam.Data.Common
{
    public class SiteMaster
    {

        public DataSet GetUpdateDate()
        {
            SqlParameter[] Params = new SqlParameter[0];
            return MssqlHelper.GetDataSet("[dbo].[SP_WEB_Common_Master_Site_UpdateDate_R]", Params, CommandType.StoredProcedure);
             
        }

    }
}
