namespace StudentEvaluationToolBLL
{
    using StudentEvaluationToolCommon;
    using StudentEvaluationToolDAL;
    using System.Data;

    public class UserBLL : DbContextBLL
    {

        // fields
        private UserDAL _userDAL { get; set; }
       
        // constructors
        public UserBLL(IDbConnection inConnection)
        {
            this._userDAL = new UserDAL(inConnection); 
        }
        
        public Result FetchUsers(int iUserId)
        {
            Result result = _userDAL.GetUserFromTheDatabase(iUserId);
            return result;
        }
     
        public Result UpdateRole(User user)
        {
            Result result = _userDAL.UpdateRoleInTheDatabase(user);
            return result;
        }

        public Result FetchCandidates(int iUserId, int iRoleId)
        {
            Result result = _userDAL.GetCandidatesFromTheDatabase(iUserId, iRoleId);
            return result;
        }
     
    }
}
