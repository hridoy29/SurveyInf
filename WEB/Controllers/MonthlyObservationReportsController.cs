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
    public class MonthlyObservationReportsController : Controller
    {
        public JsonResult Get_Distributor_Count(DateTime toDate)
        {
            try
            {
                MonthlyObservationReports monthlyObservationReports = new MonthlyObservationReports();

                monthlyObservationReports.monthlyObservationReports_Get_Distributor_Count = Facade.MonthlyObservationReportsDAO.Get_Distributor_count(toDate);
                monthlyObservationReports.monthlyObservationReports_Get_DistributorInfo_For_Stock_Variation= Facade.MonthlyObservationReportsDAO.Get_Distributor_Info_for_Stock_variation(toDate);
                monthlyObservationReports.monthlyObservationReports_Get_Distributor_CCBBL_Observations = Facade.MonthlyObservationReportsDAO.Get_Distributor_CCBBL_Observation(toDate);
                monthlyObservationReports.monthlyObservationReports_Get_Distributor_Infos = Facade.MonthlyObservationReportsDAO.Get_Distributor_Info(toDate);
                foreach (var distributor in monthlyObservationReports.monthlyObservationReports_Get_Distributor_Infos)
                {
                    distributor.monthlyObservationReports_Get_Distributor_StockDetails = Facade.MonthlyObservationReportsDAO.Get_Distributor_StockDetails(toDate, distributor.Id);
                    distributor.monthlyObservationReports_Get_Distributor_CCBBL_Statuses = Facade.MonthlyObservationReportsDAO.Get_Distributor_CCBBL_Status(toDate, distributor.Id);
                }           

                //var list = Facade.MonthlyObservationReportsDAO.Get_Distributor_count(toDate);

                string contentType = "application/json";
                return Json(monthlyObservationReports, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}