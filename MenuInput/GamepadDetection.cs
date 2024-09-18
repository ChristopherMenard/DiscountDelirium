using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

namespace DiscountDelirium
{
    public class GamepadDetection : MonoBehaviour
    {
        [Header("Cursor references")]
        [SerializeField] private FakeCursor m_cursor;
        [SerializeField] private Image m_image;

        private void Awake()
        {
            Cursor.visible = true;
        }
        void Start() 
        {
            MainMenuInputHandler.MouseSelected += ShowMouse;
            MainMenuInputHandler.ControllerSelected += ShowVirtualCursor;
        }

        private void ShowMouse() 
        {
            if (m_cursor != null && m_image != null) 
            {
                m_cursor.enabled = false;
                m_image.enabled = false;
            }
            Cursor.visible = true;
        }

        private void ShowVirtualCursor() 
        {
            if (m_cursor != null && m_image != null)
            {
                m_cursor.enabled = true;
                m_image.enabled = true;
            }
            Cursor.visible = false;
        }

    }
}
