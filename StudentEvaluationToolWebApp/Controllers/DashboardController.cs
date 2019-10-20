namespace StudentEvaluationToolWebApp.Controllers
{
    using StudentEvaluationToolBLL;
    using StudentEvaluationToolCommon;
    using StudentEvaluationToolWebApp.Common;
    using StudentEvaluationToolWebApp.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Landing()
        {

            IList<CandidateModel> candidateModel = new List<CandidateModel>();
            //{ new CandidateModel { FirstName =  "Giancarlo", LastName = "Rhodes", CapstoneCandidateId = 100, LMSGroupName="Rhodes-Oct-19" },
            //    new CandidateModel { FirstName =  "Kathy", LastName = "Rhodes", CapstoneCandidateId = 200,  LMSGroupName="Rhodes-Oct-19" },
            //    new CandidateModel { FirstName =  "Kaden", LastName = "Rhodes", CapstoneCandidateId = 300,  LMSGroupName="Rhodes-Oct-19" }
            //};


            if (Session["UserSession"] != null)
            {

                // TODO: receive the candidate list and then filter if the user is a employee or evaluator, admin see all candidates
                Result result = new Result();
                Mapper mapper = new Mapper();
                UserBLL userBLL = new UserBLL();


                // fetch by userid
                int iUserId = ((UserModel)Session["UserSession"]).UserID;
                int iRoleId = ((UserModel)Session["UserSession"]).RoleID;
                result = userBLL.FetchCandidates(iUserId, iRoleId);
                candidateModel = mapper.CandidateListToCandidateModelList(result.ListOfCandidates);

                return View(candidateModel);
            }
            else {

                return RedirectToAction("Login", "Home");
            
            }

        }


        [HttpGet]
        public ActionResult Users()
        {
            Result result = new Result();
            Mapper mapper = new Mapper();
            UserBLL userBLL = new UserBLL();

            // TODO: need to fetch from the database
            // this is just an hard coded example of how to pass a list to a view
            //List<UserModel> list = new List<UserModel>();
            //list.Add(new UserModel { UserID = 100, FirstName = "Joe", LastName = "Smith", Username = "joe29", RoleID=1, RoleName = "Administror", Password = "password123" });
            //list.Add(new UserModel { UserID = 200, FirstName = "Kate", LastName = "Watson", Username = "kate29", RoleID = 3, RoleName="Employee", Password="password123" });
            //list.Add(new UserModel { UserID = 300, FirstName = "John", LastName = "Evan", Username = "John29", RoleID = 2, RoleName="Evaluator", Password="password123" });


            // this will return all list of all the users in the database
            result = userBLL.FetchUsers(0);


            // maps the list from User to UserModel
            List<UserModel> userModelsList = mapper.UserListToUserModelList(result.ListOfUsers);

            return View(userModelsList);
        }



        [HttpGet]
        public ActionResult RoleChange(int iUserId)
        {

         
            if (Session["UserSession"] != null &&
                ((UserModel)Session["UserSession"]).RoleName == RoleType.Administrator.ToString())
            {
                // role is admin

                ChangeRoleModel model = new ChangeRoleModel();
                Result result = new Result();
                Mapper mapper = new Mapper();
                UserBLL userBLL = new UserBLL();

                // this will return all list of all the users in the database
                result = userBLL.FetchUsers(iUserId);


                // maps the list from User to UserModel
                // should only contain one and only one user
                model = mapper.UserToChangeModel(result.ListOfUsers[0]);


                return View(model);

            }
            else {

                // not a admin or null
                // send them to logout action

                return RedirectToAction("Logout", "Home");

            }

           
        }



        [HttpPost]
        public ActionResult RoleChange(ChangeRoleModel iChangeRoleModel)
        {

            ChangeRoleModel model = new ChangeRoleModel();
            Result result = new Result();
            Mapper mapper = new Mapper();
            UserBLL userBLL = new UserBLL();

            // update the role
            result = userBLL.UpdateRole(mapper.ChangeModelToCommonUser(iChangeRoleModel));


            // update the roles
            iChangeRoleModel.UserRoles = Utility.GetRoles();

            // update the message
            iChangeRoleModel.ModifyDialog(result);

            return View(iChangeRoleModel);
        }


    }
}