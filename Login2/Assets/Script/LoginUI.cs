using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mirror.Examples.Chat
{
    public class LoginUI : MonoBehaviour
    {
        [Header("UI Elements")]
        public InputField usernameInput;
        public InputField passwordInput;
        public Button hostButton;
        public Button clientButton;
        public Text errorText;

        public static LoginUI instance;

        void Awake()
        {
            instance = this;
        }

        private bool usernameVacio = false;
        private bool passwordVacio = false;

        public void ToggleButtonsName(string username)
        {
            usernameVacio = !string.IsNullOrWhiteSpace(username);
            hostButton.interactable = usernameVacio && passwordVacio;
            clientButton.interactable = usernameVacio && passwordVacio;
        }

        public void ToggleButtonsPassword(string password)
        {
            passwordVacio = !string.IsNullOrWhiteSpace(password);
            hostButton.interactable = usernameVacio && passwordVacio;
            clientButton.interactable = usernameVacio && passwordVacio;
        }
    }
}
