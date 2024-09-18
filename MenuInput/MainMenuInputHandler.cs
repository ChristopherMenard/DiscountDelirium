using CartControl;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace DiscountDelirium
{
    public class MainMenuInputHandler : MonoBehaviour
    {
        private MainInputs m_mainInputs;
        public static MainMenuInputHandler Instance;
        public static Action SelectEvent;
        public static Action BackEvent;
        public static Action MouseSelected;
        public static Action ControllerSelected;

        private void Awake()
        {
            m_mainInputs = new MainInputs();

            if (Instance != null)
            {
                Destroy(this);
                return;
            }
            Instance = this;

            m_mainInputs.Menu.Select.started += OnSelect;
            m_mainInputs.Menu.Back.started += OnBack;
            m_mainInputs.Menu.Navigate.started += OnNavigate;
            m_mainInputs.Menu.Navigate.performed += OnNavigate;
        }

        private void Update()
        {
            MouseMovement();
        }

        private void OnSelect(InputAction.CallbackContext context) 
        {
            SelectEvent?.Invoke();
        }

        private void OnBack(InputAction.CallbackContext context)
        {
            BackEvent?.Invoke();
        }

        private void OnNavigate(InputAction.CallbackContext context)
        {
            ControllerSelected?.Invoke();
        }

        private void MouseMovement()
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                MouseSelected?.Invoke();
            }
        }

        private void OnEnable()
        {
            m_mainInputs.Enable();
        }
        private void OnDisable()
        {
            m_mainInputs.Disable();
        }

    }
}
