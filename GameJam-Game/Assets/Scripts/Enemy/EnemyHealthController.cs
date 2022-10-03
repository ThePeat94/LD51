using System;
using System.Collections.Generic;
using Nidavellir.Audio;
using Nidavellir.Scriptables;
using Nidavellir.Scriptables.Audio;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Nidavellir
{
    public class EnemyHealthController : MonoBehaviour
    {
        [SerializeField] private EnemySO m_enemyConfig;
        [SerializeField] private EnemyPathWalker m_enemyPathWalker;
        [SerializeField] private ResourceData m_resourceData;
        [SerializeField] private List<SfxData> spawnSfxData;
        [SerializeField] private List<SfxData> deathSfxData;

        private ResourceController m_resourceController;
        private bool dead;

        public event Action OnDeath;

        public ResourceController ResourceController => this.m_resourceController;


        public void Init(ResourceData resourceData, EnemySO config)
        {
            this.m_resourceData = resourceData;
            this.m_enemyConfig = config;
            this.m_resourceController = new(resourceData);

            if (spawnSfxData != null && spawnSfxData.Count > 0)
            {
                SfxPlayer.Instance.PlayOneShot(spawnSfxData[Random.Range(0, spawnSfxData.Count)]);
            }
        }

        public void TakeDamage(float amount)
        {
            this.m_resourceController.SubtractResource(amount);

            if (this.m_resourceController.CurrentValue <= 0 && !dead)
            {
                Die();
            }
        }

        private void Die()
        {
            dead = true;
            CurrencyController.Instance.AddCurrency(this.m_enemyConfig.CurrencyReward);

            GetComponent<Collider>().enabled = false;

            m_enemyPathWalker.Animator.SetTrigger(EnemyPathWalker.Die);
            m_enemyPathWalker.enabled = false;

            if (deathSfxData != null && deathSfxData.Count > 0)
            {
                SfxPlayer.Instance.PlayOneShot(deathSfxData[Random.Range(0, deathSfxData.Count)]);
            }

            Destroy(this.gameObject, 1.5f);

            OnDeath?.Invoke();
        }
    }
}