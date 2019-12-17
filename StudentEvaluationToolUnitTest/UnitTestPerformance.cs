using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentEvaluationToolBLL;
using StudentEvaluationToolCommon;

namespace StudentEvaluationToolUnitTest
{
    [TestClass]
    public class UnitTestPerformance
    {
        // setup
        public UnitTestPerformance() 
        { 
            
        
        }


        [TestMethod]
        public void Mocking_Some_Data()
        {
            // arrange
            var dataset = new DataSet();
            dataset.Tables.AddRange(new[]{
            GenerateDataTable<Person>(5000)});

            // act
            int _expected = 5000;
            int _actual = dataset.Tables[0].Rows.Count;

            // assert
            Assert.AreEqual(_expected, _actual);

        }



       

        private DataTable GenerateDataTable<T>(int rows)
        {
            var datatable = new DataTable(typeof(T).Name);
            typeof(T).GetProperties().ToList().ForEach(
                x => datatable.Columns.Add(x.Name));
            Builder<T>.CreateListOfSize(rows).Build()
                .ToList().ForEach(
                    x => datatable.LoadDataRow(x.GetType().GetProperties().Select(
                        y => y.GetValue(x, null)).ToArray(), true));
            return datatable;
        }


    }

}
    class Person
    {
        public string First { get; set; }
        public string Last { get; set; }
        public DateTime Birthday { get; set; }
    }


