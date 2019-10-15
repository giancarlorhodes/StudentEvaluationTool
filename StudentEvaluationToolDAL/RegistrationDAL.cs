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

    public class RegistrationDAL
    {

        public Result result = new Result();
        // public string DbConnection = "Server=LAPTOP-1RTOL5OV\\SQLEXPRESS;Database=StudentEvaluation;Trusted_Connection=True;";

        public string DbConnection = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;


        public Result CreateNewUser(User user)
        {


            //var DbConnection = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

            try
            {
                // write all my database code here
                // establish the connection 
                using (SqlConnection conn = new SqlConnection(DbConnection))
                {
                    // create the command
                    using (SqlCommand command = new SqlCommand("sp_CreateNewUser", conn))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandTimeout = 30;
                        conn.Open();

                        // do some work to call the stored procedure for adding
                        command.Parameters.AddWithValue("@parmFirstName", SqlDbType.VarChar).Value = user.FirstName;
                        command.Parameters.AddWithValue("@parmLastName", SqlDbType.VarChar).Value = user.LastName;
                        command.Parameters.AddWithValue("@parmUsername", SqlDbType.VarChar).Value = user.Username;
                        command.Parameters.AddWithValue("@parmPassword", SqlDbType.VarChar).Value = user.Password;
                        if (string.IsNullOrEmpty(user.Salt))
                        {
                            // default salt
                            command.Parameters.AddWithValue("@parmSalt", SqlDbType.VarChar).Value = "salt123";
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@parmSalt", SqlDbType.VarChar).Value = user.Salt;
                        }
                        command.Parameters.AddWithValue("@parmEmail", SqlDbType.VarChar).Value = user.Email;
                        // default is 3 = Employee role
                        command.Parameters.AddWithValue("@parmRoleId_FK", SqlDbType.Int).Value = 3;

                        // call the non query to execute the stored procedure
                        command.ExecuteNonQuery();

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


        public Result LoginAttempt(User user)
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