using UnityEngine;
using Cysharp.Threading.Tasks;

public class LanguageSwitcher : MonoBehaviour
{
    private void Awake() => G.LanguageSwitcher = this;

    /// <summary>
    /// ������������� ���� ��� ����. �������: ru, en
    /// </summary>
    public async void SetLanguage(string language) => await ChangeLanguage(language);

    private async UniTask ChangeLanguage(string language) => await LocalizationManager.SetLanguage(language);
}
