using System;
using System.Collections.Generic;
using Soruce.UI;
using Soruce.UI.Model_Entity;
using UnityEngine;

namespace Soruce.View.UI
{
    public class ButtonView : MonoBehaviour
    {
        private Mtable mtable;
        [SerializeField]
        GameObject table;
        private void Start()
        { 
            mtable = new Mtable();
            mtable.Initialize();
           
        }

        public void click()
        {
            Debug.Log( table.GetComponent<TableView>().mtable.tableCard.Count);
        }
    }
}