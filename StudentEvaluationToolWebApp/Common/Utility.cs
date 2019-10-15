

namespace StudentEvaluationToolWebApp.Common
{

    using StudentEvaluationToolCommon;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;


    public static class Utility
    {

        public static IEnumerable<SelectListItem> GetRoles()

        {

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


    }
}