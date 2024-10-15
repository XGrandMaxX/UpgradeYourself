using System.Collections.Generic;
using System.IO;
using Develop.Scripts.Data.Models;
using UnityEngine;

namespace Develop.Scripts.Data.Repositories
{
    [System.Serializable]
    public sealed class TaskLoader
    {
        public List<Task> LoadTasks(string jsonFilePath)
        {
            if (File.Exists(jsonFilePath))
            {
                string json = File.ReadAllText(jsonFilePath);
                TaskListWrapper taskListWrapper = JsonUtility.FromJson<TaskListWrapper>(json);

                return taskListWrapper.tasks;
            }

            Debug.LogError("JSON file not found at: " + jsonFilePath);
            return new List<Task>();
        }
    }

    [System.Serializable]
    public sealed class TaskListWrapper
    {
        public List<Task> tasks;
    }
}