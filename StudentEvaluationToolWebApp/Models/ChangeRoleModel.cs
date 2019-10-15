

namespace StudentEvaluationToolWebApp.Models
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class ChangeRoleModel : BaseModel
    {

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int SelectedRoleId { get; set; }

    
        public IEnumerable<SelectListItem> UserRoles { get; set; }
        public int UserId { get; set; }
    }
}