using System.Collections;
using System.Collections.Generic;
using Mirror;
using System;
using UnityEngine;
using UnityEngine.UI;



namespace Mirror.Examples.Chat
{
    public class ChatBehavior : NetworkBehaviour
    {
        [SerializeField] private Text chatText = null;
        [SerializeField] private InputField TextoEntrada = null;


        [Header("Diagnostic - Do Not Edit")]
        public string localPlayerName;

        Dictionary<NetworkConnectionToClient, string> connNames = new Dictionary<NetworkConnectionToClient, string>();

        public static ChatBehavior instance;

        void Awake()
        {
            instance = this;
        }

        [Command(requiresAuthority = false)]
        public void CmdSend(string message, NetworkConnectionToClient sender = null)
        {
            if (!connNames.ContainsKey(sender))
                connNames.Add(sender, sender.identity.GetComponent<Player>().playerName);

            if (!string.IsNullOrWhiteSpace(message))
                RpcReceive(connNames[sender], message.Trim());
        }

        [ClientRpc]
        public void RpcReceive(string playerName, string message)
        {

            string prettyMessage = playerName == localPlayerName ?
                $"<color=red>{playerName}:</color> {message}" :
                $"<color=blue>{playerName}:</color> {message}";
            AppendMessage(prettyMessage);
        }

        public void OnEndEdit(string input)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetButtonDown("Submit"))
                SendMessage();
        }

        // Called by OnEndEdit above and UI element SendButton.OnClick
        public void SendMessage()
        {
            if (!string.IsNullOrWhiteSpace(TextoEntrada.text))
            {
                CmdSend(TextoEntrada.text.Trim());
                TextoEntrada.text = string.Empty;
                TextoEntrada.ActivateInputField();
            }
        }

        internal void AppendMessage(string message)
        {
            StartCoroutine(AppendAndScroll(message));
        }

        IEnumerator AppendAndScroll(string message)
        {
            chatText.text += message + "\n";

            // it takes 2 frames for the UI to update ?!?!
            yield return null;
            yield return null;
        }
    }
}
