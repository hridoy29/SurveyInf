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
	public class LU_ItemGroupDAO : IDisposable
	{
		private static volatile LU_ItemGroupDAO instance;
		private static readonly object lockObj = new object();
		public static LU_ItemGroupDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_ItemGroupDAO();
			}
			return instance;
		}
		public static LU_ItemGroupDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_ItemGroupDAO();
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

		public LU_ItemGroupDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_ItemGroup> Get(Int32? id = null)
		{
			try
			{
				List<LU_ItemGroup> LU_ItemGroupLst = new List<LU_ItemGroup>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_ItemGroupLst = dbExecutor.FetchData<LU_ItemGroup>(CommandType.StoredProcedure, "wsp_LU_ItemGroup_Get", colparameters);
				return LU_ItemGroupLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_ItemGroup> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_ItemGroup> LU_ItemGroupLst = new List<LU_ItemGroup>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_ItemGroupLst = dbExecutor.FetchData<LU_ItemGroup>(CommandType.StoredProcedure, "wsp_LU_ItemGroup_GetDynamic", colparameters);
				return LU_ItemGroupLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_ItemGroup> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_ItemGroup> LU_ItemGroupLst = new List<LU_ItemGroup>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_ItemGroupLst = dbExecutor.FetchDataRef<LU_ItemGroup>(CommandType.StoredProcedure, "LU_ItemGroup_GetPaged", colparameters, ref rows);
				return LU_ItemGroupLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_ItemGroup _LU_ItemGroup, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@paramId", _LU_ItemGroup.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramGroupName", _LU_ItemGroup.GroupName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_ItemGroup.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_ItemGroup.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_ItemGroup_Post", colparameters, true);
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
