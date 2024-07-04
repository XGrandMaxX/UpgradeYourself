using TMPro;

namespace DB
{
    public sealed class RegisterUser
    {
        internal TMP_InputField _loginField;
        internal TMP_InputField _passwordField;
        
        public RegisterUser(TMP_InputField loginField, TMP_InputField passwordField)
        {
            _loginField = loginField;
            _passwordField = passwordField;
        }
    }
}
