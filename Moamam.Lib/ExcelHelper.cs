using System;
using System.Data;

using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;

namespace Moamam.Lib
{
    public class ExcelHelper
    {
        IWorkbook _workBook;
        ISheet _sheet;

        public void ExportExcel(string path, DataTable dt, string fileName, string[] HeaderList, string sheetName)
        {
            try
            {
                InitializeExcel(sheetName);
                GenerateHeader(dt, HeaderList);
                GenerateData(dt);
                if (string.IsNullOrEmpty(path))
                {
                    WebWriteToFile(fileName);
                }
                else
                {
                    WriteToFile(path);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void InitializeExcel(string sheetName)
        {
            _workBook = new HSSFWorkbook();
            _sheet = _workBook.CreateSheet(sheetName);
        }

        void GenerateHeader(DataTable dt, string[] HeaderList)
        {
            IRow row = _sheet.CreateRow(0);

            // Create Header Style 
            ICellStyle style = _workBook.CreateCellStyle();
            style.FillForegroundColor = HSSFColor.GREY_25_PERCENT.index;
            style.FillPattern = FillPatternType.SOLID_FOREGROUND;

            style.Alignment = HorizontalAlignment.CENTER;

            style.BorderLeft = BorderStyle.THIN;
            style.BorderRight = BorderStyle.THIN;
            style.BorderTop = BorderStyle.THIN;
            style.BorderBottom = BorderStyle.THIN;
            //create a font style
            IFont font = _workBook.CreateFont();
            font.FontHeightInPoints = 10;
            style.SetFont(font);

            // Build Header 
            int index = 0;
            foreach (DataColumn column in dt.Columns)
            {
                ICell cell = row.CreateCell(column.Ordinal);
                cell.SetCellValue(HeaderList[index]);
                cell.CellStyle = style;

                index++;
            }

            for (int i = 0; i < row.LastCellNum; i++)
            {
                _sheet.AutoSizeColumn(i);
            }
        }

        void GenerateData(DataTable dt)
        {
            int rowIndex = 1;
            IRow row = null;

            // Create the format instance
            IDataFormat format = _workBook.CreateDataFormat();

            // Create currency Style 
            ICellStyle style1 = _workBook.CreateCellStyle();
            style1.DataFormat = format.GetFormat("#,##0원");

            // Create data string Style
            ICellStyle style2 = _workBook.CreateCellStyle();
            style2.DataFormat = format.GetFormat("yyyy-m-d");

            //태두리 지정
            ICellStyle style = _workBook.CreateCellStyle();
            style.BorderLeft = BorderStyle.THIN;
            style.BorderRight = BorderStyle.THIN;
            style.BorderTop = BorderStyle.THIN;
            style.BorderBottom = BorderStyle.THIN;
            // Build Details (rows) 
            foreach (DataRow dRow in dt.Rows)
            {
                // Create new row in sheet 
                row = _sheet.CreateRow(rowIndex);

                foreach (DataColumn column in dt.Columns)
                {
                    ICell cell = row.CreateCell(column.Ordinal);
                    cell.CellStyle = style;

                    switch (column.DataType.FullName)
                    {
                        case "System.String":
                            cell.SetCellValue(dRow[column].ToString());
                            break;

                        case "System.Int":
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Double":
                        case "System.Decimal":
                            if (dRow[column].ToString() != "")
                                cell.SetCellValue(Convert.ToDouble(dRow[column].ToString()));
                            else
                                cell.SetCellValue(dRow[column].ToString());

                            break;

                        case "System.DateTime":
                            if (dRow[column].ToString() != "")
                                cell.SetCellValue(Convert.ToDateTime(dRow[column].ToString()));
                            else
                                cell.SetCellValue(dRow[column].ToString());

                            break;

                        default:
                            cell.SetCellValue(dRow[column].ToString());
                            break;
                    }
                }

                rowIndex++;
            }
        }

        void WebWriteToFile(string fileName)
        {
            using (var exportData = new MemoryStream())
            {
                _workBook.Write(exportData);
                string saveAsFileName = string.Format("{0}.xls", System.Web.HttpUtility.UrlEncode(fileName));

                System.Web.UI.Page page = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;

                page.Response.ContentType = "application/vnd.ms-excel";
                page.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                page.Response.Clear();
                page.Response.BinaryWrite(exportData.GetBuffer());
                page.Response.End();
            }
        }

        void WriteToFile(string path)
        {
            //Write the stream data of workbook to the root directory
            FileStream file = new FileStream(path, FileMode.Create);
            _workBook.Write(file);
            file.Close();
        }
    }
}