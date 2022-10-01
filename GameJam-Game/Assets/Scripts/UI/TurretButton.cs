using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Nidavellir
{
    public class TurretButton : MonoBehaviour
    {
        [SerializeField] private Image image;

        [SerializeField] private Button button;

        public Action<TurretType> OnButtonClick;

        private Turret turret;

        public void SetTurret(Turret turret)
        {
            this.turret = turret;

            switch (turret.Type)
            {
                case TurretType.Red:
                    image.color = Color.red;
                    break;
                case TurretType.Blue:
                    image.color = Color.blue;
                    break;
                case TurretType.Green:
                    image.color = Color.green;
                    break;
                default:
                    image.color = Color.red;
                    break;
            }
        }

        private void Awake()
        {
            button.onClick.AddListener(() =>
            {
                OnButtonClick?.Invoke(turret.Type);
            });
        }
    }
}