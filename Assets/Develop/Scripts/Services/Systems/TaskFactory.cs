using Develop.Scripts.Data.Models;

namespace Develop.Scripts.Services.Systems
{
    public sealed class TaskFactory
    {
        public Task CreateTask(Task task)
        {
            Task newTask = new()
            {
                Id = task.Id,

                Title = task.Title,
                Category = task.Category,

                recomendedLevel = task.recomendedLevel,
                Reward = task.Reward,

                IsCompleted = false
            };
            
            return newTask;
        }
    }
}
