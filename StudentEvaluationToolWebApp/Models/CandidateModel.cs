namespace StudentEvaluationToolWebApp.Models
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;


    public class CandidateModel : BaseModel
    {

        public int UserId { get; set; }        
        public int CandidateId { get; set; }
        public string CandidateFirstName { get; set; }
        public string CandidateLastName { get; set; }
        public int LMSUserId { get; set; }
        public int LMSGroupId { get; set; }
        public string LMSGroupName { get; set; }
        public  int LMSCourseId { get; set; }
        public int CandidateActive { get; set; }
        public int EvaluatorId { get; set; }
        public string EvaluatorFirstName { get; set; }
        public string EvaluatorFLastName { get; set; }
        public int EvaluatorActive { get; set; }






    }
}