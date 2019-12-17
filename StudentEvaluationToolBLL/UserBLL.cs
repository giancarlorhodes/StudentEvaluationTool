namespace StudentEvaluationToolBLL
{
    using StudentEvaluationToolCommon;
    using StudentEvaluationToolDAL;

    public class UserBLL : DbContextBLL
    {
        public UserDAL UserDAL { get; set; }
       
        // constructor - base will be called first, then the subtype
        public UserBLL() : base() 
        {
            UserDAL = new UserDAL(base.Connection);
            base.Connection.Open();
        }
        

        public Result FetchUsers(int iUserId)
        {
            Result result = UserDAL.GetUserFromTheDatabase(iUserId);
            return result;
        }

      

        public Result UpdateRole(User user)
        {
            Result result = UserDAL.UpdateRoleInTheDatabase(user);
            return result;
        }

        public Result FetchCandidates(int iUserId, int iRoleId)
        {
            Result result = UserDAL.GetCandidatesFromTheDatabase(iUserId, iRoleId);
            return result;
        }

        // destructor
        ~UserBLL() 
        {
           base. Connection.Close();
        }


    }
}
