using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DiscountDelirium
{
    public class VFXPool : MonoBehaviour
    {
        [Header("Preference")]
        [SerializeField] private int m_amountOfVFX;
        [SerializeField] private GameObject m_VFXPrefab;
        private List<GameObject> m_pooledVFX = new List<GameObject>();
        

        private void Start()
        {
            for (int i = 0; i < m_amountOfVFX; i++)
            {
                GameObject VFX = Instantiate(m_VFXPrefab, this.transform);
                m_pooledVFX.Add(VFX);
            }
        }

        public GameObject GetPooledVFX()
        {
            for (int i = 0; i < m_pooledVFX.Count; i++)
            {
                //if (!m_pooledVFX[i].activeInHierarchy)
                if (!m_pooledVFX[i].GetComponent<ParticleSystem>().isPlaying)
                {
                    return m_pooledVFX[i];
                }
            }
            return null;
        }
    }
}
