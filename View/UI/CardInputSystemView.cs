

using System;
using Soruce.UI;
using Soruce.UI.Model_Entity;
using UnityEditor;
using UnityEngine;
namespace Soruce.View.UI
{
    public class CardInputSystemView : MonoBehaviour
    {
        private int sort;
        private MCardInputSystem mCardInputSystem;
        //是否回歸
        public bool isRead  = true;
        //入手後初始為
        [SerializeField]
        private Vector3 startPos;
        [SerializeField]
        private Quaternion CardRotaion;
        [SerializeField]
        public cardData cardData;
        [SerializeField]
        public bool isEnterBox = false;
      
        private void Awake()
        {
            mCardInputSystem = new MCardInputSystem();
            mCardInputSystem.camera = Camera.main;
        }
        private void Update()
        {
            if (mCardInputSystem.isDrag)
            { 
              Drag();
            }
        }

        public void selectedCard(bool Drag , Vector3 scale)
        {
            changsScale(scale,0.0f);
            mCardInputSystem.isDrag = Drag;
            if (!isRead)
            {
                BackCard();
            }
        }
        private void Drag()
        {
            Vector3 mouseWorldPos = Input.mousePosition;
            mouseWorldPos.z = 11.0f;
            transform.position = mCardInputSystem.camera.ScreenToWorldPoint(mouseWorldPos);
        }
        private void changsScale(Vector3 scale,float Posz)
        {
            this.transform.localScale = scale;
            transform.position = new Vector3(transform.position.x,transform.position.y,Posz);
        }
        public void isReturCard()
        {
            startPos = transform.position;
            CardRotaion = transform.rotation;
            isRead  = false;
        }

        public void BackCard()
        {
            if (!isEnterBox)
            {
                transform.position = startPos;
                transform.rotation = CardRotaion;
            }
        }

        public void OnMouseEnter()
        {
            
            sort = GetComponent<SpriteRenderer>().sortingOrder;
            GetComponent<SpriteRenderer>().sortingOrder = 100;
            changsScale(new Vector3(1.2f,1.2f,1.2f),0.0f);;
        }

        public void OnMouseExit()
        {
            GetComponent<SpriteRenderer>().sortingOrder = sort;
            changsScale(new Vector3(1.0f,1.0f,1.0f),0.0f);
        }
    }
}