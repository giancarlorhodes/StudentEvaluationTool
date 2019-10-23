namespace StudentEvaluationToolWebApp.Controllers
{
    using StudentEvaluationToolBLL;
    using StudentEvaluationToolCommon;
    using StudentEvaluationToolWebApp.Common;
    using StudentEvaluationToolWebApp.Filter;
    using StudentEvaluationToolWebApp.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class DashboardController : Controller
    {
        // GET: Dashboard
        [MustBeLoggedIn]
        public ActionResult Index()
        {
            return RedirectToAction("Landing");
        }

        [HttpGet]
        [MustBeInRole(Roles = "Administrator, Evaluator, Employee")]
        public ActionResult Landing()
        {

            IList<CandidateModel> candidateModel = new List<CandidateModel>();
         
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


        [HttpGet]
        [MustBeInRole(Roles = "Administrator")]
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
        [MustBeInRole(Roles = "Administrator")]
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
        [MustBeInRole(Roles = "Administrator")]
        public ActionResult RoleChange(ChangeRoleModel iChangeRoleModel)
        {


            // TODO: need to be able to associate user with candidate or evaluator

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