using BoxSystem;
using CartControl;
using System.Collections;
using TMPro;
using UnityEngine;
using static Manager.AudioManager;

namespace DiscountDelirium
{
    public class Range : Weapon
    {
        [Header("References")]
        [SerializeField] private GameObject m_pointToShoot;
        [SerializeField] private ParticleSystem m_particleSmoke;
        [SerializeField] private CartStateMachine m_cartStateMachine;
        [SerializeField] private TextMeshProUGUI m_text;

        [Header("Ammo")]
        [SerializeField] private GameObject m_ammoPrebab;
        [SerializeField] private GameObject m_ammoPoolPrefab;

        [Header("Stats")]
        [SerializeField] private int m_maxAmmo;
        [SerializeField] private float[] m_force;
        [SerializeField] private float[] m_fireRate;

        private int m_actualAmmo;
        private bool m_canFire = true;

        private void Awake()
        {
            GameObject pool = Instantiate(m_ammoPoolPrefab);
            pool.GetComponent<AmmoPool>().SetAmmo(m_ammoPrebab);
        }

        private void Start()
        {
            base.Start();
            m_actualAmmo = m_maxAmmo;
            m_text.text = m_actualAmmo.ToString();
        }

        public override void UseWeapon()
        {
            if (!m_canUseWeapon)
            {
                Debug.LogWarning("Range Weapon unabled");
                return;
            }
            if (m_canFire && m_actualAmmo > 0) 
            {
                if (AmmoPool.instance == null) 
                {
                    Debug.LogError("No instance");
                }
                GameObject ammo = AmmoPool.instance.GetPooledAmmo();
                if (ammo == null)
                {
                    return;
                }
                _AudioManager.PlaySoundEffectsOneShot(ESound.CannonSound, transform.position);
                Debug.Log("RANGE: " + transform.position);
                StartCoroutine("Fire", ammo); 
            }
        }

        IEnumerator Fire(GameObject ammo)
        {
            Debug.Log("Range Weapon Used");
            m_canFire = false;
            m_actualAmmo--;
            m_text.text = m_actualAmmo.ToString();
            m_particleSmoke.Play();

            ammo.transform.position = m_pointToShoot.transform.position;
            ammo.transform.rotation = m_pointToShoot.transform.rotation;
            ammo.SetActive(true);
            ammo.GetComponent<Ammunition>().ShowAmmo();

            Vector3 force = m_pointToShoot.transform.forward * m_force[PlayerPrefs.GetInt("Ranged", 0)];
            force *= 1 + m_cartStateMachine.LocalVelocity.z * 0.1f;
            ammo.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);

            yield return new WaitForSeconds(1f / m_fireRate[PlayerPrefs.GetInt("Ranged", 0)]);
            m_canFire = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("AmmoStash")) 
            {
                other.gameObject.transform.parent.gameObject.GetComponent<AmmoCollectAnimCtrlr>().ActivateCollect();
                m_actualAmmo = m_maxAmmo;
                m_text.text = m_actualAmmo.ToString();
            }
        }

    }
}
