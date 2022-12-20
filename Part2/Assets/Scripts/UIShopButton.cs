using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UIShopButton : UIButton {
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI cost;
    [SerializeField] Image thumbnail;

    public IPurchasable purchasable { get; private set; }

    public void Initialize(IPurchasable crop) {
        this.purchasable = crop;
        title.text = crop.name;
        cost.text = UIShop.GetPriceText(purchasable);
        thumbnail.sprite = Resources.Load<Sprite>($"Thumbnails/{crop.name}");
        ShopManager.SeedsChanged += UpdateText;
        if(crop.coinsCost > 0)
            ShopManager.CoinsChanged += UpdateText;
        UpdateText();
    }

    public override void OnPointerDown(PointerEventData eventData) {
        if(ShopManager.CanAfford(purchasable)) {
            AudioManager.PlaySound("Click");
        } else {
            AudioManager.PlaySound("Error");
        }
        pressSequence?.Kill();
        pressSequence = DOTween.Sequence();
        pressSequence.Append(contents.DOScale(0.9f, 0.1f));
    }

    void UpdateText() {
        if(ShopManager.CanAfford(purchasable))
            cost.color = Color.white;
        else
            cost.color = Color.red;
    }
}
