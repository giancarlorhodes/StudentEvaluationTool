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

    public class DashboardController : BaseController
    {


        // fields
        private Mapper _mapper;
        private UserBLL _userBLL;

        public DashboardController() 
        {

            _mapper = new Mapper();
            _userBLL = new UserBLL(base.Connection);

        }

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
          

            // fetch by userid
            int iUserId = ((UserModel)Session["UserSession"]).UserID;
            int iRoleId = ((UserModel)Session["UserSession"]).RoleID;
            result = _userBLL.FetchCandidates(iUserId, iRoleId);
            candidateModel = _mapper.CandidateListToCandidateModelList(result.ListOfCandidates);

            return View(candidateModel);
           
        }


        [HttpGet]
        [MustBeInRole(Roles = "Administrator")]
        public ActionResult Users()
        {
            Result result = new Result();       


            // TODO: need to fetch from the database
            // this is just an hard coded example of how to pass a list to a view
            //List<UserModel> list = new List<UserModel>();
            //list.Add(new UserModel { UserID = 100, FirstName = "Joe", LastName = "Smith", Username = "joe29", RoleID=1, RoleName = "Administror", Password = "password123" });
            //list.Add(new UserModel { UserID = 200, FirstName = "Kate", LastName = "Watson", Username = "kate29", RoleID = 3, RoleName="Employee", Password="password123" });
            //list.Add(new UserModel { UserID = 300, FirstName = "John", LastName = "Evan", Username = "John29", RoleID = 2, RoleName="Evaluator", Password="password123" });

            // this will return all list of all the users in the database
            result = _userBLL.FetchUsers(0);

            // maps the list from User to UserModel
            List<UserModel> userModelsList = _mapper.UserListToUserModelList(result.ListOfUsers);

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

                // this will return all list of all the users in the database
                result = _userBLL.FetchUsers(iUserId);


                // maps the list from User to UserModel
                // should only contain one and only one user
                model = _mapper.UserToChangeModel(result.ListOfUsers[0]);


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

            // update the role
            result = _userBLL.UpdateRole(_mapper.ChangeModelToCommonUser(iChangeRoleModel));

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
            // populate the initial dropdown list
            ClassNameModel model = new ClassNameModel();
            return View(model);

        }


        [HttpPost]
        public ActionResult BuildChart(string iClassName)
        {

            // TESTING ONLY
            //string classname = "MA-DEV-2019-JUN";
            string classname = iClassName;

            // get data from db
            // assume we are only getting back data from single class 'MA-DEV-2019-JUN'
            ChartBLL chartBLL = new ChartBLL();
            List<Chart> chartData = chartBLL.GetChartData(classname).ListOfCharts.ToList();
         
            // get the types of data                        
            //string[] typesOfData = new string[] { "Wonderlic Cognitive", "Wonderlic Motivation", "Wonderlic Personality", "Bootcamp Technical",
            //                                            "Bootcamp Self-Learning", "Capstone Score" };
            // get the distinct from PerformanceName
            int countOfTypes = chartData.Select(m => m.PerformanceName).Distinct().Count();
            string[] typesOfData = new string[countOfTypes];
            List<string> dataNamesList = chartData.Select(m => m.PerformanceName).Distinct().ToList<string>();

            // need to populate the array
            int index = 0;
            foreach (var item in dataNamesList)
            {
                typesOfData[index] = item;
                index++;
            }

            // this will work much better now
            ChartModel chartModel = new ChartModel(typesOfData);
           
          
            RandomColorGenerator r = new RandomColorGenerator();
     
            // this is number to students that will be the final chart
            List<StudentEvaluationToolWebApp.Models.DataSets> lData = new List<DataSets>(chartData.Select(m => m.LMSUserId).Distinct().Count());



            // EXAMPLE DATA 
            //lData.Add(new ChartDataSets
            //{
            //    label = "Student A",
            //    data = new int[] { 80, 46, 14, 60, 60, 65 },
            //    backgroundColor = new string[] { "transparent" },
            //    borderColor = new string[] { "rgba(200,0,0,0.6)" },
            //    pointBackgroundColor = "rgba(200,0,0,0.6)",
            //    pointBorderColor = "rgba(200,0,0,0.6)"
            //});


            // make the datasets
            List<int> distintStudentIds = chartData.Select(m => m.LMSUserId).Distinct().ToList<int>();
            foreach (var lmsuserid in distintStudentIds)
            {

                // need to filter out just to this student
                List<Chart> chartStudentRows = chartData.Where(m => m.LMSUserId == lmsuserid).ToList();
                Chart studentCurrent = chartStudentRows.FirstOrDefault();
                string name = studentCurrent.FirstName + " " +  studentCurrent.LastName;
                int[] studentData = new int[chartStudentRows.Count()]; // number of rows for data for this student
                index = 0;
                // add data to the array
                foreach (var item in chartStudentRows)
                {
                    studentData[index] = Convert.ToInt32(item.ResultValue);
                    index++;
                }
                // student data should now be complete

                string studentColor = r.RGBAString();
                DataSets chartDataSetsCurrentStudent = new DataSets
                {
                    label = name,
                    data = studentData,
                    backgroundColor = new string[] { "transparent" },
                    borderColor = new string[] { studentColor },
                    pointBackgroundColor = studentColor,
                    pointBorderColor = studentColor
                };

                lData.Add(chartDataSetsCurrentStudent);

            }
                     

            chartModel.dataSets = lData;
            return Json(chartModel, JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        //public ActionResult BuildChart()
        //{



        //    string[] typesOfData = new string[] { "Wonderlic Cognitive", "Wonderlic Motivation", "Wonderlic Personality", "Bootcamp Technical",
        //                                                "Bootcamp Self-Learning", "Capstone Score" };
        //    ChartModel chartModel = new ChartModel(typesOfData);

        //    //// Fetch the DataTable from DataBase.
        //    //System.Data.DataTable dt = new System.Data.DataTable();
        //    //dt.Columns.AddRange(new System.Data.DataColumn[3] { new System.Data.DataColumn("ChartId", typeof(int)), new System.Data.DataColumn("ChartFields", typeof(string)),
        //    //    new System.Data.DataColumn("xaxisval",typeof(int)) });

           

        //    RandomColorGenerator r = new RandomColorGenerator();


        //    string _s = r.RGBAString();

        //    List<StudentEvaluationToolWebApp.Models.DataSets> lData = new List<DataSets>(3);

        //    lData.Add(new DataSets
        //    {
        //        label = "Student A",
        //        data = new int[] { 80, 46, 14, 60, 60, 65 },
        //        backgroundColor = new string[] { "transparent" },
        //        borderColor = new string[] { "rgba(200,0,0,0.6)" },
        //        pointBackgroundColor = "rgba(200,0,0,0.6)",
        //        pointBorderColor = "rgba(200,0,0,0.6)"
        //    });
        //    lData.Add(new DataSets
        //    {
        //        label = "Student B",
        //        data = new int[] { 87, 23, 10, 69, 45, 69 },
        //        backgroundColor = new string[] { "transparent" },
        //        borderColor = new string[] { "rgba(0,0,200,0.6)" },
        //        pointBackgroundColor = "rgba(0,0,200,0.6)",
        //        pointBorderColor = "rgba(0,0,200,0.6)"
        //    });
        //    lData.Add(new DataSets
        //    {
        //        label = "Student C",
        //        data = new int[] { 45, 34, 34, 78, 45, 67 },
        //        backgroundColor = new string[] { "transparent" },
        //        borderColor = new string[] { _s },
        //        pointBackgroundColor = _s,
        //        pointBorderColor = _s
        //    });



        //    chartModel.dataSets = lData;
        //    return Json(chartModel, JsonRequestBehavior.AllowGet);


        //}



        //TODO - candidates



        // TODO - evaluators


    }
}