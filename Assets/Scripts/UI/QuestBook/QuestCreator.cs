using System.Collections.Generic;
using Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI.QuestBook
{
    public class QuestCreator : MonoBehaviour
    {
        [SerializeField] private AllQuests _allQuests;
        [SerializeField] private QuestCard[] questCard;
        [SerializeField] private Sprite[] _questCardBackgrounds;
        
        private readonly Dictionary<(float min, float max), QuestLevel> _levelRanges = new()
        {
            { (0, 75), QuestLevel.Base },
            { (75, 200), QuestLevel.Easy },
            { (200, 350), QuestLevel.Medium },
            { (350, 500), QuestLevel.Hard },
            { (500, 700), QuestLevel.Expert },
            { (700, float.MaxValue), QuestLevel.Absolute }
        };
        
        private readonly Dictionary<QuestRarity, (float min, float max)> _rarityRanges = new()
        {
            { QuestRarity.Common, (0, 45) },
            { QuestRarity.Uncommon, (45, 70) },
            { QuestRarity.Rare, (70, 85) },
            { QuestRarity.Epic, (85, 95) },
            { QuestRarity.Legendary, (95, 99) },
            { QuestRarity.Unique, (99, 100) }
        };

        private readonly Dictionary<QuestRarity, float> _rarityRates = new()
        {
            { QuestRarity.Common, 1 },
            { QuestRarity.Uncommon, 1.2f },
            { QuestRarity.Rare, 1.5f },
            { QuestRarity.Epic, 2f },
            { QuestRarity.Legendary, 3f },
            { QuestRarity.Unique, 5f }
        };

        private void Start()
        {
            VerifyQuestTimeElapsed();
            
            GenerateQuests();
        }

        private void GenerateQuests()
        {
            for (int i = 0; i < 8; i++)
            {
                QuestType type = RandomizeQuestType();
                QuestLevel level = GetQuestLevel(type);
                QuestRarity rarity = RandomizeQuestRarity();

                List<Quest> quests = _allQuests.GetAllQuestsByTypeAndLevel(type, level);
                
                Quest newFilteredQuest = quests[Random.Range(0, quests.Count)];
                Reward newReward = GetNewQuestReward(rarity, newFilteredQuest);
                Sprite questCardBackground = GetBackgroundAtRarity(rarity);

                questCard?[i].UpdateUI(newReward, newFilteredQuest, questCardBackground);
            }
        }
        
        private Sprite GetBackgroundAtRarity(QuestRarity rarity)
        {
            return rarity switch
            {
                QuestRarity.Common => _questCardBackgrounds[0],
                QuestRarity.Uncommon => _questCardBackgrounds[1],
                QuestRarity.Rare => _questCardBackgrounds[2],
                QuestRarity.Epic => _questCardBackgrounds[3],
                QuestRarity.Legendary => _questCardBackgrounds[4],
                QuestRarity.Unique => _questCardBackgrounds[5],
                _ => _questCardBackgrounds[0]
            };
        }

        private Reward GetNewQuestReward(QuestRarity rarity, Quest quest)
        {
            float rewardRate = GetRewardMultiplier(rarity);
            
            Reward newReward = quest.Rewards.GetReward(rewardRate);

            return newReward;
        }
        
        private float GetRewardMultiplier(QuestRarity rarity) => _rarityRates[rarity];

        private QuestType RandomizeQuestType() => (QuestType)Random.Range(0, (int)QuestType.Professional + 1);

        private QuestRarity RandomizeQuestRarity()
        {
            float randomValue = Random.Range(0f, 100f);

            foreach (var entry in _rarityRanges)
            {
                if (randomValue >= entry.Value.min && randomValue < entry.Value.max)
                    return entry.Key;
            }
            
            return QuestRarity.Common;
        }

        private QuestLevel GetQuestLevel(QuestType type)
        {
            float value = type switch
            {
                QuestType.Intellectual => UserSession.UserData.IntellectualAbilities,
                QuestType.Physical => UserSession.UserData.PhysicalAbilities,
                QuestType.Professional => UserSession.UserData.ProfessionalAbilities,
                _ => 0
            };

            foreach (var range in _levelRanges)
            {
                if (value >= range.Key.min && value < range.Key.max)
                    return range.Value;
            }
            
            return QuestLevel.Base;
        }
        private void VerifyQuestTimeElapsed()
        {
            
        }
    }
}
