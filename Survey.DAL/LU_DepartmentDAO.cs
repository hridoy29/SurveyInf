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
	public class LU_DepartmentDAO : IDisposable
	{
		private static volatile LU_DepartmentDAO instance;
		private static readonly object lockObj = new object();
		public static LU_DepartmentDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_DepartmentDAO();
			}
			return instance;
		}
		public static LU_DepartmentDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_DepartmentDAO();
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

		public LU_DepartmentDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_Department> Get(Int32? id = null)
		{
			try
			{
				List<LU_Department> LU_DepartmentList = new List<LU_Department>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@DepartmentId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_DepartmentList = dbExecutor.FetchData<LU_Department>(CommandType.StoredProcedure, "wsp_LU_Department_Get", colparameters);
				return LU_DepartmentList;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_Department> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_Department> LU_DepartmentList = new List<LU_Department>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_DepartmentList = dbExecutor.FetchData<LU_Department>(CommandType.StoredProcedure, "wsp_LU_Department_GetDynamic", colparameters);
				return LU_DepartmentList;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_Department> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_Department> LU_UserGroupLst = new List<LU_Department>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_UserGroupLst = dbExecutor.FetchDataRef<LU_Department>(CommandType.StoredProcedure, "wsp_LU_Department_GetPaged", colparameters, ref rows);
				return LU_UserGroupLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_Department _LU_Department, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[3]{
				new Parameters("@DeptName", _LU_Department.Name, DbType.String, ParameterDirection.Input),
				new Parameters("@IsActive", _LU_Department.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@CreatorId", _LU_Department.CreatorId, DbType.Int32, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_Department_Post", colparameters, true);
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
