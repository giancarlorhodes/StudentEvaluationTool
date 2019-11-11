using StudentEvaluationToolCommon;
using StudentEvaluationToolDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEvaluationToolBLL
{
    public class ChartBLL
    {
        public Result GetChartData(string className)
        {
            Result result = new Result();
            ChartDAL chartDAL = new ChartDAL();
            result = chartDAL.GetChartDataForClass(className);
            return result;
        }
    }
}
