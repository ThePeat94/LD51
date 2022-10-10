using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nidavellir.Towers.TargetFinding
{
    public class ClosestEnemyStrategy : ITowerTargetStrategy
    {
        private BaseTowerStats m_baseTowerStats;
        private Transform m_sender;
        
        public ClosestEnemyStrategy(BaseTowerStats towerStats, Transform sender)
        {
            this.m_baseTowerStats = towerStats;
            this.m_sender = sender;
        }

        public List<EnemyHealthController> FindEnemiesInRange(int count) =>
            Physics.OverlapSphere(this.m_sender.position, this.m_baseTowerStats.Range)
                .Select(c => c.GetComponent<EnemyHealthController>())
                .Where(c => c != null)
                .OrderBy(x => Vector3.Distance(this.m_sender.position, x.transform.position))
                .Take(count)
                .ToList();
    }
}