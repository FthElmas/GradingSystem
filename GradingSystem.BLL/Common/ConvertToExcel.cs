using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
namespace GradingSystem.BLL.Common
{
    public class ConvertToExcel
    {
        public ConvertToExcel()
        {
            string connectionString = "server = .; Database = GradingSystemDB; Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                
                string sqlQuery1 = "SELECT Sutun1, Sutun2 FROM Tablo1"; // İlk tablo adı ve sütunları
                string sqlQuery2 = "SELECT Sutun3, Sutun4 FROM Tablo2"; // İkinci tablo adı ve sütunları

                SqlDataAdapter dataAdapter1 = new SqlDataAdapter(sqlQuery1, connection);
                SqlDataAdapter dataAdapter2 = new SqlDataAdapter(sqlQuery2, connection);

                DataTable dataTable1 = new DataTable();
                DataTable dataTable2 = new DataTable();

                dataAdapter1.Fill(dataTable1);
                dataAdapter2.Fill(dataTable2);

                connection.Close();

                
                DataTable selectedDataTable = new DataTable();

                
                foreach (DataColumn column in dataTable1.Columns)
                {
                    selectedDataTable.Columns.Add(column.ColumnName, column.DataType);
                }

                
                foreach (DataColumn column in dataTable2.Columns)
                {
                    selectedDataTable.Columns.Add(column.ColumnName, column.DataType);
                }

                
                foreach (DataRow row in dataTable1.Rows)
                {
                    DataRow newRow = selectedDataTable.NewRow();
                    foreach (DataColumn column in dataTable1.Columns)
                    {
                        newRow[column.ColumnName] = row[column.ColumnName];
                    }
                    selectedDataTable.Rows.Add(newRow);
                }

                
                foreach (DataRow row in dataTable2.Rows)
                {
                    DataRow newRow = selectedDataTable.NewRow();
                    foreach (DataColumn column in dataTable2.Columns)
                    {
                        newRow[column.ColumnName] = row[column.ColumnName];
                    }
                    selectedDataTable.Rows.Add(newRow);
                }

                
                ExportToExcel(selectedDataTable, "veri.xlsx"); // Excel dosyası adını güncelleyin
            }
        }
        static void ExportToExcel(DataTable dataTable, string excelFilePath)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;

            // Excel tablosuna veriyi yaz
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    worksheet.Cells[i + 1, j + 1] = dataTable.Rows[i][j].ToString();
                }
            }

            
            workbook.SaveAs(excelFilePath);
            workbook.Close();
            excelApp.Quit();

            //interop service dışında daha farklı bir service bul
            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

            Console.WriteLine("Excel dosyası oluşturuldu: " + Path.GetFullPath(excelFilePath));
        }
    }
}
