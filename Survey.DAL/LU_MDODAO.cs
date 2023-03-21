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
	public class LU_MDODAO : IDisposable
	{
		private static volatile LU_MDODAO instance;
		private static readonly object lockObj = new object();
		public static LU_MDODAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_MDODAO();
			}
			return instance;
		}
		public static LU_MDODAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_MDODAO();
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

		public LU_MDODAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_MDO> Get(Int32? id = null)
		{
			try
			{
				List<LU_MDO> LU_MDOLst = new List<LU_MDO>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_MDOLst = dbExecutor.FetchData<LU_MDO>(CommandType.StoredProcedure, "wsp_LU_MDO_Get", colparameters);
				return LU_MDOLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_MDO> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_MDO> LU_MDOLst = new List<LU_MDO>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_MDOLst = dbExecutor.FetchData<LU_MDO>(CommandType.StoredProcedure, "wsp_LU_MDO_GetDynamic", colparameters);
				return LU_MDOLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_MDO> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_MDO> LU_MDOLst = new List<LU_MDO>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_MDOLst = dbExecutor.FetchDataRef<LU_MDO>(CommandType.StoredProcedure, "wsp_LU_MDO_GetPaged", colparameters, ref rows);
				return LU_MDOLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_MDO _LU_MDO, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[6]{
				new Parameters("@paramId", _LU_MDO.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCode", _LU_MDO.Code, DbType.String, ParameterDirection.Input),
				new Parameters("@paramName", _LU_MDO.Name, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_MDO.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_MDO.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_MDO_Post", colparameters, true);
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
