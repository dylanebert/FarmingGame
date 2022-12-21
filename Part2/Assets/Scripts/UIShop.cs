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
    [SerializeField] Image settingsButton;
    [SerializeField] RectTransform cropsContent;
    [SerializeField] RectTransform upgradesContent;
    [SerializeField] RectTransform settingsContent;

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

    public Tab currentTab { get; private set; }

    List<UIShopButton> cropButtons = new List<UIShopButton>();
    List<UIShopButton> upgradeButtons = new List<UIShopButton>();
    Vector2 minSize;
    Vector2 maxSize;
    Sequence openSequence;
    Sequence contentSequence;
    Color buttonLight;
    Color buttonDark;
    bool active;

    void HandleButtonClicked(UIButton button) {
        UIShopButton shopButton = button as UIShopButton;
        ShopManager.TryBuyCrop((ICrop)shopButton.purchasable);
    }

    void HandleButtonHovered(UIButton button) {
        UIShopButton shopButton = button as UIShopButton;
        infoBox.SetActive(true);
        infoTitle.text = shopButton.purchasable.name;
        infoDescription.text = shopButton.purchasable.description;
    }

    void HandleButtonUnhovered(UIButton button) {
        infoBox.SetActive(false);
    }

    void OnDisable() {
        openSequence?.Kill();
        contentSequence?.Kill();
    }

    void OnDestroy() {
        UpgradeManager.Changed -= PopulateUpgrades;
        for(int i = 0; i < cropButtons.Count; i++) {
            cropButtons[i].Clicked -= HandleButtonClicked;
            cropButtons[i].Hovered -= HandleButtonHovered;
            cropButtons[i].Unhovered -= HandleButtonUnhovered;
        }
        for(int i = 0; i < upgradeButtons.Count; i++) {
            upgradeButtons[i].Clicked -= HandleUpgradeButtonClicked;
            upgradeButtons[i].Hovered -= HandleButtonHovered;
            upgradeButtons[i].Unhovered -= HandleButtonUnhovered;
        }
    }

    void Awake() {
        instance = this;
        buttonLight = new Color(.2f, .2f, .2f);
        buttonDark = new Color(.1f, .1f, .1f);
        minSize = new Vector2(0, 0);
        maxSize = new Vector2(828, 392);
        body.sizeDelta = minSize;
    }

    void Start() {
        foreach(ICrop crop in CropManager.crops.Values) {
            UIShopButton button = Instantiate(shopButtonPrefab, cropsContent);
            button.Clicked += HandleButtonClicked;
            button.Hovered += HandleButtonHovered;
            button.Unhovered += HandleButtonUnhovered;
            button.Initialize(crop);
            cropButtons.Add(button);
        }
        List<int> a = new List<int>();
        List<int> b = new List<int>();
        for(int i = 0; i < cropButtons.Count; i++) {
            if(i % 6 < 3) {
                a.Add(i);
            } else {
                b.Add(i);
            }
        }

        while(a.Count - b.Count > 1) {
            int i = a[a.Count - 1];
            a.RemoveAt(a.Count - 1);
            b.Add(i);
        }
        int idx = 0;
        while(a.Count > 0) {
            cropButtons[a[0]].transform.SetSiblingIndex(idx);
            a.RemoveAt(0);
            idx++;
        }
        while(b.Count > 0) {
            cropButtons[b[0]].transform.SetSiblingIndex(idx);
            b.RemoveAt(0);
            idx++;
        }
        UpgradeManager.Changed += PopulateUpgrades;
        PopulateUpgrades();
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
        if(currentTab == Tab.Crops) {
            leftButton.gameObject.SetActive(cropIndex > 0);
            rightButton.gameObject.SetActive(cropIndex < (cropButtons.Count - 1) / 6);
        }
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
        if(currentTab == Tab.Upgrades) {
            leftButton.gameObject.SetActive(upgradeIndex > 0);
            rightButton.gameObject.SetActive(upgradeIndex < (upgradeButtons.Count - 1) / 6);
        }
    }

    void HandleUpgradeButtonClicked(UIButton button) {
        UIShopButton shopButton = button as UIShopButton;
        ShopManager.TryBuyUpgrade((IUpgrade)shopButton.purchasable);
    }

    void PopulateUpgrades() {
        foreach(UIShopButton button in upgradeButtons.ToList()) {
            button.Clicked -= HandleUpgradeButtonClicked;
            button.Hovered -= HandleButtonHovered;
            button.Unhovered -= HandleButtonUnhovered;
            if(button != null && button.gameObject != null)
                Destroy(button.gameObject);
        }
        upgradeButtons.Clear();
        foreach(IUpgrade upgrade in UpgradeManager.available) {
            UIShopButton button = Instantiate(shopButtonPrefab, upgradesContent);
            button.Clicked += HandleUpgradeButtonClicked;
            button.Hovered += HandleButtonHovered;
            button.Unhovered += HandleButtonUnhovered;
            button.Initialize(upgrade);
            upgradeButtons.Add(button);
        }
        List<int> a = new List<int>();
        List<int> b = new List<int>();
        for(int i = 0; i < upgradeButtons.Count; i++) {
            if(i % 6 < 3) {
                a.Add(i);
            } else {
                b.Add(i);
            }
        }

        while(a.Count - b.Count > 1) {
            int i = a[a.Count - 1];
            a.RemoveAt(a.Count - 1);
            b.Add(i);
        }
        int idx = 0;
        while(a.Count > 0) {
            upgradeButtons[a[0]].transform.SetSiblingIndex(idx);
            a.RemoveAt(0);
            idx++;
        }
        while(b.Count > 0) {
            upgradeButtons[b[0]].transform.SetSiblingIndex(idx);
            b.RemoveAt(0);
            idx++;
        }
        upgradeIndex = 0;
        UpdateUpgradeIndex();
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
        switch(currentTab) {
            case Tab.Crops:
                cropIndex--;
                UpdateCropIndex();
                break;
            case Tab.Upgrades:
                upgradeIndex--;
                UpdateUpgradeIndex();
                break;
        }
    }

    public void Right() {
        switch(currentTab) {
            case Tab.Crops:
                cropIndex++;
                UpdateCropIndex();
                break;
            case Tab.Upgrades:
                upgradeIndex++;
                UpdateUpgradeIndex();
                break;
        }
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
                settingsButton.color = buttonLight;
                cropsContent.gameObject.SetActive(true);
                upgradesContent.gameObject.SetActive(false);
                settingsContent.gameObject.SetActive(false);
                UpdateCropIndex();
                break;
            case Tab.Upgrades:
                cropsButton.color = buttonLight;
                upgradesButton.color = buttonDark;
                settingsButton.color = buttonLight;
                cropsContent.gameObject.SetActive(false);
                upgradesContent.gameObject.SetActive(true);
                settingsContent.gameObject.SetActive(false);
                UpdateUpgradeIndex();
                break;
            case Tab.Settings:
                cropsButton.color = buttonLight;
                upgradesButton.color = buttonLight;
                settingsButton.color = buttonDark;
                cropsContent.gameObject.SetActive(false);
                upgradesContent.gameObject.SetActive(false);
                settingsContent.gameObject.SetActive(true);
                leftButton.gameObject.SetActive(false);
                rightButton.gameObject.SetActive(false);
                break;
        }
    }

    public enum Tab {
        Crops,
        Upgrades,
        Settings,
    }
}
