using System.Collections.Generic;
using Soruce.UI.Model_Entity;

public class GameState
{
    public List<MessageDTO> messages = new List<MessageDTO>();

    public void AddMessage(MessageDTO msg)
    {
        messages.Add(msg);
    }
}