using Unity.VisualScripting;

namespace Soruce.UI.Client.socketDTO
{
    public class BaseChatSendDTO
    {
        public string type = "";
        public Payload payload = new();
        [System.Serializable]
        public class Payload
        {
            public string user { get; set;}
            public string text{ get; set;}
        }
    }
}