using BoxSystem;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace DiscountDelirium
{
    public class Target : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] float m_stopCount;
        [SerializeField] float m_delayBetweenStop;
        [SerializeField] private TowerBoxSystem m_towerBoxSystem;
        [SerializeField] private Rigidbody m_rigidbody;
        [SerializeField] private GameObject m_VFX;

        private float m_delayCount;
        public TowerBoxSystem GetTower() 
        {
            return m_towerBoxSystem;
        }

        public Rigidbody GetRigidbody() 
        {
            return m_rigidbody;
        }

        public void StopMovement() 
        {
            m_VFX.SetActive(true);
            m_delayCount = 0;
            StartCoroutine("Delay");
        }

        IEnumerator Delay() 
        {
            //m_rigidbody.velocity = Vector3.zero;
            m_rigidbody.drag = 7;
            yield return new WaitForSeconds(m_delayBetweenStop);

            if (m_delayCount < m_stopCount) 
            {
                m_delayCount++;
                StartCoroutine("Delay");
            }
            else 
            {
                m_VFX.SetActive(false);
                m_rigidbody.drag = 0;
            }
        }
    }
}
