namespace StudentEvaluationToolBLL
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    public class DbContextBLL : IDisposable
    {

        private IDbConnection _connection;
        //public IDbConnection Connection { get => _connection; set => _connection = value; }

        // default constructor
        public DbContextBLL()
        {
            // this is the only place where I have a reference to SQL Server specific implementation.
            // this  line would have to changes if the DB tyes where changes SQL SERVER ---> ORACLE as an example
            // we could pass this dependancy up the chain, introduce the bootstrapper technique, and finally a
            // DI container


            // OLD REMOVE
            //Connection = new SqlConnection(); 
            //Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        }


        public DbContextBLL(IDbConnection inConnection) 
        {
            this._connection = inConnection;
        
        }


      

        public void Dispose()
        {
            //this stuff takes a lot of resources and should be disposed of correctly. 
            this.Dispose();
        }

    }
}
