using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CropInfoDisplay : MonoBehaviour {
    public static CropInfoDisplay instance { get; private set; }

    [SerializeField] GameObject root;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] GameObject statusGrowing;
    [SerializeField] UIButton harvestButton;
    [SerializeField] Image harvestGlow;
    [SerializeField] Image growthBar;
    [SerializeField] UIButton removeButton;

    bool m_harvestable;
    bool harvestable {
        get {
            return m_harvestable;
        }
        set {
            if(m_harvestable != value) {
                m_harvestable = value;
                HarvestableChanged();
            }
        }
    }

    Plot boundPlot;
    float t;

    void Awake() {
        instance = this;
        root.SetActive(false);
    }

    void Update() {
        if(boundPlot == null) return;
        growthBar.fillAmount = boundPlot.growth;
        harvestable = boundPlot.growth >= 1;
        if(harvestable) {
            t += Time.unscaledDeltaTime;
            if(t > 1) t -= 1;
            harvestGlow.color = new Color(1, 1, 1, Mathf.Sin(t * Mathf.PI * 2) * 0.05f + 0.05f);
        }
    }

    void HarvestableChanged() {
        if(harvestable) {
            statusGrowing.SetActive(false);
            harvestButton.gameObject.SetActive(true);
        } else {
            statusGrowing.SetActive(true);
            harvestButton.gameObject.SetActive(false);
        }
    }

    public void Show(Plot plot) {
        boundPlot = plot;
        title.text = plot.currentCrop.name;
        growthBar.fillAmount = boundPlot.growth;
        HarvestableChanged();
        root.SetActive(true);
    }

    public void Sell() {
        if(boundPlot == null) return;
        ShopManager.Salvage(boundPlot.currentCrop);
        CropManager.RemoveCrop(boundPlot);
        CropManager.DeselectPlot();
        AudioManager.PlaySound("Seed");
    }

    public void Harvest() {
        if(boundPlot == null) return;
        CropManager.HarvestCrop(boundPlot);
    }

    public void ToggleRemoveButton(bool active) {
        removeButton.gameObject.SetActive(active);
    }

    public void Hide() {
        root.SetActive(false);
        statusGrowing.SetActive(false);
        boundPlot = null;
    }
}
