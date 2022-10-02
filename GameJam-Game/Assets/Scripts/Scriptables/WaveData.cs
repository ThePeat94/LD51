using System.Collections.Generic;
using Nidavellir.Util;
using UnityEngine;

namespace Nidavellir.Scriptables
{
    [CreateAssetMenu(fileName = "Wave Data", menuName = "Wave Data", order = 0)]
    public class WaveData : ScriptableObject
    {
        [SerializeField] private List<SpawnGroup> m_spawnGroups;

        public List<SpawnGroup> SpawnGroups => this.m_spawnGroups;
    }
}