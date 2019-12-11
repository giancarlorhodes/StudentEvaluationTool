namespace StudentEvaluationToolBLL
{

    using StudentEvaluationToolDAL;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    public class SqlServerDbContextBLL : IDisposable
    {

        private IDbConnection _connection;

        // default constructor
        public SqlServerDbContextBLL()
        {
            Connection = new SqlConnection(); // TODO: injection here
            Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;


        }

        public IDbConnection Connection { get => _connection; set => _connection = value; }

        public void Dispose()
        {
            //this stuff takes a lot of resources and should be disposed of correctly. 
            this.Dispose();
        }

    }
}
