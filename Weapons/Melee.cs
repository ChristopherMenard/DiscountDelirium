using BoxSystem;
using System.Collections;
using UnityEngine;
using static Manager.AudioManager;

namespace DiscountDelirium
{
    public class Melee : Weapon
    {
        [Header("Tower References and parameters")]
        [SerializeField] private GrabItemTrigger m_playerGrabTrigger;
        [SerializeField] protected float m_animationTime = 1;
        [SerializeField] private int[] m_itemsToStealLevel;

        [Header("References")]
        private Animator m_animator;
        [SerializeField] private TrailRenderer m_trailRenderer;
        [SerializeField] private VFXPool m_VFXPool;

        [Header("Stats")]
        [SerializeField] private float[] m_speedLevel;
        [SerializeField] private GameObject[] m_models;

        private int m_weaponIndex;

        private void Awake()
        {       
            m_animator = GetComponent<Animator>();
            m_animator.speed = m_speedLevel[PlayerPrefs.GetInt("Melee", 0)];
            m_weaponIndex = PlayerPrefs.GetInt("Melee", 0);
            ChangeWeaponModel();

            m_trailRenderer.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!m_isWeaponActive)
            {
                return;
            }

			//Debug.LogWarning("Melee hitted target");
			if (other.gameObject.layer == LayerMask.NameToLayer("Target")) 
            {
                PlayVFX(other.ClosestPoint(transform.position));
                StealItems(other.gameObject.GetComponent<Target>().GetTower());
            }
        }

        public override void UseWeapon()
        {
            if (!m_canUseWeapon)
            {
                return;
            }
			m_animator.SetTrigger("ActivateWeapon");
        }

        public void ActivateTrail()
        {
			m_isWeaponActive = true;
			m_trailRenderer.enabled = true;
        }

        public void DeactivateTrail()
        {
			m_isWeaponActive = false;
			m_trailRenderer.enabled = false;
        }

        public void ChangeWeaponModel() 
        {
            foreach (var model in m_models) 
            {
                model.SetActive(false);
            }
            m_weaponIndex = PlayerPrefs.GetInt("Melee", 0);
            m_models[m_weaponIndex].SetActive(true);
        }

        public void UpdateMeleeWeapon() 
        {
            if (m_animator != null)
            {
                m_animator.speed = m_speedLevel[PlayerPrefs.GetInt("Melee", 0)];
            }
            ChangeWeaponModel();
        }

        public void PlaySound() 
        {
            _AudioManager.PlaySoundEffectsOneShot(ESound.MeleeSwoosh, transform.position);
        }

        private void PlayVFX(Vector3 pos) 
        {
            GameObject vfx = m_VFXPool.GetPooledVFX();
            if (vfx != null)
            {
                vfx.transform.position = pos;
                vfx.GetComponent<ParticleSystem>().Play();
                _AudioManager.PlaySoundEffectsOneShot(ESound.Hit, transform.position);
            }
        }

        //-----------------Stealing-----------------//
        public void StealItems(TowerBoxSystem clientTower)
        {
            for (int i = 0; i < m_itemsToStealLevel[PlayerPrefs.GetInt("Melee", 0)]; i++)
            {
                m_playerGrabTrigger.StealItemFromOtherTower(clientTower);
            }
        }
    }
}
