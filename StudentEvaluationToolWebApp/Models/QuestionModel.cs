using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentEvaluationToolWebApp.Models
{
    public class QuestionModel 
    {

        public string EvalName { get; set; }
        public string LMSGroupName { get; set; }
        public int CapstoneCandidateId { get; set; }
        public string CandidateName { get; set; }
        public int QuestionNumber { get; set; }
        public string QuestionText { get; set; }
        public string TypeShort { get; set; }
        public int RangeMin { get; set; }
        public int RangeMax { get; set; }
        public int CapstoneEvaluationResultId { get; set; }
        public int ResultValue { get; set; }
        public int UserIdEvaluator { get; set; }
        public int CapstoneEvaluatorId { get; set; }
        public string EvaluatorName { get; set; }
        public string JobTitle { get; set; }


    }
}