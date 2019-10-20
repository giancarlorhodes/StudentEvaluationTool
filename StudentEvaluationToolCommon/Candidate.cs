namespace StudentEvaluationToolCommon
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Candidate
    {
        public int UserId { get; set; }
        public int CapstoneCandidateId { get; set; }
        public string CandidateFirstName { get; set; }
        public string CandidateLastName { get; set; }
        public int CandidateLMSUserId { get; set; }
        public int CandidateLMSGroupId { get; set; }
        public string CandidateLMSGroupName { get; set; }
        public int CandidateLMSCourseId { get; set; }
        public int CandidateActiveFlag { get; set; }
	    public int CapstoneEvaluatorId { get; set; }
	    public string EvaluatorFirstName { get; set; }
	    public string EvaluatorLastName { get; set; }
	    public string EvaluatorJobTitle { get; set; }
	    public int EvaluatorActiveFlag { get; set; }

    }
}
