namespace StudentEvaluationToolWebApp.Models
{
    using StudentEvaluationToolCommon;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class BaseModel
    {
        public string DialogMessageType { get; set; }

        public string DialogMessage { get; set; }

        public BaseModel()
        {

            DialogMessage = "";
            DialogMessageType = ResultType.NOT_SET.ToString();

        }


        public void ModifyDialog(Result result) {


            this.DialogMessage = result.ResultMessage;
            this.DialogMessageType = result.ResultType.ToString();

        }
    }
}