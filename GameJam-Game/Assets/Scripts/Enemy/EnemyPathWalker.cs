using System;
using Nidavellir.Audio;
using Nidavellir.PathManagement;
using Nidavellir.Scriptables.Audio;
using UnityEngine;

namespace Nidavellir
{
    public class EnemyPathWalker : MonoBehaviour
    {
        public static readonly int Walking = Animator.StringToHash("Walking");
        public static readonly int Die = Animator.StringToHash("Die");

        private EventHandler m_reachedGoal;

        [SerializeField] private SfxData m_walkingSfx;
        
        
        [Header("References")] 
        public Animator Animator;
        
        private int m_targetPointIndex;
        private EnemyStats m_enemyStats;
        private AudioSource m_walkAudioSource;

        public Path Path { get; set; }
        
        public event EventHandler ReachedGoal
        {
            add => this.m_reachedGoal += value;
            remove => this.m_reachedGoal -= value;
        }

        private void Awake()
        {
            this.m_enemyStats = this.GetComponent<EnemyStats>();
            Animator.SetBool(EnemyPathWalker.Walking, true);
        }

        // Start is called before the first frame update
        void Start()
        {
            this.m_targetPointIndex = 0;
            if(this.m_walkingSfx != null)
                this.m_walkAudioSource = SfxPlayer.Instance.PlayLoopingSfx(this.m_walkingSfx);
        }

        private void OnDestroy()
        {
            if(this.m_walkAudioSource)
                Destroy(this.m_walkAudioSource);
        }

        // Update is called once per frame
        void Update()
        {
            var targetPoint = this.Path.WayPoints[this.m_targetPointIndex].position;

            if (Vector3.Distance(this.transform.position, targetPoint) > 0.8f)
            {
                var newPosition = Vector3.MoveTowards(this.transform.position, targetPoint, this.m_enemyStats.MovementSpeed * Time.deltaTime);
                
                this.transform.position = newPosition;
                this.transform.LookAt(new Vector3(targetPoint.x, transform.position.y, targetPoint.z));

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
                this.m_reachedGoal?.Invoke(this.gameObject, System.EventArgs.Empty);
                Destroy(this.gameObject, 1.5f);
            }
        }
    }
}
