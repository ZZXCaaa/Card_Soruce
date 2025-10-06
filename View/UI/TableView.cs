using System;
using System.Collections.Generic;
using System.Linq;
using Soruce.Model.Model_E;
using Soruce.UI.Model_Entity;
using UnityEngine;
using UnityEngine.Splines;

namespace Soruce.View.UI
{
    public class TableView : MonoBehaviour
    {

        public Mtable mtable;
        [SerializeField]
        private SplineContainer Tablespline;
        //Test 以後更改偵測玩家
        public List<cardData> othercardDatas = new ();
        //---------
        private void Start()
        {
            mtable = new Mtable();
            mtable.Initialize();
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Card")|| other.gameObject.GetComponent<CardInputSystemView>() == null)
            {
                return;
            }
            other.gameObject.GetComponent<CardInputSystemView>().isEnterBox = true;
            mtable.tableCard.Add(other.gameObject);
            cardPosMove();
        }

 
        public void OnCollisionExit2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Card"))
            {
                return;
            }
            other.gameObject.GetComponent<CardInputSystemView>().isEnterBox = false;
            mtable.tableCard.Remove(other.gameObject);
            //cardPosMove();
        }
        public void cardPosMove()
        {
            if (mtable.tableCard == null)
            {
                return;
            }
            // 獲取曲線對象引用
            Spline spline = Tablespline.Spline;

            // 設定最大卡牌數量為13
            int maxCards = 13;
            // 計算實際要排列的卡牌數量，不超過最大值
            int actualCount = Mathf.Min( mtable.tableCard.Count, maxCards);
            // 確保至少有1個間距(未使用的變量)
            int count = Mathf.Max(actualCount - 1, 1);

            // 計算卡牌之間的間距，以1除以最大卡牌數
            float spacing = 1f / maxCards;
            // 計算第一張卡牌的位置參數t，實現居中效果
            // 0.5f 是曲線的中點，(actualCount - 1) * spacing / 2f 是向左偏移量
            float firstT = 0.5f - (actualCount - 1) * spacing * Mathf.Min(actualCount,maxCards/2)/maxCards;

            // 遍歷所有需要排列的卡牌
            mtable.tableCard = mtable.tableCard
            .OrderBy(card => card.GetComponent<CardInputSystemView>().cardData.number) // 数字从小到大优先
            .ThenByDescending(card => card.GetComponent<CardInputSystemView>().cardData.type) // 其他花色排序
            .ToList();
            for (int i = 0; i < actualCount; i++)
            {
                // 計算當前卡牌在曲線上的t參數值
                float t = firstT + i * spacing;
                // 確保t值在0到1之間，防止超出曲線範圍
                t = Mathf.Clamp01(t);
                // 根據t值計算曲線上的位置
                Vector3 position = spline.EvaluatePosition(t);
                // 將z座標設為0，確保卡牌在2D平面上
                position.z = 0f;
                // 在y軸上加上3個單位的偏移
                position.y += 2f;
                // 設置卡牌的位置
                mtable.tableCard[i].transform.position = position;
                mtable.tableCard[i].transform.rotation = new Quaternion(0,0,0,0);
                mtable.tableCard[i].GetComponent<SpriteRenderer>().sortingOrder = i;
                // 輸出調試信息：顯示卡牌編號、t值和最終位置
            }
        }
    }
}