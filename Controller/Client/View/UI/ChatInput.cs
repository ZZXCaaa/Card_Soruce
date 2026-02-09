using System;
using Soruce.UI.Client.socketDTO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Soruce.View.UI
{
    public class ChatInput : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField input;
        public WebSocketClient client;
        public void OnSendClicked()
        {
            var dto = new BaseChatSendDTO();
            dto.type = "chat.send";
            dto.payload.user = "Player1";
            dto.payload.text = input.text;
            Debug.Log(dto.payload.text);
            client.SendChat(dto);
        }

        private void Awake()
        {
            if (client == null)
                client = FindObjectOfType<WebSocketClient>();
        }

        private void Start()
        {
            var dto = new BaseChatSendDTO();
            dto.type = "chat.DealingCards";
            client.SendChat(dto);
        }

     
    }
    
}