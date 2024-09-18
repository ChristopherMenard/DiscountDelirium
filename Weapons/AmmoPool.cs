using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Pool;

namespace DiscountDelirium
{
    public class AmmoPool : MonoBehaviour
    {
        public static AmmoPool instance;

        [Header("Preference")]
        [SerializeField] private int m_amountOfAmmo;
        private List<GameObject> m_pooledAmmo = new List<GameObject>();
        private GameObject m_ammoPrefab;

        private void Awake()
        {
            if (instance == null) 
            {
                instance = this;
            }
        }


        private void Start()
        {
            for (int i = 0; i < m_amountOfAmmo; i++) 
            {
                GameObject ammo = Instantiate(m_ammoPrefab);
                ammo.SetActive(false);
                m_pooledAmmo.Add(ammo);
            }
        }

        public GameObject GetPooledAmmo() 
        {
            for(int i = 0; i < m_pooledAmmo.Count; i++) 
            {
                if (!m_pooledAmmo[i].activeInHierarchy) 
                {
                    return m_pooledAmmo[i];
                }
            }
            return null;
        }

        public void SetAmmo(GameObject prefab)
        {
            if (m_ammoPrefab == null) 
            {
                m_ammoPrefab = prefab;
            }
        }
    }
}
