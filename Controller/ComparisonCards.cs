using System;
using System.Collections.Generic;
using System.Linq;
using Soruce.Model.Model_E;
using Soruce.UI.Model_Entity;
using Soruce.View.UI;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Soruce.UI
{
    delegate void CardFunction();
    public class ComparisonCards : MonoBehaviour
    {
        E_CardType ECardType;
        List<GameObject> TableCard = new List<GameObject>();
        List<cardData> otherCard = new List<cardData>();
        
        public void CardClick(GameObject tableGameObject)
        {
            TableCard = tableGameObject.GetComponent<TableView>().mtable.tableCard;
            otherCard = tableGameObject.GetComponent<TableView>().othercardDatas;
            switch (otherCard.Count)
            {
                case 1:
                    if (TableCard.Count > 1)
                    {
                        Debug.Log(ErrorMag.ERROR_QUANTITY);
                        return;
                    }
                    ECardType = E_CardType.Leaflet;
                    break;
                case 2:
                    if (TableCard.Count > 2)
                    {
                        Debug.Log(ErrorMag.ERROR_QUANTITY);
                        return;
                    }
                    break;
                case 3:
                    if (TableCard.Count > 3)
                    {
                        Debug.Log(ErrorMag.ERROR_QUANTITY);
                        return;
                    }
                    break;
                case 5:
                    if (TableCard.Count > 5)
                    {
                        Debug.Log(ErrorMag.ERROR_QUANTITY);
                        return;
                    }
                    break;
            }
            ComparisontypeIG();
            ComparisonCard();
        }
        private void ComparisontypeIG()
        {
            if (compCardValue(4))
            {
                ECardType = E_CardType.IronRod;
                return;
            }
            if (compCardValue(2) && compCardValue(3))
            {
                ECardType = E_CardType.gourd;
                return;
            }

            if (compCardValue(3))
            {
                ECardType = E_CardType.Cop;
                return;
            }

            if (compCardValue(2))
            {
                ECardType = E_CardType.Double;
                return;
            }
            MatchTypeStraightOrFlush();
            Debug.Log(ECardType);
        }

        private bool MatchTypeStraightOrFlush()
        {
            List<cardData> card = new List<cardData>();
            foreach (var item in TableCard)
            {
                card.Add(item.GetComponent<CardInputSystemView>().cardData);
            }
            var otherGroupint = otherCard.OrderBy(x => x.number).ToList();
            var Groupint = card.OrderBy(x => x.number).ToList();
            for (int i = 0; i < otherGroupint.Count -1; i++)
            {
                if (otherGroupint[i+1].number - otherGroupint[i].number != 1)
                {
                    Debug.Log("敵人牌未有順");
                     return false;
                }
            }
            for (int i = 0; i < Groupint.Count -1; i++)
            {
                if (Groupint[i+1].number - Groupint[i].number != 1)
                {
                    Debug.Log("自身牌未有順");
                    return false;
                }
            }
            ECardType = E_CardType.Straight;
            if ( otherGroupint.All(c=>c.type == otherGroupint[0].type) && Groupint.All(c=>c.type == Groupint[0].type))
            {
                ECardType = E_CardType.Flush;
                return true;
            }
           return true;
        }
        private bool compCardValue(int number)
        {
            List<cardData> card = new List<cardData>();
            foreach (var item in TableCard)
            {
                card.Add(item.GetComponent<CardInputSystemView>().cardData);
            }
            var otherGroupint = otherCard.GroupBy(card => card.number).Where(group => group.Count() == number).ToList();
            var Groupint = card.GroupBy(card => card.number).Where(group => group.Count() == number).ToList();
            if (otherGroupint.Count == 0)
            {
                Debug.Log("敵人牌不對" +number);
                return false;
            }
            if (Groupint.Count == 0)
            {
                Debug.Log("自身牌不對" +number);
                return false;
            }
            return true;
        }
        private void ComparisonCard()
        {
            List<cardData> card = new List<cardData>();
            foreach (var item in TableCard)
            {
                card.Add(item.GetComponent<CardInputSystemView>().cardData);
            }
            switch (ECardType)
            {
                //-------比較牌首--------------------
                case E_CardType.Leaflet:
                case E_CardType.Double:
                case E_CardType.Cop:
                //大老二維基百科版本7
                case E_CardType.Straight:
                case E_CardType.Flush:
                    ComparingValuesandSuit(card[0],otherCard[0]);
                    break;
                //------------------------------
                
                //比較條子的第一張
                case E_CardType.gourd:
                    var playerThree = card.GroupBy(x => x.number).Where(g => g.Count() == 3).First();
                    var otherThree = otherCard.GroupBy(x => x.number).Where(g => g.Count() == 3).First();
                    ComparingValuesandSuit(playerThree.First(),otherThree.First());
                    break;
                //-------------------------
                
                //------比較4個想同值得牌首-----------
                case E_CardType.IronRod:
                    var playerfour = card.GroupBy(x => x.number).Where(g => g.Count() == 4).First();
                    var otherfour = otherCard.GroupBy(x => x.number).Where(g => g.Count() == 4).First();
                    ComparingValuesandSuit(playerfour.First(),otherfour.First());
                    break;
                //-----------------------
            }
        }
        private void ComparingValuesandSuit(cardData playerCard , cardData otherCard)
        {
            if (ECardType != E_CardType.Straight && ECardType != E_CardType.Flush)
            {
                playerCard.number = playerCard.number == 1 ? 14 : playerCard.number;
                otherCard.number = otherCard.number == 1 ? 14 : otherCard.number;
                playerCard.number = playerCard.number == 2 ? 99 : playerCard.number;
                otherCard.number = otherCard.number == 2 ? 99 : otherCard.number;   
            }
            Debug.Log(playerCard.number + " " + otherCard.number);
            Debug.Log(playerCard.number>otherCard.number? "玩家牌較大" :"敵方較大");
            if (playerCard.number == otherCard.number)
            {
                   Debug.Log(playerCard.type > otherCard.type ? "玩家牌較大" :"敵方較大");
            }
            
        }
    }
}