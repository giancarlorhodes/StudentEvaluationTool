using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentEvaluationToolCommon;
using StudentEvaluationToolDAL;

namespace StudentEvaluationToolBLL
{
    public class EvaluationBLL : DbContextBLL
    {
        // fields
        private EvaluationDAL _evaluationDAL { get; set; }

        // constructor
        public EvaluationBLL(IDbConnection inConnection) 
        {
            _evaluationDAL = new EvaluationDAL(inConnection);            
        }

        public Result GetQuestionsAndResults(int iUserIdEvaluator, int iCandidateId, int iEvaluatorId)
        {            
            Result result = _evaluationDAL.GetCandidatesResult(iUserIdEvaluator, iCandidateId, iEvaluatorId);
            return result;            
        }

        public Result UpdateCandidateCapstoneScore(List<Question> iListOfQuestions)
        {
            Result result = _evaluationDAL.UpdateCandidateScores(iListOfQuestions);
            return result;
        }
    }
}
