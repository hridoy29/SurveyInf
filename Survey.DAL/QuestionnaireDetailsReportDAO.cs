using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Common;
using DbExecutor;
using SurveyEntity;
using Survey.Entity;

namespace SurveyDAL
{
    public class QuestionnaireDetailsReportDAO : IDisposable
    {
        private static volatile QuestionnaireDetailsReportDAO instance;
        private static readonly object lockObj = new object();
        public static QuestionnaireDetailsReportDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new QuestionnaireDetailsReportDAO();
            }
            return instance;
        }
        public static QuestionnaireDetailsReportDAO GetInstanceThreadSafe
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new QuestionnaireDetailsReportDAO();
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

        public QuestionnaireDetailsReportDAO()
        {
            //dbExecutor = DBExecutor.GetInstanceThreadSafe;
            dbExecutor = new DBExecutor();
        }

        public List<QuestionnaireDetailsReport> Get(DateTime toDate, int distributorId)
        {
            try
            {
                List<QuestionnaireDetailsReport> QuestionnaireDetailsReport_getLst = new List<QuestionnaireDetailsReport>();
                Parameters[] colparameters = new Parameters[2]{
                new Parameters("@paramDate", toDate, DbType.Date, ParameterDirection.Input),
                new Parameters("@distributorid", distributorId, DbType.Int32, ParameterDirection.Input)
                };
                QuestionnaireDetailsReport_getLst = dbExecutor.FetchData<QuestionnaireDetailsReport>(CommandType.StoredProcedure, "wsp_QuestionnaireDetailsReport_Get", colparameters);
                return QuestionnaireDetailsReport_getLst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<QuestionnaireDetailsReport> GetListByUserExport( DateTime toDate, int distributorId)
        {
            try
            {
                if(toDate==null)
                {
                   
                    toDate = Convert.ToDateTime("1993-01-01");
                }
                List<QuestionnaireDetailsReport> TRN_SurveyReports_getLst = new List<QuestionnaireDetailsReport>();
                Parameters[] colparameters = new Parameters[1]{
                  new Parameters("@paramDate", toDate, DbType.DateTime, ParameterDirection.Input)
                };
                 //TRN_SurveyReports_getLst = dbExecutor.FetchData<TRN_SurveyReports_Export_Infinigent_Formate>(CommandType.StoredProcedure, "wsp_SurveyReports_Get_By_Userid_bak_new", colparameters);
                TRN_SurveyReports_getLst = dbExecutor.FetchData<QuestionnaireDetailsReport>(CommandType.StoredProcedure, "wsp_QuestionnaireDetailsReport_Get", colparameters);

                return TRN_SurveyReports_getLst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TRN_SurveyReports_get> GetListByUserid(int id)
        {
            try
            {
                List<TRN_SurveyReports_get> TRN_SurveyReports_getLst = new List<TRN_SurveyReports_get>();
                Parameters[] colparameters = new Parameters[1]{
                new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
                  
                };
                TRN_SurveyReports_getLst = dbExecutor.FetchData<TRN_SurveyReports_get>(CommandType.StoredProcedure, "wsp_SurveyReports_Get_By_Userid_bak", colparameters);
                return TRN_SurveyReports_getLst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public List<TRN_SchemeAuditChild> GetImageLocation(string number)
        //{
        //    try
        //    {
        //        List<TRN_SchemeAuditChild> GetImageLocation_getList = new List<TRN_SchemeAuditChild>();
        //        Parameters[] colparameters = new Parameters[1]{
        //        new Parameters("@paramNumber", number, DbType.String, ParameterDirection.Input)
        //        };
        //        GetImageLocation_getList = dbExecutor.FetchData<TRN_SchemeAuditChild>(CommandType.StoredProcedure, "wsp_TRN_SchemeAuditChild_Get", colparameters);
        //        return GetImageLocation_getList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<LU_AIC> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
        //{
        //	try
        //	{
        //		List<LU_AIC> LU_AICLst = new List<LU_AIC>();
        //		Parameters[] colparameters = new Parameters[5]{
        //		new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
        //		new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
        //		new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
        //		new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
        //		new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
        //		};
        //              LU_AICLst = dbExecutor.FetchDataRef<LU_AIC>(CommandType.StoredProcedure, "wsp_LU_AIC_GetPaged", colparameters, ref rows);
        //		return LU_AICLst;
        //	}
        //	catch (Exception ex)
        //	{
        //		throw ex;
        //	}
        //}
        //public string Post(LU_AIC _LU_AIC, string transactionType)
        //{
        //	string ret = string.Empty;
        //	try
        //	{
        //		Parameters[] colparameters = new Parameters[8]{
        //		new Parameters("@paramId", _LU_AIC.Id, DbType.Int32, ParameterDirection.Input),
        //		new Parameters("@paramName", _LU_AIC.Name, DbType.String, ParameterDirection.Input),
        //		new Parameters("@paramCreatorId", _LU_AIC.CreatorId, DbType.Int32, ParameterDirection.Input),
        //		new Parameters("@paramCreationDate", _LU_AIC.CreationDate, DbType.DateTime, ParameterDirection.Input),
        //		new Parameters("@paramModifierId", _LU_AIC.ModifierId, DbType.Int32, ParameterDirection.Input),
        //		new Parameters("@paramModificationDate", _LU_AIC.ModificationDate, DbType.DateTime, ParameterDirection.Input),
        //		new Parameters("@paramIsActive", _LU_AIC.IsActive, DbType.Boolean, ParameterDirection.Input),
        //		new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
        //		};
        //		dbExecutor.ManageTransaction(TransactionType.Open);
        //		ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_AIC_Post", colparameters, true);
        //		dbExecutor.ManageTransaction(TransactionType.Commit);
        //	}
        //	catch (DBConcurrencyException except)
        //	{
        //		dbExecutor.ManageTransaction(TransactionType.Rollback);
        //		throw except;
        //	}
        //	catch (Exception ex)
        //	{
        //		dbExecutor.ManageTransaction(TransactionType.Rollback);
        //		throw ex;
        //	}
        //	return ret;
        //}
    }
}
