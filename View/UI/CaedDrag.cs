
using System;
using UnityEngine;
using UnityEngine.EventSystems;
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

        public void OnMouseDown()
        {
            isDrag   = true;
            Debug.Log("OnPointerDown");
           // x = transform.localPosition.x - Input.mousePosition.x;
           // y = transform.localPosition.y - Input.mousePosition.y;
        }
        public void OnMouseUp()
        {
            Debug.Log("Down");
            isDrag = false;
        }

        private void Update()
        {
            if (isDrag)
            {
                Drag();
            }
        }
        private void Drag()
        {
            Vector3 mouseWorldPos = Input.mousePosition;
            mouseWorldPos.z = 10.0f;
            transform.position = camera.ScreenToWorldPoint(mouseWorldPos);

        }
        private void Awake()
        {
            if (camera == null)
                camera = Camera.main;
        }

    }
}