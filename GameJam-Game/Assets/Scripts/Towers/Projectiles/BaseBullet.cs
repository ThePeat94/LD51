using UnityEngine;

namespace Nidavellir.Towers.Projectiles
{
    public class BaseBullet : Projectile
    {
        public override void Init(GameObject target, Vector3 targetPosition, float damage)
        {
            base.Init(target, targetPosition, damage);
            transform.LookAt(TargetPosition);
        }
        
        public override void Move(float deltaTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, MoveSpeed * deltaTime);

            if (Vector3.Distance(transform.position, TargetPosition) <= 0.01f)
            {
                //TODO do damage to enemy script
                
                Object.Destroy(gameObject);
            }
        }
    }
}