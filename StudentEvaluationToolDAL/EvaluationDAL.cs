using StudentEvaluationToolCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEvaluationToolDAL
{
    public class EvaluationDAL
    {


        public Result result = new Result();
        public string DbConnection = System.Configuration.ConfigurationManager.
            ConnectionStrings["DbConnection"].ConnectionString;


        public Result GetCandidatesResult(int iUserIdEvaluator, int iCandidateId = 0, int iEvaluatorId = 0)
        {
            List<Question> listOfQuestions = new List<Question>();

            try
            {
                // establish the connection 
                using (SqlConnection conn = new SqlConnection(DbConnection))
                {
                    // create the command
                    using (SqlCommand command = new SqlCommand("sp_GetCandidatesResults", conn))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandTimeout = 30;

                        // do some work to call the stored procedure for adding
                        command.Parameters.AddWithValue("@parmCapstoneCandidateId", SqlDbType.Int).Value = iCandidateId; // this cause sp to return all row
                        command.Parameters.AddWithValue("@parmCapstoneEvaluatorId", SqlDbType.Int).Value = iEvaluatorId; 

                        conn.Open();

                        // reader loop
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // loop thru the resultset and create object and add to list
                            while (reader.Read())
                            {
                                Question tempQuestion = new Question();
                                tempQuestion.EvalName = reader["EvalName"].ToString();
                                tempQuestion.LMSGroupName = reader["LMSGroupName"].ToString();
                                tempQuestion.CapstoneCandidateId = (int)reader["CapstoneCandidateId"];
                                tempQuestion.CandidateName = reader["CandidateName"].ToString();
                                tempQuestion.QuestionNumber = (int)reader["QuestionNumber"];
                                tempQuestion.QuestionText = reader["Question"].ToString();
                                tempQuestion.TypeShort = reader["TypeShort"].ToString();
                                tempQuestion.RangeMin = (int)reader["RangeMin"];
                                tempQuestion.RangeMax = (int)reader["RangeMax"];
                                tempQuestion.CapstoneEvaluationResultId = (int)reader["CapstoneEvaluationResultId"];
                                tempQuestion.ResultValue = reader["ResultValue"] != DBNull.Value ? (int)reader["ResultValue"] : 0; // could be null
                                tempQuestion.UserIdEvaluator = reader["UserIdEvaluator"] != DBNull.Value ? (int)reader["UserIdEvaluator"] : 0; // could be null;
                                tempQuestion.CapstoneEvaluatorId = (int)reader["CapstoneEvaluatorId"];
                                tempQuestion.EvaluatorName = reader["EvaluatorName"].ToString();
                                tempQuestion.JobTitle = reader["JobTitle"].ToString();

                                // add to list
                                listOfQuestions.Add(tempQuestion);
                            }
                            result.ListOfQuestionResult = listOfQuestions;
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

        public Result UpdateCandidateScores(List<Question> iListOfQuestions)
        {
            try
            {

                // write all my database code here
                // establish the connection 
                using (SqlConnection conn = new SqlConnection(DbConnection))
                {
                    // open the connection once before the loop
                    conn.Open();
                    
                    foreach (Question question in iListOfQuestions)
                    {
                        // create the command
                        using (SqlCommand command = new SqlCommand("sp_UpdateCandidateResult", conn))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandTimeout = 30;
             
                            // do some work to call the stored procedure for adding
                            command.Parameters.AddWithValue("@parmCapstoneEvaluationResultId", SqlDbType.Int).Value = question.CapstoneEvaluationResultId;
                            command.Parameters.AddWithValue("@parmResultValue", SqlDbType.Int).Value = question.ResultValue;
                           
                            // call the non query to execute the stored procedure
                            command.ExecuteNonQuery();
                        }

                    }
                    
                    result.ResultType = ResultType.Success;
                    result.ResultMessage = "Scores updated successful.";

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


                result.ResultType = ResultType.Failure;
                result.ResultMessage = "Scores update failed.";
            }

            return result;
        }

    }
}
