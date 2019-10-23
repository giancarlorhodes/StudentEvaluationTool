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
        [HttpGet]
        public ActionResult ViewEvaluation(int iCandidateId, int iEvaluatorId, int iUserIdEvaluator)
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditEvaluation(int iCandidateId, int iEvaluatorId, int iUserIdEvaluator)
        {
            Result result = new Result();
            Mapper mapper = new Mapper();
            EvaluationBLL evalBLL = new EvaluationBLL();

            // this will return all list of all the users in the database
            result = evalBLL.GetQuestionsAndResults(iUserIdEvaluator, iCandidateId, iEvaluatorId);

            // map it
            QuestionModelList model = new QuestionModelList();
            model.ListOfQuestionModel = mapper.QuestionListToQuestionModelList(result.ListOfQuestionResult);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditEvaluation(QuestionModelList iQuestionModelList)
        {
            Result result = new Result();
            Mapper mapper = new Mapper();
            EvaluationBLL evalBLL = new EvaluationBLL();

            List<Question> listOfQuestions = mapper.ListOfQuestionModelToListofQuestion(iQuestionModelList.ListOfQuestionModel);
            result = evalBLL.UpdateCandidateCapstoneScore(listOfQuestions);


            // need to set the success or failure in model
            iQuestionModelList.DialogMessageType = result.ResultType.ToString();
            iQuestionModelList.DialogMessage = result.ResultMessage;

            return View(iQuestionModelList);
        }




    }
}