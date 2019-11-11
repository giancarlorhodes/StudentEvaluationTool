using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentEvaluationToolWebApp.Common
{
    public static class BootCamp
    {
        public static List<string> BootCampNames { get; set; }

        static BootCamp()
        {
            BootCampNames.Add("MA-DEV-2019-JUN");
            BootCampNames.Add("MA-DEV-2019-SEPT");
        }

    }
}