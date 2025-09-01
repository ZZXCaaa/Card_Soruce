using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

namespace Soruce.UI.Model_Entity
{
    public class cardPosEntity
    {
        public int maxSize {get; set; }
        public GameObject cardPrefab{ get; set;}
        public SplineContainer splineContainer{get;set;}
        public Transform spawnPoint{get;set;}
        public List<GameObject> handCards{ get; set; }  
        public List<float> timer{ get; set; } 
        public float duration{ get; set; }
        public List<cardData> cardDatas { get; set; }  

        public void Initialize()
        {
            handCards = new List<GameObject>();
            timer = new List<float>();
            cardDatas = new List<cardData>();
        }
    }
}