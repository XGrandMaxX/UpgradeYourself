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
        private readonly TaskLoader _taskLoader;
        private readonly TaskFactory _taskFactory;
        private readonly MyUserProfile _userProfile;

        public static List<Task> CreatedTasks { get; private set; } = new(1);

        public TaskCreationManager(
            TaskLoader taskLoader,
            TaskFactory taskFactory,
            MyUserProfile userProfile)
        {
            _taskLoader = taskLoader;
            _taskFactory = taskFactory;
            _userProfile = userProfile;

            Initialize();
        }

        //Do not place in the Resources folder!
        private static string GetJsonFilePath(string category, int minLevel, int maxLevel)
            => $"{Application.dataPath}/Develop/Configs/[{category}]TasksConfig({minLevel}-{maxLevel}lvl).json";

        private void Initialize()
        {
            LoadTasksForCategory("Sport", _userProfile.GetLevelByCategory("Sport"));
            LoadTasksForCategory("Career", _userProfile.GetLevelByCategory("Career"));
        }

        private void LoadTasksForCategory(string category, int level)
        {
            List<Task> tasks = GetLoadedTasksForCategory(category, level);

            if (tasks.Count <= 0)
            {
                Debug.LogError($"Json tasks count for {category} <= 0");
                return;
            }

            foreach (var task in tasks)
            {
                Debug.Log($"Creating task: <color=yellow>{task.Title}</color> in category: <color=yellow>{category}</color>, with ID: <color=yellow>{task.Id}</color>");
                Task newTask = _taskFactory.CreateTask(task);

                CreatedTasks.Add(newTask);
            }
        }

        private List<Task> GetLoadedTasksForCategory(string category, int level)
        {
            string path = GetJsonFilePathForLevel(category, level);

            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError("Путь к JSON файлу не найден.");
                return new List<Task>();
            }

            return _taskLoader.LoadTasks(path);
        }

        private string GetJsonFilePathForLevel(string category, int level)
        {
            return level switch
            {
                <= 10 => GetJsonFilePath(category, 0, 10),
                <= 20 => GetJsonFilePath(category, 11, 20),
                <= 30 => GetJsonFilePath(category, 21, 30),
                _ => throw new ArgumentOutOfRangeException(nameof(level), "Уровень должен быть от 0 до 30")
            };
        }
    }
}
