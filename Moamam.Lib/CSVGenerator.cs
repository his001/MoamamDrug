using System.Text;
using System.Data;
using System.IO;

namespace Moamam.Lib
{
    public class CSVGenerator
    {
        static private string _fileName;

        static public int CreateCSV(DataSet ds, string outFileName)
        {
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return 0;

            _fileName = outFileName;

            int affectedCount = ds.Tables[0].Rows.Count;
            int colCount = ds.Tables[0].Columns.Count;

            StreamWriter sw = new StreamWriter(_fileName, false, Encoding.UTF8);
            try
            {
                //--- make header ---
                string headerStr = "";
                foreach (DataColumn col in ds.Tables[0].Columns)
                    headerStr += string.Format("{0},", col.ColumnName);
                //remove the last comma(,)
                headerStr = headerStr.Remove(headerStr.Length - 1);
                sw.WriteLine(headerStr);

                //--- make content ---
                string lineStr = "";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    lineStr = "";
                    for (int i = 0; i < colCount; i++)
                        lineStr += string.Format("{0}", row[i]).Replace(",", "|") + ",";

                    //remove the last comma(,)
                    lineStr = lineStr.Remove(lineStr.Length - 1);
                    sw.WriteLine(lineStr);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                sw.Close();
                sw.Dispose();
            }

            return affectedCount;
        }
    }
}
