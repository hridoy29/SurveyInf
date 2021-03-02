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
using System.Data;
using OfficeOpenXml;
using WEB.Model;

namespace WEB.Controllers
{
    public class SurveyReportController : Controller
    {

        public JsonResult Get()
        {
            try
            {
                var list = Facade.LU_SurveyReportsDAO.Get();
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

        public void getExport(List<TRN_SurveyReports_get> tRN_SurveyReports_Get)
        {

            DownloadXLfile dl = new DownloadXLfile();
            dl.createXLfile(tRN_SurveyReports_Get);

        }
     


        //    //[HttpPost]
        //    //public string Post(LU_AIC obj, string transactionType)
        //    //{
        //    //    string ret = string.Empty;

        //    //    try
        //    //    {
        //    //        obj.CreatorId = 1;
        //    //        obj.ModifierId = 1;
        //    //        obj.CreationDate = DateTime.Now;
        //    //        obj.ModificationDate = DateTime.Now;
        //    //        ret = Facade.LU_AICDAO.Post(obj, transactionType);
        //    //        return ret;
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        return ex.Message;
        //    //    }
        //    //}
        //}

        //[HttpPost]
        //public FileResult getExport(List<TRN_SurveyReports_get> tRN_SurveyReports_Get)
        //{

        //    DataTable dt = new DataTable("Grid");
        //    dt.Columns.AddRange(new DataColumn[4] { new DataColumn("AICName"),
        //                                    new DataColumn("ASMName"),
        //                                    new DataColumn("ChallanAmount"),
        //                                    new DataColumn("CommentDetails") });

        //    var customers = from customer in tRN_SurveyReports_Get.Take(10)
        //                    select customer;

        //    foreach (var customer in customers)
        //    {
        //        dt.Rows.Add(customer.AICName, customer.ASMName, customer.ChallanAmount, customer.CommentDetails);
        //    }

        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        wb.Worksheets.Add(dt);
        //        using (MemoryStream stream = new MemoryStream())
        //        {
        //            wb.SaveAs(stream);
        //            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
        //        }
        //    }
        //}
    }
}