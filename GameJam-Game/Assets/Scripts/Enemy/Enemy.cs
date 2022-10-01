using System;
using System.Collections;
using System.Collections.Generic;
using Nidavellir.PathManagement;
using Nidavellir.Scriptables;
using Unity.VisualScripting;
using UnityEngine;

namespace Nidavellir
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Path path;
        [SerializeField] private int targetPointIndex;
        [SerializeField] private EnemySO enemySettings;

        private EnemyStats m_enemyStats;

        public EnemySO EnemySettings => enemySettings;

        private void Awake()
        {
            this.m_enemyStats = this.GetComponent<EnemyStats>();
        }

        // Start is called before the first frame update
        void Start()
        {
            targetPointIndex = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Distance(this.transform.position, this.path.WayPoints[targetPointIndex].position) > 0.1f)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, this.path.WayPoints[targetPointIndex].position, this.m_enemyStats.MovementSpeed * Time.deltaTime);
            }
            else
            {
                if (targetPointIndex < path.WayPoints.Count - 1)
                {
                    targetPointIndex += 1;
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<PlayerHealthController>(out var playerHealthController))
            {
                playerHealthController.TakeDamage(this.m_enemyStats.Damage);
                Destroy(this.gameObject);
            }
        }
    }
}
