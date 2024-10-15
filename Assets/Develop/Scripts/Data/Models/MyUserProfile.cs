using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public sealed class MyUserProfile
{
    public int GlobalLevel { get; private set; }

    private readonly Dictionary<string, int> categoryLevels = new();

    public MyUserProfile()
    {
        LoadCategoryLevels();

        CalculateGlobalLevel();
    }

    private void LoadCategoryLevels()
    {
        categoryLevels["Sport"] = PlayerPrefs.GetInt("SportLevel", 0);
        categoryLevels["Career"] = PlayerPrefs.GetInt("CareerLevel", 0);
    }

    public int GetLevelByCategory(string category) => categoryLevels.TryGetValue(category, out int level) ? level : 0;

    public void SetLevelForCategory(string category, int level)
    {
        if (!categoryLevels.ContainsKey(category))
        {
            Debug.Log($"Such a category {category} does not exist.");
            return;
        }
        categoryLevels[category] = level;

        CalculateGlobalLevel();

        SaveLevelToPlayerPrefs(category+"Level", level);
    }

    private void CalculateGlobalLevel()
    {
        GlobalLevel = GetLevelFromPlayerPrefs("UserGlobalLevel");

        if (GlobalLevel > 0)
        {
            return;
        }

        foreach (var level in categoryLevels.Values)
        {
            GlobalLevel += level;
        }

        SaveLevelToPlayerPrefs("UserGlobalLevel", GlobalLevel);
    }


    private void SaveLevelToPlayerPrefs(string saveTitle, int level)
    {
        PlayerPrefs.SetInt(saveTitle, level);
        PlayerPrefs.Save();
    }

    private int GetLevelFromPlayerPrefs(string getTitle) => PlayerPrefs.GetInt(getTitle, 0);
}
