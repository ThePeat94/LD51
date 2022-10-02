using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nidavellir.PathManagement
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private List<Transform> m_wayPoints = new List<Transform>();
        [SerializeField] private PlayerHealthController m_playerBase;
        
        public List<Transform> WayPoints => this.m_wayPoints;

        private void Awake()
        {
            this.m_wayPoints.Add(this.m_playerBase.transform);
        }

        private void OnDrawGizmos()
        {
            Transform previousWayPoint = null;
            foreach (var wayPoint in this.m_wayPoints)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(wayPoint.position, 1f);
                
                if (previousWayPoint != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(wayPoint.position, previousWayPoint.position);
                }

                previousWayPoint = wayPoint;
            }

            if (this.m_playerBase && previousWayPoint != null)
            {
                Gizmos.DrawLine(previousWayPoint.position, this.m_playerBase.transform.position);
            }
        }
    }
}