namespace StudentEvaluationToolDAL
{
    using StudentEvaluationToolCommon;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public class UserDAL
    {

        Result result = new Result();
       

        // using IDbConnection
        public Result GetUserFromTheDatabase(int iUserId, IDbConnection iConnection)
        {

            List<User> listOfUsers = new List<User>();

            try
            {
              
                // create the command
                // using (IDbCommand command = new SqlCommand("sp_GetAllUsers", iConnection as SqlConnection))
                using (IDbCommand command = iConnection.CreateCommand())    
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "sp_GetAllUsers";
                    command.CommandTimeout = 30;

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
                        // loop thru the resultset and create object and add to list
                        while (reader.Read())
                        {
                            User userTemp = new User();
                            userTemp.FirstName = reader["FirstName"].ToString();
                            userTemp.LastName = reader["LastName"].ToString();
                            userTemp.Username = reader["Username"].ToString();
                            userTemp.Password = reader["Password"].ToString();
                            userTemp.Email = reader["Email"].ToString();
                            userTemp.Salt = reader["Salt"].ToString();
                            userTemp.RoleID = (int)reader["RoleId_FK"];
                            userTemp.RoleName = reader["RoleName"].ToString();
                            userTemp.UserID = (int)reader["UserId"];
                            // add to list
                            listOfUsers.Add(userTemp);
                        }
                    }
                }

                result.ListOfUsers = listOfUsers;
                
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

        //public Result GetUserFromTheDatabase(int iUserId)
        //{

        //    List<User> listOfUsers = new List<User>();
        //    string DbConnection = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

        //    try
        //    {

             
        //        // establish the connection 
        //        using (SqlConnection conn = new SqlConnection(DbConnection))
        //        {
        //            // create the command
        //            using (SqlCommand command = new SqlCommand("sp_GetAllUsers", conn))
        //            {
        //                command.CommandType = System.Data.CommandType.StoredProcedure;
        //                command.CommandTimeout = 30;


        //                // do some work to call the stored procedure for adding
        //                command.Parameters.AddWithValue("@parmUserId", SqlDbType.Int).Value = iUserId;
        //                conn.Open();

        //                // reader loop
        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    // loop thru the resultset and create object and add to list
        //                    while (reader.Read())
        //                    {
        //                        User userTemp = new User();
        //                        userTemp.FirstName = reader["FirstName"].ToString();
        //                        userTemp.LastName = reader["LastName"].ToString();
        //                        userTemp.Username = reader["Username"].ToString();
        //                        userTemp.Password = reader["Password"].ToString();
        //                        userTemp.Email = reader["Email"].ToString();
        //                        userTemp.Salt = reader["Salt"].ToString();
        //                        userTemp.RoleID = (int)reader["RoleId_FK"];
        //                        userTemp.RoleName = reader["RoleName"].ToString();
        //                        userTemp.UserID = (int)reader["UserId"];
        //                        // add to list
        //                        listOfUsers.Add(userTemp);
        //                    }
        //                }
        //            }
        //            // close connection
        //            conn.Close();
        //            result.ListOfUsers = listOfUsers;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.ResultMessage = "";

        //        ExceptionHandling exceptionHandling = new ExceptionHandling();

        //        // log to file
        //        exceptionHandling.WriteExceptionToFile(ex);

        //        // log to database
        //        exceptionHandling.WriteExceptionToDatabase(ex);
        //    }

        //    return result;
        //}

        // using IDbConnection

        public Result GetCandidatesFromTheDatabase(int iUserId, int iRoleId, IDbConnection iConnection)
        {
            IList<Candidate> listOfCandidates = new List<Candidate>();

            try
            {

                // create the command
                //sing (SqlCommand command = new SqlCommand("sp_GetActiveCandidatesByRoleAndUser", conn))
                //{
                using (IDbCommand command = iConnection.CreateCommand())
                {     
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "sp_GetActiveCandidatesByRoleAndUser";
                        command.CommandTimeout = 30;
                        // do some work to call the stored procedure for adding
                        //command.Parameters.AddWithValue("@parm_UserId", SqlDbType.Int).Value = iUserId;
                        //command.Parameters.AddWithValue("@parm_RoleId", SqlDbType.Int).Value = iRoleId;

                        IDbDataParameter parmUserId = command.CreateParameter();
                        parmUserId.DbType = DbType.Int32;
                        parmUserId.ParameterName = "@parmUserId";
                        parmUserId.Value = iUserId;
                        command.Parameters.Add(parmUserId);

                        IDbDataParameter parmRoleId = command.CreateParameter();
                        parmRoleId.DbType = DbType.Int32;
                        parmRoleId.ParameterName = "@parmUserId";
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

        //public Result GetCandidatesFromTheDatabase(int iUserId, int iRoleId)
        //{
        //    IList<Candidate> listOfCandidates = new List<Candidate>();
        //    string DbConnection = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        //    try
        //    {
        //        // establish the connection 
        //        using (SqlConnection conn = new SqlConnection(DbConnection))
        //        {
        //            // create the command
        //            using (SqlCommand command = new SqlCommand("sp_GetActiveCandidatesByRoleAndUser", conn))
        //            {
        //                command.CommandType = System.Data.CommandType.StoredProcedure;
        //                command.CommandTimeout = 30;
        //                // do some work to call the stored procedure for adding
        //                command.Parameters.AddWithValue("@parm_UserId", SqlDbType.Int).Value = iUserId;
        //                command.Parameters.AddWithValue("@parm_RoleId", SqlDbType.Int).Value = iRoleId;

        //                conn.Open();

        //                // reader loop
        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    // loop thru the resultset and create object and add to list
        //                    while (reader.Read())
        //                    {
        //                        Candidate candidate = new Candidate();
        //                        candidate.UserId = (int)reader["UserId"];
        //                        candidate.CapstoneCandidateId = (int)reader["CapstoneCandidateId"];
        //                        candidate.CandidateFirstName = reader["CandidateFirstName"].ToString();
        //                        candidate.CandidateLastName = reader["CandidateLastName"].ToString();
        //                        candidate.CandidateLMSUserId = (int)reader["CandidateLMSUserId"];
        //                        candidate.CandidateLMSGroupId = (int)reader["CandidateLMSGroupId"];
        //                        candidate.CandidateLMSGroupName = reader["CandidateLMSGroupName"].ToString();
        //                        candidate.CandidateLMSCourseId = (int)reader["CandidateLMSCourseId"];
        //                        candidate.CandidateActiveFlag = (int)reader["CandidateActiveFlag"];

        //                        candidate.CapstoneEvaluatorId = (int)reader["CapstoneEvaluatorId"];
        //                        candidate.EvaluatorFirstName = reader["EvaluatorFirstName"].ToString();
        //                        candidate.EvaluatorLastName = reader["EvaluatorLastName"].ToString();
        //                        candidate.EvaluatorJobTitle = reader["EvaluatorJobTitle"].ToString();
        //                        candidate.EvaluatorActiveFlag = (int)reader["EvaluatorActiveFlag"];

        //                        // add to list
        //                        listOfCandidates.Add(candidate);
        //                    }
        //                }
        //            }
        //            // close connection
        //            conn.Close();
        //            result.ListOfCandidates = listOfCandidates;
        //            //result.ListOfCandidates = this.FilterCandidatesByUserId(listOfCandidates, iUserId); ;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.ResultMessage = "";

        //        ExceptionHandling exceptionHandling = new ExceptionHandling();

        //        // log to file
        //        exceptionHandling.WriteExceptionToFile(ex);

        //        // log to database
        //        exceptionHandling.WriteExceptionToDatabase(ex);
        //    }

        //    return result;
        //}

       
        public Result UpdateRoleInTheDatabase(User iUser, IDbConnection iConnection)
        {

            string DbConnection = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

            try
            {

                // create the command
                //using (SqlCommand command = new SqlCommand("sp_UpdateUserRole", conn))
                using (IDbCommand command = iConnection.CreateCommand())
                {
                  
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
                        parmRoleId.ParameterName = "@parmUserId";
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


        //public Result UpdateRoleInTheDatabase(User user)
        //{

        //    string DbConnection = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

        //    try
        //    {
        //        // write all my database code here
        //        // establish the connection 
        //        using (SqlConnection conn = new SqlConnection(DbConnection))
        //        {
        //            // create the command
        //            using (SqlCommand command = new SqlCommand("sp_UpdateUserRole", conn))
        //            {
        //                command.CommandType = System.Data.CommandType.StoredProcedure;
        //                command.CommandTimeout = 30;
        //                conn.Open();

        //                // do some work to call the stored procedure for adding
        //                command.Parameters.AddWithValue("@parmUserId", SqlDbType.Int).Value = user.UserID;
        //                command.Parameters.AddWithValue("@parmRoleId", SqlDbType.Int).Value = user.RoleID;

        //                // call the non query to execute the stored procedure
        //                command.ExecuteNonQuery();

        //                result.ResultType = ResultType.Success;
        //                result.ResultMessage = "User role updated.";

        //            }
        //            // close connection
        //            conn.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHandling exceptionHandling = new ExceptionHandling();

        //        // log to file
        //        exceptionHandling.WriteExceptionToFile(ex);

        //        // log to database
        //        exceptionHandling.WriteExceptionToDatabase(ex);


        //        result.ResultType = ResultType.Failure;
        //        result.ResultMessage = "Did not update user role.";
        //    }

        //    return result;
        //}



    }
}
