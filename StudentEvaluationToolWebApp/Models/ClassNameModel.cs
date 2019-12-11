namespace StudentEvaluationToolWebApp.Models
{
    using StudentEvaluationToolWebApp.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class ClassNameModel
    {

        public string SelectedClassName { get; set; }

        public IEnumerable<SelectListItem> ClassNames { get; set; }


        public ClassNameModel() {

            this.ClassNames = Utility.GetClassNames();
        
        }

    }
}