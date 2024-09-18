using CartControl;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DiscountDelirium
{
    public class WeaponInputHandler : MonoBehaviour
    {
        private MainInputs m_mainInputs;
        public static WeaponInputHandler Instance;
        public static Action MeleeAttack;
        public static Action RangeAttack;

        private void Awake()
        {
            m_mainInputs = new MainInputs();

            if (Instance != null)
            {
                Destroy(this);
                return;
            }
            Instance = this;

            m_mainInputs.Cart.MeleeAttack.started += OnMeleeAttack;
            m_mainInputs.Cart.RangeAttack.started += OnRangeAttack;
        }

        private void OnMeleeAttack(InputAction.CallbackContext context)
        {
            MeleeAttack?.Invoke();
        }
        private void OnRangeAttack(InputAction.CallbackContext context)
        {
            RangeAttack?.Invoke();
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
