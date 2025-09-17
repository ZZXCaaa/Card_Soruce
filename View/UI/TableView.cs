
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

namespace Soruce.View.UI
{
    public class TableView : MonoBehaviour
    {
        public List<GameObject> tableCard;
        [SerializeField]
        private SplineContainer Tablespline;
        [SerializeField]
        private float a;
        public void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Card"))
            {
                return;
            }
            other.gameObject.GetComponent<CardInputSystemView>().isEnterBox = true;
            tableCard.Add(other.gameObject);
        }
        public void OnCollisionExit2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Card"))
            {
                return;
            }
            other.gameObject.GetComponent<CardInputSystemView>().isEnterBox = false;
            tableCard.Remove(other.gameObject);
        }
        public void cardPosMove()
        {
            Spline spline = Tablespline.Spline;

            int maxCards = 13;
            int actualCount = Mathf.Min(tableCard.Count, maxCards);
            int count = Mathf.Max(actualCount - 1, 1);

// 計算平均間距並居中排列
            float spacing = 1f / maxCards;
            float firstT = 0.5f - (actualCount - 1) * spacing / 2f;

            for (int i = 0; i < actualCount; i++)
            {
                float t = firstT + i * spacing;
                t = Mathf.Clamp01(t); // 保證在 Spline 範圍內

                Vector3 position = spline.EvaluatePosition(t);
                position.z = 0f; // 固定在 2D 平面

                tableCard[i].transform.position = position;

                Debug.Log($"Card {i} → t: {t:F2}, pos: {position}");
            }


        }

        private void Update()
        {
            cardPosMove();
        }
    }
}