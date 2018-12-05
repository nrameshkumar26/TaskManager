using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBench;
using TaskManager.Controllers;
using TaskManager.Data.Models.Custom;
using System.Web.Script.Serialization;

namespace TaskManager.NBench
{
    public class NBenchTest
    {
        TaskController taskController = new TaskController();
        private Counter _objCounter;

        [PerfSetup]
        public void SetUp(BenchmarkContext context)
        {
            _objCounter = context.GetCounter("TaskCounter");
        }

        [PerfBenchmark(Description = "Counter iteration performance test GETPARENTTASK()", NumberOfIterations = 15, RunMode = RunMode.Throughput, TestMode = TestMode.Measurement, RunTimeMilliseconds = 1000)]
        [CounterMeasurement("TaskCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void NBench_GetParentTask()
        {
            var bytes = new byte[1024];
            var result = taskController.GetParentTask();
            _objCounter.Increment();
        }

        [PerfBenchmark(Description = "Counter iteration performance test for GETALLTASK()", NumberOfIterations = 15, RunMode = RunMode.Throughput, TestMode = TestMode.Measurement, RunTimeMilliseconds = 1000)]
        [CounterMeasurement("TaskCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void NBench_GetAllTask()
        {
            var bytes = new byte[1024];
            var result = taskController.GetParentTask();
            _objCounter.Increment();
        }

        [PerfBenchmark(Description = "Counter iteration performance test INSERTTASK()", NumberOfIterations = 15, RunMode = RunMode.Throughput, TestMode = TestMode.Measurement, RunTimeMilliseconds = 1000)]
        [CounterMeasurement("TaskCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void NBench_InsertTask()
        {
            var bytes = new byte[1024];
            TaskModel addTask = new TaskModel();
            addTask.Task = "Task NBench";
            addTask.StartDate = DateTime.Now;
            addTask.EndDate = DateTime.Now;
            addTask.Priority = 15;
            addTask.ParentId = 3;
            JavaScriptSerializer objJavascript = new JavaScriptSerializer();
            var testModels = objJavascript.Serialize(addTask);
            var isAdded = taskController.InsertTaskDetails(testModels);
            _objCounter.Increment();
        }

        [PerfBenchmark(Description = "Counter iteration performance test UPDATETASK()", NumberOfIterations = 15, RunMode = RunMode.Throughput, TestMode = TestMode.Measurement, RunTimeMilliseconds = 1000)]
        [CounterMeasurement("TaskCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void NBench_UpdateTask()
        {
            var bytes = new byte[1024];
            TaskModel updateTask = new TaskModel();
            updateTask.TaskId = 2005;
            updateTask.Task = "Task from NBench";
            updateTask.StartDate = DateTime.Now;
            updateTask.EndDate = DateTime.Now;
            updateTask.Priority = 30;
            updateTask.ParentId = 2;
            JavaScriptSerializer objJavascript = new JavaScriptSerializer();
            var testModels = objJavascript.Serialize(updateTask);
            var isUpdated = taskController.InsertTaskDetails(testModels);
            _objCounter.Increment();
        }

        [PerfBenchmark(Description = "Counter iteration performance test ENDTASK()", NumberOfIterations = 15, RunMode = RunMode.Throughput, TestMode = TestMode.Measurement, RunTimeMilliseconds = 1000)]
        [CounterMeasurement("TaskCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void NBench_EndTask()
        {
            var bytes = new byte[1024];
            TaskModel endTask = new TaskModel();
            endTask.TaskId = 2005;
            endTask.Task = "Task from NBench";
            endTask.StartDate = DateTime.Now;
            endTask.EndDate = DateTime.Now;
            endTask.Priority = 30;
            endTask.ParentId = 2;
            JavaScriptSerializer objJavascript = new JavaScriptSerializer();
            var testModels = objJavascript.Serialize(endTask);
            var isSuccess = taskController.UpdateEndTask(testModels);
            _objCounter.Increment();
        }

        [PerfCleanup]
        public void Clean()
        {
            //Does nothing
        }
    }
}
