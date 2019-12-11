

namespace StudentEvaluationToolWebApp.Common
{

    using StudentEvaluationToolCommon;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;


    public static class Utility
    {
        
        // roles for the application
        public static IEnumerable<SelectListItem> GetRoles()

        {
            // creating a IEnumerable that will be used indropddownfor helper
            SelectListItem selectListItemAdministrator = new SelectListItem();
            selectListItemAdministrator.Value = Convert.ToString((int)RoleType.Administrator);
            selectListItemAdministrator.Text = RoleType.Administrator.ToString();

            var roles = new List<SelectListItem>
            {
                selectListItemAdministrator,
                new SelectListItem { Value =  Convert.ToString((int)RoleType.Evaluator), Text = RoleType.Evaluator.ToString() },
                new SelectListItem { Value =  Convert.ToString((int)RoleType.Employee), Text = RoleType.Employee.ToString() }
            };

            SelectList roleList = new SelectList(roles, "Value", "Text");
            return roleList;
        }

        public static IEnumerable<SelectListItem> GetClassNames() 
        {
            // TODO: not very dynamic right now, need to fetch from the database
            var names = new List<SelectListItem>
            {
                new SelectListItem {Value = "Select Class", Text = "Select Class"},
                new SelectListItem {Value = "MA-DEV-2019-JUN", Text = "MA-DEV-2019-JUN"},
                new SelectListItem {Value = "MA-DEV-2019-SEPT", Text = "MA-DEV-2019-SEPT"}
            };

            SelectList nameList = new SelectList(names, "Value", "Text");
            return nameList;
        }







    }
}