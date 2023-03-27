using Survey.Entity;
using SurveyDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WEB.Controllers
{
    public class AssetConfigController : Controller
    {
        public JsonResult Get()
        {
            try
            {
                var list = Facade.LU_Asset_ConfigDAO.Get();
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
                var list = Facade.LU_Asset_ConfigDAO.GetDynamic(where, orderBy);
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public string Post(LU_Asset_Config obj, string transactionType)
        {
            string ret = string.Empty;

            try
            {
                obj.CreationDate = DateTime.Now;
                obj.ModificationDate = DateTime.Now;
                ret = Facade.LU_Asset_ConfigDAO.Post(obj, transactionType);
                return ret;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}