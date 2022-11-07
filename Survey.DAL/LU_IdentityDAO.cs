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
	public class LU_IdentityDAO : IDisposable
	{
		private static volatile LU_IdentityDAO instance;
		private static readonly object lockObj = new object();
		public static LU_IdentityDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_IdentityDAO();
			}
			return instance;
		}
		public static LU_IdentityDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_IdentityDAO();
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

		public LU_IdentityDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_Identity> Get(Int32? id = null)
		{
			try
			{
				List<LU_Identity> LU_IdentityLst = new List<LU_Identity>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_IdentityLst = dbExecutor.FetchData<LU_Identity>(CommandType.StoredProcedure, "wsp_LU_Identity_Get", colparameters);
				return LU_IdentityLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_Identity> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_Identity> LU_IdentityLst = new List<LU_Identity>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_IdentityLst = dbExecutor.FetchData<LU_Identity>(CommandType.StoredProcedure, "wsp_LU_Identity_GetDynamic", colparameters);
				return LU_IdentityLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_Identity> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_Identity> LU_IdentityLst = new List<LU_Identity>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_IdentityLst = dbExecutor.FetchDataRef<LU_Identity>(CommandType.StoredProcedure, "wsp_LU_Identity_GetPaged", colparameters, ref rows);
				return LU_IdentityLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_Identity _LU_Identity, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@paramId", _LU_Identity.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramDesignation", _LU_Identity.Designation, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_Identity.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_Identity.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_Identity_Post", colparameters, true);
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
