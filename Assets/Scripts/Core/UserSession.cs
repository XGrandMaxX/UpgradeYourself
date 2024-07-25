using DB;

namespace Core
{
    public static class UserSession
    {
        internal static UserData UserData { get; private set; }

        public static void UploadUserSession(UserData userData) => UserData = userData;
    }
}
