using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.Examples.Chat
{
    public class Player : NetworkBehaviour
    {
        public static readonly HashSet<string> playerNames = new HashSet<string>();

        [SyncVar(hook = nameof(OnPlayerNameChanged))]
        public string playerName;

        // RuntimeInitializeOnLoadMethod -> fast playmode without domain reload
        [UnityEngine.RuntimeInitializeOnLoadMethod]
        static void ResetStatics()
        {
            playerNames.Clear();
        }

        void OnPlayerNameChanged(string _, string newName)
        {
            ChatBehavior.instance.localPlayerName = playerName;
        }

        public override void OnStartServer()
        {
            playerName = (string)connectionToClient.authenticationData;
        }

        /********* EJEMPLO DE LOS METODOS PARA EL MOVIMIENTO DE UN PLAYER (UNIDAD)***********/

        [SerializeField] private Vector3 movement = new Vector3();

         private void Update()
         {                    
            //Ejemplo del movimiento del Player
             if (!hasAuthority) { return; }
             if (!Input.GetKeyDown(KeyCode.UpArrow)) { return; }
             CmdMove();
         }

         [Command]
         private void CmdMove()
         {
             RpcMove();
         }

         [ClientRpc]
         private void RpcMove() => transform.Translate(movement);
        /*******************************/
    }
}
