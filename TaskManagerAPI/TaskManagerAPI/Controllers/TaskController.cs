using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.ComponentModel;
using TaskManagerAPI.DTO;
using TaskManagerAPI.Entity;
using Container = Microsoft.Azure.Cosmos.Container;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "TaskManager";
        public string ContainerName = "TaskContainer1";

        public readonly Container _container;
        public TaskController()
        {
            _container = GetContainer();
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(TaskModel taskModel)
        {
            try
            {
                Tasks taskEntity = new Tasks();

                taskEntity.Name=taskModel.Name;
                taskEntity.TaskDesc=taskModel.TaskDesc;

                taskEntity.Id=Guid.NewGuid().ToString();
                taskEntity.UId = taskEntity.Id;
                taskEntity.DocumentType = "Task";

                taskEntity.CreatedOn = DateTime.Now;
                taskEntity.CreatedByName = "Kumar";
                taskEntity.CreatedBy = "Kumar's UId";

                taskEntity.UpdatedOn= DateTime.Now;
                taskEntity.UpdatedByName = "Kumar";
                taskEntity.UpdatedBy = "Kumar's UId";

                taskEntity.Version = 1;
                taskEntity.Active = true;
                taskEntity.Archieved = false;

                Tasks response=await _container.CreateItemAsync(taskEntity);

                taskModel.Name = response.Name;
                taskModel.TaskDesc = response.TaskDesc;

                return Ok(taskModel);

            }
            catch (Exception ex)
            {
                return BadRequest("Data Adding Failed" + ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask(string uId, string name, string taskDesc)
        {

            Tasks existingTask = _container.GetItemLinqQueryable<Tasks>(true).Where(q => q.DocumentType == "Task" && q.UId == uId).AsEnumerable().FirstOrDefault();
            if (existingTask != null)
            {
                existingTask.Name=name;
                existingTask.TaskDesc=taskDesc;
                existingTask.Version++;

                try
                {
                    var response = await _container.UpsertItemAsync(existingTask, new PartitionKey(uId));
                    if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return Ok("Task Updated Successfully");
                    }
                    else
                    {
                        return BadRequest("Failed to Update Task");
                    }

                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                
            }

            return BadRequest("UId Not Found");
        }

        [HttpPost]
        public IActionResult GetTaskByUId(string uId)
        {
            try
            {
                Tasks tasks = _container.GetItemLinqQueryable<Tasks>(true).Where(q => q.DocumentType == "Task" && q.UId==uId).AsEnumerable().FirstOrDefault();
                
                var taskModel = new TaskModel();
                taskModel.Name = tasks.Name;
                taskModel.TaskDesc = tasks.TaskDesc;
                return Ok(taskModel);
            }
            catch (Exception ex)
            {
                return BadRequest("Data Get Failed");
            }
        }

        [HttpPost]
        public IActionResult GetAllTask()
        {
            try
            {
                var taskList = _container.GetItemLinqQueryable<Tasks>(true).Where(q=>q.DocumentType=="Task").AsEnumerable().ToList();
                return Ok(taskList);
            }
            catch (Exception ex)
            {
                return BadRequest("Data Get Failed");
            }
        }

        [HttpDelete]
        public async Task DeleteTask(string uId)
        {
            await _container.DeleteItemAsync<Tasks>(uId,new PartitionKey(uId));
        }
      private Container GetContainer()
        {
            CosmosClient cosmosclient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosclient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;
        }
    }
}
