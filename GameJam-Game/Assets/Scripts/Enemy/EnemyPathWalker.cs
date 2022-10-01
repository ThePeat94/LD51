using System;
using System.Collections;
using System.Collections.Generic;
using Nidavellir.PathManagement;
using Nidavellir.Scriptables;
using Unity.VisualScripting;
using UnityEngine;

namespace Nidavellir
{
    public class EnemyPathWalker : MonoBehaviour
    {
        private int m_targetPointIndex;
        private EnemyStats m_enemyStats;

        public Path Path { get; set; }

        private void Awake()
        {
            this.m_enemyStats = this.GetComponent<EnemyStats>();
        }

        // Start is called before the first frame update
        void Start()
        {
            this.m_targetPointIndex = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Distance(this.transform.position, this.Path.WayPoints[this.m_targetPointIndex].position) > 0.1f)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, this.Path.WayPoints[this.m_targetPointIndex].position, this.m_enemyStats.MovementSpeed * Time.deltaTime);
            }
            else
            {
                if (this.m_targetPointIndex < this.Path.WayPoints.Count - 1)
                {
                    this.m_targetPointIndex += 1;
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
