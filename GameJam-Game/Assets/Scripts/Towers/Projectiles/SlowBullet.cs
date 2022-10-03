using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir.Towers.Projectiles
{
    public class SlowBullet : BaseBullet
    {
        [SerializeField] private EnemyBuffData m_debuffData;

        protected override void HitTarget()
        {
            this.target.GetComponent<EnemyStats>().ApplyDebuff(this.m_debuffData);
            base.HitTarget();
        }
    }
}