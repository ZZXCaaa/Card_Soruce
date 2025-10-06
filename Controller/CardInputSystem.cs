
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
        [SerializeField]
        GameObject tableCard;
        public void OnEnable()
        {
            InputActionMap actionMap = Mouseaction.FindActionMap("CardButton");
            mouseAction =  actionMap.FindAction("MouseButton");
            mouseAction.performed += callButtonMouse;
            mouseAction.canceled  += exitButtonMouse;
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
                CatchCardDrang(true,new Vector3(1.2f,1.2f,1.2f));
                tableCard.GetComponent<TableView>().cardPosMove();
        }
        public void exitButtonMouse(InputAction.CallbackContext context)
        {
            CatchCardDrang(false,new Vector3(1.0f,1.0f,1.0f));
            if (tableCard.GetComponent<TableView>() == null)
            {
                return;
            }
            tableCard.GetComponent<TableView>().cardPosMove();
        }
        
        private void CatchCardDrang(bool isDrag , Vector3 scale)
        {
            Vector3 mouseWorldPos = Input.mousePosition;
            mouseWorldPos.z = 11.0f;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseWorldPos);
            RaycastHit2D hit = Physics2D.Raycast(worldPos,Vector2.zero);
            if (hit.collider == null)
            {
                return;
            }
            Debug.Log(hit.collider.gameObject);
            CardInputSystemView cardInputSystemView = hit.collider.GetComponentInParent<CardInputSystemView>();
            if (cardInputSystemView == null)
            {
                return;
            }

            if (cardInputSystemView.isRead)
            {
                cardInputSystemView.isReturCard();
            }
            cardInputSystemView.selectedCard(isDrag,scale);
        }
        
    }
}