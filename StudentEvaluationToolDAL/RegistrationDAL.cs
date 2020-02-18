namespace StudentEvaluationToolDAL
{
    using StudentEvaluationToolCommon;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class RegistrationDAL : ContextDAL
    {

        public Result result = new Result();     
        //private IDbConnection _connection;
        private ExceptionHandling _exceptionHandling;

        public RegistrationDAL(IDbConnection inConnection) : base(inConnection)
        { 
            this._exceptionHandling = new ExceptionHandling();
        }

        public Result CreateNewUser(User inUser)
        {

            try
            {
                // open the connection
                base.Connection.Open();

                using (IDbCommand _command = base.Connection.CreateCommand())
                {
               
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandTimeout = 30;
                    _command.CommandText = "sp_CreateNewUser";

                    IDbDataParameter _parmFirstName = _command.CreateParameter();
                    _parmFirstName.DbType = DbType.String;
                    _parmFirstName.ParameterName = "@parmFirstName";
                    _parmFirstName.Value = inUser.FirstName;
                    _command.Parameters.Add(_parmFirstName);

                    IDbDataParameter _parmLastName = _command.CreateParameter();
                    _parmLastName.DbType = DbType.String;
                    _parmLastName.ParameterName = "@parmLastName";
                    _parmLastName.Value = inUser.LastName;
                    _command.Parameters.Add(_parmLastName);

                    IDbDataParameter _parmUsername = _command.CreateParameter();
                    _parmUsername.DbType = DbType.String;
                    _parmUsername.ParameterName = "@parmUsername";
                    _parmUsername.Value = inUser.Username;
                    _command.Parameters.Add(_parmUsername);

                    IDbDataParameter _parmPassword = _command.CreateParameter();
                    _parmPassword.DbType = DbType.String;
                    _parmPassword.ParameterName = "@parmPassword";
                    _parmPassword.Value = inUser.Password;
                    _command.Parameters.Add(_parmPassword);

                    // call the non query to execute the stored procedure
                    _command.ExecuteNonQuery();
                   
                    result.ResultType = ResultType.Success;
                    result.ResultMessage = "User registration completed. Please log in.";

                    // close connection
                    base.Connection.Close();

                }
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
                result.ResultMessage = "User registration failed.";

                throw;
            }

            return result;
        }


        public Result LoginAttempt(User user)
        {   
            List<User> listOfUsers = new List<User>();
      
            try
            {
                // open the connection
                base.Connection.Open();

                // create the command
                using (IDbCommand command = base.Connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 30;
                    command.CommandText = "sp_GetAllUsers";

                    IDbDataParameter _parmUserId = command.CreateParameter();
                    _parmUserId.DbType = DbType.Int32;
                    _parmUserId.ParameterName = "@parmUserId";
                    _parmUserId.Value = 0; // this zero will cause sp to return all users
                    command.Parameters.Add(_parmUserId);

                    // reader loop
                    using (IDataReader reader = command.ExecuteReader())
                        {
                            // loop thru the resultset and create object and add to list
                            while (reader.Read())
                            {
                                User userTemp = new User();
                                userTemp.UserID = (int)reader["UserId"];
                                userTemp.FirstName = reader["FirstName"].ToString();
                                userTemp.LastName = reader["LastName"].ToString();
                                userTemp.Username = reader["Username"].ToString();
                                userTemp.Password = reader["Password"].ToString();
                                userTemp.Email = reader["Email"].ToString();
                                userTemp.Salt = reader["Salt"].ToString();
                                userTemp.RoleID = (int)reader["RoleId_FK"];
                                userTemp.RoleName = reader["RoleName"].ToString();

                                // add to list
                                listOfUsers.Add(userTemp);
                            }
                        }
                    }

                    // close connection
                    base.Connection.Close();
                    result.ListOfUsers = listOfUsers;
                    result.ResultType = ResultType.Success;
                    result.ResultMessage = "Method: LoginAttempt succeeded.";

            }
            catch (Exception ex)
            {

                // close connection
                base.Connection.Close();
                result.ResultType = ResultType.Failure;
                result.ResultMessage = "Method: LoginAttempt failed.";

                // log to file
                this._exceptionHandling.WriteExceptionToFile(ex);

                // log to database
                this._exceptionHandling.WriteExceptionToDatabase(ex);

                throw;
            }

            return result;
        }

    }
}