namespace Soruce.UI.Client.socketDTO
{
        [System.Serializable]
        public class ServerMessage
        {
            public string type;
            public int[] player;    // 修改为 int 数组
            public int maxSiz; 
        }
}