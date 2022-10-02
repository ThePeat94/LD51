using Nidavellir.Audio;
using Nidavellir.Scriptables;
using Nidavellir.Scriptables.Audio;
using UnityEngine;

namespace Nidavellir
{
    public class CurrencyController : MonoBehaviour
    {
        private static CurrencyController instance;
        [SerializeField] private Resource currencyResource;
        [SerializeField] private SfxData gainCurrencySfxData;

        public static CurrencyController Instance => instance;
        public Resource CurrencyResource => currencyResource;

        private CurrencyController()
        {
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void AddCurrency(int amount)
        {
            currencyResource.ResourceController.Add(amount);

            SfxPlayer.Instance.PlayOneShot(gainCurrencySfxData);
        }

        public bool BuyItem(int amount)
        {
            if (!currencyResource.ResourceController.CanAfford(amount))
                return false;

            currencyResource.ResourceController.UseResource(amount);
            return true;
        }

        [ContextMenu("AddCurrency")]
        private void TestAdd()
        {
            AddCurrency(100);
        }
    }
}