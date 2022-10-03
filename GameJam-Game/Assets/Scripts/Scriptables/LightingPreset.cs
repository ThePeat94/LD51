using System;
using UnityEngine;

namespace Nidavellir.Scriptables
{
    [Serializable]
    [CreateAssetMenu(fileName = "LightingPreset", menuName = "Data/Lighting Preset")]
    public class LightingPreset: ScriptableObject
    {
        public Gradient AmbientColor;
        public Gradient DirectionalColor;
        public Gradient FogColor;
        public AnimationCurve directionalAngle;
    }
}