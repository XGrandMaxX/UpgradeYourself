using System;
using UnityEngine;

namespace UI.QuestBook
{
    [Serializable]
    public struct Reward
    {
        [SerializeField] internal Sprite[] RewardImg;
        
        [Min(0),SerializeField] internal float LevelExp;
        [Min(0),SerializeField] internal float UpCoins;
        
        internal float PhysicalAbilities;
        internal float IntellectualAbilities;
        internal float ProfessionalAbilities;
        
        public Reward GetReward(float multiplier = 1)
        {
#if UNITY_EDITOR
            if (multiplier < 1)
                throw new Exception("Multiplier must be greater than or equal to 1");
#endif

            Reward reward = this;
            reward.RewardImg = RewardImg;
            reward.LevelExp *= multiplier;
            reward.UpCoins *= multiplier;
            reward.PhysicalAbilities *= multiplier;
            reward.IntellectualAbilities *= multiplier;
            reward.ProfessionalAbilities *= multiplier;
            
            Debug.Log($"{reward.RewardImg}, {reward.LevelExp}, {reward.UpCoins}");
            return reward;
        }
    }
}
