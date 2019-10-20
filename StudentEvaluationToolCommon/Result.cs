﻿namespace StudentEvaluationToolCommon
{

    using System.Collections.Generic;

    public class Result
    {
        public List<User> ListOfUsers { get; set; }

        public IList<Candidate> ListOfCandidates { get; set; }

        public ResultType ResultType { get; set; }

        public string ResultMessage { get; set; }

        public Result() {

            ListOfUsers = new List<User>();
            ListOfCandidates = new List<Candidate>();
            this.ResultType = ResultType.Failure;
        }

    }
}
