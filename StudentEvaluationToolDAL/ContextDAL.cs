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

		#region General Context (connection and dispose stuff)
		//connection to Database stuff like dispose and checkconnection
		//SqlConnection _con = new SqlConnection();

		public ContextDAL(IDbConnection iDbConnection)
		{
			iDbConnection = new SqlConnection();
		}

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
