using DbExecutor;
using Survey.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Survey.DAL
{
    public class LU_AssetInfoDAO
    {
		private static volatile LU_AssetInfoDAO instance;
		private static readonly object lockObj = new object();
		public static LU_AssetInfoDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_AssetInfoDAO();
			}
			return instance;
		}
		public static LU_AssetInfoDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_AssetInfoDAO();
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

		public LU_AssetInfoDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_AssetInfo> Get(Int32? moduleId = null)
		{
			try
			{
				List<LU_AssetInfo> LU_AssetInfoLst = new List<LU_AssetInfo>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", moduleId, DbType.Int32, ParameterDirection.Input)
				};
				LU_AssetInfoLst = dbExecutor.FetchData<LU_AssetInfo>(CommandType.StoredProcedure, "wsp_LU_AssetInfo_Get", colparameters);
				return LU_AssetInfoLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_AssetInfo> GetDynamic(string whereCondition, string orderByExpression)
		{
			try
			{
				List<LU_AssetInfo> LU_AssetInfoLst = new List<LU_AssetInfo>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_AssetInfoLst = dbExecutor.FetchData<LU_AssetInfo>(CommandType.StoredProcedure, "wsp_LU_AssetInfo_GetDynamic", colparameters);
				return LU_AssetInfoLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_AssetInfo> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_AssetInfo> LU_AssetInfoLst = new List<LU_AssetInfo>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_AssetInfoLst = dbExecutor.FetchDataRef<LU_AssetInfo>(CommandType.StoredProcedure, "wsp_LU_AssetInfo_GetPaged", colparameters, ref rows);
				return LU_AssetInfoLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_AssetInfo _LU_AssetInfo, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[15]{
			    new Parameters("@paramId", _LU_AssetInfo.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramAssetNumber", _LU_AssetInfo.AssetNumber, DbType.String, ParameterDirection.Input),
				new Parameters("@paramSerialNumber", _LU_AssetInfo.SerialNumber, DbType.String, ParameterDirection.Input),
				new Parameters("@paramDistributorId", _LU_AssetInfo.DistributorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramAICId", _LU_AssetInfo.AICId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramASMId", _LU_AssetInfo.ASMId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramMDOId", _LU_AssetInfo.MDOId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramOutletId", _LU_AssetInfo.OutletId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCoolerId", _LU_AssetInfo.CoolerId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramIsCancelled", _LU_AssetInfo.IsCancelled, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_AssetInfo.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCreationDate", _LU_AssetInfo.CreationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramModifierId", _LU_AssetInfo.ModifierId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramModificationDate", _LU_AssetInfo.ModificationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_AssetInfo_Post", colparameters, true);
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
