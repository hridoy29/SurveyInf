using DbExecutor;
using Survey.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Survey.DAL
{
    public class MonthlyObservationReportsDAO
    {
		private static volatile MonthlyObservationReportsDAO instance;
		private static readonly object lockObj = new object();
		public static MonthlyObservationReportsDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new MonthlyObservationReportsDAO();
			}
			return instance;
		}
		public static MonthlyObservationReportsDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new MonthlyObservationReportsDAO();
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

		public MonthlyObservationReportsDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<MonthlyObservationReports_Get_distributor_count> Get_Distributor_count(DateTime toDate)
		{
			try
			{
				List<MonthlyObservationReports_Get_distributor_count> distributorList = new List<MonthlyObservationReports_Get_distributor_count>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@reportdate", toDate, DbType.Date, ParameterDirection.Input)
				
				};
				distributorList = dbExecutor.FetchData<MonthlyObservationReports_Get_distributor_count>(CommandType.StoredProcedure, "wsp_QuestionnaireMonthlyObservationReport_Get_Distributor_selection", colparameters);
				return distributorList;
			}
			catch (Exception ex)
			{
				throw ex;
            }
        }
        public List<MonthlyObservationReports_Get_distributorInfo_for_Stock_variation> Get_Distributor_Info_for_Stock_variation(DateTime toDate)
        {
            try
            {
                List<MonthlyObservationReports_Get_distributorInfo_for_Stock_variation> distributorIntoList = new List<MonthlyObservationReports_Get_distributorInfo_for_Stock_variation>();
                Parameters[] colparameters = new Parameters[1]{
                new Parameters("@reportdate", toDate, DbType.Date, ParameterDirection.Input)

                };
                distributorIntoList = dbExecutor.FetchData<MonthlyObservationReports_Get_distributorInfo_for_Stock_variation>(CommandType.StoredProcedure, "wsp_QuestionnaireMonthlyObservationReport_Get_Distributor_Stock_Variation_Details", colparameters);
                return distributorIntoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		public List<MonthlyObservationReports_Get_distributor_CCBBL_Observation> Get_Distributor_CCBBL_Observation(DateTime toDate)
		{
			try
			{
				List<MonthlyObservationReports_Get_distributor_CCBBL_Observation> distributorIntoList = new List<MonthlyObservationReports_Get_distributor_CCBBL_Observation>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@reportdate", toDate, DbType.Date, ParameterDirection.Input)

				};
				distributorIntoList = dbExecutor.FetchData<MonthlyObservationReports_Get_distributor_CCBBL_Observation>(CommandType.StoredProcedure, "wsp_QuestionnaireMonthlyObservationReport_Get_Distributor_CCBBL_Observation", colparameters);
				return distributorIntoList;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<MonthlyObservationReports_Get_distributor_Info> Get_Distributor_Info(DateTime toDate)
		{
			try
			{
				List<MonthlyObservationReports_Get_distributor_Info> distributorIntoList = new List<MonthlyObservationReports_Get_distributor_Info>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@reportdate", toDate, DbType.Date, ParameterDirection.Input)

				};
				distributorIntoList = dbExecutor.FetchData<MonthlyObservationReports_Get_distributor_Info>(CommandType.StoredProcedure, "wsp_QuestionnaireMonthlyObservationReport_Get_Distributor_Info", colparameters);
				return distributorIntoList;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<List<MonthlyObservationReports_Get_distributor_StockDetails>> Get_Distributor_StockDetails(DateTime toDate, int distributorId)
		{
			try
			{
				List<MonthlyObservationReports_Get_distributor_StockDetails> distributorIntoList = new List<MonthlyObservationReports_Get_distributor_StockDetails>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@reportdate", toDate, DbType.Date, ParameterDirection.Input),
				new Parameters("@DistributorId", distributorId, DbType.Int32, ParameterDirection.Input)

				};
				distributorIntoList = dbExecutor.FetchData<MonthlyObservationReports_Get_distributor_StockDetails>(CommandType.StoredProcedure, "wsp_QuestionnaireMonthlyObservationReport_Get_Distributor_StockDetails", colparameters);

				List<List<MonthlyObservationReports_Get_distributor_StockDetails>> _List = new List<List<MonthlyObservationReports_Get_distributor_StockDetails>>();
				_List = distributorIntoList
											.GroupBy(u => new { u.IssueText })
											.Select(grp => grp.ToList())
											.ToList();
				return _List;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public MonthlyObservationReports_Get_distributor_CCBBL_Status_2 Get_Distributor_CCBBL_Status(DateTime toDate, int distributorId)
		{
			try
			{
				List<MonthlyObservationReports_Get_distributor_CCBBL_Status> distributorIntoList = new List<MonthlyObservationReports_Get_distributor_CCBBL_Status>();
				Parameters[] colparameters = new Parameters[2]{
			new Parameters("@reportdate", toDate, DbType.Date, ParameterDirection.Input),
			new Parameters("@DistributorId", distributorId, DbType.Int32, ParameterDirection.Input)
		};

				distributorIntoList = dbExecutor.FetchData<MonthlyObservationReports_Get_distributor_CCBBL_Status>(CommandType.StoredProcedure, "wsp_QuestionnaireMonthlyObservationReport_Get_Distributor_CCBBL_Status", colparameters);


				//MonthlyObservationReports_Get_distributor_Info monthlyObservationReports_Get_Distributor_Info = new MonthlyObservationReports_Get_distributor_Info();
				MonthlyObservationReports_Get_distributor_CCBBL_Status monthlyObservationReports_Get_Distributor_CCBBL_Status = new MonthlyObservationReports_Get_distributor_CCBBL_Status();
				MonthlyObservationReports_Get_distributor_CCBBL_Status_2 monthlyObservationReports_Get_Distributor_CCBBL_Status_2  = new MonthlyObservationReports_Get_distributor_CCBBL_Status_2();

				monthlyObservationReports_Get_Distributor_CCBBL_Status_2.Status_list = distributorIntoList;

				foreach (var distributor in distributorIntoList)
                {
                    if (distributor.Status == "Improvement")
                    {
						monthlyObservationReports_Get_Distributor_CCBBL_Status_2.Improvment.Add(distributor);
                    }
                    else
                    {
						monthlyObservationReports_Get_Distributor_CCBBL_Status_2.Continuing.Add(distributor);
                    }
                }

				return monthlyObservationReports_Get_Distributor_CCBBL_Status_2;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
