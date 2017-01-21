using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace Moamam.Lib
{
    public class MssqlHelper
    {
        public MssqlHelper() { }


        private static string GetConnectionString(string alias)
        {
            string strConnection = string.Empty;

            if (string.IsNullOrEmpty(alias))
            {
                //strConnection = ConfigurationManager.ConnectionStrings[
                //    ConfigurationManager.ConnectionStrings["ConnectionStringDefault"].ConnectionString
                //    ].ConnectionString;
                strConnection = DbConStr.getDbConstr();
            }
            else
                strConnection = ConfigurationManager.ConnectionStrings[alias].ConnectionString;

            return strConnection;
        }

        public static SqlConnection GetConnection(string alias)
        {
            SqlConnection conn = new SqlConnection(GetConnectionString(alias));
            return conn;
        }


        #region DataSet

        public static DataSet GetDataSet(string commandText, CommandType commandType)
        {
            return ExecuteDataSet(string.Empty, commandText, null, commandType);
        }

        public static DataSet GetDataSet(string commandText, SqlParameter[] commandParameters, CommandType commandType)
        {
            return ExecuteDataSet(string.Empty, commandText, commandParameters, commandType);
        }

        public static DataSet GetDataSet(string alias, string commandText, CommandType commandType)
        {
            return ExecuteDataSet(alias, commandText, null, commandType);
        }

        public static DataSet GetDataSet(string alias, string commandText, SqlParameter[] commandParameters, CommandType commandType)
        {
            return ExecuteDataSet(alias, commandText, commandParameters, commandType);
        }

        #endregion

        #region DataScalar

        public static object GetDataScalar(string commandText, CommandType commandType)
        {
            return ExecuteScalar(string.Empty, commandText, null, commandType);
        }

        public static object GetDataScalar(string commandText, SqlParameter[] commandParameters, CommandType commandType)
        {
            return ExecuteScalar(string.Empty, commandText, commandParameters, commandType);
        }

        public static object GetDataScalar(string alias, string commandText, CommandType commandType)
        {
            return ExecuteScalar(alias, commandText, null, commandType);
        }

        public static object GetDataScalar(string alias, string commandText, SqlParameter[] commandParameters, CommandType commandType)
        {
            return ExecuteScalar(alias, commandText, commandParameters, commandType);
        }

        #endregion

        #region Execute

        public static int Execute(string commandText, CommandType commandType)
        {
            return ExecuteNonQuery(string.Empty, commandText, null, commandType);
        }

        public static int Execute(string commandText, SqlParameter[] commandParameters, CommandType commandType)
        {
            return ExecuteNonQuery(string.Empty, commandText, commandParameters, commandType);
        }

        public static int Execute(string alias, string commandText, CommandType commandType)
        {
            return ExecuteNonQuery(alias, commandText, null, commandType);
        }

        public static int Execute(string alias, string commandText, SqlParameter[] commandParameters, CommandType commandType)
        {
            return ExecuteNonQuery(alias, commandText, commandParameters, commandType);
        }

        #endregion


        #region MsSQL Data Command

        public static DataSet ExecuteDataSet(string alias, string commandText, SqlParameter[] commandParameters, CommandType commandType)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;

            #region ########## sp log 남기기 ##########
            string _logParam = string.Empty;
            try {
                foreach (var param in commandParameters) { 
                    if(_logParam==string.Empty)
                    {
                        _logParam = " "+param.ParameterName.ToString() +"='"+ param.Value.ToString()+"'";
                    }
                    else
                    {
                        _logParam = _logParam + ", " + param.ParameterName.ToString() + "='" + param.Value.ToString() + "'";
                    }
                    
                }
            }catch(Exception ex){ }
            
            string SpName = commandText.Replace("[", "").Replace("]", "");
            try { QueryTrace("execute " + SpName + " " + _logParam); } catch (Exception e) { }

            string chkSpName = SpName.Substring(SpName.Length - 2, 2);
            if (chkSpName.IndexOf("_") > -1 && chkSpName != "_R")
            {
                Log_UserAction(chkSpName);
            }
            #endregion ########## sp log 남기기 ##########
            //QueryTrace("execute " + commandText + _logParam);

            try
            {
                cmd = new SqlCommand();
                cmd.CommandText = AntiHack.rtnSQLInj(commandText);
                cmd.CommandType = commandType;
                if (commandParameters != null)
                    cmd.Parameters.AddRange(commandParameters);

                con = new SqlConnection(GetConnectionString(alias));
                con.Open();
                cmd.Connection = con;

                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (con != null)
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    con.Dispose();
                }
                if (cmd != null) cmd.Dispose();
                if (da != null) da.Dispose();
            }

            return ds;
        }


        public static object ExecuteScalar(string alias, string commandText, SqlParameter[] commandParameters, CommandType commandType)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            object objRtn = null;

            #region ########## sp log 남기기 ##########
            string _logParam = string.Empty;
            try
            {
                foreach (var param in commandParameters)
                {
                    if (_logParam == string.Empty)
                    {
                        _logParam = " " + param.ParameterName.ToString() + "='" + param.Value.ToString() + "'";
                    }
                    else
                    {
                        _logParam = _logParam + ", " + param.ParameterName.ToString() + "='" + param.Value.ToString() + "'";
                    }

                }
            }
            catch (Exception ex) { }

            string SpName = commandText.Replace("[", "").Replace("]", "");
            try { QueryTrace("execute " + SpName + " " + _logParam); }
            catch (Exception e) { }

            string chkSpName = SpName.Substring(SpName.Length - 2, 2);
            if (chkSpName.IndexOf("_") > -1 && chkSpName != "_R")
            {
                Log_UserAction(chkSpName);
            }
            #endregion ########## sp log 남기기 ##########
            //QueryTrace(commandText);

            try
            {
                cmd = new SqlCommand();
                cmd.CommandText = AntiHack.rtnSQLInj(commandText);
                cmd.CommandType = commandType;
                if (commandParameters != null)
                    cmd.Parameters.AddRange(commandParameters);

                con = new SqlConnection(GetConnectionString(alias));
                con.Open();
                cmd.Connection = con;

                objRtn = cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (con != null)
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    con.Dispose();
                }
                if (cmd != null) cmd.Dispose();
            }

            return objRtn;
        }

        public static int ExecuteNonQuery(string alias, string commandText, SqlParameter[] commandParameters, CommandType commandType)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int intRtn = 0;

            //QueryTrace(commandText);
            #region ########## sp log 남기기 ##########
            string _logParam = string.Empty;
            try
            {
                foreach (var param in commandParameters)
                {
                    if (_logParam == string.Empty)
                    {
                        _logParam = " " + param.ParameterName.ToString() + "='" + param.Value.ToString() + "'";
                    }
                    else
                    {
                        _logParam = _logParam + ", " + param.ParameterName.ToString() + "='" + param.Value.ToString() + "'";
                    }

                }
            }
            catch (Exception ex) { }

            string SpName = commandText.Replace("[", "").Replace("]", "");
            try { QueryTrace("execute " + SpName + " " + _logParam); }
            catch (Exception e) { }

            string chkSpName = SpName.Substring(SpName.Length - 2, 2);
            if (chkSpName.IndexOf("_") > -1 && chkSpName != "_R")
            {
                Log_UserAction(chkSpName);
            }
            #endregion ########## sp log 남기기 ##########

            try
            {
                cmd = new SqlCommand();
                cmd.CommandText = AntiHack.rtnSQLInj(commandText);
                cmd.CommandType = commandType;
                if (commandParameters != null)
                    cmd.Parameters.AddRange(commandParameters);

                con = new SqlConnection(GetConnectionString(alias));
                con.Open();
                cmd.Connection = con;

                intRtn = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (con != null)
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    con.Dispose();
                }
                if (cmd != null) cmd.Dispose();
            }

            return intRtn;
        }

        public static int SqlBulkCopy(string alias, string table, DataTable dt)
        {
            SqlConnection con = null; 
            int intRtn = 0;

            if (dt == null) return intRtn;

            try
            {
                con = new SqlConnection(GetConnectionString(alias));
                con.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                {
                    bulkCopy.DestinationTableName = table;
                    bulkCopy.WriteToServer(dt);
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                if (con != null)
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    con.Dispose();
                }
            }

            return intRtn;
        }


        public static void QueryTrace(string query)
        {
            HttpContext ctx = HttpContext.Current;
            if (ConfigurationManager.AppSettings["QueryTraceMode"].ToString().ToLower() != "true") return;

            string traceInfo ="URL: " + ctx.Request.Url.ToString() + "\r\nStacktrace:---\r\n" + query ;
            SysLogger.WriteLog("QueryString:\r\n" + traceInfo);
        }

        #endregion


        #region #################   Log_UserAction ####################
        public static void Log_UserAction(string _CUD)
        {
            if (_CUD == "_S") { _CUD = "SAVE"; }
            else if (_CUD == "_D") { _CUD = "DELETE"; }
            else if (_CUD == "_C") { _CUD = "CREATE"; }
            else if (_CUD == "_I") { _CUD = "INSERT"; }
            else { _CUD = "READ"; }

            if (_CUD != "READ")
            {
                try
                {
                    string _userId =string.Empty;
                    string _menuCd =string.Empty;
                    string _groupCd = string.Empty;

                    _userId = HttpContext.Current.Session["userID"].ToString();
                    _menuCd = HttpContext.Current.Session["menuCd"].ToString();
                    _groupCd = HttpContext.Current.Session["groupCd"].ToString();
                    if(_userId!="")
                    { 
                        string strSql = @" insert into users_log (log_time, user_id, group_cd, menu_cd, use_type, connect_ip) values (getdate(),'{0}',{1},{2},'{3}','{4}')";
                        strSql = string.Format(strSql, _userId, _groupCd, _menuCd, _CUD, HttpContext.Current.Request.UserHostAddress);
                        ExecuteNonQuery4Log("Log",strSql,null,CommandType.Text);
                    }
                }
                catch (Exception ex) { }
            }
        }
    

        public static void ExecuteNonQuery4Log(string alias, string commandText, SqlParameter[] commandParameters, CommandType commandType)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int intRtn = 0;

            try
            {
                cmd = new SqlCommand();
                cmd.CommandText = AntiHack.rtnSQLInj(commandText);
                cmd.CommandType = commandType;
                if (commandParameters != null)
                    cmd.Parameters.AddRange(commandParameters);

                con = new SqlConnection(GetConnectionString(alias));
                con.Open();
                cmd.Connection = con;

                intRtn = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (con != null)
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    con.Dispose();
                }
                if (cmd != null) cmd.Dispose();
            }

            //return intRtn;
        }
        #endregion #################   Log_UserAction ####################
    }
}
