using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class UIShop : MonoBehaviour {
    public static UIShop instance { get; private set; }

    [SerializeField] RectTransform body;
    [SerializeField] UIShopButton shopButtonPrefab;
    [SerializeField] UIButton leftButton;
    [SerializeField] UIButton rightButton;
    [SerializeField] GameObject infoBox;
    [SerializeField] TextMeshProUGUI infoTitle;
    [SerializeField] TextMeshProUGUI infoDescription;
    [SerializeField] Image cropsButton;
    [SerializeField] Image upgradesButton;
    [SerializeField] RectTransform cropsContent;
    [SerializeField] RectTransform upgradesContent;

    int _cropIndex;
    int cropIndex {
        get {
            return _cropIndex;
        }
        set {
            if(_cropIndex != value) {
                _cropIndex = value;
                UpdateCropIndex();
            }
        }
    }

    int _upgradeIndex;
    int upgradeIndex {
        get {
            return _upgradeIndex;
        }
        set {
            if(_upgradeIndex != value) {
                _upgradeIndex = value;
                UpdateUpgradeIndex();
            }
        }
    }

    List<UIShopButton> cropButtons = new List<UIShopButton>();
    List<UIShopButton> upgradeButtons = new List<UIShopButton>();
    Vector2 minSize;
    Vector2 maxSize;
    Sequence openSequence;
    Sequence contentSequence;
    Color buttonLight;
    Color buttonDark;
    Tab currentTab;
    bool active;

    void Awake() {
        instance = this;
        buttonLight = new Color(.2f, .2f, .2f);
        buttonDark = new Color(.1f, .1f, .1f);
        minSize = new Vector2(0, 0);
        maxSize = new Vector2(828, 392);
        body.sizeDelta = minSize;
        foreach(ICrop crop in CropManager.crops.Values) {
            UIShopButton button = Instantiate(shopButtonPrefab, cropsContent);
            button.Clicked += (UIButton button) => {
                UIShopButton shopButton = button as UIShopButton;
                ShopManager.TryBuyCrop((ICrop)shopButton.purchasable);
            };
            button.Hovered += (UIButton button) => {
                UIShopButton shopButton = button as UIShopButton;
                infoBox.SetActive(true);
                infoTitle.text = shopButton.purchasable.name;
                infoDescription.text = shopButton.purchasable.description;
            };
            button.Unhovered += (UIButton button) => {
                infoBox.SetActive(false);
            };
            button.Initialize(crop);
            cropButtons.Add(button);
        }
        UpgradeManager.Changed += PopulateUpgrades;
        PopulateUpgrades();
        UpdateCropIndex();
        SetTab(Tab.Crops);
    }

    void UpdateCropIndex() {
        contentSequence?.Kill();
        contentSequence = DOTween.Sequence();
        RectTransform content = null;
        switch(currentTab) {
            case Tab.Crops:
                content = cropsContent;
                break;
            case Tab.Upgrades:
                content = upgradesContent;
                break;
        }
        contentSequence.Append(content.DOAnchorPos(new Vector2(-cropIndex * 384, 0), 0.2f).SetEase(Ease.OutQuint));
        leftButton.gameObject.SetActive(cropIndex > 0);
        rightButton.gameObject.SetActive(cropIndex < cropButtons.Count / 6);
    }

    void UpdateUpgradeIndex() {
        contentSequence?.Kill();
        contentSequence = DOTween.Sequence();
        RectTransform content = null;
        switch(currentTab) {
            case Tab.Crops:
                content = cropsContent;
                break;
            case Tab.Upgrades:
                content = upgradesContent;
                break;
        }
        contentSequence.Append(content.DOAnchorPos(new Vector2(-upgradeIndex * 384, 0), 0.2f).SetEase(Ease.OutQuint));
        leftButton.gameObject.SetActive(upgradeIndex > 0);
        rightButton.gameObject.SetActive(upgradeIndex < upgradeButtons.Count / 6);
    }

    void PopulateUpgrades() {
        foreach(UIShopButton button in upgradeButtons.ToList()) {
            Destroy(button.gameObject);
        }
        upgradeButtons.Clear();
        foreach(IUpgrade upgrade in UpgradeManager.available) {
            UIShopButton button = Instantiate(shopButtonPrefab, upgradesContent);
            button.Clicked += (UIButton button) => {
                UIShopButton shopButton = button as UIShopButton;
                ShopManager.TryBuyUpgrade((IUpgrade)shopButton.purchasable);
            };
            button.Hovered += (UIButton button) => {
                UIShopButton shopButton = button as UIShopButton;
                infoBox.SetActive(true);
                infoTitle.text = shopButton.purchasable.name;
                infoDescription.text = shopButton.purchasable.description;
            };
            button.Unhovered += (UIButton button) => {
                infoBox.SetActive(false);
            };
            button.Initialize(upgrade);
            upgradeButtons.Add(button);
        }
    }

    void OnDisable() {
        openSequence?.Kill();
        contentSequence?.Kill();
    }

    public void Show() {
        if(active) return;
        AudioManager.PlaySound("Page");
        openSequence?.Kill();
        openSequence = DOTween.Sequence();
        openSequence.Append(body.DOSizeDelta(maxSize, 0.2f).SetEase(Ease.OutQuint));
        active = true;
    }

    public void Hide() {
        if(!active) return;
        openSequence?.Kill();
        body.sizeDelta = minSize;
        active = false;
    }

    public void Toggle() {
        if(active) {
            Hide();
        } else {
            Show();
        }
    }

    public void Left() {
        cropIndex--;
        UpdateCropIndex();
    }

    public void Right() {
        cropIndex++;
        UpdateCropIndex();
    }

    public static string GetPriceText(IPurchasable crop) {
        if(crop.coinsCost > 0 && crop.seedsCost > 0)
            return $"{crop.seedsCost} seeds, {crop.coinsCost} coins";
        else if(crop.seedsCost > 0)
            return $"{crop.seedsCost} seeds";
        else if(crop.coinsCost > 0)
            return $"{crop.coinsCost} coins";
        else
            return "Free";
    }

    public void SetTab(string tab) {
        SetTab((Tab)System.Enum.Parse(typeof(Tab), tab));
    }

    public void SetTab(Tab tab) {
        currentTab = tab;
        switch(tab) {
            case Tab.Crops:
                cropsButton.color = buttonDark;
                upgradesButton.color = buttonLight;
                cropsContent.gameObject.SetActive(true);
                upgradesContent.gameObject.SetActive(false);
                break;
            case Tab.Upgrades:
                cropsButton.color = buttonLight;
                upgradesButton.color = buttonDark;
                cropsContent.gameObject.SetActive(false);
                upgradesContent.gameObject.SetActive(true);
                break;
        }
    }

    public enum Tab {
        Crops,
        Upgrades,
    }
}
