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
        string DbConnection = "Server=LAPTOP-1RTOL5OV\\SQLEXPRESS;Database=StudentEvaluation;Trusted_Connection=True;";

        public Result GetUserFromTheDatabase(int iUserId)
        {

            List<User> listOfUsers = new List<User>();

            try
            {

                // establish the connection 
                using (SqlConnection conn = new SqlConnection(DbConnection))
                {
                    // create the command
                    using (SqlCommand command = new SqlCommand("sp_GetAllUsers", conn))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandTimeout = 30;


                        // do some work to call the stored procedure for adding
                        command.Parameters.AddWithValue("@parmUserId", SqlDbType.Int).Value = iUserId;
                        conn.Open();

                        // reader loop
                        using (SqlDataReader reader = command.ExecuteReader())
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
                    // close connection
                    conn.Close();
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

        public Result UpdateRoleInTheDatabase(User user)
        {
            try
            {
                // write all my database code here
                // establish the connection 
                using (SqlConnection conn = new SqlConnection(DbConnection))
                {
                    // create the command
                    using (SqlCommand command = new SqlCommand("sp_UpdateUserRole", conn))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandTimeout = 30;
                        conn.Open();

                        // do some work to call the stored procedure for adding
                        command.Parameters.AddWithValue("@parmUserId", SqlDbType.Int).Value = user.UserID;
                        command.Parameters.AddWithValue("@parmRoleId", SqlDbType.Int).Value = user.RoleID;
                      
                        // call the non query to execute the stored procedure
                        command.ExecuteNonQuery();

                        result.ResultType = ResultType.Success;
                        result.ResultMessage = "User role updated.";

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


                result.ResultType = ResultType.Failure;
                result.ResultMessage = "Did not update user role.";
            }

            return result;
        }
    }
}
