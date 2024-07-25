using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.QuestBook
{
    public class QuestCard : MonoBehaviour
    {
        [Header("Quest card Settings")] 
        [SerializeField] private Image[] _questCardRewards;
        [SerializeField] private Image _questCardBackground;
        [SerializeField] private TMP_Text _questCardDescription;

        public void UpdateUI(Reward reward, Quest newFilteredQuest, Sprite questCardBackground)
        {
            _questCardDescription.text = newFilteredQuest.Description;

            _questCardBackground.sprite = questCardBackground;
            
            for (int i = 0; i < reward.RewardImg.Length; i++)
                _questCardRewards[i].sprite = reward.RewardImg[i];
        }
    }
}
