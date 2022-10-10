using System;
using Nidavellir.Scriptables;
using Nidavellir.Towers.Shooting;
using Nidavellir.Towers.TargetFinding;
using UnityEngine;

namespace Nidavellir.Towers
{
    public class BaseTower : MonoBehaviour
    {
        [SerializeField] private TowerSO m_towerConfig;

        private BaseTowerStats m_baseTowerStats;
        private void Awake()
        {
        }
    }
}