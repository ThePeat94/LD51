using System;
using DG.Tweening;
using Nidavellir.EventArgs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Nidavellir.UI
{
    public class EnemyHealthUI : MonoBehaviour
    {
        [Header("References")] 
        public Slider healthSlider;
        public TextMeshProUGUI healthValueText;
        public EnemyHealthUIEffect EnemyHealthUIEffect;
        
        private EnemyHealthController enemyHealthController;
        private Bounds colliderBounds;

        private void Start()
        {
            var healthController = GetComponentInParent<EnemyHealthController>();
            colliderBounds = GetComponentInParent<Collider>().bounds;

            Init(new Vector3(colliderBounds.center.x, colliderBounds.max.y, colliderBounds.center.z), healthController);
        }

        public void Init(Vector3 position, EnemyHealthController enemyHealthController)
        {
            this.enemyHealthController = enemyHealthController;
            enemyHealthController.ResourceController.ValueChanged += EnemyHealthChanged;

            transform.position = position;

            healthSlider.maxValue = this.enemyHealthController.ResourceController.MaxValue;
            
            UpdateUIValues(this.enemyHealthController.ResourceController.CurrentValue);
        }

        private void EnemyHealthChanged(object sender, ResourceValueChangedEventArgs e)
        {
            UpdateUIValues(e.NewValue);

            //only spawn UI effect on health loss
            if (e.OldValue > e.NewValue)
            {
                var effect = Instantiate(EnemyHealthUIEffect.gameObject).GetComponent<UI.EnemyHealthUIEffect>();
                effect.transform.SetParent(transform, false);
                effect.Init(Vector3.zero, (int)(e.OldValue - e.NewValue));
            }
        }

        private void UpdateUIValues(float newValue)
        {
            healthSlider.value = enemyHealthController.ResourceController.CurrentValue;
            healthValueText.text = ((int)newValue).ToString();
        }
    }
}
