using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Soruce.UI.Client.socketDTO;
using UnityEngine;
using Soruce.View.UI;

public class WebSocketClient : MonoBehaviour
{
    private ClientWebSocket socket;
    private bool isdealingCards = false;
    [SerializeField] 
    private cardPosView cardPosView;

    
    async void Awake()
    {
        socket = new ClientWebSocket();
        await socket.ConnectAsync(new Uri("ws://localhost:3000/ws"), CancellationToken.None);
        Debug.Log("伺服器連接");
        ReceiveLoop();
    }
    async void ReceiveLoop()
    {
        var buffer = new byte[1024];
        while (socket.State == WebSocketState.Open)
        {
            var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            string jason = Encoding.UTF8.GetString(buffer,0,result.Count);
            var packet = JsonUtility.FromJson<ServerMessage>(jason);
                Debug.Log(jason);
                Debug.Log(packet.type);
                if (packet.player != null)
                {
                    Debug.Log(packet.player.Length);
                }
                if (packet.player != null)
                {
                    Debug.Log($"玩家卡牌: [{string.Join(", ", packet.player)}]");
                }

            //  Debug.Log(packet.payload.message);
          
            switch (packet.type)
            {
                case "card.dealCards":
                    if (!isdealingCards)
                    {
                        dealingCards(packet.player,packet.maxSiz);
                    }
                    break;
            }
            //HandleMessage(jason);
            
        }
        
    }
    public async void SendChat(BaseChatSendDTO dto)
    {
        string json = JsonUtility.ToJson(dto);
        byte[] bytes = Encoding.UTF8.GetBytes(json);

        await socket.SendAsync
        (
            new ArraySegment<byte>(bytes),
            WebSocketMessageType.Text,
            true,
            CancellationToken.None
        );
    }

    private void  dealingCards(int[] cardAll ,int MaxSize)
    {
        cardPosView.DealingCards(cardAll,MaxSize);
        isdealingCards = true;
    }
    
}