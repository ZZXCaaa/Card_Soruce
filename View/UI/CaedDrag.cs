
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Soruce.View.UI
{
    public class CaedDrag : MonoBehaviour
    {
        [SerializeField]
        private bool isDrag = false;
        [SerializeField]
        private Camera camera;
     
        private float x;
        private float y;
        
        
        private void Awake()
        {
            if (camera == null)
                camera = Camera.main;
        }
        private void Update()
        {
            if (isDrag)
            { 
              Drag();
            }
        }

        public void selectedCard(bool Drag , Vector3 scale)
        {
            changsScale(scale,0.0f);
            isDrag = Drag;
        }
        private void Drag()
        {
            Vector3 mouseWorldPos = Input.mousePosition;
            mouseWorldPos.z = 9.0f;
            transform.position = camera.ScreenToWorldPoint(mouseWorldPos);
        }
        private void changsScale(Vector3 scale,float Posz)
        {
            this.transform.localScale = scale;
            transform.position = new Vector3(transform.position.x,transform.position.y,Posz);
        }
    }
}