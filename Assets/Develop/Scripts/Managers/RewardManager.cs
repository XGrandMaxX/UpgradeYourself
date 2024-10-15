using Develop.Scripts.Data.Models;
using UnityEngine;

namespace Develop.Scripts.Managers
{
    public class RewardManager
    {
        public Reward GiveReward(Task task)
        {
            if (task.IsCompleted)
            {
                //Логика выдачи награды
                Debug.Log($"Reward given: {task.Reward.Coins} for completing {task.Title}");
                //Здесь можно добавить код для увеличения количества очков или других наград
            }

            return null;
        }
    }
}