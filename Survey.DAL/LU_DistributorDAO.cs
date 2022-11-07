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
	public class LU_DistributorDAO : IDisposable
	{
		private static volatile LU_DistributorDAO instance;
		private static readonly object lockObj = new object();
		public static LU_DistributorDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_DistributorDAO();
			}
			return instance;
		}
		public static LU_DistributorDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_DistributorDAO();
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

		public LU_DistributorDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_Distributor> Get(Int32? id = null)
		{
			try
			{
				List<LU_Distributor> LU_DistributorLst = new List<LU_Distributor>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_DistributorLst = dbExecutor.FetchData<LU_Distributor>(CommandType.StoredProcedure, "wsp_LU_Distributor_Get", colparameters);
				return LU_DistributorLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_Distributor> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_Distributor> LU_DistributorLst = new List<LU_Distributor>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_DistributorLst = dbExecutor.FetchData<LU_Distributor>(CommandType.StoredProcedure, "wsp_LU_Distributor_GetDynamic", colparameters);
				return LU_DistributorLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_Distributor> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_Distributor> LU_DistributorLst = new List<LU_Distributor>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_DistributorLst = dbExecutor.FetchDataRef<LU_Distributor>(CommandType.StoredProcedure, "LU_Distributor_GetPaged", colparameters, ref rows);
				return LU_DistributorLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_Distributor _LU_Distributor, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[8]{
				new Parameters("@paramId", _LU_Distributor.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramDbCode", _LU_Distributor.DbCode, DbType.String, ParameterDirection.Input),
				new Parameters("@paramName", _LU_Distributor.Name, DbType.String, ParameterDirection.Input),
				new Parameters("@paramMobile", _LU_Distributor.Mobile, DbType.String, ParameterDirection.Input),
				new Parameters("@paramGccCode", _LU_Distributor.GccCode, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_Distributor.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_Distributor.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_Distributor_Post", colparameters, true);
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
