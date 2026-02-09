
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Soruce.UI.Model_Entity;
using UnityEngine;
using UnityEngine.Splines;

namespace Soruce.View.UI
{
    public  class cardPosView : MonoBehaviour
    {
        private cardPosEntity _cardPosEntity = new cardPosEntity();
        [SerializeField]
        private int maxSiz;
        [SerializeField]
        private GameObject cardprefab;
        [SerializeField]
        private SplineContainer splineContainer;
        [SerializeField]
        private Transform spawnPoint;
        public List<cardData> cardDatas = new ();
        public void DealingCards(int[] cardAll ,int MaxSize)
        {
            _cardPosEntity.Initialize();
            _cardPosEntity.maxSize = MaxSize;
            _cardPosEntity.cardPrefab = cardprefab;
            _cardPosEntity.splineContainer = splineContainer;
            _cardPosEntity.spawnPoint = spawnPoint;
            _cardPosEntity.cardDatas = cardDatas;
            //打亂
            //for (int i = 0; i < cardDatas.Count; i++)
            //{
              //  int rang =  Random.Range(i,cardDatas.Count);
               // var card = cardDatas[i];
               // cardDatas[i] = cardDatas[rang];
                //cardDatas[rang] = card;
            //}
            RuneCardPos();
        }
         private IEnumerator delaySpawCard(float delaytime = 0.1f)
        {
            InvokeRepeating("UpdateCardPos",0.1f,0.01f);
            for (int i = 0; i < _cardPosEntity.maxSize; i++)
            {
                GameObject g =Instantiate(_cardPosEntity.cardPrefab,_cardPosEntity.spawnPoint.position,_cardPosEntity.spawnPoint.rotation);
                g.GetComponent<SpriteRenderer>().sprite = _cardPosEntity.cardDatas[i].ImageSprite;
                g.GetComponent<SpriteRenderer>().sortingOrder = i;
                
                g.GetComponent<CardInputSystemView>().cardData = _cardPosEntity.cardDatas[i];
                g.tag = "Card";
                _cardPosEntity.handCards.Add(g);
                _cardPosEntity.timer.Add(0.0f);
                yield return new WaitForSeconds(delaytime);
            } 
            onChangCardPos();
        }
        public void UpdateCardPos()
        {
            if (_cardPosEntity.handCards.Count  == 0)
            {
                return;
            }
            float cardSpacing = 1.0f / _cardPosEntity.maxSize;
            float firstCardPos = 0.5f - (_cardPosEntity.handCards.Count-1)*cardSpacing/2;
            Spline spline = _cardPosEntity.splineContainer.Spline;
         
            for (int i = 0; i < _cardPosEntity.handCards.Count; i++)
            {
                _cardPosEntity.timer[i]= _cardPosEntity.timer[i] + 0.05f;
                float p = firstCardPos + i * cardSpacing;
                Vector3 splinePosition = spline.EvaluatePosition(p);
                splinePosition.y += 1.0f;
                splinePosition.x += 1.0f;
                Vector3 forwoed = spline.EvaluateTangent(p);
                Vector3 up = spline.EvaluateUpVector(p);
                Quaternion rotation = Quaternion.LookRotation(up,Vector3.Cross(up,forwoed.normalized));
                _cardPosEntity.handCards[i].transform.position =  Vector3.Lerp(this.transform.localPosition, splinePosition,_cardPosEntity.timer[i]);
                _cardPosEntity.handCards[i].transform.localRotation =  Quaternion.Lerp(transform.rotation,rotation,_cardPosEntity.timer[i]);
                Vector3 pos =  _cardPosEntity.handCards[i].transform.position;
                pos.z = 0.0f;
                _cardPosEntity.handCards[i].transform.position = pos;
            }
            if (_cardPosEntity.timer.All(t=> t>=1.0f))
            {
                CancelInvoke("UpdateCardPos");
              
            }
        }
        public void RuneCardPos()
        {
            StartCoroutine(delaySpawCard(0.1f));
        
        }

        private void onChangCardPos()
        {
            _cardPosEntity.handCards = _cardPosEntity.handCards
                .OrderBy(data => data.GetComponent<CardInputSystemView>().cardData.number)
                .ThenBy(data => data.GetComponent<CardInputSystemView>().cardData.type)
                .ToList();
            UpdateCardPos();
        }
    }
}