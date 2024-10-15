using System;

namespace Develop.Scripts.Data.Models
{
    [System.Serializable]
    public class Task
    {
        public int Id;

        public string Title;
        public string Category;

        public Reward Reward;
        public Range recomendedLevel;

        public bool IsCompleted;

        public void CompleteTask() => IsCompleted = true;
    }

    [System.Serializable]
    public class Reward
    { 
        public int Coins;
        public int Exp;
    }
}
