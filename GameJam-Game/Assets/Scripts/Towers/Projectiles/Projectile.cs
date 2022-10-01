using UnityEngine;

namespace Nidavellir.Towers.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        [Header("Fields")]
        [SerializeField] private float moveSpeed;

        private float damage;
        private GameObject target;
        private Vector3 targetPosition;

        public float MoveSpeed => moveSpeed;
        public GameObject Target => target;
        public Vector3 TargetPosition => targetPosition;

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
