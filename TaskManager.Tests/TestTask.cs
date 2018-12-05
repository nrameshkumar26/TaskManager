using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using TaskManager.Controllers;
using TaskManager.Data.Models.Custom;
using System.Web.Script.Serialization;

namespace TaskManager.Tests
{
    [TestClass]
    public class TestTask
    {
        TaskController controller = new TaskController();

        [TestMethod]
        public void GetAllTask()
        {
            var result = controller.GetAllTask();
            Assert.IsTrue(result.Count > 0);
        }
        [TestMethod]
        public void GetParentTask()
        {
            var result = controller.GetParentTask();
            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void InsertTask()
        {
            TaskModel addTask = new TaskModel();
            addTask.Task = "New Task from Test Method";
            addTask.StartDate = DateTime.Now;
            addTask.EndDate = DateTime.Now;
            addTask.Priority = 15;
            addTask.ParentId = 3;
            JavaScriptSerializer objJavascript = new JavaScriptSerializer();
            var testModels = objJavascript.Serialize(addTask);
            var isAdded = controller.InsertTaskDetails(testModels);
            Assert.AreEqual("ADD", isAdded);
        }

        [TestMethod]
        public void UpdateTask()
        {
            TaskModel updateTask = new TaskModel();
            updateTask.TaskId = 10;
            updateTask.Task = "Task from Test Method";
            updateTask.StartDate = DateTime.Now;
            updateTask.EndDate = DateTime.Now;
            updateTask.Priority = 1;
            updateTask.ParentId = 3;
            JavaScriptSerializer objJavascript = new JavaScriptSerializer();
            var testModels = objJavascript.Serialize(updateTask);
            var isUpdated = controller.InsertTaskDetails(testModels);
            Assert.AreEqual("UPDATE", isUpdated);
        }

        [TestMethod]
        public void EndTask()
        {
            TaskModel endTask = new TaskModel();
            endTask.TaskId = 10;
            endTask.Task = "Task from Test Method";
            endTask.StartDate = DateTime.Now;
            endTask.EndDate = DateTime.Now;
            endTask.Priority = 1;
            endTask.ParentId = 3;
            JavaScriptSerializer objJavascript = new JavaScriptSerializer();
            var testModels = objJavascript.Serialize(endTask);
            var isSuccess = controller.UpdateEndTask(testModels);
            Assert.AreEqual(true, isSuccess);
        }
    }
}
