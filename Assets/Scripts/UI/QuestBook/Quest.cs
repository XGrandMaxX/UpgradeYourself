using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.QuestBook
{
    public enum QuestType
    {
        Physical,       // Физические квесты: связанные с физической активностью и упражнениями
        Intellectual,   // Интеллектуальные квесты: связанные с умственной деятельностью, обучением и саморазвитием
        Professional    // Профессиональные квесты: связанные с развитием хард скиллов и профессиональных навыков
    }
    
    public enum QuestLevel
    {
        Base,           //1-75 level
        Easy,           //76-200 level
        Medium,         //201-350 level
        Hard,           //351-500 level
        Expert,         //501-700 level
        Absolute        //701+ level
    }
    
    public enum QuestRarity
    {
        Common,         //Base reward rate
        Uncommon,       //reward rate * 1.2f
        Rare,           //reward rate * 1.5f
        Epic,           //reward rate * 2
        Legendary,      //reward rate * 3
        Unique          //reward rate * 5
    }
    
    [CreateAssetMenu(menuName = "AllQuests", fileName = "AllQuests")]
    public class AllQuests : ScriptableObject
    {
        [SerializeField] private List<QuestList> questLists;

        public List<Quest> GetAllQuestsByTypeAndLevel(QuestType type, QuestLevel level) 
            => questLists
                .SelectMany(ql => ql.GetQuestsByType(type))
                .Where(q => q.Level == level)
                .ToList();
        
        //!OBSOLETE!
        
        // public List<Quest> FilterQuests(List<Quest> quests, QuestLevel level)
        // {
        //     return quests
        //         .Where(q => q.Level == level)
        //         .ToList();
        // }
    }
    
        
    [CreateAssetMenu(menuName = "QuestList", fileName = "QuestList")]
    public class QuestList : ScriptableObject
    {
        [SerializeField] private List<Quest> _physicalQuests;
        [SerializeField] private List<Quest> _intellectualQuests;
        [SerializeField] private List<Quest> _professionalQuests;

        public List<Quest> GetQuestsByType(QuestType type)
        {
            return type switch
            {
                QuestType.Physical => _physicalQuests,
                QuestType.Intellectual => _intellectualQuests,
                QuestType.Professional => _professionalQuests,
                _ => new List<Quest>()
            };
        }
    }
    
    
    [System.Serializable]
    public struct Quest
    {
        [field: SerializeField, TextArea(1, 5)] internal string Description { get; private set; }
        
        [field: SerializeField] internal QuestLevel Level { get; private set; }
        
        
        [field: SerializeField] internal Reward Rewards { get; private set; }
    }
}
