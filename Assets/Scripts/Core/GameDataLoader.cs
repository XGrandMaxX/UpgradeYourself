using System.Threading;
using DB;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class GameDataLoader : MonoBehaviour
    {
        [SerializeField] private Slider _physicalAbilitiesSlider;
        [SerializeField] private Slider _intellectualAbilitiesSlider;
        [SerializeField] private Slider _userExpSlider;

        [Space(20)] 
        
        [SerializeField] private TMP_Text _physicalAbilitiesValueText;
        [SerializeField] private TMP_Text _intellectualAbilitiesValueText;
        [SerializeField] private TMP_Text _userExpValueText;
        
        [Space(20)]
        
        [SerializeField] private TMP_Text _userLevel;
        [SerializeField] private TMP_Text _userUpCoins;
        [SerializeField] private TMP_Text _userName;
        
        private readonly CancellationTokenSource _source = new();
        
        private void Start() => LoadAndApplyData();
        private void LoadAndApplyData()
        {
            UserData userData = UserSession.UserData;
            
            _physicalAbilitiesSlider.value = userData.PhysicalAbilities;
            _intellectualAbilitiesSlider.value = userData.IntellectualAbilities;
            _userExpSlider.value = userData.LevelExp;
            
            _physicalAbilitiesValueText.text = $"{userData.PhysicalAbilities}/10000";
            _intellectualAbilitiesValueText.text = $"{userData.IntellectualAbilities}/10000";
            _userExpValueText.text = $"{userData.LevelExp}/100";
            
            _userLevel.text = $"Level {userData.Level}";
            _userUpCoins.text = userData.UpCoins.ToString();
            _userName.text = userData.Login;
        }

        private void OnDestroy()
        {
            _source.Cancel();
            _source.Dispose();
        }
    }
}
