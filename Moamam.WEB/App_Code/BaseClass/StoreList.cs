using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data;
using Moamam.Lib;

public class Store
{
	public string STORE         { get; set; }
    public string STORE_NAME    { get; set; }
}


public class StoreList : BasePage
{
    public static List<Store> StoreLists()
    {
        try
        {
            List<Store> StoreList = new List<Store>();

            OracleParameter[] param = { new OracleParameter("P_STORE", "")
                                      , new OracleParameter{ ParameterName = "V_CURSOR", Direction = ParameterDirection.Output, OracleType = OracleType.Cursor }
                                      };

            OracleDataReader dr = OracleHelper.ExecuteReader(MyWebConfig.connectionString, CommandType.StoredProcedure, "PKG_PVS_WEB_LIST_SQL.PR_PVS_STORE_LIST", param);

            while (dr.Read())
            {
                StoreList.Add(new Store { STORE = dr["STORE"].ToString(), STORE_NAME = dr["STORE_NAME"].ToString() });
            }

            return StoreList;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    /// <summary>
    /// 법인(HT/HC)별 점포목록 조회
    /// </summary>
    /// <param name="pStore">점포번호</param>
    /// <returns>DataSet</returns>
    public static List<Store> GetStoreListTZGroup(string pStore)
    {
        try
        {
            List<Store> StoreList = new List<Store>();

            OracleParameter[] param = {
                                        new OracleParameter("P_STORE"   , pStore)
                                      , new OracleParameter("V_CURSOR"  , OracleType.Cursor)
                                      };
            param[1].Direction = ParameterDirection.Output;

            OracleDataReader dr = OracleHelper.ExecuteReader(MyWebConfig.connectionString, CommandType.StoredProcedure, "PKG_PVS_WEB_LIST_SQL.PR_PVS_STORE_TZGROUP_LIST", param);

            while (dr.Read())
            {
                StoreList.Add(new Store { STORE = dr["STORE"].ToString(), STORE_NAME = dr["STORE_NAME"].ToString() });
            }

            return StoreList;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


}