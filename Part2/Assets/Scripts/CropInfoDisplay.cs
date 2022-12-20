using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CropInfoDisplay : MonoBehaviour {
    public static CropInfoDisplay instance { get; private set; }

    [SerializeField] GameObject root;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] UIButton harvestButton;
    [SerializeField] Image harvestBG;
    [SerializeField] TextMeshProUGUI harvestText;
    [SerializeField] UIButton waterButton;
    [SerializeField] Image harvestGlow;
    [SerializeField] Image waterGlow;
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

    bool m_watered;
    bool watered {
        get {
            return m_watered;
        }
        set {
            if(m_watered != value) {
                m_watered = value;
                WateredChanged();
            }
        }
    }

    Plot boundPlot;
    float t;

    void Awake() {
        instance = this;
        m_watered = true;
        m_harvestable = false;
        root.SetActive(false);
    }

    void Update() {
        if(boundPlot == null) return;
        UpdateStatus();
    }

    void UpdateStatus() {
        growthBar.fillAmount = boundPlot.growth;
        watered = boundPlot.watered;
        harvestable = boundPlot.harvestable;
        if(harvestable) {
            t += Time.unscaledDeltaTime;
            if(t > 1) t -= 1;
            harvestGlow.color = new Color(1, 1, 1, Mathf.Sin(t * Mathf.PI * 2) * 0.05f + 0.05f);
        } else if(!watered) {
            t += Time.unscaledDeltaTime;
            if(t > 1) t -= 1;
            waterGlow.color = new Color(1, 1, 1, Mathf.Sin(t * Mathf.PI * 2) * 0.05f + 0.05f);
        }
    }

    void HarvestableChanged() {
        if(harvestable) {
            if(boundPlot.harvestCount == 0) {
                int count = boundPlot.currentCrop.seedYield;
                harvestBG.color = Palette.instance.seeds;
                harvestText.text = $"{count} seeds ready";
            } else {
                int count = boundPlot.currentCrop.coinYield;
                harvestBG.color = Palette.instance.coins;
                harvestText.text = $"{count} coins ready";
            }
            harvestButton.gameObject.SetActive(true);
        } else {
            harvestButton.gameObject.SetActive(false);
        }
    }

    void WateredChanged() {
        if(watered) {
            waterButton.gameObject.SetActive(false);
        } else {
            waterButton.gameObject.SetActive(true);
        }
    }

    public void Show(Plot plot) {
        boundPlot = plot;
        title.text = plot.currentCrop.name;
        UpdateStatus();
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
        boundPlot.Harvest();
    }

    public void Water() {
        if(boundPlot == null) return;
        boundPlot.Water();
    }

    public void ToggleRemoveButton(bool active) {
        removeButton.gameObject.SetActive(active);
    }

    public void Hide() {
        root.SetActive(false);
        boundPlot = null;
        watered = false;
        harvestable = false;
    }
}
