﻿using Survey.Entity;
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
    public class QuestionnairePhysicalStocksReportController : Controller
    {
        public JsonResult Get(DateTime fromDate, DateTime toDate, int? distributorId = null, string distributorName = null, string surveyorName = null)
        {
            try
            {
                var list = Facade.QuestionnairePhysicalStocksReportDAO.Get(fromDate, toDate, distributorId, distributorName, surveyorName);
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
       
 
        public void getExport(DateTime fromDate, DateTime toDate, int? distributorId = null, string distributorName = null, string surveyorName = null)
        {

            List<ExpandableQuestionnairePhysicalStocksReport> tRN_SurveyReports_Get = new List<ExpandableQuestionnairePhysicalStocksReport>();
            List<ExpandableQuestionnairePhysicalStocksReport> tRN_SurveyReports_List = new List<ExpandableQuestionnairePhysicalStocksReport>();
            tRN_SurveyReports_Get = Facade.QuestionnairePhysicalStocksReportDAO.Get(fromDate, toDate, distributorId, distributorName, surveyorName);

            foreach (var item in tRN_SurveyReports_Get)
            {
                ExpandableQuestionnairePhysicalStocksReport physicalStocksReport = new ExpandableQuestionnairePhysicalStocksReport();
                physicalStocksReport = item;
                QuestionnairePhysicalStocksReport questionnairePhysicalStocksReport = item.ItemGroupWisePhysicalStockList.LastOrDefault();
                physicalStocksReport.SystemStockSum = questionnairePhysicalStocksReport.SystemStock;
                physicalStocksReport.BBDDamageSum = questionnairePhysicalStocksReport.BBDDamage;
                physicalStocksReport.PhysicalStockSum = questionnairePhysicalStocksReport.PhysicalStock;
                physicalStocksReport.DifferenceSum = questionnairePhysicalStocksReport.Difference;
                tRN_SurveyReports_List.Add(physicalStocksReport);
            }

            var gv = new GridView();
            gv.DataSource = tRN_SurveyReports_List;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
             
            Response.AddHeader("content-disposition", "attachment; filename=QuestionnaireDetailsReportExcel.xls");
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