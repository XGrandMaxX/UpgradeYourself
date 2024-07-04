using DB;
using UnityEngine;

namespace Core
{
    public sealed class UserSession : MonoBehaviour
    {
        internal static UserSession Instance;
        private UserSession() {}

        [field: SerializeField] internal UserData UserData { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }
        
        public void SaveUserSession(UserData userData) => UserData = userData;
    }
}
