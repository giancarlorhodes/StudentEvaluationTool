namespace StudentEvaluationToolBLL
{
    using StudentEvaluationToolCommon;
    using StudentEvaluationToolDAL;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    public class UserBLL : SqlServerDbContextBLL
    {

        
        public UserBLL() : base() 
        {
            base.Connection.Open();
        }


        public Result FetchUsers(int iUserId)
        {
            Result result = new Result();
            UserDAL userDAL = new UserDAL();
   
            result = userDAL.GetUserFromTheDatabase(iUserId, base.Connection);
            return result;

        }

        public Result UpdateRole(User user)
        {
            Result result = new Result();
            UserDAL userDAL = new UserDAL();
            result = userDAL.UpdateRoleInTheDatabase(user, base.Connection);
            return result;
        }

        public Result FetchCandidates(int iUserId, int iRoleId)
        {
            Result result = new Result();
            UserDAL userDAL = new UserDAL();
            result = userDAL.GetCandidatesFromTheDatabase(iUserId, iRoleId, base.Connection);
            return result;
        }

        // destructor
        ~UserBLL() 
        {
           base. Connection.Close();

        }


    }
}
