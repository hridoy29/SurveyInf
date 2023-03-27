using DbExecutor;
using Survey.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Survey.DAL
{
    public class LU_OutletDAO
    {
		private static volatile LU_OutletDAO instance;
		private static readonly object lockObj = new object();
		public static LU_OutletDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_OutletDAO();
			}
			return instance;
		}
		public static LU_OutletDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_OutletDAO();
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

		public LU_OutletDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_Outlet> Get(Int32? id = null)
		{
			try
			{
				List<LU_Outlet> LU_OutletLst = new List<LU_Outlet>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_OutletLst = dbExecutor.FetchData<LU_Outlet>(CommandType.StoredProcedure, "wsp_LU_Outlet_Get", colparameters);
				return LU_OutletLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_Outlet _LU_Outlet, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[11]{
				new Parameters("@paramId", _LU_Outlet.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCode", _LU_Outlet.Code, DbType.String, ParameterDirection.Input),
				new Parameters("@paramName", _LU_Outlet.Name, DbType.String, ParameterDirection.Input),
				new Parameters("@paramAddress", _LU_Outlet.Address, DbType.String, ParameterDirection.Input),
				new Parameters("@paramContactNo", _LU_Outlet.ContactNo, DbType.String, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_Outlet.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_Outlet.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCreationDate", _LU_Outlet.CreationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramModifierId", _LU_Outlet.ModifierId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramModificationDate", _LU_Outlet.ModificationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_Outlet_Post", colparameters, true);
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
