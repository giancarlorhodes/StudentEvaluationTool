namespace StudentEvaluationToolDAL
{
    using StudentEvaluationToolCommon;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class UserDAL 
    {

        public Result result = new Result();
        private IDbConnection _connection;

        public UserDAL(IDbConnection connection)
        {
            this._connection = connection;
        }


        public Result GetUserFromTheDatabase(int iUserId)
        {

            List<User> listOfUsers = new List<User>();

            try
            {
                // create the command
                using (IDbCommand command = this._connection.CreateCommand())
                {
                    // set command properties
                    command.CommandText = "sp_GetAllUsers";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 30;

                    // parameters here
                    // do some work to call the stored procedure for adding
                    //command.Parameters.AddWithValue("@parmUserId", SqlDbType.Int).Value = iUserId;
                    IDbDataParameter parameter = command.CreateParameter();
                    parameter.DbType = DbType.Int32;
                    parameter.ParameterName = "@parmUserId";
                    parameter.Value = iUserId;
                    command.Parameters.Add(parameter);

                    // reader loop
                    using (IDataReader reader = command.ExecuteReader())
                    {

                        //TODO: reader.GetInt32(reader.GetOrdinal("FirstName")); ?? HOW MUCH FASTER??
                        // loop thru the resultset and create object and add to list 

                        int _firstNamePosition = reader.GetOrdinal("FirstName");
                        int _lastNamePosition = reader.GetOrdinal("LastName");
                        int _usernamePosition = reader.GetOrdinal("Username");
                        int _passwordPosition = reader.GetOrdinal("Password");
                        int _emailPosition = reader.GetOrdinal("Email");
                        int _saltPosition = reader.GetOrdinal("Salt");
                        int _roleIdPosition = reader.GetOrdinal("RoleId_FK");
                        int _roleName = reader.GetOrdinal("RoleName");
                        int _userId = reader.GetOrdinal("UserId");

                        while (reader.Read())
                        {
                            User userTemp = new User();

                            //// slower ??
                            //userTemp.FirstName = reader["FirstName"].ToString();                           
                            //userTemp.LastName = reader["LastName"].ToString();
                            //userTemp.Username = reader["Username"].ToString();
                            //userTemp.Password = reader["Password"].ToString();
                            //userTemp.Email = reader["Email"].ToString();
                            //userTemp.Salt = reader["Salt"].ToString();
                            //userTemp.RoleID = (int)reader["RoleId_FK"];
                            //userTemp.RoleName = reader["RoleName"].ToString();
                            //userTemp.UserID = (int)reader["UserId"];

                            // faster
                            userTemp.FirstName = reader.GetString(_firstNamePosition); // what happens with nulls
                            userTemp.LastName = reader.GetString(_lastNamePosition);
                            userTemp.Username = reader.GetString(_usernamePosition);
                            userTemp.Password = reader.GetString(_passwordPosition);
                            userTemp.Email = reader.GetString(_emailPosition);
                            userTemp.Salt = reader.GetString(_saltPosition);
                            userTemp.RoleID = reader.GetInt32(_roleIdPosition); // not nullable
                            userTemp.RoleName = reader.GetString(_roleName);
                            userTemp.UserID = reader.GetInt32(_userId);

                            // add to list
                            listOfUsers.Add(userTemp);
                        }
                    }

                    result.ListOfUsers = listOfUsers;
                }
            }
            catch (Exception ex)
            {
                result.ResultMessage = "";

                ExceptionHandling exceptionHandling = new ExceptionHandling();

                // log to file
                exceptionHandling.WriteExceptionToFile(ex);

                // log to database
                exceptionHandling.WriteExceptionToDatabase(ex);
            }

            return result;
        }

        public Result GetCandidatesFromTheDatabase(int iUserId, int iRoleId)
        {
            IList<Candidate> listOfCandidates = new List<Candidate>();

            try
            {

                // create the command
                using (IDbCommand command = this._connection.CreateCommand())
                {

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "sp_GetActiveCandidatesByRoleAndUser";
                    command.CommandTimeout = 30;

                    IDbDataParameter parmUserId = command.CreateParameter();
                    parmUserId.DbType = DbType.Int32;
                    parmUserId.ParameterName = "@parmUserId";
                    parmUserId.Value = iUserId;
                    command.Parameters.Add(parmUserId);

                    IDbDataParameter parmRoleId = command.CreateParameter();
                    parmRoleId.DbType = DbType.Int32;
                    parmRoleId.ParameterName = "@parmRoleId";
                    parmRoleId.Value = iRoleId;
                    command.Parameters.Add(parmRoleId);

                    // reader loop
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        // loop thru the resultset and create object and add to list
                        while (reader.Read())
                        {
                            Candidate candidate = new Candidate();
                            candidate.UserId = (int)reader["UserId"];
                            candidate.CapstoneCandidateId = (int)reader["CapstoneCandidateId"];
                            candidate.CandidateFirstName = reader["CandidateFirstName"].ToString();
                            candidate.CandidateLastName = reader["CandidateLastName"].ToString();
                            candidate.CandidateLMSUserId = (int)reader["CandidateLMSUserId"];
                            candidate.CandidateLMSGroupId = (int)reader["CandidateLMSGroupId"];
                            candidate.CandidateLMSGroupName = reader["CandidateLMSGroupName"].ToString();
                            candidate.CandidateLMSCourseId = (int)reader["CandidateLMSCourseId"];
                            candidate.CandidateActiveFlag = (int)reader["CandidateActiveFlag"];

                            candidate.CapstoneEvaluatorId = (int)reader["CapstoneEvaluatorId"];
                            candidate.EvaluatorFirstName = reader["EvaluatorFirstName"].ToString();
                            candidate.EvaluatorLastName = reader["EvaluatorLastName"].ToString();
                            candidate.EvaluatorJobTitle = reader["EvaluatorJobTitle"].ToString();
                            candidate.EvaluatorActiveFlag = (int)reader["EvaluatorActiveFlag"];

                            // add to list
                            listOfCandidates.Add(candidate);
                        }
                    }
                }

                result.ListOfCandidates = listOfCandidates;


            }
            catch (Exception ex)
            {
                result.ResultMessage = "";

                ExceptionHandling exceptionHandling = new ExceptionHandling();

                // log to file
                exceptionHandling.WriteExceptionToFile(ex);

                // log to database
                exceptionHandling.WriteExceptionToDatabase(ex);
            }

            return result;
        }

        public Result UpdateRoleInTheDatabase(User iUser)
        {
            try
            {
                using (IDbCommand command = this._connection.CreateCommand())
                {

                    // setting command properties
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 30;
                    command.CommandText = "sp_UpdateUserRole";

                    // do some work to call the stored procedure for adding
                    //command.Parameters.AddWithValue("@parmUserId", SqlDbType.Int).Value = user.UserID;
                    //command.Parameters.AddWithValue("@parmRoleId", SqlDbType.Int).Value = user.RoleID;
                    IDbDataParameter parmUserId = command.CreateParameter();
                    parmUserId.DbType = DbType.Int32;
                    parmUserId.ParameterName = "@parmUserId";
                    parmUserId.Value = iUser.UserID;
                    command.Parameters.Add(parmUserId);

                    IDbDataParameter parmRoleId = command.CreateParameter();
                    parmRoleId.DbType = DbType.Int32;
                    parmRoleId.ParameterName = "@parmRoleId";
                    parmRoleId.Value = iUser.RoleID;
                    command.Parameters.Add(parmRoleId);

                    // call the non query to execute the stored procedure
                    command.ExecuteNonQuery();

                    result.ResultType = ResultType.Success;
                    result.ResultMessage = "User role updated.";
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
                result.ResultMessage = "Did not update user role.";
            }

            return result;
        }


    }
}
