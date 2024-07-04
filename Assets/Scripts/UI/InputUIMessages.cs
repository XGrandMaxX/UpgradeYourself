using System;
using System.Diagnostics.CodeAnalysis;
using TMPro;

namespace UI
{
    public sealed class InputUIMessages
    {
        private readonly TMP_Text _wrongRegisterText;
        private readonly TMP_Text _wrongLoginText;

        public InputUIMessages(TMP_Text wrongRegisterText, TMP_Text wrongLoginText)
        {
            _wrongRegisterText = wrongRegisterText;
            _wrongLoginText = wrongLoginText;
        }

        public void SetRegisterErrorMessage(string message)
            => _wrongRegisterText.text = message;
        public void SetLoginErrorMessage(string message) 
            => _wrongLoginText.text = message;


        [DoesNotReturn]
        public void ThrowRegisterErrorException(string message) 
            => throw new Exception(_wrongRegisterText.text = message);

        [DoesNotReturn]
        public void ThrowLoginErrorException(string message) 
            => throw new Exception(_wrongLoginText.text = message);
    }
}
