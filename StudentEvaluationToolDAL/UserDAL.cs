namespace StudentEvaluationToolDAL
{
    using StudentEvaluationToolCommon;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class UserDAL 
    {

        public Result result = new Result(); // TODO: create an base result class and build other result classes that inherit from that
        private IDbConnection _connection;
        private ExceptionHandling _exceptionHandling;

        public UserDAL(IDbConnection connection)
        {
            this._connection = connection;
            this._exceptionHandling = new ExceptionHandling();
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


                    // open connection
                    _connection.Open();

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
                    _connection.Close();
                }
            }
            catch (Exception ex)
            {
                _connection.Close();
                result.ResultMessage = "";

                // log to file
                _exceptionHandling.WriteExceptionToFile(ex);

                // log to database
                _exceptionHandling.WriteExceptionToDatabase(ex);

                throw;
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


                    // open connection
                    _connection.Open();

                    // reader loop
                    using (IDataReader _reader = command.ExecuteReader())
                    {
                        int _userIDPosition = _reader.GetOrdinal("UserId");
                        int _capstoneCandidateIdPosition = _reader.GetOrdinal("CapstoneCandidateId");
                        int _candidateFirstNamePosition = _reader.GetOrdinal("CandidateFirstName");

                        int _candidateLastNamePosition = _reader.GetOrdinal("CandidateLastName");
                        int _candidateLMSUserIdPosition = _reader.GetOrdinal("CandidateLMSUserId");
                        int _candidateLMSGroupIdPosition = _reader.GetOrdinal("CandidateLMSGroupId");
                        int _candidateLMSGroupNamePostion = _reader.GetOrdinal("CandidateLMSGroupName");
                        int _candidateLMSCourseIdPostion = _reader.GetOrdinal("CandidateLMSCourseId");
                        int _candidateActiveFlagPosition = _reader.GetOrdinal("CandidateActiveFlag");

                        int _capstoneEvaluatorIdPosition = _reader.GetOrdinal("CapstoneEvaluatorId");
                        int _evaluatorFirstNamePostion = _reader.GetOrdinal("EvaluatorFirstName");
                        int _evaluatorLastNamePosition = _reader.GetOrdinal("EvaluatorLastName");
                        int _evaluatorJobTitlePosition = _reader.GetOrdinal("EvaluatorJobTitle");
                        int _evaluatorActiveFlagPostion = _reader.GetOrdinal("EvaluatorActiveFlag");


                        // loop thru the resultset and create object and add to list
                        while (_reader.Read())
                        {
                            Candidate candidate = new Candidate();
                            candidate.UserId = _reader.GetInt32(_userIDPosition);
                            candidate.CapstoneCandidateId = _reader.GetInt32(_capstoneCandidateIdPosition);
                            candidate.CandidateFirstName = _reader.GetString(_candidateFirstNamePosition);
                            candidate.CandidateLastName = _reader.GetString(_candidateLastNamePosition);
                            candidate.CandidateLMSUserId = _reader.GetInt32(_candidateLMSUserIdPosition);
                            candidate.CandidateLMSGroupId = _reader.GetInt32(_candidateLMSGroupIdPosition);
                            candidate.CandidateLMSGroupName = _reader.GetString(_candidateLMSGroupNamePostion);
                            candidate.CandidateLMSCourseId = _reader.GetInt32(_candidateLMSCourseIdPostion);
                            candidate.CandidateActiveFlag = _reader.GetInt32(_candidateActiveFlagPosition);

                            candidate.CapstoneEvaluatorId = _reader.GetInt32(_capstoneEvaluatorIdPosition);
                            candidate.EvaluatorFirstName = _reader.GetString(_evaluatorFirstNamePostion);
                            candidate.EvaluatorLastName = _reader.GetString(_evaluatorLastNamePosition);
                            candidate.EvaluatorJobTitle = _reader.GetString(_evaluatorJobTitlePosition);
                            candidate.EvaluatorActiveFlag = _reader.GetInt32(_evaluatorActiveFlagPostion);

                            // add to list
                            listOfCandidates.Add(candidate);
                        }
                    }
                }

                result.ListOfCandidates = listOfCandidates;
                // close connection
                _connection.Close();

            }
            catch (Exception ex)
            {
                result.ResultMessage = "";

                // log to file
                _exceptionHandling.WriteExceptionToFile(ex);

                // log to database
                _exceptionHandling.WriteExceptionToDatabase(ex);

                throw;
            }

            return result;
        }

        public Result UpdateRoleInTheDatabase(User inUser)
        {
            try
            {
                // open the connection
                this._connection.Open();

                using (IDbCommand command = this._connection.CreateCommand())
                {

                    // setting command properties
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 30;
                    command.CommandText = "sp_UpdateUserRole";

                    // do some work to call the stored procedure for adding
                    IDbDataParameter parmUserId = command.CreateParameter();
                    parmUserId.DbType = DbType.Int32;
                    parmUserId.ParameterName = "@parmUserId";
                    parmUserId.Value = inUser.UserID;
                    command.Parameters.Add(parmUserId);

                    IDbDataParameter parmRoleId = command.CreateParameter();
                    parmRoleId.DbType = DbType.Int32;
                    parmRoleId.ParameterName = "@parmRoleId";
                    parmRoleId.Value = inUser.RoleID;
                    command.Parameters.Add(parmRoleId);

                    // call the non query to execute the stored procedure
                    command.ExecuteNonQuery();

                    result.ResultType = ResultType.Success;
                    result.ResultMessage = "User role updated.";
                }

                // close connection
                _connection.Close();

            }
            catch (Exception ex)
            {

                // close connection
                _connection.Close();
              
                // log to file
                _exceptionHandling.WriteExceptionToFile(ex);

                // log to database
                _exceptionHandling.WriteExceptionToDatabase(ex);


                result.ResultType = ResultType.Failure;
                result.ResultMessage = "Did not update user role.";

                throw;
            }

            return result;
        }


    }
}
