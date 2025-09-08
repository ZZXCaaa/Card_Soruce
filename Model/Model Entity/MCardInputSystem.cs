using System;
using UnityEngine;

namespace Soruce.UI.Model_Entity
{
    [Serializable]
    public class MCardInputSystem
    {
         public GameObject cardPrefab { get; set;} 
         public bool       isDrag { get; set; } 
         public Camera     camera{ get; set; }
         public bool isEnterBox { get; set; }
    }
}