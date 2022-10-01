using System;
using Nidavellir.Scriptables;
using UnityEngine;
using UnityEngine.UI;

namespace Nidavellir.UI
{
    public class TurretButton : MonoBehaviour
    {
        [SerializeField] private Image image;

        [SerializeField] private Button button;

        public Action<TowerSO> OnButtonClick;

        private TowerSO towerSo;

        public void SetTurret(TowerSO towerSo)
        {
            this.towerSo = towerSo;

            image.sprite = towerSo.Icon;
        }

        private void Awake()
        {
            button.onClick.AddListener(() =>
            {
                OnButtonClick?.Invoke(towerSo);
            });
        }
    }
}