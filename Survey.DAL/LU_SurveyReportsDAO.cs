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
	public class LU_SurveyReportsDAO : IDisposable
	{
		private static volatile LU_SurveyReportsDAO instance;
		private static readonly object lockObj = new object();
		public static LU_SurveyReportsDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_SurveyReportsDAO();
			}
			return instance;
		}
		public static LU_SurveyReportsDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_SurveyReportsDAO();
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

		public LU_SurveyReportsDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<TRN_SurveyReports_get> Get(Int32? id = null)
		{
			try
			{
				List<TRN_SurveyReports_get> TRN_SurveyReports_getLst = new List<TRN_SurveyReports_get>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
                TRN_SurveyReports_getLst = dbExecutor.FetchData<TRN_SurveyReports_get>(CommandType.StoredProcedure, "wsp_SurveyReports_Get_bak", colparameters);
				return TRN_SurveyReports_getLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		//public List<TRN_SurveyReports_get> GetDynamic(string whereCondition,string orderByExpression)
		//{
		//	try
		//	{
		//		List<TRN_SurveyReports_get> TRN_SurveyReports_getLst = new List<TRN_SurveyReports_get>();
		//		Parameters[] colparameters = new Parameters[2]{
		//		new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
		//		new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
		//		};
  //              TRN_SurveyReports_getLst = dbExecutor.FetchData<TRN_SurveyReports_get>(CommandType.StoredProcedure, "wsp_LU_AIC_GetDynamic", colparameters);
		//		return TRN_SurveyReports_getLst;
		//	}
		//	catch (Exception ex)
		//	{
		//		throw ex;
		//	}
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
