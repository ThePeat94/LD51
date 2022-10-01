using UnityEngine;

namespace Nidavellir.Turrets.Projectiles
{
    public class BaseBullet : Projectile
    {
        #region Overrides of Projectile

        public override void Init(GameObject target, Vector3 targetPosition)
        {
            base.Init(target, targetPosition);
            transform.LookAt(TargetPosition);
        }

        #endregion

        public override void Move(float deltaTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, MoveSpeed * deltaTime);

            if (Vector3.Distance(transform.position, TargetPosition) <= 0.01f)
            {
                //TODO do damage to enemy script
                
                Destroy(gameObject);
            }
        }
    }
}