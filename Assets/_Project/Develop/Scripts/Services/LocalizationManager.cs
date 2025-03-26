using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using Cysharp.Threading.Tasks;

public static class LocalizationManager
{
    /// <summary>
    /// ������������� ���� ���� �� ���� ������ (��������, "ru", "en").
    /// </summary>
    public static async UniTask SetLanguage(string localeCode)
    {
        await LocalizationSettings.InitializationOperation;
        var locale = LocalizationSettings.AvailableLocales.Locales.Find(l => l.Identifier.Code == localeCode);
        if (locale != null)
        {
            LocalizationSettings.SelectedLocale = locale;
        }
    }

    /// <summary>
    /// �������� �������������� ����� �� ����� �� ��������� �������.
    /// </summary>
    public static string GetLocalizedText(string table, string key)
    {
        var localizedString = new LocalizedString(table, key);
        return localizedString.GetLocalizedString();
    }
}
