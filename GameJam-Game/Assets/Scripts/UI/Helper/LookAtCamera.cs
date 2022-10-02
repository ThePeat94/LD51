using UnityEngine;

namespace Nidavellir.UI.Helper
{
    public class LookAtCamera : MonoBehaviour
    {
        private void Update()
        {
            var cameraPosition = Camera.main.transform.position;
            transform.LookAt(new Vector3(cameraPosition.x, transform.position.y, transform.position.z));
        }
    }
}