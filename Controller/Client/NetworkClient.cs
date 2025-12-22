using Quobject.SocketIoClientDotNet.Client;
using UnityEngine;
using System;
using System.Net.Sockets;
using Soruce.UI.Model_Entity;

public class NetworkClient
{
    private Socket socket;
    private GameState gameState;

    public NetworkClient(GameState state)
    {
        gameState = state;
    }

    public void Connect(string url)
    {
        socket = IO.Socket(url);

        socket.On(Socket.EVENT_CONNECT, () =>
        {
            Debug.Log("✅ Connected to server");
        });

        socket.On("messages", OnInitMessages);
        socket.On("message", OnNewMessage);
    }

    private void OnInitMessages(object data)
    {
        Debug.Log("📦 Init messages: " + data);
        // 如果你後端傳 array，之後可解析成 List
    }

    private void OnNewMessage(object data)
    {
        string json = data.ToString();
        MessageDTO msg = JsonUtility.FromJson<MessageDTO>(json);
        gameState.AddMessage(msg);
    }

    public void SendMessage(string from, string text)
    {
        socket.Emit("sendMessage", new {
            from = from,
            text = text
        });
    }
}