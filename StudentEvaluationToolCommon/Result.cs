namespace StudentEvaluationToolCommon
{

    using System.Collections.Generic;

    public class Result
    {
        public IList<User> ListOfUsers { get; set; }

        public IList<Candidate> ListOfCandidates { get; set; }

        public IList<Chart> ListOfCharts { get; set; }

        public ResultType ResultType { get; set; }

        public string ResultMessage { get; set; }
        public IList<Question> ListOfQuestionResult { get; set; }

        public Result() {

            ListOfUsers = new List<User>();
            ListOfCandidates = new List<Candidate>();
            ListOfQuestionResult = new List<Question>();
            this.ResultType = ResultType.Failure;
        }

    }
}
