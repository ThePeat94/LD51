using UnityEngine;

namespace Nidavellir.Turrets.Projectiles
{
    public class BaseBullet : Projectile
    {
        public override void Move(float deltaTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, MoveSpeed * deltaTime);
            transform.LookAt(TargetPosition);
            
            if (Vector3.Distance(transform.position, TargetPosition) <= 0.01f)
            {
                //TODO do damage to enemy script
                
                Destroy(gameObject);
            }
        }
    }
}