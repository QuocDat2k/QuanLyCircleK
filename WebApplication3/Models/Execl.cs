using System;

using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;

namespace QuanLy_CircleK.Models
{
    public class Execl
	{
		private string messager;
		[HttpPost]
		private string UploadExcelFile(HttpPostedFileBase file)
		{
			//dat ten cho file
			string _FileName = "File Name";
			//duong dan luu file

			/*string _path = Path.Combine(Server.MapPath("Excel"), _FileName);*/
			string _path = HttpContext.Current.Server.MapPath(_FileName);

			

			//luu file len server
			file.SaveAs(_path);
			return messager = "upload file thanh cong";

		}
		public ActionResult DownloadFile()
		{
			//duong dan chua file muon download
			string path = AppDomain.CurrentDomain.BaseDirectory + "Excel/";
			//chuyen file sang dang byte
			byte[] fileBytes = System.IO.File.ReadAllBytes(path + "FileMau.xlsx");
			//ten file khi download ve
			string fileName = "FileDownLoad.xlsx";
			//tra ve file
			return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
		}

		private ActionResult File(byte[] fileBytes, string octet, string fileName)
		{
			throw new NotImplementedException();
		}





		public DataTable ReadDataFromExcelFile(string filepath)

		{
			string connectionString = "";
			string fileExtention = filepath.Substring(filepath.Length - 4).ToLower();
			if (fileExtention.IndexOf("xlsx") == 0)
			{
				connectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + filepath + ";Extended Properties=\"Excel 12.0 Xml;HDR=NO\"";
			}
			else if (fileExtention.IndexOf("xls") == 0)
			{
				connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties=Excel 8.0";
			}


			// Tạo đối tượng kết nối
			OleDbConnection oledbConn = new OleDbConnection(connectionString);
			DataTable data = null;
			try
			{
				// Mở kết nối
				oledbConn.Open();

				// Tạo đối tượng OleDBCommand và query data từ sheet có tên "Sheet1"
				OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn);

				// Tạo đối tượng OleDbDataAdapter để thực thi việc query lấy dữ liệu từ tập tin excel
				OleDbDataAdapter oleda = new OleDbDataAdapter();

				oleda.SelectCommand = cmd;

				// Tạo đối tượng DataSet để hứng dữ liệu từ tập tin excel
				DataSet ds = new DataSet();

				// Đổ đữ liệu từ tập excel vào DataSet
				oleda.Fill(ds);

				data = ds.Tables[0];
			}
			catch
			{
			}
			finally
			{
				// Đóng chuỗi kết nối
				oledbConn.Close();
			}
			return data;
		}

		private static void CopyDataByBulk(DataTable dt)
		{
			//lay ket noi voi database luu trong file webconfig
			SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB Context"].ConnectionString);
			SqlBulkCopy bulkcopy = new SqlBulkCopy(con);
			bulkcopy.DestinationTableName = "Ten table";
			bulkcopy.ColumnMappings.Add(0, "ten cot 1");
			bulkcopy.ColumnMappings.Add(1, "ten cot 2");
			bulkcopy.ColumnMappings.Add(2, "ten cot 3");
			con.Open();
			bulkcopy.WriteToServer(dt);
			con.Close();
		}
	}

}		
	
