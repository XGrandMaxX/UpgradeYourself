using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public static class SceneLoader
    {
        private const int MAXIMUM_SCENE_INDEX = 1;
        
        public static void LoadNextSceneAsync()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (currentSceneIndex + 1 > MAXIMUM_SCENE_INDEX)
            {
                Debug.LogWarning($"Scene index {currentSceneIndex + 1} has not found, loading scene by 0 index");
                SceneManager.LoadSceneAsync(0);
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
