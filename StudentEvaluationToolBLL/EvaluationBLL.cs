using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentEvaluationToolCommon;
using StudentEvaluationToolDAL;

namespace StudentEvaluationToolBLL
{
    public class EvaluationBLL
    {
        public Result GetQuestionsAndResults(int iUserIdEvaluator, int iCandidateId, int iEvaluatorId)
        {
            
            Result result = new Result();
            EvaluationDAL evalDAL = new EvaluationDAL();
            result = evalDAL.GetCandidatesResult(iUserIdEvaluator, iCandidateId, iEvaluatorId);
            return result;
            
        }

        public Result UpdateCandidateCapstoneScore(List<Question> iListOfQuestions)
        {
            Result result;
            EvaluationDAL evalDAL = new EvaluationDAL();
            result = evalDAL.UpdateCandidateScores(iListOfQuestions);
            return result;
        }
    }
}
