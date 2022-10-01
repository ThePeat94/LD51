using UnityEngine;

namespace Nidavellir.Trigger
{
    [RequireComponent(typeof(SphereCollider))]
    public class SphereBaseTrigger : BaseTrigger<SphereCollider>
    {
        public override void SetSize(Vector3 size)
        {
            TriggerCollider.radius = size.x;
        }

    }
}
