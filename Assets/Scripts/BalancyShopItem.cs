using Balancy.API.Payments;
using Balancy.Models.SmartObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BalancyShopItem : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI count;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private Button buyButton;

    private StoreItem storeItem;

    private void CallBack(PurchaseProductResponseData data)
    {
        Debug.LogError(data.Error);
        Debug.LogError(data.Receipt);
    }

    public void Init(StoreItem storeItem)
    {
        this.storeItem = storeItem;
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => {
            Balancy.LiveOps.Store.PurchaseStoreItem(storeItem, CallBack);
        });
        storeItem.Sprite?.LoadSprite(sprite => icon.sprite = sprite);
        count.text = storeItem.Reward.Items[0].Count.ToString();
        price.text = storeItem.Price.Product.Price.ToString();
    }
}
