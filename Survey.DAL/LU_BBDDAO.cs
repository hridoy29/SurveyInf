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
	public class LU_BBDDAO : IDisposable
	{
		private static volatile LU_BBDDAO instance;
		private static readonly object lockObj = new object();
		public static LU_BBDDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_BBDDAO();
			}
			return instance;
		}
		public static LU_BBDDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_BBDDAO();
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

		public LU_BBDDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_BBD> Get(Int32? id = null)
		{
			try
			{
				List<LU_BBD> LU_BBDLst = new List<LU_BBD>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_BBDLst = dbExecutor.FetchData<LU_BBD>(CommandType.StoredProcedure, "wsp_LU_BBD_Get", colparameters);
				return LU_BBDLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_BBD> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_BBD> LU_BBDLst = new List<LU_BBD>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_BBDLst = dbExecutor.FetchData<LU_BBD>(CommandType.StoredProcedure, "wsp_LU_BBD_GetDynamic", colparameters);
				return LU_BBDLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_BBD> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_BBD> LU_BBDLst = new List<LU_BBD>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_BBDLst = dbExecutor.FetchDataRef<LU_BBD>(CommandType.StoredProcedure, "LU_BBD_GetPaged", colparameters, ref rows);
				return LU_BBDLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_BBD _LU_BBD, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@paramId", _LU_BBD.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramBBDName", _LU_BBD.BBDName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_BBD.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_BBD.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_BBD_Post", colparameters, true);
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
