using DB;
using TMPro;
using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class AuthenticationInstaller : MonoInstaller
    {
        [SerializeField] private TMP_InputField SIGNIN_loginField;
        [SerializeField] private TMP_InputField SIGNIN_passwordField;
        
        [SerializeField] private TMP_InputField SIGNUP_loginField;
        [SerializeField] private TMP_InputField SIGNUP_passwordField;
        public override void InstallBindings()
        {
            Container.Bind<LoginUser>().AsSingle().WithArguments(SIGNIN_loginField, SIGNIN_passwordField);
            Container.Bind<RegisterUser>().AsSingle().WithArguments(SIGNUP_loginField, SIGNUP_passwordField);
        }
    }
}
