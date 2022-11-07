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
	public class LU_ObservationDAO : IDisposable
	{
		private static volatile LU_ObservationDAO instance;
		private static readonly object lockObj = new object();
		public static LU_ObservationDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_ObservationDAO();
			}
			return instance;
		}
		public static LU_ObservationDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_ObservationDAO();
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

		public LU_ObservationDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_Observation> Get(Int32? id = null)
		{
			try
			{
				List<LU_Observation> LU_ObservationLst = new List<LU_Observation>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_ObservationLst = dbExecutor.FetchData<LU_Observation>(CommandType.StoredProcedure, "wsp_LU_Observation_Get", colparameters);
				return LU_ObservationLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_Observation> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_Observation> LU_ObservationLst = new List<LU_Observation>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_ObservationLst = dbExecutor.FetchData<LU_Observation>(CommandType.StoredProcedure, "wsp_LU_Observation_GetDynamic", colparameters);
				return LU_ObservationLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_Observation> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_Observation> LU_ObservationLst = new List<LU_Observation>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_ObservationLst = dbExecutor.FetchDataRef<LU_Observation>(CommandType.StoredProcedure, "LU_Observation_GetPaged", colparameters, ref rows);
				return LU_ObservationLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_Observation _LU_Observation, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[6]{
				new Parameters("@paramId", _LU_Observation.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramName", _LU_Observation.Name, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_Observation.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_Observation.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramIsPositiveComments", _LU_Observation.IsPositiveComments, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_Observation_Post", colparameters, true);
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
