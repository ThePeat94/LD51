using System;
using DG.Tweening;
using UnityEngine;

namespace Nidavellir.Towers
{
    public class BaseTowerAnimator : MonoBehaviour
    {
        [SerializeField] private Transform m_muzzle;

        public void Animate(GameObject target)
        {
            if (this.m_muzzle != null && target != null)
            {
                this.m_muzzle
                    .DOLookAt(target.transform.position, 0.1f)
                    .SetEase(Ease.Linear);
            }
        }
    }
}