/* Excel Data Helper 0.11b is a set of functions that can help you in consuming
 * data in a excel spreadsheet.
 * 
 * You can obtain the latest version of this library from <A href="http://www.keithrull.com/">http://www.keithrull.com</A>
 * 
 * Copyright (C) 2006  Keith Rull [ keith.rull[at]gmail.com ]
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.


 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the 
 * Free Software Foundation, Inc., 
 * 51 Franklin Street, Fifth Floor, 
 * Boston, MA  02110-1301, USA.
* */
using System;
using System.Data;
using System.Text;
using System.Data.OleDb;

namespace Moamam.Lib
{
    /// <summary>
    /// ExcelDataHelper is a set of functions that can help you in consuming data in a excel spreadsheet
    /// </summary>
    public sealed class ExcelDataHelper
    {
        private ExcelDataHelper() { }

        //excle 2000 - 2003
        //static string oledbProviderString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
        //excel 2007
        static string oledbReadProviderString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";
        static string oledbWriteProviderString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes\"";

        public static void Export(DataTable dt, string excelFilePath, string tableName)
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(string.Format(oledbWriteProviderString, excelFilePath)))
                {
                    con.Open();
                    StringBuilder strSQL = new StringBuilder();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = con;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        strSQL.Length = 0;
                        strSQL.Capacity = 0;

                        StringBuilder strvalue = new StringBuilder();
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            strvalue.Append("'" + dt.Rows[i][j].ToString().Replace("'", "") + "'");
                            if (j != dt.Columns.Count - 1)
                            {
                                strvalue.Append(",");
                            }
                        }
                        cmd.CommandText = strSQL.Append("INSERT INTO [" + tableName + "]")
                            .Append(" VALUES (").Append(strvalue).Append(")").ToString();
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch
            {
                throw;
            }

