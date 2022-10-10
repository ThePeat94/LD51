using UnityEngine;

namespace Nidavellir.Towers.Shooting
{
    public interface ITowerShootStrategy
    {
        public GameObject Target { get; }
        public void Shoot();
    }
}