using Survey.Entity;
using SurveyDAL;
using SurveyEntity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using WEB.Model;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Office.Interop.Excel;

namespace WEB.Controllers
{
    public class QuestionnaireObservationReportController : Controller
    {
        public JsonResult Get(DateTime todate,int distributorId)
        {
            try
            {
                var list = Facade.QuestionnaireObservationReportDAO.Get(todate, distributorId);
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetImageLocation(string number)
        {
            try
            {

                var list = Facade.LU_SurveyReportsDAO.GetImageLocation(number);
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
       
 
        public void getExport(DateTime todate, int distributorId)
        {

            List<QuestionnaireObservationReport> tRN_SurveyReports_Get = new List<QuestionnaireObservationReport>();
            tRN_SurveyReports_Get = Facade.QuestionnaireObservationReportDAO.Get(todate, distributorId);
            var gv = new GridView();
            gv.DataSource = tRN_SurveyReports_Get;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
             
            Response.AddHeader("content-disposition", "attachment; filename=QuestionnaireObservationReportExcel.xls");
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