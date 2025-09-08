
using UnityEngine;

namespace Soruce.View.UI
{
    public class TableView : MonoBehaviour
    {
        GameObject card;
        
        public void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Card"))
            {
                return;
            }
            //card.GetComponent<CardInputSystemView>().isEnterBox = true;
            
        }
    }
}