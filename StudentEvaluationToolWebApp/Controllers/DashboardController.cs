namespace StudentEvaluationToolWebApp.Controllers
{
    using StudentEvaluationToolBLL;
    using StudentEvaluationToolCommon;
    using StudentEvaluationToolWebApp.Common;
    using StudentEvaluationToolWebApp.Filter;
    using StudentEvaluationToolWebApp.Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
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
        [MustBeInRole(Roles = "Administrator,Evaluator,Employee")]
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


        //TODO - charts



        [HttpGet]
        //[MustBeInRole(Roles = "Administrator")]
        public ActionResult BarChartExample()
        {

            return View();

        }


        [HttpPost]
        //[MustBeInRole(Roles = "Administrator")]
        public ActionResult BuildBarChartExample()
        {

            List<object> iData = new List<object>();
            //Creating sample data  
            DataTable dt = new DataTable();
            dt.Columns.Add("Employee", System.Type.GetType("System.String"));
            dt.Columns.Add("Credit", System.Type.GetType("System.Int32"));

            DataRow dr = dt.NewRow();
            dr["Employee"] = "Sam";
            dr["Credit"] = 123;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Employee"] = "Alex";
            dr["Credit"] = 456;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Employee"] = "Michael";
            dr["Credit"] = 587;
            dt.Rows.Add(dr);
            //Looping and extracting each DataColumn to List<Object>  
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            //Source data returned as JSON  
            return Json(iData, JsonRequestBehavior.AllowGet);

        }


     

        [HttpGet]
        //[MustBeInRole(Roles = "Administrator")]
        public ActionResult RadarChartExample()
        {

            return View();

        }

        // https://www.aspforums.net/Threads/386204/Populate-Radar-Chart-from-Database-using-jQuery-ChartJS-Plugin-in-ASPNet-MVC/
        [HttpPost]
        public ActionResult BuildRadarChartExample() 
        {

            ChartModel charts = new ChartModel();
            // Fetch the DataTable from DataBase.
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.AddRange(new System.Data.DataColumn[3] { new System.Data.DataColumn("ChartId", typeof(int)), new System.Data.DataColumn("ChartFields", typeof(string)), 
                new System.Data.DataColumn("xaxisval",typeof(int)) });
            
            dt.Rows.Add(1, "Prev year", 2);
            dt.Rows.Add(2, "Prev year", 3);
            dt.Rows.Add(3, "Prev year", 5);
            dt.Rows.Add(4, "Prev year", 7);
            dt.Rows.Add(5, "Prev year", 8);
            dt.Rows.Add(6, "Prev year", 9);
            dt.Rows.Add(7, "Current year", 1);
            dt.Rows.Add(8, "Current year", 3);
            dt.Rows.Add(9, "Current year", 5);
            dt.Rows.Add(10, "Current year", 7);
            dt.Rows.Add(11, "Current year", 8);
            dt.Rows.Add(12, "Current year", 9);
            dt.Rows.Add(13, "Final year", 1);
            dt.Rows.Add(14, "Final year", 3);
            dt.Rows.Add(15, "Final year", 5);
            dt.Rows.Add(16, "Final year", 7);
            dt.Rows.Add(17, "Final year", 8);
            dt.Rows.Add(18, "Final year", 9);

            charts.labels = (from p in dt.AsEnumerable() select p.Field<string>("ChartFields")).Distinct().ToArray();

            List<string> chartFields = (from p in dt.AsEnumerable() select p.Field<string>("ChartFields")).Distinct().ToList();
            List<DataSets> dataSets = new List<DataSets>();
            foreach (string chartField in chartFields)
            {
                int[] x = (from p in dt.AsEnumerable()
                           where p.Field<string>("ChartFields") == chartField
                           select p.Field<int>("xaxisval")).ToArray();
                dataSets.Add(new DataSets()
                {
                    label = chartField,
                    data = x,
                    borderWidth = 3
                });
            }

            charts.dataSets = dataSets;
            return Json(charts, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        //[MustBeInRole(Roles = "Administrator")]
        public ActionResult Chart()
        {

            return View();

        }



        [HttpPost]
        public ActionResult BuildChart()
        {



            string[] typesOfData = new string[] { "Wonderlic Cognitive", "Wonderlic Motivation", "Wonderlic Personality", "Bootcamp Technical", 
                                                        "Bootcamp Self-Learning", "Capstone Score" };
            ChartModel chartModel = new ChartModel(typesOfData);

            // Fetch the DataTable from DataBase.
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.AddRange(new System.Data.DataColumn[3] { new System.Data.DataColumn("ChartId", typeof(int)), new System.Data.DataColumn("ChartFields", typeof(string)),
                new System.Data.DataColumn("xaxisval",typeof(int)) });

            /// TODO - database call
            //dt.Rows.Add(1, "Prev year", 2);
            //dt.Rows.Add(2, "Prev year", 3);
            //dt.Rows.Add(3, "Prev year", 5);
            //dt.Rows.Add(4, "Prev year", 7);
            //dt.Rows.Add(5, "Prev year", 8);
            //dt.Rows.Add(6, "Prev year", 9);
            //dt.Rows.Add(7, "Current year", 1);
            //dt.Rows.Add(8, "Current year", 3);
            //dt.Rows.Add(9, "Current year", 5);
            //dt.Rows.Add(10, "Current year", 7);
            //dt.Rows.Add(11, "Current year", 8);
            //dt.Rows.Add(12, "Current year", 9);
            //dt.Rows.Add(13, "Final year", 1);
            //dt.Rows.Add(14, "Final year", 3);
            //dt.Rows.Add(15, "Final year", 5);
            //dt.Rows.Add(16, "Final year", 7);
            //dt.Rows.Add(17, "Final year", 8);
            //dt.Rows.Add(18, "Final year", 9);

            //dt.Rows.Add(1, "Wonderlic Cognitive", 80);
            //dt.Rows.Add(1, "Wonderlic Motivation", 46);
            //dt.Rows.Add(1, "Wonderlic Personality", 14);
            //dt.Rows.Add(1, "Bootcamp Technical", 60);
            //dt.Rows.Add(1, "Bootcamp Self-Learning", 60);
            //dt.Rows.Add(1, "Capstone Score", 65);

            //dt.Rows.Add(2, "Wonderlic Cognitive", 79);
            //dt.Rows.Add(2, "Wonderlic Motivation", 79);
            //dt.Rows.Add(2, "Wonderlic Personality", 89);
            //dt.Rows.Add(2, "Bootcamp Technical", 76);
            //dt.Rows.Add(2, "Bootcamp Self-Learning", 70);
            //dt.Rows.Add(2, "Capstone Score", 90);

            //chartModel.labels = (from p in dt.AsEnumerable() select p.Field<string>("ChartFields")).Distinct().ToArray();

            // chartModel.labels = new string[] { "Wonderlic Cognitive",, "Wonderlic Motivation", "Wonderlic Personality",
            // "Bootcamp Technical", "Bootcamp Self-Learning", "Capstone Score"};

            // List<string> chartFields = (from p in dt.AsEnumerable() select p.Field<string>("ChartFields")).Distinct().ToList();

            //List<string> candidates = new List<string>();
            //candidates.Add("Student A");
            //candidates.Add("Student B");
            //candidates.Add("Student C");

            RandomColorGenerator r = new RandomColorGenerator();

           
            string _s = r.RGBAString();

            List<StudentEvaluationToolWebApp.Models.DataSets> lData = new List<DataSets>(3);

            lData.Add(new DataSets { label = "Student A", data = new int[] { 80, 46, 14, 60, 60, 65 }, 
                backgroundColor = new string[] { "transparent" }, borderColor = new string[] { "rgba(200,0,0,0.6)" }, pointBackgroundColor = "rgba(200,0,0,0.6)", 
                pointBorderColor = "rgba(200,0,0,0.6)" });
            lData.Add(new DataSets { label = "Student B", data = new int[] { 87, 23, 10, 69, 45, 69 }, 
                backgroundColor = new string[] { "transparent" }, borderColor = new string[] { "rgba(0,0,200,0.6)" }, pointBackgroundColor = "rgba(0,0,200,0.6)", 
                pointBorderColor = "rgba(0,0,200,0.6)" });
            lData.Add(new DataSets { label = "Student C", data = new int[] { 45, 34, 34, 78, 45, 67 }, 
                backgroundColor = new string[] { "transparent" }, borderColor = new string[] { _s }, pointBackgroundColor = _s, 
                pointBorderColor = _s  });







            ////List<DataSets> dataSets = new List<DataSets>();
            //foreach (string label in chartModel.labels)
            //{
            //    //int[] x = (from p in dt.AsEnumerable()
            //    //           where p.Field<string>("ChartFields") == chartField
            //    //           select p.Field<int>("xaxisval")).ToArray();

            //    int[] lData = new int[] { 80, 46, 14, 60, 60, 65 };


            //    dataSets.Add(new DataSets()
            //    {
            //        label = chartField,
            //        data = lData,
            //        borderWidth = "1"
            //    });
            //}

            chartModel.dataSets = lData;
            return Json(chartModel, JsonRequestBehavior.AllowGet);


        }



            //TODO - candidates



            // TODO - evaluators


        }
}