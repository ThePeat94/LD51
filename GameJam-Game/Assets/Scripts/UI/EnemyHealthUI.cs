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
        
        private EnemyHealthController enemyHealthController;
        
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
        }

        private void UpdateUIValues(float newValue)
        {
            healthSlider.value = enemyHealthController.ResourceController.CurrentValue;
            healthValueText.text = ((int)newValue).ToString();
        }
    }
}
