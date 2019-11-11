using StudentEvaluationToolCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace StudentEvaluationToolDAL
{
    public class ChartDAL
    {

        public Result result = new Result();
        public string DbConnection = System.Configuration.ConfigurationManager.
            ConnectionStrings["DbConnection"].ConnectionString;


        public Result GetChartDataForClass(string iClassName) 
        {

            List<Chart> listOfChart = new List<Chart>();

            try
            {
                // establish the connection 
                using (SqlConnection conn = new SqlConnection(DbConnection))
                {
                    // create the command
                    using (SqlCommand command = new SqlCommand("sp_GetChartData", conn))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandTimeout = 30;

                        // do some work to call the stored procedure for adding
                        command.Parameters.AddWithValue("@parmClassName", SqlDbType.VarChar).Value = iClassName;  // if empty string return nothing
                      

                        conn.Open();

                        // reader loop
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // loop thru the resultset and create object and add to list
                            while (reader.Read())
                            {
                                Chart tempChart = new Chart();                              
                                tempChart.LMSUserId = (int)reader["LMSUserId"];
                                tempChart.LMSGroupId = (int)reader["LMSGroupId"];
                                tempChart.LMSGroupName = reader["LMSGroupName"].ToString();
                                tempChart.LMSCourseId = (int)reader["LMSCourseId"]; 
                                tempChart.FirstName = reader["FirstName"].ToString();
                                //tempChart.ResultValue = reader["ResultValue"] != DBNull.Value ? (int)reader["ResultValue"] : 0; // could be null
                                //tempChart.UserIdEvaluator = reader["UserIdEvaluator"] != DBNull.Value ? (int)reader["UserIdEvaluator"] : 0; // could be null;
                                tempChart.LastName = reader["LastName"].ToString();
                                tempChart.ResultValue = Convert.ToDouble(reader["ResultValue"]);
                                tempChart.PerformanceName = reader["PerformanceName"].ToString();
                                // add to list
                                listOfChart.Add(tempChart);
                            }
                            result.ListOfCharts = listOfChart;
                        }
                    }
                    // close connection
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                ExceptionHandling exceptionHandling = new ExceptionHandling();

                // log to file
                exceptionHandling.WriteExceptionToFile(ex);

                // log to database
                exceptionHandling.WriteExceptionToDatabase(ex);
            }
            return result;

        }

    }
}
