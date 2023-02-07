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

    public void Initialize(IPurchasable purchasable) {
        this.purchasable = purchasable;
        title.text = purchasable.name;
        cost.text = UIShop.GetPriceText(this.purchasable);
        thumbnail.sprite = purchasable.GetSprite();
        ShopManager.SeedsChanged += UpdateText;
        if(purchasable.coinsCost > 0)
            ShopManager.CoinsChanged += UpdateText;
        UpdateText();
    }

    void OnDestroy() {
        ShopManager.SeedsChanged -= UpdateText;
        ShopManager.CoinsChanged -= UpdateText;
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
