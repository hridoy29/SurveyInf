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
	public class LU_CategoryDAO : IDisposable
	{
		private static volatile LU_CategoryDAO instance;
		private static readonly object lockObj = new object();
		public static LU_CategoryDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_CategoryDAO();
			}
			return instance;
		}
		public static LU_CategoryDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_CategoryDAO();
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

		public LU_CategoryDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_Category> Get(Int32? id = null)
		{
			try
			{
				List<LU_Category> LU_CategoryLst = new List<LU_Category>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_CategoryLst = dbExecutor.FetchData<LU_Category>(CommandType.StoredProcedure, "wsp_LU_Category_Get", colparameters);
				return LU_CategoryLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_Category> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_Category> LU_CategoryLst = new List<LU_Category>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_CategoryLst = dbExecutor.FetchData<LU_Category>(CommandType.StoredProcedure, "wsp_LU_Category_GetDynamic", colparameters);
				return LU_CategoryLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_Category> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_Category> LU_CategoryLst = new List<LU_Category>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_CategoryLst = dbExecutor.FetchDataRef<LU_Category>(CommandType.StoredProcedure, "LU_Category_GetPaged", colparameters, ref rows);
				return LU_CategoryLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_Category _LU_Category, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@paramId", _LU_Category.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramDescription", _LU_Category.Description, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_Category.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_Category.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_Category_Post", colparameters, true);
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
