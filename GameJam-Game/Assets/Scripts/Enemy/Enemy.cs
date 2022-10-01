using System.Collections;
using System.Collections.Generic;
using Nidavellir.PathManagement;
using UnityEngine;

namespace Nidavellir
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int m_health;
        // [SerializeField] private Vector3 m_position;
        [SerializeField] private Vector3 m_direction;
        [SerializeField] private int m_damage;
        [SerializeField] private int m_level;
        [SerializeField] private Path path;
        [SerializeField] private int targetPointIndex;

        public Vector3 Direction => m_direction;

        // Start is called before the first frame update
        void Start()
        {
            this.transform.position = Vector3.zero;
            this.m_health = 5000;
            this.m_direction = new Vector3(0, 0, -0.001f);
            targetPointIndex = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Distance(this.transform.position, this.path.WayPoints[targetPointIndex].position) > 0.1f)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, this.path.WayPoints[targetPointIndex].position, 0.001f);
            }
            else
            {
                if (targetPointIndex < path.WayPoints.Count - 1)
                {
                    targetPointIndex += 1;
                
                }
            }
            // this.m_health = this.m_health - 1;
            
            
            if (this.m_health == 0)
            {
                Destroy(gameObject);
            }
            
        }
    }
}
