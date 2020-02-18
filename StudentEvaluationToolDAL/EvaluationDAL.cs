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
    public class EvaluationDAL : ContextDAL
    {


        public Result result = new Result();
        private ExceptionHandling _exceptionHandling;

        public EvaluationDAL(IDbConnection inConnection) : base(inConnection)
        {
            this._exceptionHandling = new ExceptionHandling();
        }

        public Result GetCandidatesResult(int iUserIdEvaluator, int inCandidateId = 0, int inEvaluatorId = 0)
        {
            List<Question> listOfQuestions = new List<Question>();

            try
            {
                // open the connection
                base.Connection.Open();

                // create the command
                using (IDbCommand _command = base.Connection.CreateCommand())
                    {
                        _command.CommandType = System.Data.CommandType.StoredProcedure;
                        _command.CommandTimeout = 30;
                        _command.CommandText = "sp_GetCandidatesResults";

                        //// do some work to call the stored procedure for adding
                        //command.Parameters.AddWithValue("@parmCapstoneCandidateId", SqlDbType.Int).Value = iCandidateId; // this cause sp to return all row
                        //command.Parameters.AddWithValue("@parmCapstoneEvaluatorId", SqlDbType.Int).Value = iEvaluatorId; 


                        IDbDataParameter _parmCapstoneCandidateId = _command.CreateParameter();
                        _parmCapstoneCandidateId.DbType = DbType.Int32;
                        _parmCapstoneCandidateId.ParameterName = "@parmCapstoneCandidateId";
                        _parmCapstoneCandidateId.Value = inCandidateId;
                        _command.Parameters.Add(_parmCapstoneCandidateId);

                        IDbDataParameter _parmCapstoneEvaluatorId = _command.CreateParameter();
                        _parmCapstoneEvaluatorId.DbType = DbType.Int32;
                        _parmCapstoneEvaluatorId.ParameterName = "@parmCapstoneEvaluatorId";
                        _parmCapstoneEvaluatorId.Value = inEvaluatorId;
                        _command.Parameters.Add(_parmCapstoneEvaluatorId);


                        // reader loop
                        using (IDataReader reader = _command.ExecuteReader())
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
                           
                            }
                    }


                // close connection
                base.Connection.Close();
                result.ListOfQuestionResult = listOfQuestions;


            }
            catch (Exception ex)
            {

                // close connection
                base.Connection.Close();

                // log to file
                _exceptionHandling.WriteExceptionToFile(ex);

                // log to database
                _exceptionHandling.WriteExceptionToDatabase(ex);
            }
            return result;
        }

        public Result UpdateCandidateScores(List<Question> iListOfQuestions)
        {
            try
            {

                // open the connection
                base.Connection.Open();

                foreach (Question question in iListOfQuestions)
                {


                    // create the command
                    using (IDbCommand _command = base.Connection.CreateCommand())
                    {
                        _command.CommandType = System.Data.CommandType.StoredProcedure;
                        _command.CommandTimeout = 30;
                        _command.CommandText = "sp_UpdateCandidateResult";

                        // do some work to call the stored procedure for adding
                        //_command.Parameters.AddWithValue("@parmCapstoneEvaluationResultId", SqlDbType.Int).Value = question.CapstoneEvaluationResultId;
                        //_command.Parameters.AddWithValue("@parmResultValue", SqlDbType.Int).Value = question.ResultValue;

                        IDbDataParameter _parmCapstoneEvaluationResultId = _command.CreateParameter();
                        _parmCapstoneEvaluationResultId.DbType = DbType.Int32;
                        _parmCapstoneEvaluationResultId.ParameterName = "@parmCapstoneEvaluationResultId";
                        _parmCapstoneEvaluationResultId.Value = question.CapstoneEvaluationResultId;
                        _command.Parameters.Add(_parmCapstoneEvaluationResultId);

                        IDbDataParameter _parmResultValue = _command.CreateParameter();
                        _parmResultValue.DbType = DbType.Int32;
                        _parmResultValue.ParameterName = "@parmResultValue";
                        _parmResultValue.Value = question.ResultValue;
                        _command.Parameters.Add(_parmResultValue);

                        // call the non query to execute the stored procedure
                        _command.ExecuteNonQuery();
                    }
                }
                    
                // close connection
                base.Connection.Close();
                result.ResultType = ResultType.Success;
                result.ResultMessage = "Scores updated successful.";

            }
            catch (Exception ex)
            {
                // close connection
                base.Connection.Close();

                // log to file
                _exceptionHandling.WriteExceptionToFile(ex);

                // log to database
                _exceptionHandling.WriteExceptionToDatabase(ex);


                result.ResultType = ResultType.Failure;
                result.ResultMessage = "Scores update failed.";

                throw;
            }

            return result;
        }

    }
}
