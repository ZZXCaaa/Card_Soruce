using System;
using System.Net.Sockets;
using UnityEngine;

namespace Soruce.UI.Client.socketDTO
{
    public class HeartClient :MonoBehaviour
    {
        private float time;
        private float sendHartTime = 5.0f;
        private WebSocketClient socket;

        private void Update()
        {
            time += Time.deltaTime;
            if (time>=sendHartTime)
            {
                SendHeard();
            }
        }
        private void SendHeard()
        {
            HeartClientDTO Hdto = new HeartClientDTO();
            Hdto.type = "";
            Hdto.payload.text = "";
            Hdto.payload.user = "";
            socket.SendChat(Hdto);
        }
    }
}