
using Soruce.UI.InterFace;
using Soruce.View.UI;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Soruce.UI

{
    public class CardInputSystem : MonoBehaviour
    {
        [SerializeField]
        private InputActionAsset Mouseaction;
        private InputAction mouseAction;
        public void OnEnable()
        {
            InputActionMap actionMap = Mouseaction.FindActionMap("CardButton");
            mouseAction =  actionMap.FindAction("MouseButton");
            mouseAction.performed += callButtonMouse;
            mouseAction.canceled += exitButtonMouse;
            Mouseaction.Enable();
        }

        public void OnDisable()
        {
            mouseAction.performed -= callButtonMouse;
            mouseAction.canceled -= exitButtonMouse;
            Mouseaction.Disable();
        }

        public void callButtonMouse(InputAction.CallbackContext context)
        {

                Debug.Log("callButtonMouse");
                CatchCardDrang(true,new Vector3(1.5f,1.5f,1.5f));
                Debug.Log("OnPointerDown");
        }
        public void exitButtonMouse(InputAction.CallbackContext context)
        {
            Debug.Log("exitButtonMouse");
            CatchCardDrang(false,new Vector3(1.0f,1.0f,1.0f));
            Debug.Log("Down");
        }

        private void CatchCardDrang(bool isDrag , Vector3 scale)
        {
            Vector3 mouseWorldPos = Input.mousePosition;
            mouseWorldPos.z = 9.0f;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseWorldPos);
            RaycastHit2D hit = Physics2D.Raycast(worldPos,Vector2.zero);
            if (hit.collider == null)
            {
                Debug.Log("no hit.collider");
                return;
            }
            Debug.Log(hit.collider.gameObject);
            CaedDrag caedDrag = hit.collider.GetComponentInParent<CaedDrag>();
            if (caedDrag == null)
            {
                Debug.Log("no caedDrag");
                return;
            }
                caedDrag.selectedCard(isDrag,scale);
        }
        
    }
}