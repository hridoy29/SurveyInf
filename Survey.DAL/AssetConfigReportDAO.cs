using DbExecutor;
using Survey.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Survey.DAL
{
    public class AssetConfigReportDAO
    {
        private static volatile AssetConfigReportDAO instance;
        private static readonly object lockObj = new object();
        public static AssetConfigReportDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new AssetConfigReportDAO();
            }
            return instance;
        }
        public static AssetConfigReportDAO GetInstanceThreadSafe
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new AssetConfigReportDAO();
                        }
                    }
                }
                return instance;
            }
        }

        public void Dispose()
        {
            ((IDisposable)GetInstanceThreadSafe).Dispose();
        }

        DBExecutor dbExecutor;

        public AssetConfigReportDAO()
        {
            //dbExecutor = DBExecutor.GetInstanceThreadSafe;
            dbExecutor = new DBExecutor();
        }

        public List<Asset_configReports> Get(int? id)
        {
            try
            {
                List<Asset_configReports> Asset_configReportsLst = new List<Asset_configReports>();
                Parameters[] colparameters = new Parameters[1]{
                new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
                };
                Asset_configReportsLst = dbExecutor.FetchData<Asset_configReports>(CommandType.StoredProcedure, "rpt_LU_AssetConfig", colparameters);
                return Asset_configReportsLst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Asset_configReports> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder,int id, ref int rows)
        {
            try
            {
                List<Asset_configReports> Asset_configReportlst = new List<Asset_configReports>();
                Parameters[] colparameters = new Parameters[6]{
                new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
                new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
                new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
                new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
                new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
                new Parameters("@UserId", id, DbType.Int32, ParameterDirection.Input),
                };
                Asset_configReportlst = dbExecutor.FetchDataRef<Asset_configReports>(CommandType.StoredProcedure, "rpt_LU_AssetConfig_GetPaged", colparameters, ref rows);
                
                return Asset_configReportlst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Asset_configReports> GetDynamic(string whereCondition, string orderByExpression)
        {
            try
            {
                List<Asset_configReports> LU_Asset_ConfigLst = new List<Asset_configReports>();
                Parameters[] colparameters = new Parameters[2]{
                new Parameters("@WhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
                new Parameters("@OrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
                };
                LU_Asset_ConfigLst = dbExecutor.FetchData<Asset_configReports>(CommandType.StoredProcedure, "rpt_LU_AssetConfig_GetDynamic", colparameters);
                return LU_Asset_ConfigLst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Asset_configReportsFormat> GetReportDynamic(string whereCondition, string orderByExpression,int id)
        {
            try
            {
                List<Asset_configReportsFormat> LU_Asset_ConfigLst = new List<Asset_configReportsFormat>();
                Parameters[] colparameters = new Parameters[3]{
                new Parameters("@WhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
                new Parameters("@OrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
                new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input),
                };
                LU_Asset_ConfigLst = dbExecutor.FetchData<Asset_configReportsFormat>(CommandType.StoredProcedure, "rpt_LU_AssetConfig_Report_GetDynamic", colparameters);
                return LU_Asset_ConfigLst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Asset_configReportsFormat> GetListByUserExport(int id, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                if (fromDate == null && toDate == null)
                {
                    fromDate = Convert.ToDateTime("1993-01-01");
                    toDate = Convert.ToDateTime("1993-01-01");
                }
                List<Asset_configReportsFormat> Asset_configReportsLst = new List<Asset_configReportsFormat>();
                Parameters[] colparameters = new Parameters[3]{
                new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input),
                 new Parameters("@paramfromDate", fromDate, DbType.DateTime, ParameterDirection.Input),
                  new Parameters("@paramtoDate", toDate, DbType.DateTime, ParameterDirection.Input)
                };
                //Asset_configReportsLst = dbExecutor.FetchData<TRN_SurveyReports_Export_Infinigent_Formate>(CommandType.StoredProcedure, "wsp_SurveyReports_Get_By_Userid_bak_new", colparameters);
                Asset_configReportsLst = dbExecutor.FetchData<Asset_configReportsFormat>(CommandType.StoredProcedure, "rpt_LU_AssetConfig_ExcelFormat", colparameters);

                return Asset_configReportsLst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Asset_configReports> GetListByUserid(int id)
        {
            try
            {
                List<Asset_configReports> Asset_configReportsLst = new List<Asset_configReports>();
                Parameters[] colparameters = new Parameters[1]{
                new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)

                };
                Asset_configReportsLst = dbExecutor.FetchData<Asset_configReports>(CommandType.StoredProcedure, "wsp_SurveyReports_Get_By_Userid_bak", colparameters);
                return Asset_configReportsLst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
