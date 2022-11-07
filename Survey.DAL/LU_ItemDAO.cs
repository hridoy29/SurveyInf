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
	public class LU_ItemDAO : IDisposable
	{
		private static volatile LU_ItemDAO instance;
		private static readonly object lockObj = new object();
		public static LU_ItemDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_ItemDAO();
			}
			return instance;
		}
		public static LU_ItemDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_ItemDAO();
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

		public LU_ItemDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_Item> Get(Int32? id = null)
		{
			try
			{
				List<LU_Item> LU_ItemLst = new List<LU_Item>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_ItemLst = dbExecutor.FetchData<LU_Item>(CommandType.StoredProcedure, "wsp_LU_Item_Get", colparameters);
				return LU_ItemLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_Item> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_Item> LU_ItemLst = new List<LU_Item>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_ItemLst = dbExecutor.FetchData<LU_Item>(CommandType.StoredProcedure, "wsp_LU_Item_GetDynamic", colparameters);
				return LU_ItemLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_Item> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_Item> LU_ItemLst = new List<LU_Item>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_ItemLst = dbExecutor.FetchDataRef<LU_Item>(CommandType.StoredProcedure, "LU_Item_GetPaged", colparameters, ref rows);
				return LU_ItemLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_Item _LU_Item, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[8]{
				new Parameters("@paramId", _LU_Item.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramItemCode", _LU_Item.ItemCode, DbType.String, ParameterDirection.Input),
				new Parameters("@paramName", _LU_Item.Name, DbType.String, ParameterDirection.Input),
				new Parameters("@paramItemGroupId", _LU_Item.ItemGroupId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCategoryId", _LU_Item.CategoryId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_Item.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_Item.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_Item_Post", colparameters, true);
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
