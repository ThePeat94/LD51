using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Nidavellir.UI
{
    public class EnemyHealthUIEffect : MonoBehaviour
    {
        [Header("References")] 
        public TextMeshProUGUI valueText;

        private const float minOffsetX = 1f;
        private const float maxOffsetX = 2.5f;
        private const float minOffsetY = 1f;
        private const float maxOffsetY = 2f;
        private const float animationDuration = 1f;
        
        public void Init(Vector3 position, int value)
        {
            var color = valueText.color;
            transform.localPosition = position;
            valueText.text = value.ToString();

            if (Random.Range(0, 2) == 0)
                transform.DOLocalMove(new Vector3(Random.Range(minOffsetX, maxOffsetX), Random.Range(minOffsetY, maxOffsetY), 0f), animationDuration).SetEase(Ease.OutCubic);
            else
                transform.DOLocalMove(new Vector3(Random.Range(-maxOffsetX, -minOffsetX), Random.Range(minOffsetY, maxOffsetY), 0f), animationDuration).SetEase(Ease.OutCubic);

            valueText.DOColor(new Color(color.r, color.g, color.b, 0f), animationDuration).SetEase(Ease.InCubic);
            
            Destroy(gameObject, EnemyHealthUIEffect.animationDuration + 0.1f);
        }
    }
}