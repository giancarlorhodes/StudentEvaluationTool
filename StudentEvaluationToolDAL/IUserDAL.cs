using StudentEvaluationToolCommon;

namespace StudentEvaluationToolDAL
{
    public interface IUserDAL
    {
        Result GetCandidatesFromTheDatabase(int iUserId, int iRoleId);
        Result GetUserFromTheDatabase(int iUserId);
        Result UpdateRoleInTheDatabase(User iUser);
    }
}