using UnityEngine;

namespace Nidavellir.Towers.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        [Header("Fields")]
        [SerializeField] protected float moveSpeed;

        protected float damage;
        protected GameObject target;
        protected Vector3 targetPosition;

        public virtual void Init(GameObject target, Vector3 targetPosition, float damage)
        {
            this.target = target;
            this.targetPosition = targetPosition;
            this.damage = damage;
        }

        public abstract void Move(float deltaTime);

        private void LateUpdate()
        {
            Move(Time.deltaTime);
        }
    }
}
