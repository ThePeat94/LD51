using System.Linq;
using Nidavellir.Audio;
using Nidavellir.Scriptables.Audio;
using UnityEngine;

namespace Nidavellir.Towers.Projectiles
{
    public class AoeBullet : Projectile
    {
        [SerializeField] private float hitRange;
        [SerializeField] private SfxData hitSfxData;

        private Vector3 initialPosition;
        private float initialDistance;

        public override void Init(GameObject target, Vector3 targetPosition, float damage)
        {
            base.Init(target, targetPosition, damage);
            transform.LookAt(targetPosition);
            initialPosition = transform.position;

            initialDistance = Vector3.Distance(transform.position, this.targetPosition);
        }

        public override void Move(float deltaTime)
        {
            var distance = Vector3.Distance(transform.position, targetPosition);

            var targetPos = targetPosition;

            if (distance > initialDistance / 2f)
            {
                targetPos.y += initialDistance / 4f;
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPos, this.moveSpeed * deltaTime);
            
            if (distance <= 0.01f)
            {
                if (target != null)
                {
                    var unitsInHitRange = Physics.OverlapSphere(this.transform.position, hitRange);
                    
                    foreach (var unit in unitsInHitRange)
                    {
                        if (unit.TryGetComponent<EnemyHealthController>(out var enemyHealthController))
                        {
                            enemyHealthController.TakeDamage(damage);
                        }
                    }

                    SfxPlayer.Instance.PlayOneShot(hitSfxData);
                }

                Destroy(gameObject);
            }
        }
    }
}