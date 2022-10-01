using UnityEngine;

namespace Nidavellir.Tower.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        [Header("Fields")] 
        [SerializeField] private float damage;
        [SerializeField] private float moveSpeed;

        private GameObject target;
        private Vector3 targetPosition;

        public float Damage => damage;
        public float MoveSpeed => moveSpeed;
        public GameObject Target => target;
        public Vector3 TargetPosition => targetPosition;

        public virtual void Init(GameObject target, Vector3 targetPosition)
        {
            this.target = target;
            this.targetPosition = targetPosition;
        }

        public abstract void Move(float deltaTime);

        private void LateUpdate()
        {
            Move(Time.deltaTime);
        }
    }
}
