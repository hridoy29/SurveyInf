using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Common;
using DbExecutor;
using SurveyEntity;

namespace SurveyDAL
{
	public class LU_IssuesDAO : IDisposable
	{
		private static volatile LU_IssuesDAO instance;
		private static readonly object lockObj = new object();
		public static LU_IssuesDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_IssuesDAO();
			}
			return instance;
		}
		public static LU_IssuesDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_IssuesDAO();
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

		public LU_IssuesDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_Issues> Get(Int32? id = null)
		{
			try
			{
				List<LU_Issues> LU_IssuesList = new List<LU_Issues>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@Id", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_IssuesList = dbExecutor.FetchData<LU_Issues>(CommandType.StoredProcedure, "wsp_LU_Issues_Get", colparameters);
				return LU_IssuesList;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_Issues> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_Issues> LU_IssuesList = new List<LU_Issues>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_IssuesList = dbExecutor.FetchData<LU_Issues>(CommandType.StoredProcedure, "wsp_LU_Issues_GetDynamic", colparameters);
				return LU_IssuesList;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_Issues> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_Issues> LU_IssuesLst = new List<LU_Issues>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_IssuesLst = dbExecutor.FetchDataRef<LU_Issues>(CommandType.StoredProcedure, "wsp_LU_Issues_GetPaged", colparameters, ref rows);
				return LU_IssuesLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_Issues _LU_Issues, string transactionType)
		{
			string ret = string.Empty;
			try
			{

			
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@paramId", _LU_Issues.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramName", _LU_Issues.Name, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_Issues.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_Issues.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_Issues_Post", colparameters, true);
				dbExecutor.ManageTransaction(TransactionType.Commit);
			}
			catch (DBConcurrencyException except)
			{
				dbExecutor.ManageTransaction(TransactionType.Rollback);
				throw except;
			}
			catch (Exception ex)
			{
				dbExecutor.ManageTransaction(TransactionType.Rollback);
				throw ex;
			}
			return ret;
		}
	}
}
