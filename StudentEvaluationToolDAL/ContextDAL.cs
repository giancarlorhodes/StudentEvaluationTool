using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEvaluationToolDAL
{
    public class ContextDAL
    {
		// fields
		private IDbConnection _connection;
		private ExceptionHandling _exceptionHandling;

		// properties
		public IDbConnection Connection { get => _connection; }

		// constructors
		public ContextDAL(IDbConnection inConnection)
		{
			this._connection = inConnection; 
		}


		#region General Context (connection and dispose stuff)
		//connection to Database stuff like dispose and checkconnection

		//public void Dispose()
		//{
		//	_con.Dispose();
		//}
		//void CheckConnection(IDbConnection iDbConnection)
		//{       // checks connection status and (re)opens connection when needed
		//	try
		//	{
		//		switch (iDbConnection.State)
		//		{
		//			case (System.Data.ConnectionState.Closed):
		//				_con.Open();
		//				break;
		//			case (System.Data.ConnectionState.Broken):
		//				_con.Close();
		//				_con.Open();
		//				break;
		//			case (System.Data.ConnectionState.Open):
		//				// no action needed. (but who left the gate open?)
		//				break;
		//		}
		//	}
		//	catch (Exception ex) 
		//	{

		//	}
		//}


		//public string ConnectionString
		//{   // The connectionstring will be provided from the configmanager through the BLL
		//	get { return _con.ConnectionString; }
		//	set { _con.ConnectionString = value; }
		//}



		#endregion General Context

	}
}
