using DbExecutor;
using Survey.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Survey.DAL
{
    public class LU_Asset_ConfigDAO
    {
		private static volatile LU_Asset_ConfigDAO instance;
		private static readonly object lockObj = new object();
		public static LU_Asset_ConfigDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_Asset_ConfigDAO();
			}
			return instance;
		}
		public static LU_Asset_ConfigDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_Asset_ConfigDAO();
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

		public LU_Asset_ConfigDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_Asset_Config> Get(Int32? moduleId = null)
		{
			try
			{
				List<LU_Asset_Config> LU_Asset_ConfigLst = new List<LU_Asset_Config>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", moduleId, DbType.Int32, ParameterDirection.Input)
				};
				LU_Asset_ConfigLst = dbExecutor.FetchData<LU_Asset_Config>(CommandType.StoredProcedure, "wsp_LU_Asset_Config_Get", colparameters);
				return LU_Asset_ConfigLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_Asset_Config> GetDynamic(string whereCondition, string orderByExpression)
		{
			try
			{
				List<LU_Asset_Config> LU_Asset_ConfigLst = new List<LU_Asset_Config>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@WhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@OrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_Asset_ConfigLst = dbExecutor.FetchData<LU_Asset_Config>(CommandType.StoredProcedure, "wsp_LU_Asset_Config_GetDynamic", colparameters);
				return LU_Asset_ConfigLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_Asset_Config> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_Asset_Config> LU_Asset_ConfigLst = new List<LU_Asset_Config>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_Asset_ConfigLst = dbExecutor.FetchDataRef<LU_Asset_Config>(CommandType.StoredProcedure, "wsp_LU_Asset_Config_GetPaged", colparameters, ref rows);
				return LU_Asset_ConfigLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_Asset_Config _LU_Asset_Config, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[24]{
				new Parameters("@paramId", _LU_Asset_Config.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramAssetNumber", _LU_Asset_Config.AssetNumber, DbType.String, ParameterDirection.Input),
				new Parameters("@paramSerialNumber", _LU_Asset_Config.SerialNumber, DbType.String, ParameterDirection.Input),
				new Parameters("@paramDistributorId", _LU_Asset_Config.DistributorId, DbType.String, ParameterDirection.Input),
				new Parameters("@paramDistributorName", _LU_Asset_Config.DistributorName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramAICName", _LU_Asset_Config.AICName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramASMName", _LU_Asset_Config.ASMName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramMDOId", _LU_Asset_Config.MDOId, DbType.String, ParameterDirection.Input),
				new Parameters("@paramMDOName", _LU_Asset_Config.MDOName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOutletId", _LU_Asset_Config.OutletId, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOutletName", _LU_Asset_Config.OutletName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOutletAddress", _LU_Asset_Config.OutletAddress, DbType.String, ParameterDirection.Input),
				new Parameters("@paramContactNo", _LU_Asset_Config.ContactNo, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCoolerModel", _LU_Asset_Config.CoolerModel, DbType.String, ParameterDirection.Input),
				new Parameters("@paramIsRepeated", _LU_Asset_Config.IsRepeated, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramNightCover", _LU_Asset_Config.NightCover, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramDeviceNumber", _LU_Asset_Config.Device_Number, DbType.String, ParameterDirection.Input),
				new Parameters("@paramIsUpdated", _LU_Asset_Config.IsUpdated, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramIsCancelled", _LU_Asset_Config.IsCancelled, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_Asset_Config.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCreationDate", _LU_Asset_Config.CreationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramModifierId", _LU_Asset_Config.ModifierId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramModificationDate", _LU_Asset_Config.ModificationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_Asset_Config_Post", colparameters, true);
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
