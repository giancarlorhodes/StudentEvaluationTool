using StudentEvaluationToolWebApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentEvaluationToolWebApp.Models
{
    public class ChartModel
    {

        public string[] labels { get; set; }
        public List<DataSets> dataSets { get; set; }

      
        public ChartModel(string[] iLabels) 
        {
            this.labels = iLabels;
            this.dataSets = new List<DataSets>();
            

        }

        public ChartModel() { }

    }


    public class DataSets
    {
        public string label { get; set; }
        public string[] backgroundColor { get; set; }
        public string[] borderColor { get; set; }
        public int borderWidth { get; set; }
        public int[] data { get; set; }

        public int pointRadiuis { get; set; }
        public string pointBackgroundColor { get; set; }
        public string pointBorderColor { get; set; }
        public int pointHoverRadius { get; set; }

        public DataSets() {

            this.borderWidth = 3; 
            this.pointRadiuis = 3;
            this.pointHoverRadius = 3;
        }
    }
}