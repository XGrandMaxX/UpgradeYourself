using TMPro;

namespace DB
{
    public sealed class LoginUser
    {
        internal TMP_InputField _loginField;
        internal TMP_InputField _passwordField;
        
        public LoginUser(TMP_InputField loginField, TMP_InputField passwordField)
        {
            _loginField = loginField;
            _passwordField = passwordField;
        }
    }
}
