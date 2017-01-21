using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data;
using Moamam.Lib;

public class Week
{
    public string DESC_WEEK_NO { get; set; }
    public string WEEK { get; set; }
    public string ST_DATE { get; set; }
    public string ED_DATE { get; set; }
}

public class WeekList : BasePage
{
    public static List<Week> WeekLists()
    {
        try
        {
            List<Week> WeekList = new List<Week>();

            OracleParameter[] param = { new OracleParameter("P_WEEK_NO", "")
                                      , new OracleParameter{ ParameterName = "V_CURSOR", Direction = ParameterDirection.Output, OracleType = OracleType.Cursor }
                                      };

            OracleDataReader dr = OracleHelper.ExecuteReader(MyWebConfig.connectionString, CommandType.StoredProcedure, "PKG_PVS_WEB_LIST_SQL.PR_PVS_WEEK_LIST", param);

            while (dr.Read())
            {
                WeekList.Add(new Week { DESC_WEEK_NO = dr["DESC_WEEK_NO"].ToString(), WEEK = dr["WEEK"].ToString(), ST_DATE = dr["ST_DATE"].ToString(), ED_DATE = dr["ED_DATE"].ToString() });
            }

            return WeekList;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}