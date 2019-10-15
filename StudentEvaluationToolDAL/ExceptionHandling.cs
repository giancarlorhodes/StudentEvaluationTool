namespace StudentEvaluationToolDAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public class ExceptionHandling
    {
        public void WriteExceptionToFile(Exception ex)
        {
            using (StreamWriter writer = File.AppendText("D:\\Temp\\StudentEvaluationToolExcepection.log"))
            {
                String e = "DATETIME STAMP: " + DateTime.Now.ToString() + ";\n STACK TRACE: " + ex.StackTrace
                    + ";\n MESSAGE: " + ex.Message + ";\n INNER EXCEPTION: " + ex.InnerException;
                writer.WriteLine(e);
                writer.Close();
                writer.Dispose();
            }
        }

        public void WriteExceptionToDatabase(Exception ex)
        {           
            string DbConnection = "Server=LAPTOP-1RTOL5OV\\SQLEXPRESS;Database=StudentEvaluation;Trusted_Connection=True;";


            var lStackTrace = new StackTrace(ex);
            var lThisasm = Assembly.GetExecutingAssembly();
            var lMethodname = lStackTrace.GetFrames().Select(f => f.GetMethod()).First(m => m.Module.Assembly == lThisasm).Name;

            try
            {
                // write all my database code here
                // establish the connection 
                using (SqlConnection conn = new SqlConnection(DbConnection))
                {
                    // create the command
                    using (SqlCommand command = new SqlCommand("sp_LogException", conn))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandTimeout = 30;
                        conn.Open();

                        // do some work to call the stored procedure for adding
                        command.Parameters.AddWithValue("@parmExceptionStackTrace", SqlDbType.VarChar).Value = ex.StackTrace;
                        command.Parameters.AddWithValue("@parmExceptionMessage", SqlDbType.VarChar).Value = ex.Message;
                        command.Parameters.AddWithValue("@parmExceptionSource", SqlDbType.VarChar).Value = ex.Source;
                        command.Parameters.AddWithValue("@parmExceptionURL", SqlDbType.VarChar).Value = lMethodname;
                        command.Parameters.AddWithValue("@parmLogdate", SqlDbType.DateTime).Value = DateTime.Now;

                        // call the non query to execute the stored procedure
                        command.ExecuteNonQuery();
                    }
                    // close connection
                    conn.Close();
                }
            }
            catch (Exception ex_again)
            {
                
                ExceptionHandling exceptionHandling = new ExceptionHandling();

                // log to file
                exceptionHandling.WriteExceptionToFile(ex_again);
             
            }
         
        }

    }
}
