using System;
using UnityEngine;

namespace Nidavellir.Trigger
{
    [RequireComponent(typeof(Collider))]
    public abstract class BaseTrigger<TTriggerCollider> : MonoBehaviour
        where TTriggerCollider : Collider
    {
        public event Action<GameObject> EventOnTriggerEnter;
        public event Action<GameObject> EventOnTriggerStay;
        public event Action<GameObject> EventOnTriggerExit;
        
        [Header("References")] 
        public TTriggerCollider TriggerCollider;

        public virtual void Init(Vector3 size)
        {
            SetSize(size);
        }

        public abstract void SetSize(Vector3 size);
        
        public void OnTriggerEnter(Collider other)
        {
            EventOnTriggerEnter?.Invoke(other.gameObject);
        }

        public void OnTriggerStay(Collider other)
        {
            EventOnTriggerStay?.Invoke(other.gameObject);
        }

        public void OnTriggerExit(Collider other)
        {
            EventOnTriggerExit?.Invoke(other.gameObject);
        }
    }
}
