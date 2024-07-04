using System.Collections;
using Core;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DB
{
    public sealed class DBFacade : MonoBehaviour
    {
        private DBConnector _connector;
        private RegisterUser _registerUser;
        private LoginUser _loginUser;
        
        [SerializeField] private Button _registerButton;
        [SerializeField] private Button _loginButton;

        [SerializeField] private SwipingUIElements _swipingUIElements;

        [Inject]
        private void Construct(DBConnector connector, RegisterUser registerUser, LoginUser loginUser)
        {
            _connector = connector;

            _registerUser = registerUser;
            _loginUser = loginUser;
            
            AddButtonListeners();
        }
        
        private void AddButtonListeners()
        {
            _registerButton.onClick.AddListener(CreateNewUser);
            _loginButton.onClick.AddListener(LoginUser);
        }
        private void RemoveButtonListeners()
        {
            _registerButton.onClick.RemoveListener(CreateNewUser);
            _loginButton.onClick.RemoveListener(LoginUser);
        }
        
        private async void CreateNewUser()
        {
            if (string.IsNullOrEmpty(_registerUser._loginField.text) || string.IsNullOrEmpty(_registerUser._passwordField.text))
                return;
            
            StartCoroutine(ButtonCooldown(.5f, _registerButton));

            await _connector.CreateNewUserAsync(_registerUser._loginField.text, _registerUser._passwordField.text);
            
            _loginUser._loginField.text = _registerUser._loginField.text;

            _swipingUIElements.SwipePanelsAsync();

            ClearFields(
                _registerUser._loginField,
                _registerUser._passwordField,
                _loginUser._passwordField);
        }

        private async void LoginUser()
        { 
            if (string.IsNullOrEmpty(_loginUser._loginField.text) || string.IsNullOrEmpty(_loginUser._passwordField.text))
                return;
            
            StartCoroutine(ButtonCooldown(.5f, _loginButton));
            
            UserData currentUser = await _connector.LoginUserAsync(_loginUser._loginField.text, _loginUser._passwordField.text);
            
            UserSession.Instance.SaveUserSession(currentUser);
            
            SceneLoader.LoadNextSceneAsync();
        }

        //TODO: First implement sending emails, and only after that add automatic password change!
        private async void RestoreUserPassword()
        {
            await _connector.SetNewPasswordAsync("null", "null");
            Debug.Log($"Successful password change!");
        }

        
        //TODO: Move ClearFields and ButtonCooldown to a separate class!
        private void ClearFields(params TMP_InputField[] fields)
        {
            foreach (var field in fields)
                field.text = null;
        }

        private IEnumerator ButtonCooldown(float time, Button button)
        {
            button.interactable = false;
            yield return new WaitForSeconds(time);
            button.interactable = true;
        }

        private void OnDestroy() => RemoveButtonListeners();
    }
}
