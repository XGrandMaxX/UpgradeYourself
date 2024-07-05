using System;
using System.Threading;
using System.Threading.Tasks;
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
        
        [Space(20)]
        
        private UserSession _userSession;
        private CancellationTokenSource _source = new();
        private async void Start()
        {
            await FindUserSession();
            LoadAndApplyData();
        }

        private async Task FindUserSession(int maxRetries = 40, int delayMilliseconds = 250)
        {
            int retryCount = 0;
    
            while (retryCount < maxRetries)
            {
                _userSession = FindObjectOfType<UserSession>();
        
                if (_userSession != null)
                    return;
        
                retryCount++;
        
                try
                {
                    await Task.Delay(delayMilliseconds, _source.Token);
                }
                catch (TaskCanceledException)
                {
                    // Логика для обработки отмены задачи, если необходимо.
                    throw new OperationCanceledException("Operation was cancelled", _source.Token);
                }
            }

            throw new Exception("User Session was not found!");
        }
        
        private void LoadAndApplyData()
        {
            _physicalAbilitiesSlider.value = _userSession.UserData.PhysicalAbilities;
            _intellectualAbilitiesSlider.value = _userSession.UserData.IntellectualAbilities;
            _userExpSlider.value = _userSession.UserData.Exp;

            _physicalAbilitiesValueText.text = $"{_userSession.UserData.PhysicalAbilities}/10000";
            _intellectualAbilitiesValueText.text = $"{_userSession.UserData.IntellectualAbilities}/10000";
            _userExpValueText.text = $"{_userSession.UserData.Exp}/100";
            
            _userLevel.text =  $"Level: {_userSession.UserData.Level}";
            _userUpCoins.text = _userSession.UserData.UpCoins.ToString();
        }

        private void OnDestroy()
        {
            _source.Cancel();
            _source.Dispose();
        }
    }
}
