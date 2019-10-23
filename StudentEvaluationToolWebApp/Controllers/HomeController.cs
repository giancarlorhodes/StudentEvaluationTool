﻿namespace StudentEvaluationToolWebApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using StudentEvaluationToolBLL;
    using StudentEvaluationToolCommon;
    using StudentEvaluationToolWebApp.Common;
    using StudentEvaluationToolWebApp.Filter;
    using StudentEvaluationToolWebApp.Models;

    /// <summary>
    /// Name:           Giancarlo Rhodes 
    /// Company:        Onshore Outsourcing
    /// Description:    Using the register process    
    /// </summary>
    public class HomeController : Controller
    {

        [HttpGet]      
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";
        //    ViewBag.Title = "Contact About";
        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";
        //    ViewBag.Title = "Contact Page";
        //    return View();
        //}

        [HttpPost]
        public ActionResult Register(UserModel userModel)
        {
            //ViewBag.Message = "Your application description page.";
            //ViewBag.Title = "Another Page";


            //TODO : need to prevent duplicate usernames

            if (ModelState.IsValid)
            {

                // need a mapper to go from UserModel to User (from common library)
                Mapper mapper = new Mapper();

                // create the user object that will receive the mapped object
                StudentEvaluationToolCommon.User user = new StudentEvaluationToolCommon.User();
                user = mapper.UserModelToCommonUser(userModel);

                // create BLL object for registration process
                RegistrationBLL registration = new RegistrationBLL();
                // passed the mapped 
                Result result = registration.Register(user);
                userModel.DialogMessageType = result.ResultType.ToString();
                userModel.DialogMessage = result.ResultMessage;

                return View(userModel);
            }
            else
            {
                return View(userModel);
            }         
        }

        [HttpGet]
        public ActionResult Register()
        {
            //ViewBag.Message = "Your application description page.";
            //ViewBag.Title = "Another Page";


            UserModel model = new UserModel();

            return View(model);
        }


        [HttpGet]
        public ActionResult Login()
        {           
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel userModel)
        {

            // TODO: need to dialog messsage for user or password incorrect.

            // need a mapper to go from UserModel to User (from common library)
            Mapper mapper = new Mapper();

            // create the user object that will receive the mapped object
            StudentEvaluationToolCommon.User user = new StudentEvaluationToolCommon.User();
            user = mapper.UserModelToCommonUser(userModel);

            // create BLL object for registration process
            RegistrationBLL login = new RegistrationBLL();
            // passed the mapped 
            Result result = login.Login(user);


            if (result.ListOfUsers.Count == 1)
            {
                userModel.FirstName = result.ListOfUsers[0].FirstName;
                userModel.LastName = result.ListOfUsers[0].LastName;
                userModel.RoleID = result.ListOfUsers[0].RoleID;
                userModel.RoleName = result.ListOfUsers[0].RoleName;
                userModel.UserID = result.ListOfUsers[0].UserID;
                userModel.Username = result.ListOfUsers[0].Username;
                Session["UserSession"] = userModel;


                // Advanced Auth LMS
                Session["AUTHUsername"] = result.ListOfUsers[0].Username;
                Session["AUTHRoles"] = result.ListOfUsers[0].RoleName;

                return RedirectToAction("Landing", "Dashboard");
            }
            else 
            {
                // TODO: error message wrong passsword or username
                return View();
            }
            
        }

        [HttpGet]
        [MustBeLoggedIn]
        public ActionResult LogOut()
        {
            // logout code
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request


            // go to the login
            return RedirectToAction("Login", "Home");
        }
    }
}