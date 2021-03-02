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
            ConvertToDt convertToDt = new ConvertToDt();
            convertToDt.ConvertToDataTable(tRN_SurveyReports_Get);
        }
        //[HttpPost]
        //public string Post(LU_AIC obj, string transactionType)
        //{
        //    string ret = string.Empty;

        //    try
        //    {
        //        obj.CreatorId = 1;
        //        obj.ModifierId = 1;
        //        obj.CreationDate = DateTime.Now;
        //        obj.ModificationDate = DateTime.Now;
        //        ret = Facade.LU_AICDAO.Post(obj, transactionType);
        //        return ret;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
    }
}