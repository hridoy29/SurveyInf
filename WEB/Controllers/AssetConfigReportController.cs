using Survey.Entity;
using SurveyDAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.Controllers
{
    public class AssetConfigReportController : Controller
    {
        // GET: AssetConfigReport
        public JsonResult Get(int id)
        {
            try
            {
                var list = Facade.AssetConfigReportDAO.Get(id);
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetDynamic(string where, string orderBy)
        {
            try
            {
                var list = Facade.AssetConfigReportDAO.GetDynamic(where, orderBy);
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public void getExport(int userId, DateTime? fromDate, DateTime? toDate)
        {

            List<Asset_configReportsFormat> Asset_configReports_ = new List<Asset_configReportsFormat>();
            Asset_configReports_ = Facade.AssetConfigReportDAO.GetListByUserExport(userId, fromDate, toDate);
            var gv = new GridView();
            gv.DataSource = Asset_configReports_;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;

            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();

            //string path = @"D:\UserManager.xlsx";  //@getpathfromappconfig + "\\" + FileName + ".xlsx";
            //System.IO.FileInfo file = new System.IO.FileInfo(path);
            //string Outgoingfile = "UserManager" + ".xlsx";
            //if (file.Exists)
            //{
            //    Response.Clear();
            //    Response.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);
            //    Response.AddHeader("Content-Length", file.Length.ToString());
            //    Response.ContentType = "application/vnd.ms-excel";
            //    Response.WriteFile(file.FullName);

            //}
            //else
            //{
            //    Response.Write("This file does not exist.");
            //}
        }
    }
}