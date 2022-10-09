using System;
using DG.Tweening;
using UnityEngine;

namespace Nidavellir
{
    public class FloatingOrb : MonoBehaviour
    {
        private void Start()
        {
            this.transform.DOLocalMove(this.transform.localPosition + new Vector3(0, 3, 0), 2f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}