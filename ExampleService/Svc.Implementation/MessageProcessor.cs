using Newtonsoft.Json;
using Svc.Implementation.Model;
using System;
using System.Threading.Tasks;

namespace Svc.Implementation
{
    public class MessageProcessorExample
    {
        private readonly ServiceDbContext _db;

        public MessageProcessorExample(ServiceDbContext db)
        {
            _db = db;
        }

        public async Task Process(string jsonMessage)
        {
            var command = JsonConvert.DeserializeObject<MessageModelExample>(jsonMessage);

            var longRunningTask = _db.Tasks.Add(new LongRunningTask
            {
                Result = "No result",
                RunningTimeSeconds = command.NewTaskTime,
                Status = LongRunningTaskStatus.InProgress.ToString("G"),
                TaskName = command.NewTaskName
            });

            await _db.SaveChangesAsync();

            await Task.Delay(TimeSpan.FromSeconds(command.NewTaskTime));

            longRunningTask.Entity.Result = $"Task '{longRunningTask.Entity.Id}'-'{longRunningTask.Entity.TaskName}' completed after '{longRunningTask.Entity.RunningTimeSeconds}' seconds.";
            longRunningTask.Entity.Status = LongRunningTaskStatus.Completed.ToString("G");

            await _db.SaveChangesAsync();
        }
    }

    public class MessageModelExample
    {
        public string NewTaskName { get; set; }
        public int NewTaskTime { get; set; }
    }
}
