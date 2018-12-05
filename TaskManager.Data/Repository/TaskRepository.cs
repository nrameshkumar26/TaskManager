using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Data.Models;
using TaskManager.Data.Models.Custom;

namespace TaskManager.Data.Repository
{
    public class TaskRepository
    {
        #region GetParentTask
        /// <summary>
        /// Method to get parent tasks
        /// </summary>
        /// <returns></returns>
        public List<TaskModel> GetParentTask()
        {
            using (TaskManagerEntities entity = new TaskManagerEntities())
            {
                var parentTasks = (from task in entity.ParentTasks
                                   select new TaskModel()
                                   {
                                       ParentId = task.Parent_Id,
                                       ParentTask = task.Parent_Task
                                   }).ToList();

                return parentTasks;
            }
        }
        #endregion 

        #region GetAllTask
        /// <summary>
        /// Method to get all task
        /// </summary>
        /// <returns></returns>
        public List<TaskModel> GetAllTask()
        {
            using (TaskManagerEntities entity = new TaskManagerEntities())
            {
                var taskList = (from task in entity.Tasks.Include("ParentTask") orderby task.Task_Id descending
                                select new TaskModel()
                                {
                                    TaskId = task.Task_Id,
                                    Task = task.Task1,
                                    ParentTask = task.ParentTask.Parent_Task,
                                    Priority = task.Priority,
                                    StartDate = task.Start_Date,
                                    EndDate = task.End_Date,
                                    ParentId = task.ParentTask.Parent_Id,
                                    IsActive = task.IsActive
                                }).ToList();

                if (taskList != null)
                {
                    foreach (var item in taskList)
                    {
                        if (item.StartDate != null)
                            item.StartDateString = item.StartDate.ToString();
                        if (item.EndDate != null)
                            item.EndDateString = item.EndDate.ToString();
                    }
                }
                return taskList;
            }
        }
        #endregion

        #region InsertTask
        /// <summary>
        /// Method to create new task or update an existing task
        /// </summary>
        /// <param name="taskModel"></param>
        /// <returns></returns>
        public string InsertTask(TaskModel taskModel)
        {
            string result =string.Empty;
            using (TaskManagerEntities entity = new TaskManagerEntities())
            {
                if (taskModel != null)
                {
                    Task addTask = new Task();
                    addTask.Task1 = taskModel.Task;
                    if (taskModel.StartDateString != null)
                        addTask.Start_Date = Convert.ToDateTime(taskModel.StartDateString);
                    if (taskModel.EndDateString != null)
                        addTask.End_Date = Convert.ToDateTime(taskModel.EndDateString);
                    addTask.Priority = taskModel.Priority;
                    addTask.Parent_Id = taskModel.ParentId;
                    addTask.Task_Id = taskModel.TaskId;
                    addTask.IsActive = true;
                    result = addTask.Task_Id == 0 ? "ADD" : "UPDATE";
                    entity.Entry(addTask).State = addTask.Task_Id == 0 ? System.Data.Entity.EntityState.Added : System.Data.Entity.EntityState.Modified;
                    entity.SaveChanges();
                }
            }
            return result;
        }
        #endregion

        #region UpdateTask
        /// <summary>
        /// Method to end task
        /// </summary>
        /// <param name="taskModel"></param>
        /// <returns></returns>
        public bool UpdateTask(TaskModel taskModel)
        {
            using (TaskManagerEntities entity = new TaskManagerEntities())
            {
                if (taskModel != null && taskModel.TaskId != 0)
                {
                    Task endTask = new Task();
                    endTask.Task_Id = taskModel.TaskId;
                    endTask.Task1 = taskModel.Task;
                    if (taskModel.StartDateString != null)
                        endTask.Start_Date = Convert.ToDateTime(taskModel.StartDateString);
                    if (taskModel.EndDateString != null)
                        endTask.End_Date = Convert.ToDateTime(taskModel.EndDateString);
                    endTask.Priority = taskModel.Priority;
                    endTask.Parent_Id = taskModel.ParentId;
                    endTask.IsActive = false;
                    entity.Entry(endTask).State = System.Data.Entity.EntityState.Modified;
                    entity.SaveChanges();
                }
                return true;
            }
            
        }
        #endregion
    }
}
