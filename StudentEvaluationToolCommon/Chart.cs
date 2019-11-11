using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEvaluationToolCommon
{
    public class Chart
    {

        public int LMSUserId { get; set; }
        public int LMSGroupId { get; set; }
        public string LMSGroupName { get; set; }
        public int LMSCourseId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double ResultValue { get; set; }
        public string PerformanceName { get; set; }

    }
}
