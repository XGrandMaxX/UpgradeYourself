using System;
using System.Collections.Generic;
using Develop.Scripts.Data.Models;
using Develop.Scripts.Data.Repositories;
using Develop.Scripts.Services.Systems;
using UnityEngine;

namespace Develop.Scripts.Managers
{
    public sealed class TaskCreationManager
    {
        private TaskLoader _taskLoader;
        private RewardManager _rewardManager;
        private TaskFactory _taskFactory;

        public static List<Task> CreatedTasks { get; private set; } = new(1);

        public TaskCreationManager(TaskLoader taskLoader, RewardManager rewardManager, TaskFactory taskFactory)
        {
            _taskLoader = taskLoader;
            _rewardManager = rewardManager;
            _taskFactory = taskFactory;

            Initialize();
        }

        //Do not place in the Resources folder!
        private static string GetJsonFilePath(int minLevel, int maxLevel) 
            => $"{Application.dataPath}/Develop/Configs/TasksConfig({minLevel}-{maxLevel}lvl).json";

        private void Initialize()
        {
            List<Task> jsonTasks = GetLoadedTasks(1);
            
            if (jsonTasks.Count <= 0)
            {
                Debug.LogError("Json tasks count <= 0");
                return;
            }

            foreach (var jsonTask in jsonTasks)
            {
                Debug.Log($"Creating task: <color=yellow>{jsonTask.Title}</color>, with ID: <color=yellow>{jsonTask.Id}</color>, {jsonTask.Reward.Coins}");

                Task task = _taskFactory.CreateTask(jsonTask);

                CreatedTasks.Add(task);
            }
        }

        private List<Task> GetLoadedTasks(int level)
        {
            string path = GetJsonFilePathForLevel(level);

            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError("Путь к JSON файлу не найден.");
                return new List<Task>();
            }

            return _taskLoader.LoadTasks(path);
        }


        private string GetJsonFilePathForLevel(int level)
        {
            return level switch
            {
                >= 0 and <= 10 => GetJsonFilePath(0, 10),
                >= 11 and <= 20 => GetJsonFilePath(11, 20),
                >= 21 and <= 30 => GetJsonFilePath(21, 30),
                _ => throw new ArgumentOutOfRangeException(nameof(level), "Уровень должен быть от 0 до 30")
            };
        }
    }
}
