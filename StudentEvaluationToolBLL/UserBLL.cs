namespace StudentEvaluationToolBLL
{
    using StudentEvaluationToolCommon;
    using StudentEvaluationToolDAL;
    using System;

    public class UserBLL
    {

        public Result FetchUsers(int iUserId)
        {
            Result result = new Result();
            UserDAL userDAL = new UserDAL();
            result = userDAL.GetUserFromTheDatabase(iUserId);
            return result;

        }

        public Result UpdateRole(User user)
        {
            Result result = new Result();
            UserDAL userDAL = new UserDAL();
            result = userDAL.UpdateRoleInTheDatabase(user);
            return result;
        }

        public Result FetchCandidates(int iUserId, int iRoleId)
        {
            Result result = new Result();
            UserDAL userDAL = new UserDAL();
            result = userDAL.GetCandidatesFromTheDatabase(iUserId, iRoleId);
            return result;
        }
    }
}
