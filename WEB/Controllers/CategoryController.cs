using SurveyDAL;
using SurveyEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WEB.Controllers
{
    public class CategoryController : Controller
    {
        public JsonResult Get()
        {
            try
            {
                var list = Facade.LU_CategoryDAO.Get();
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

                var list = Facade.LU_CategoryDAO.GetDynamic(where, orderBy);
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public string Post(LU_Category obj, string transactionType)
        {
            string ret = string.Empty;

            try
            {
            
                ret = Facade.LU_CategoryDAO.Post(obj, transactionType);
                return ret;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}