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

    public class EvaluationController : BaseController
    {

        // fields
        private EvaluationBLL _evaluation;
        private Mapper _mapper;



        public EvaluationController()
        {

            // create BLL object for registration process
            _evaluation = new EvaluationBLL(base.Connection);
            _mapper = new Mapper();
        }





        [HttpGet]
        public ActionResult ViewEvaluation(int iCandidateId, int iEvaluatorId, int iUserIdEvaluator)
        {
            Result result = new Result();


            // this will return all list of all the users in the database
            result = _evaluation.GetQuestionsAndResults(iUserIdEvaluator, iCandidateId, iEvaluatorId);

            // map it
            QuestionModelList model = new QuestionModelList();
            model.ListOfQuestionModel = _mapper.QuestionListToQuestionModelList(result.ListOfQuestionResult);

            return View(model);
        }


        [HttpGet]
        public ActionResult EditEvaluation(int iCandidateId, int iEvaluatorId, int iUserIdEvaluator)
        {
            Result result = new Result();

            // this will return all list of all the users in the database
            result = _evaluation.GetQuestionsAndResults(iUserIdEvaluator, iCandidateId, iEvaluatorId);

            // map it
            QuestionModelList model = new QuestionModelList();
            model.ListOfQuestionModel = _mapper.QuestionListToQuestionModelList(result.ListOfQuestionResult);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditEvaluation(QuestionModelList iQuestionModelList)
        {
            Result result = new Result();

            List<Question> listOfQuestions = _mapper.ListOfQuestionModelToListofQuestion(iQuestionModelList.ListOfQuestionModel);
            result = _evaluation.UpdateCandidateCapstoneScore(listOfQuestions);


            // need to set the success or failure in model
            iQuestionModelList.DialogMessageType = result.ResultType.ToString();
            iQuestionModelList.DialogMessage = result.ResultMessage;

            return View(iQuestionModelList);
        }



    }
    
}