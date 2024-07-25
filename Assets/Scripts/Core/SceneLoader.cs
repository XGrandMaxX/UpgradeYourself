using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public static class SceneLoader
    {
        private static readonly int MaximumSceneIndex;

        static SceneLoader() => MaximumSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
        
        public static void LoadNextSceneAsync()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (currentSceneIndex + 1 > MaximumSceneIndex)
            {
                Debug.LogWarning($"Scene index {currentSceneIndex + 1} has not found, loading scene by 0 index");
                SceneManager.LoadSceneAsync(0);
                return;
            }

            SceneManager.LoadSceneAsync(currentSceneIndex + 1);
        }

        public static void ReloadCurrentSceneAsync()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            
            SceneManager.LoadSceneAsync(currentSceneIndex);
        }
    }
}
