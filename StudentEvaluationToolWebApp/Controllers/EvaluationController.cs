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

    public class EvaluationController : Controller
    {
        
        public ActionResult ViewOnly(int iCandidateId, int iEvaluatorId, int iUserId)
        {
            return View();
        }


        public ActionResult EditEvaluation(int iCandidateId, int iEvaluatorId, int iUserId)
        {
            return View();
        }



    }
}