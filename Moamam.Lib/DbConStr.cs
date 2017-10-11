using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Moamam.Lib
{
    public class DbConStr
    {
        public static string getDbConstr()
        {
            string _conStr = @"server=";
            return _conStr;
        }

        #region #################### getJson / XML ##############################

        protected string GetDBValue(object obj)
        {
            return obj == DBNull.Value ? "" : obj.ToString();
        }

        protected string GetDBValue(object obj, string defaultValue)
        {
            return obj == DBNull.Value ? defaultValue : obj.ToString();
        }

        public static SqlParameterCollection InitSqlParameterCollection()
        {
            return (SqlParameterCollection)typeof(SqlParameterCollection).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null).Invoke(null);
        }

        #region ############### CommonSpCall ###############
        public static DataSet CommonSpCall(string spName, SqlParameterCollection param)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(
                    //System.Configuration.ConfigurationManager.ConnectionStrings["ADODB"].ToString()
                    Moamam.Lib.DbConStr.getDbConstr().ToString()
                    ))
                {
                    var command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandType = CommandType.StoredProcedure;

                    command.CommandText = AntiHack.rtnSQLInj(spName);
                    if (param != null)
                    {
                        foreach (SqlParameter sqlPra in param)
                        {
                            command.Parameters.Add("@" + AntiHack.rtnSQLInj(sqlPra.ParameterName), sqlPra.SqlDbType);
                            command.Parameters["@" + sqlPra.ParameterName].Value = sqlPra.Value;
                        }
                    }
                    var adapt = new SqlDataAdapter();
                    adapt.SelectCommand = command;
                    adapt.Fill(ds);
                }
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        #endregion ############### CommonSpCall ###############

        public string baseCommonSpCaller(string spName, SqlParameterCollection param)
        {
            string sErrorType = string.Empty;       //E:Error, I:Information, C:Confirm, CT:커스텀 이벤트
            string sErrorMessage = string.Empty;

            DataSet dsData = CommonSpCaller(ref sErrorType, ref sErrorMessage, spName, param);

            GridHelper gridHelper = new GridHelper();
            System.Text.StringBuilder sbReturnXml = new System.Text.StringBuilder();

            if (dsData != null && dsData.Tables.Count > 0)
            {
                XmlResultInfo xmlResultInfo = null;
                for (int iCnt = 0; iCnt < dsData.Tables.Count; iCnt++)
                {
                    xmlResultInfo = new XmlResultInfo();

                    if (dsData.Tables[iCnt].Rows.Count > 0)
                    {
                        //XML Properties
                        if (sErrorType.Length == 0 || !sErrorType.Equals("E"))
                        {
                            xmlResultInfo.Result = "S";
                        }
                        else
                        {
                            xmlResultInfo.Result = "F";
                        }
                        xmlResultInfo.ResultType = sErrorType;
                        xmlResultInfo.ResultMessage = sErrorMessage;
                        xmlResultInfo.BindID = dsData.Tables[iCnt].TableName + iCnt.ToString();
                        //xmlResultInfo.ReturnValueType = ReturnValue.ReturnValueType.RecordSet;
                        DataView dvData = dsData.Tables[iCnt].DefaultView;
                        //dvData.Sort = Request["SortInfo"] != null ? Request["SortInfo"] : "";
                        DataTable dtFilterdData = dvData.ToTable();
                        sbReturnXml.Append(gridHelper.ResultDataToXmlString(dtFilterdData, xmlResultInfo));
                    }
                    else
                    {
                        xmlResultInfo.Result = "S";
                        xmlResultInfo.ResultType = "E";
                        xmlResultInfo.ResultMessage = "Global_NoDataInform";

                        sbReturnXml.Append(gridHelper.ResultDataToXmlString(xmlResultInfo));
                    }
                }
            }

            //Response.Clear();
            //Response.ContentType = "text/xml";
            //Response.Write(gridHelper.AddRootHeader(sbReturnXml.ToString()));
            //Response.End();
            return gridHelper.AddRootHeader(sbReturnXml.ToString());
        }
        private DataSet CommonSpCaller(ref string errorType, ref string errorMessage, string spName, SqlParameterCollection param)
        {
            DataSet dsReturn = null;

            try
            {
                dsReturn = CommonSpCall(spName, param);
                if (dsReturn == null || dsReturn.Tables.Count == 0 || dsReturn.Tables[0].Rows.Count == 0)
                {
                    errorType = "I";
                    errorMessage = "Global_NoDataInform";
                }
                return dsReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        #region ###################### CommonCallSpGetJson ######################
        public string baseCommonCallSpGetJson(string spName, SqlParameterCollection param)
        {
            string sErrorType = string.Empty;       //E:Error, I:Information, C:Confirm, CT:커스텀 이벤트
            string sErrorMessage = string.Empty;

            DataSet dsData = CommonSpCaller(ref sErrorType, ref sErrorMessage, spName, param);

            string jsonString = string.Empty;

            if (dsData != null && dsData.Tables.Count > 0)
            {
                jsonString = JsonConvert.SerializeObject(dsData.Tables[0]);
            }
            //Response.Clear();
            ////Response.ContentType = "text/xml";
            //Response.ContentType = "application/json; charset=utf-8";
            //Response.Write(jsonString);
            //Response.End();
            return jsonString;
        }

        #endregion ###################### CommonCallSpGetJson ######################


        public class DataFiller
        {
            public static DataTable ConvertTo<T>(IList<T> list)
            {
                DataTable table = CreateTable<T>();
                Type entityType = typeof(T);
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

                foreach (T item in list)
                {
                    DataRow row = table.NewRow();

                    foreach (PropertyDescriptor prop in properties)
                    {
                        row[prop.Name] = prop.GetValue(item);
                    }

                    table.Rows.Add(row);
                }

                return table;
            }

            public static IList<T> ConvertTo<T>(IList<DataRow> rows)
            {
                IList<T> list = null;

                if (rows != null)
                {
                    list = new List<T>();

                    foreach (DataRow row in rows)
                    {
                        T item = CreateItem<T>(row);
                        list.Add(item);
                    }
                }

                return list;
            }

            public static IList<T> ConvertTo<T>(DataTable table)
            {
                if (table == null)
                {
                    return null;
                }

                List<DataRow> rows = new List<DataRow>();

                foreach (DataRow row in table.Rows)
                {
                    rows.Add(row);
                }

                return ConvertTo<T>(rows);
            }

            public static T CreateItem<T>(DataRow row)
            {
                T obj = default(T);
                if (row != null)
                {
                    obj = Activator.CreateInstance<T>();

                    foreach (DataColumn column in row.Table.Columns)
                    {
                        PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                        try
                        {
                            object value = row[column.ColumnName];
                            prop.SetValue
                                (obj, value.ToString(), null);
                        }
                        catch
                        {
                            // You can log something here
                            throw;
                        }
                    }
                }

                return obj;
            }

            public static DataTable CreateTable<T>()
            {
                Type entityType = typeof(T);
                DataTable table = new DataTable(entityType.Name);
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

                foreach (PropertyDescriptor prop in properties)
                {
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }

                return table;
            }
        }

        public class GridHelper
        {
            string GridColumnDelimiter = ConfigurationManager.AppSettings["GridColumnDelimiter"];
            string GridRowDelimiter = ConfigurationManager.AppSettings["GridRowDelimiter"];
            string GridSchemaDelimiter = ConfigurationManager.AppSettings["GridSchemaDelimiter"];

            public enum FormatType { Date, Number, Decimal, TelNo, Card, IDNo, Custom, ZipCode, Percent, Percent1, Percent2 }; //HTML, 

            //FormatType 기본값
            //Date : ####-##-##
            //Number : #,###,###
            //TelNo : ##(#)-###(#)-####
            //Card : ####-####-####-####
            //IDNo : ######-#######
            //Decimal : #,###,###.#
            //Percent : #,##0.##%

            private Boolean _addSumRow = false;
            private int[][] _sumColIndex;

            private Boolean _isFormat = false;
            private string[][] _colName;
            private FormatType[][] _formatType;
            private string[][] _formatString;


            private string _bindid = "bind";

            public GridHelper()
            {
                //
                // TODO: 생성자 논리를 여기에 추가합니다.
                //
            }

            /// <summary>
            /// 지정한 컬럼의 데이터 포맷을 변경한다.
            /// </summary>
            /// <param name="source">원본 DataTable</param>
            /// <param name="colName">포맷을 적용할 컬럼명</param>
            /// <param name="formatString">포맷 문자열</param>
            /// <returns></returns>
            public DataTable SetFormat(DataTable dt, string[] colName, FormatType[] formatType, string[] formatString)
            {
                return dt;
            }

            /// <summary>
            /// 지정한 컬럼에 대해 정렬을 한다.
            /// </summary>
            /// <param name="dt">원본 DataTable</param>
            /// <param name="colName">정렬할 컬럼명</param>
            /// <param name="direction">정렬 방향(asc, desc)</param>
            /// <returns></returns>
            public DataTable SetSort(DataTable dt, string colName, string direction)
            {
                return dt;
            }


            #region DataSetToXmlString Public Methods
            /// <summary>
            /// DataSet의 내용을 Xml 문자열로 변환한다.
            /// </summary>
            /// <param name="ds">변환할 DataSet</param>
            /// <param name="xmlResultInfo">DataTable별 처리결과</param>
            /// <returns></returns>
            public string DataSetToXmlString(DataSet ds, XmlResultInfo[] xmlResultInfo)
            {
                return GetXmlString(ds, xmlResultInfo);
            }

            public string DataSetToXmlString(DataSet ds, XmlResultInfo[] xmlResultInfo, string bindID)
            {
                this._bindid = bindID;
                return GetXmlString(ds, xmlResultInfo);
            }

            public string DataSetToXmlString(DataSet ds, XmlResultInfo[] xmlResultInfo, int[][] sumColIndex)
            {
                if (sumColIndex.Length > 0)
                    this._addSumRow = true;

                this._sumColIndex = sumColIndex;

                return GetXmlString(ds, xmlResultInfo);
            }

            public string DataSetToXmlString(DataSet ds, XmlResultInfo[] xmlResultInfo, string[][] colName, FormatType[][] formatType, string[][] formatString)
            {
                if (colName.Length > 0)
                    this._isFormat = true;

                this._colName = colName;
                this._formatType = formatType;
                this._formatString = formatString;

                return GetXmlString(ds, xmlResultInfo);
            }

            public string DataSetToXmlString(DataSet ds, XmlResultInfo[] xmlResultInfo, Boolean addSumRow, string[] colName, FormatType[] formatType, string[] formatString)
            {
                return GetXmlString(ds, xmlResultInfo);
            }
            #endregion

            #region ResultDataToXmlString Public Methods
            /// <summary>
            /// DataTable을 XML로 변환한다.
            /// </summary>
            /// <param name="dt">DataTable</param>
            /// <param name="xmlResultInfo">결과정보</param>
            /// <returns></returns>
            public string ResultDataToXmlStringBackUp(DataTable dt, XmlResultInfo xmlResultInfo)
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    return ResultDataToXmlString(xmlResultInfo);
                }

                StringBuilder strXml = new StringBuilder();
                StringBuilder strXmlSum = new StringBuilder();
                StringBuilder strXmlData = null;

                strXml.Append("<XML id=\"" + xmlResultInfo.BindID + "\" result=\"" + xmlResultInfo.Result + "\" resultType=\"" + xmlResultInfo.ResultType + "\" resultMessage=\"" + xmlResultInfo.ResultMessage + "\">");
                strXml.Append("<" + dt.TableName + ">");

                //합계처리
                if (xmlResultInfo.SumColumns.Count > 0)
                {
                    strXmlSum.Append("<Data>");

                    //RowIndex 추가
                    strXmlSum.Append("<RowIndex>합계</RowIndex>");

                    for (int iColS = 0; iColS < dt.Columns.Count; iColS++)
                    {
                        strXmlSum.Append("<" + dt.Columns[iColS].ColumnName + ">");

                        if (xmlResultInfo.SumColumns.Contains(dt.Columns[iColS].ColumnName))
                        {
                            if (xmlResultInfo.FormatType.ContainsKey(dt.Columns[iColS].ColumnName))
                                strXmlSum.Append(SetFormat(dt.Compute("Sum(" + dt.Columns[iColS].ColumnName + ")", "").ToString(), (GridHelper.FormatType)xmlResultInfo.FormatType[dt.Columns[iColS].ColumnName]));
                            else
                                strXmlSum.Append(dt.Compute("Sum(" + dt.Columns[iColS].ColumnName + ")", "").ToString());
                        }

                        strXmlSum.Append("</" + dt.Columns[iColS].ColumnName + ">");
                    }

                    strXmlSum.Append("</Data>");
                }

                strXmlData = new StringBuilder();
                //Data
                for (int iRow = 0; iRow < dt.Rows.Count; iRow++)
                {
                    strXmlData.Append("<Data>");

                    //RowIndex 추가
                    strXmlData.Append("<RowIndex>" + (iRow + 1).ToString() + "</RowIndex>");

                    for (int iCol = 0; iCol < dt.Columns.Count; iCol++)
                    {
                        strXmlData.Append("<" + dt.Columns[iCol].ColumnName + ">");

                        //Format 설정
                        if (xmlResultInfo.FormatType.ContainsKey(dt.Columns[iCol].ColumnName))
                            strXmlData.Append(SetFormat(dt.Rows[iRow][iCol].ToString(), (GridHelper.FormatType)xmlResultInfo.FormatType[dt.Columns[iCol].ColumnName]));
                        else
                            strXmlData.Append(dt.Rows[iRow][iCol].ToString());

                        strXmlData.Append("</" + dt.Columns[iCol].ColumnName + ">");
                    }

                    strXmlData.Append("</Data>");
                }

                strXml.Append(strXmlSum.ToString());    //Sum 추가
                strXml.Append(strXmlData.ToString());   //Data 추가

                strXml.Append("</" + dt.TableName + ">");
                strXml.Append("</XML>");


                return strXml.ToString();
            }


            public string ResultDataToXmlString(DataTable dt, XmlResultInfo xmlResultInfo)
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    return ResultDataToXmlString(xmlResultInfo);
                }

                StringBuilder strXml = new StringBuilder();
                StringBuilder strXmlSum = new StringBuilder();
                StringBuilder strXmlData = null;

                strXml.Append("<XML id=\"" + xmlResultInfo.BindID + "\" result=\"" + xmlResultInfo.Result + "\" resultType=\"" + xmlResultInfo.ResultType + "\" resultMessage=\"" + xmlResultInfo.ResultMessage + "\">");
                strXml.Append("<" + dt.TableName + ">");

                //합계처리
                if (xmlResultInfo.SumColumns.Count > 0)
                {
                    strXmlSum.Append("<Data>");

                    //RowIndex 추가
                    strXmlSum.Append("<RowIndex>합계</RowIndex>");

                    for (int iColS = 0; iColS < dt.Columns.Count; iColS++)
                    {
                        strXmlSum.Append("<" + dt.Columns[iColS].ColumnName + ">");

                        if (xmlResultInfo.SumColumns.Contains(dt.Columns[iColS].ColumnName))
                        {

                            if (xmlResultInfo.FormatType.ContainsKey(dt.Columns[iColS].ColumnName))
                                strXmlSum.Append(SetFormat(dt.Compute("Sum(" + dt.Columns[iColS].ColumnName + ")", "").ToString(), (GridHelper.FormatType)xmlResultInfo.FormatType[dt.Columns[iColS].ColumnName]));
                            else
                                strXmlSum.Append(dt.Compute("Sum(" + dt.Columns[iColS].ColumnName + ")", "").ToString());
                        }
                        else if (xmlResultInfo.AvgColumns.Contains(dt.Columns[iColS].ColumnName)) /*평균컬럼이 있을경우 [2014.07.21] Jang.jiyoug*/
                        {
                            if (xmlResultInfo.FormatType.ContainsKey(dt.Columns[iColS].ColumnName))
                                strXmlSum.Append(SetFormat(dt.Compute("Avg(" + dt.Columns[iColS].ColumnName + ")", "").ToString(), (GridHelper.FormatType)xmlResultInfo.FormatType[dt.Columns[iColS].ColumnName]));
                            else
                                strXmlSum.Append(dt.Compute("Avg(" + dt.Columns[iColS].ColumnName + ")", "").ToString());
                        }
                        else if (xmlResultInfo.SetComputeColumns.ContainsKey(dt.Columns[iColS].ColumnName)) /*지정된 Compute 컬럼이 있을경우 [2014.07.21] Jang.jiyoug*/
                        {
                            if (xmlResultInfo.FormatType.ContainsKey(dt.Columns[iColS].ColumnName))
                            {

                                double computeValue = 0;
                                double.TryParse(dt.Compute(xmlResultInfo.SetComputeColumns[dt.Columns[iColS].ColumnName].ToString(), "").ToString(), out computeValue); //double 로 parse
                                if (computeValue.ToString() == "Infinity") computeValue = 0; // 0으로 나눠 무한값을 가질때는 0으로 표시하도록 함 [2014.07.29] Jang.jiyoug
                                strXmlSum.Append(SetFormat(computeValue.ToString(), (GridHelper.FormatType)xmlResultInfo.FormatType[dt.Columns[iColS].ColumnName]));
                            }
                            else
                            {
                                strXmlSum.Append(dt.Compute(xmlResultInfo.SetComputeColumns[dt.Columns[iColS].ColumnName].ToString(), "").ToString());
                            }
                        }

                        if (dt.Columns[iColS].ColumnName.ToUpper() == "STATEROW")
                            strXmlSum.Append("S");

                        strXmlSum.Append("</" + dt.Columns[iColS].ColumnName + ">");
                    }

                    strXmlSum.Append("</Data>");
                }

                strXmlData = new StringBuilder();
                //Data
                for (int iRow = 0; iRow < dt.Rows.Count; iRow++)
                {
                    strXmlData.Append("<Data>");

                    //RowIndex 추가
                    strXmlData.Append("<RowIndex>" + (iRow + 1).ToString() + "</RowIndex>");

                    for (int iCol = 0; iCol < dt.Columns.Count; iCol++)
                    {
                        strXmlData.Append("<" + dt.Columns[iCol].ColumnName + ">");

                        //Format 설정
                        if (xmlResultInfo.FormatType.ContainsKey(dt.Columns[iCol].ColumnName))
                            strXmlData.Append(SetFormat(dt.Rows[iRow][iCol].ToString(), (GridHelper.FormatType)xmlResultInfo.FormatType[dt.Columns[iCol].ColumnName]));
                        else
                        {
                            //strXmlData.Append(dt.Rows[iRow][iCol].ToString());
                            if (dt.Rows[iRow][iCol].ToString().ToUpper().IndexOf("CDATA") == -1)
                                strXmlData.Append("<![CDATA[" + dt.Rows[iRow][iCol].ToString() + "]]>");
                            else
                                strXmlData.Append(dt.Rows[iRow][iCol].ToString());
                        }

                        strXmlData.Append("</" + dt.Columns[iCol].ColumnName + ">");
                    }

                    strXmlData.Append("</Data>");
                }

                strXml.Append(strXmlSum.ToString());    //Sum 추가
                strXml.Append(strXmlData.ToString());   //Data 추가

                strXml.Append("</" + dt.TableName + ">");
                strXml.Append("</XML>");


                return strXml.ToString();
            }
            /// <summary>
            /// 단일 결과 값을 XML로 변환한다.
            /// </summary>
            /// <param name="result">결과값</param>
            /// <param name="xmlResultInfo">결과정보</param>
            /// <returns></returns>
            public string ResultDataToXmlString(string result, XmlResultInfo xmlResultInfo)
            {
                StringBuilder strXml = new StringBuilder();

                if (xmlResultInfo.BindID.Trim().Length > 0)
                    this._bindid = xmlResultInfo.BindID.Trim();

                strXml.Append("<XML id=\"" + xmlResultInfo.BindID + "\" result=\"" + xmlResultInfo.Result + "\" resultType=\"" + xmlResultInfo.ResultType + "\" resultMessage=\"" + xmlResultInfo.ResultMessage + "\">");

                strXml.Append("<Table>");
                strXml.Append("<Data>");
                strXml.Append("<ReturnValue>");

                strXml.Append(result);

                strXml.Append("</ReturnValue>");
                strXml.Append("</Data>");
                strXml.Append("</Table>");

                strXml.Append("</XML>");

                return strXml.ToString();
            }

            /// <summary>
            /// ResultData가 없이 에러가 발생한 경우 XML 헤더만 전송한다.
            /// </summary>
            /// <param name="xmlResultInfo">결과정보</param>
            /// <returns></returns>
            public string ResultDataToXmlString(XmlResultInfo xmlResultInfo)
            {
                StringBuilder strXml = new StringBuilder();

                strXml.Append("<XML id=\"" + xmlResultInfo.BindID + "\" result=\"" + xmlResultInfo.Result + "\" resultType=\"" + xmlResultInfo.ResultType + "\" resultMessage=\"" + xmlResultInfo.ResultMessage + "\">");

                strXml.Append("</XML>");

                return strXml.ToString();
            }
            #endregion

            #region MakeDataTableFromRawData
            /// <summary>
            /// 구분자로 구성된 그리드 데이터에서 DataTable을 추출한다.
            /// </summary>
            /// <param name="gridSource">그리드 데이터 소스</param>
            /// <returns></returns>
            public DataTable MakeDataTableFromRawData(string gridHTMLSource)
            {
                string _gridHTMLSource = HttpUtility.UrlDecode(gridHTMLSource);

                string[] stringSeparators = new string[] { GridSchemaDelimiter };
                string[] stringSeparators2 = new string[] { GridRowDelimiter };
                string[] stringSeparators3 = new string[] { GridColumnDelimiter };
                string[] arrMain = _gridHTMLSource.Split(stringSeparators, StringSplitOptions.None);
                string[] arrHeader = arrMain[0].Split(stringSeparators3, StringSplitOptions.None);
                string[] arrContents = arrMain[1].Split(stringSeparators2, StringSplitOptions.None);

                DataTable dt = new DataTable();
                DataRow dr = null;

                //헤더 생성
                for (int i = 0; i < arrHeader.Length - 1; i++)
                {
                    dt.Columns.Add(new DataColumn(arrHeader[i]));
                }

                //Row 생성
                for (int j = 0; j < arrContents.Length - 1; j++)
                {
                    string[] arrRow = arrContents[j].Split(stringSeparators3, StringSplitOptions.None);
                    dr = dt.NewRow();

                    for (int k = 0; k < arrRow.Length - 1; k++)
                    {
                        dr[k] = arrRow[k].ToString();
                    }

                    dt.Rows.Add(dr);
                }

                return dt;
            }
            #endregion

            public string AddRootHeader(string xmlTables)
            {
                return "<Root>" + xmlTables + "</Root>";
            }

            private string GetXmlString(DataSet ds, XmlResultInfo[] xmlResultInfo)
            {
                StringBuilder strXml = new StringBuilder();
                StringBuilder strXmlData = null;
                StringBuilder strXmlSum = null;
                int[] iSum = null;

                strXml.Append("<Root>");

                for (int iTable = 0; iTable < ds.Tables.Count; iTable++)
                {
                    //1개의 DataTable당 1개의 XML 태그를 추가
                    strXml.Append("<XML id=\"" + _bindid + iTable.ToString() + "\" result=\"" + xmlResultInfo[iTable].Result + "\" resultType=\"" + xmlResultInfo[iTable].ResultType + "\" resultMessage=\"" + xmlResultInfo[iTable].ResultMessage + "\">");
                    strXml.Append("<" + ds.Tables[iTable].TableName + ">");

                    if (_addSumRow)
                        iSum = new int[ds.Tables[iTable].Columns.Count];

                    strXmlData = new StringBuilder();
                    //Data
                    for (int iRow = 0; iRow < ds.Tables[iTable].Rows.Count; iRow++)
                    {
                        strXmlData.Append("<Data>");

                        //RowIndex 추가
                        strXmlData.Append("<RowIndex>" + (iRow + 1).ToString() + "</RowIndex>");

                        for (int iCol = 0; iCol < ds.Tables[iTable].Columns.Count; iCol++)
                        {
                            strXmlData.Append("<" + ds.Tables[iTable].Columns[iCol].ColumnName + ">");

                            if (_isFormat)
                            {
                                strXmlData.Append(SetFormatString(ds.Tables[iTable].Rows[iRow][iCol].ToString(), ds.Tables[iTable].TableName, iRow, ds.Tables[iTable].Columns[iCol].ColumnName));
                            }
                            else
                            {
                                //strXmlData.Append(ds.Tables[iTable].Rows[iRow][iCol].ToString());
                                if (ds.Tables[iTable].Rows[iRow][iCol].ToString().ToUpper().IndexOf("<![CDATA[") == -1)
                                    strXmlData.Append("<![CDATA[" + ds.Tables[iTable].Rows[iRow][iCol].ToString() + "]]>");
                                else
                                    strXmlData.Append(ds.Tables[iTable].Rows[iRow][iCol].ToString());
                            }

                            strXmlData.Append("</" + ds.Tables[iTable].Columns[iCol].ColumnName + ">");

                            if (_addSumRow && _sumColIndex[iTable] != null && IsSumCol(iTable, iCol) && ds.Tables[iTable].Rows[iRow][iCol] != null)
                                iSum[iCol] += Int32.Parse(ds.Tables[iTable].Rows[iRow][iCol].ToString());
                        }

                        strXmlData.Append("</Data>");
                    }

                    strXmlSum = new StringBuilder();
                    //Sum
                    if (_addSumRow && _sumColIndex[iTable] != null && _sumColIndex[iTable].Length > 0 && _sumColIndex[iTable][0] != -1)
                    {
                        strXmlSum.Append("<Data>");

                        //RowIndex 추가
                        strXmlSum.Append("<RowIndex></RowIndex>");

                        for (int iCol = 0; iCol < ds.Tables[iTable].Columns.Count; iCol++)
                        {
                            strXmlSum.Append("<" + ds.Tables[iTable].Columns[iCol].ColumnName + ">");
                            strXmlSum.Append(iSum[iCol].ToString());
                            strXmlSum.Append("</" + ds.Tables[iTable].Columns[iCol].ColumnName + ">");
                        }

                        strXmlSum.Append("</Data>");
                    }

                    strXml.Append(strXmlSum.ToString());    //합계 추가
                    strXml.Append(strXmlData.ToString());   //Data 추가

                    strXml.Append("</" + ds.Tables[iTable].TableName + ">");
                    strXml.Append("</XML>");
                }

                strXml.Append("</Root>");

                return strXml.ToString();
            }

            private Boolean IsSumCol(int iTable, int iCol)
            {
                for (int i = 0; i < _sumColIndex[iTable].Length; i++)
                {
                    if (_sumColIndex[iTable][i] == iCol)
                        return true;
                }

                return false;
            }

            private string SetFormatString(string strValue, string tableName, int iRow, string colName)
            {
                //포맷타입과 포맷스트링을 찾는다.
                for (int i = 0; i < _colName.Length; i++)
                {
                    if (_colName[i][0] == tableName)
                    {
                        for (int j = 0; j < _colName[i].Length; j++)
                        {
                            if (_colName[i][j] == colName) //포맷스트링을 적용할 컬럼을 찾았다!!!
                            {
                                FormatType applyFormatType = _formatType[i][j - 1];
                                string applyFormatString = _formatString[i][j - 1];

                                //적용한다.
                                switch (applyFormatType)
                                {
                                    case FormatType.Number:
                                        return ConvertCurrency(strValue);
                                        break;
                                    default:
                                        return strValue;
                                        break;
                                }
                            }
                        }
                    }
                }

                return strValue;
            }

            public string SetFormat(string colValue, GridHelper.FormatType formatType)
            {
                try
                {
                    switch (formatType)
                    {
                        case FormatType.Card:
                            if (colValue.Length == 16)
                            {
                                //####-####-####-####
                                return colValue.Substring(0, 4) + "-" + colValue.Substring(4, 4) + "-" + colValue.Substring(8, 4) + "-" + colValue.Substring(12, 4);
                            }
                            else
                            {
                                return colValue;
                            }
                            break;

                        case FormatType.Custom:
                            break;

                        case FormatType.Date:
                            if (colValue.Length == 8)
                            {
                                //Date : ####-##-##
                                return colValue.Substring(0, 4) + "-" + colValue.Substring(4, 2) + "-" + colValue.Substring(6, 2);
                            }
                            else if (colValue.Length == 6)//YearMonth --> ####-## [Add By Yang.ChunJiang 2011-05-26]
                            {
                                return colValue.Substring(0, 4) + "-" + colValue.Substring(4, 2);
                            }
                            else
                            {
                                return DateTime.Parse(colValue).ToString("yyyy-MM-dd");
                            }
                            break;

                        case FormatType.Decimal:

                            try
                            {
                                if (string.IsNullOrEmpty(colValue)) return "0"; //NULL값일 경우 0으로 보여지도록 추가 [2015.01.12]
                                double.Parse(colValue);
                                //                        return String.Format("{0:0,000.00}", Decimal.Parse(colValue));
                                return String.Format("{0:#,##0.##}", double.Parse(colValue)); //소숫점은 DB에서 나오는대로 보여주도록 수정 [2014.08.28]
                            }
                            catch (Exception e)
                            {
                                return colValue;
                            }
                            break;

                        case FormatType.IDNo:
                            if (colValue.Length == 13)
                            {
                                //IDNo : ######-#######
                                return colValue.Substring(0, 6) + "-" + colValue.Substring(6, 7);
                            }
                            else
                            {
                                return colValue;
                            }
                            break;
                        case FormatType.ZipCode:
                            if (colValue.Length == 6)
                            {
                                //ZipCode : ###-###
                                return colValue.Substring(0, 3) + "-" + colValue.Substring(3, 3);
                            }
                            else
                            {
                                return colValue;
                            }
                            break;

                        case FormatType.Number:
                            try
                            {
                                if (string.IsNullOrEmpty(colValue)) return "0";
                                double.Parse(colValue);
                                if (colValue.IndexOf(".") > 0)
                                    return ConvertCurrency(colValue.Substring(0, colValue.IndexOf(".")));
                                else
                                    return ConvertCurrency(colValue);
                            }
                            catch (Exception e)
                            {
                                return colValue;
                            }

                            break;
                        case FormatType.Percent: /*Percent FormatType 정수 추가 [2014.08.28]*/

                            try
                            {
                                double.Parse(colValue);
                                return String.Format("{0:#,##0}%", double.Parse(colValue));
                            }
                            catch (Exception e)
                            {
                                return colValue;
                            }
                            break;
                        case FormatType.Percent1: /*Percent FormatType 소숫점 첫째자리 추가 [2014.11.17]*/

                            try
                            {
                                double.Parse(colValue);
                                return String.Format("{0:#,##0.0}%", double.Parse(colValue));
                            }
                            catch (Exception e)
                            {
                                return colValue;
                            }
                            break;

                        case FormatType.Percent2: /*Percent FormatType 소숫점 둘째자리 추가 [2014.08.28]*/

                            try
                            {
                                double.Parse(colValue);
                                return String.Format("{0:#,##0.00}%", double.Parse(colValue));
                            }
                            catch (Exception e)
                            {
                                return colValue;
                            }
                            break;

                        case FormatType.TelNo:

                            if (colValue.Length >= 9 && colValue.Substring(0, 2) == "02")
                            {
                                if (colValue.Length == 9)
                                {
                                    return colValue.Substring(0, 2) + "-" + colValue.Substring(2, 3) + "-" + colValue.Substring(5, 4);
                                }
                                else if (colValue.Length == 10)
                                {
                                    return colValue.Substring(0, 2) + "-" + colValue.Substring(2, 4) + "-" + colValue.Substring(6, 4);
                                }
                                else
                                {
                                    return colValue;
                                }
                            }
                            else
                            {
                                if (colValue.Length == 10)
                                {
                                    return colValue.Substring(0, 3) + "-" + colValue.Substring(3, 3) + "-" + colValue.Substring(6, 4);
                                }
                                else if (colValue.Length == 11)
                                {
                                    return colValue.Substring(0, 3) + "-" + colValue.Substring(3, 4) + "-" + colValue.Substring(7, 4);
                                }
                                else if (colValue.Length == 12)
                                {
                                    return colValue.Substring(0, 4) + "-" + colValue.Substring(4, 4) + "-" + colValue.Substring(8, 4);
                                }
                                else
                                {
                                    return colValue;
                                }
                            }

                            break;

                        default:
                            return colValue;
                            break;
                    }
                }
                catch
                {
                    return colValue;
                }

                return colValue;
            }
        }


        /// <summary>
        /// Xml 변환시 추가할 속성정보 정의용 클래스
        /// </summary>
        public class XmlResultInfo
        {
            private string _result; //S:성공, F:실패
            private string _resultType; //E:Error, I:Information, C:Confirm
            private string _resultMessage; //에러메세지
            private ReturnValue.ReturnValueType _returnValueType; //결과 데이터 타입
            private ArrayList _sumColumns = new ArrayList(); // 합계처리 컬럼
            private ArrayList _avgColumns = new ArrayList(); // 평균값처리 컬럼 추가 [2014.07.21] Jang.jiyoung
            private Hashtable _setComputeColumns = new Hashtable(); //합계 Row에 Compute 지정된컬럼 추가 [2014.07.21] Jang.jiyoung
            private Hashtable _sortColumns = new Hashtable(); // 정렬 컬럼
            private Hashtable _formatType = new Hashtable(); // 포맷 타입
            private Hashtable _formatString = new Hashtable(); // 포맷 문자열    
            private string _returnValue; //SingleValue String
            private string _bindID = "bind"; //Xml BindID

            public string Result
            {
                get { return _result; }
                set { _result = value; }
            }

            public string ResultType
            {
                get { return _resultType; }
                set { _resultType = value; }
            }

            public string ResultMessage
            {
                get { return _resultMessage; }
                set { _resultMessage = value; }
            }

            public ReturnValue.ReturnValueType ReturnValueType
            {
                get { return _returnValueType; }
                set { _returnValueType = value; }
            }

            public ArrayList SumColumns
            {
                get { return _sumColumns; }
                set { _sumColumns = value; }
            }

            public ArrayList AvgColumns
            {
                get { return _avgColumns; }
                set { _avgColumns = value; }
            }

            public Hashtable SetComputeColumns
            {
                get { return _setComputeColumns; }
                set { _setComputeColumns = value; }
            }

            public Hashtable FormatType
            {
                get { return _formatType; }
                set { _formatType = value; }
            }

            public Hashtable FormatString
            {
                get { return _formatString; }
                set { _formatString = value; }
            }

            public string ReturnValue
            {
                get { return _returnValue; }
                set { _returnValue = value; }
            }

            public string BindID
            {
                get { return _bindID; }
                set { _bindID = value; }
            }

            public Hashtable SortColumns
            {
                get { return _sortColumns; }
                set { _sortColumns = value; }
            }
        }

        public class ReturnValue
        {
            public enum ReturnValueType { RecordSet, SingleValue };
        }

        #region public static string ConvertCurrency(string currency)
        public static string ConvertCurrency(string currency)
        {
            if (currency == "0")
                return currency;
            else
                return String.Format("{0:#,#}", Double.Parse(currency));
        }
        #endregion
        #endregion #################### getJson / XML ##############################
    }
}
