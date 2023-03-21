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
	public class LU_ShortNoteDAO : IDisposable
	{
		private static volatile LU_ShortNoteDAO instance;
		private static readonly object lockObj = new object();
		public static LU_ShortNoteDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_ShortNoteDAO();
			}
			return instance;
		}
		public static LU_ShortNoteDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_ShortNoteDAO();
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

		public LU_ShortNoteDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_ShortNote> Get(Int32? id = null)
		{
			try
			{
				List<LU_ShortNote> LU_ShortNoteLst = new List<LU_ShortNote>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_ShortNoteLst = dbExecutor.FetchData<LU_ShortNote>(CommandType.StoredProcedure, "wsp_LU_ShortNote_Get", colparameters);
				return LU_ShortNoteLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_ShortNote> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_ShortNote> LU_ShortNoteLst = new List<LU_ShortNote>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_ShortNoteLst = dbExecutor.FetchData<LU_ShortNote>(CommandType.StoredProcedure, "wsp_LU_ShortNote_GetDynamic", colparameters);
				return LU_ShortNoteLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_ShortNote> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_ShortNote> LU_ShortNoteLst = new List<LU_ShortNote>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_ShortNoteLst = dbExecutor.FetchDataRef<LU_ShortNote>(CommandType.StoredProcedure, "wsp_LU_ShortNote_GetPaged", colparameters, ref rows);
				return LU_ShortNoteLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_ShortNote _LU_ShortNote, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@paramId", _LU_ShortNote.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramName", _LU_ShortNote.Name, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_ShortNote.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_ShortNote.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_ShortNote_Post", colparameters, true);
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
