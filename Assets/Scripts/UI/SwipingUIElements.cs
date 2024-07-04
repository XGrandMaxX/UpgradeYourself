using DG.Tweening;
using UnityEngine;
using Zenject;

namespace UI
{
    public sealed class SwipingUIElements : MonoBehaviour
    {
        [SerializeField] private RectTransform _panel1;
        [SerializeField] private RectTransform _panel2;
        
        [Min(0),SerializeField] private float _animationDuration = 1f;
        
        private bool _operationInProgress;
        private bool _isPanel1Active = true;
        
        private InputUIMessages _inputUIMessages;

        [Inject]
        private void Construct(InputUIMessages inputUIMessages) => _inputUIMessages = inputUIMessages;

        public async void SwipePanelsAsync()
        {
            if (_operationInProgress)
                return;
            
            _inputUIMessages.SetRegisterErrorMessage(null);
            _inputUIMessages.SetLoginErrorMessage(null);

            _operationInProgress = true;
            
            RectTransform activePanel = _isPanel1Active ? _panel1 : _panel2;
            RectTransform nextPanel = _isPanel1Active ? _panel2 : _panel1;

            await activePanel.DOAnchorPosX(-activePanel.rect.width, _animationDuration)
                .SetEase(Ease.OutExpo)
                .AsyncWaitForCompletion();

            await nextPanel.DOAnchorPosX(0f, _animationDuration)
                .SetEase(Ease.OutExpo)
                .AsyncWaitForCompletion()
                .ContinueWith(_ => _operationInProgress = false);
            
            _isPanel1Active = !_isPanel1Active;
        }
    }
}
