using System.Collections.Generic;
using System.Web.Http;
using TaskManager.Business;
using TaskManager.Data.Models.Custom;

namespace TaskManager.Controllers
{
    public class TaskController : ApiController
    {
        TaskBusiness taskBusiness;

        [HttpGet]
        public List<TaskModel> GetParentTask()
        {
            taskBusiness = new TaskBusiness();
            var result = taskBusiness.GetParentTask();
            return result;
        }

        [HttpGet]
        public List<TaskModel> GetAllTask()
        {
            taskBusiness = new TaskBusiness();
            var result = taskBusiness.GetAllTask();
            return result;
        }

        [HttpPost]
        public string InsertTaskDetails(object task)
        {
            string result = string.Empty;
            taskBusiness = new TaskBusiness();
            result = taskBusiness.InsertTask(task);
            return result;
        }

        [HttpPost]
        public bool UpdateEndTask(object task)
        {
            taskBusiness = new TaskBusiness();
            return taskBusiness.UpdateTask(task);
        }
     }
}
