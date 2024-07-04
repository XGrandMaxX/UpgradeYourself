using TMPro;
using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class InputUIMessagesInstaller : MonoInstaller
    {
        [SerializeField] private TMP_Text _wrongSingUpText;
        [SerializeField] private TMP_Text _wrongSingInText;
        
        public override void InstallBindings() 
            => Container
                .Bind<InputUIMessages>()
                .AsSingle()
                .WithArguments(_wrongSingUpText, _wrongSingInText);
    }
}