using System;
using System.Collections.Generic;
using Soruce.Model.Model_E;
using UnityEngine;

namespace Soruce.UI.Model_Entity
{
    [Serializable]
    public struct cardData
    {
        public int number;
        public E_cardData type;
        public Sprite ImageSprite;
    }
}