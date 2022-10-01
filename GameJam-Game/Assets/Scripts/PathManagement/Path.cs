using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nidavellir.PathManagement
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private List<Transform> m_wayPoints;

        public List<Transform> WayPoints => this.m_wayPoints;

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
        }
    }
}