            /*
            try
            {
                using (OleDbConnection con = new OleDbConnection(string.Format(oledbProviderString, excelFilePath)))
                {
                    con.Open();
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.Append("CREATE TABLE ").Append("[" + tableName + "]");
                    strSQL.Append("(");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        strSQL.Append("[" + dt.Columns[i].ColumnName + "] text,");
                    }
                    strSQL = strSQL.Remove(strSQL.Length - 1, 1);
                    strSQL.Append(")");

                    OleDbCommand cmd = new OleDbCommand(strSQL.ToString(), con);
                    cmd.ExecuteNonQuery();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        strSQL.Length = 0;
                        strSQL.Capacity = 0;

                        StringBuilder strfield = new StringBuilder();
                        StringBuilder strvalue = new StringBuilder();
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            strfield.Append("[" + dt.Columns[j].ColumnName + "]");
                            strvalue.Append("'" + dt.Rows[i][j].ToString().Replace("'", "") + "'");
                            if (j != dt.Columns.Count - 1)
                            {
                                strfield.Append(",");
                                strvalue.Append(",");
                            }
                            else
                            {
                            }
                        }
                        cmd.CommandText = strSQL.Append(" INSERT INTO [" + tableName + "]( ")
                            .Append(strfield.ToString())
                            .Append(") values (").Append(strvalue).Append(")").ToString();
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch
            {
                throw;
            }
            */
        }

        /// <summary>
        /// a method that retrieves the worksheet names from the specified excel worksheet
        /// </summary>
        /// <param name="excelFilePath">excel document path</param>
        /// <returns>an array of worksheet names</returns>
        public static string[] WorkSheetNames(string excelFilePath)
        {
            //an array that would hold the extracted worksheet names
            string[] workSheetNames;
            //create a connection to the excel worksheet
            using (OleDbConnection oledbConnection = new OleDbConnection(string.Format(oledbReadProviderString, excelFilePath)))
            {
                //open the connection
                oledbConnection.Open();
                // Get all of the Table names from the Excel workbook 
                DataTable dataTable = oledbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                //oledbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                //specify the dimension of the array
                workSheetNames = new string[dataTable.Rows.Count];
                //Add the Table name to the string array. 
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    //append to the array the worksheet names
                    workSheetNames[i] = (string)dataTable.Rows[i]["TABLE_NAME"];
                }
                //close the connection
                oledbConnection.Close();
            }
            //return the array
            return workSheetNames;
        }


        /// <summary>
        /// Get columns in the specified excel document
        /// </summary>
        /// <param name="excelFilePath">excel document path</param>
        /// <returns>an array of colum names</returns>
        public static string[] ColumnNamesArray(string excelFilePath)
        {
            //Get the columnames from the Excel spreadsheet
            DataColumnCollection columCollection = ColumnNamesCollection(excelFilePath);


            //an array that would hold the column names
            string[] columNames = new string[columCollection.Count];
            //iterate thru all the columns in our DataColumnCollection
            foreach (DataColumn column in columCollection)
            {
                //specify the index of the string in our array and the value of the column name
                columNames[column.Ordinal] = column.ColumnName;
            }


            //return the column names
            return columNames;
        }


        /// <summary>
        /// Get columns in the specified excel document
        /// </summary>
        /// <param name="excelFilePath">excel document path</param>
        /// <returns>a DataColumCollection colum names</returns>
        public static DataColumnCollection ColumnNamesCollection(string excelFilePath)
        {
            return ColumnNamesCollection(excelFilePath, "Sheet1$");
        }


        /// <summary>
        /// Get columns in the specified excel document and spreadsheet
        /// </summary>
        /// <param name="excelFilePath">excel document path</param>
        /// <param name="sheetName">sheetname to read</param>
        /// <returns>a DataColumCollection colum names</returns>
        public static DataColumnCollection ColumnNamesCollection(string excelFilePath, string sheetName)
        {
            DataColumnCollection columNames;
            //create a new connection to the specified excel path
            using (OleDbConnection oledbConnection = new OleDbConnection(string.Format(oledbReadProviderString, excelFilePath)))
            {
                string sqlStatement = "Select top 1 * from [" + sheetName + "]";
                //open the connection
                oledbConnection.Open();


                //Create an OleDbDataAdapter for our connection
                OleDbDataAdapter adapter = new OleDbDataAdapter(sqlStatement, oledbConnection);


                //Create a DataTable for our data
                DataTable table = new DataTable();


                adapter.Fill(table);


                columNames = table.Columns;


                //close the connection
                oledbConnection.Close();
            }
            //return the column names
            return columNames;
        }


        /// <summary>
        /// Converts the Excel Spreadsheet to Dataset
        /// </summary>
        /// <param name="excelFilePath"></param>
        /// <returns>a dataset containing all the worksheets inside the spreadsheet</returns>
        public static DataSet ExcelToDataSet(string excelFilePath)
        {
            //get an array of the worksheetnames in our spreadsheet
            string[] workSheets = ExcelDataHelper.WorkSheetNames(excelFilePath);
            //create a new dataset
            DataSet dataSet = new DataSet();
            //iterate thru all the worksheetnames is our array
            foreach (string workSheet in workSheets)
            {
                //create a new DataTable specifying the DataTable name
                DataTable dataTable = new DataTable(workSheet);
                //Get the contents of our worksheet and put it into a DataTable
                dataTable = ExcelToDataTable(excelFilePath, workSheet);
                //Add our DataTable to our DataSet
                dataSet.Tables.Add(dataTable);
            }
            // return our DataSet
            return dataSet;
        }


        /// <summary>
        /// Converts the Excel Spreadsheet into a DataTable
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>This returns the first sheet into a datatable</returns>
        public static DataTable ExcelToDataTable(string filePath)
        {
            //return our DataTable
            return ExcelToDataTable(filePath, 0);
        }


        /// <summary>
        /// Converts the Excel Spreadsheet into a DataTable
        /// </summary>
        /// <param name="filePath">excel file path</param>
        /// <param name="index">the index of the worksheet to return</param>
        /// <returns>a datatable containing the values form the specied index in the spreadsheet</returns>
        public static DataTable ExcelToDataTable(string filePath, int index)
        {
            //return our DataTable
            return ExcelToDataSet(filePath).Tables[index];
        }


        /// <summary>
        /// Converts the Excel spreadsheet into a DataTable
        /// </summary>
        /// <param name="filePath">excel file path</param>
        /// <param name="workSheet">worksheet name</param>
        /// <returns>a datatable containing the values form the specified worksheetname in the spreadsheet</returns>
        public static DataTable ExcelToDataTable(string filePath, string workSheet)
        {
            //Create a new table named after the worksheet
            DataTable dataTable = new DataTable(workSheet);
            //Create a new connection to out Excel spreadsheet
            using (OleDbConnection oledbConnection = new OleDbConnection(string.Format(oledbReadProviderString, filePath)))
            {
                //the SQL stament to use in fetching the records from the excel spreadsheet
                string sqlStatement = @"SELECT * FROM [" + workSheet + "]";
                //open our connection to the spreadsheet
                oledbConnection.Open();
                try
                {
                    //create an OledbAdapter that we will use to to access the data.
                    OleDbDataAdapter adapter = new OleDbDataAdapter(sqlStatement, oledbConnection);
                    //fill the datable with the values
                    adapter.Fill(dataTable);
                }
                catch { }
                //close the oledb connection
                oledbConnection.Close();
            }
            //return our DataTable
            return dataTable;
        }
    }
}