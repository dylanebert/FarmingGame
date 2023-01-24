using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Plot : MonoBehaviour {
    public event UnityAction WateredChanged;
    public event UnityAction HarvestableChanged;

    public ICrop currentCrop { get; private set; }

    public float growth { get; private set; }
    public bool active { get; private set; }

    bool _watered;
    public bool watered {
        get {
            return _watered;
        }
        private set {
            if(_watered != value) {
                _watered = value;
                OnWateredChanged();
            }
        }
    }

    bool _harvestable;
    public bool harvestable {
        get {
            return _harvestable;
        }
        private set {
            if(_harvestable != value) {
                _harvestable = value;
                OnHarvestableChanged();
            }
        }
    }

    public int harvestCount { get; private set; }

    public GameObject dirtMesh;
    public GameObject highlight;
    public GameObject waterIcon;
    public SpriteRenderer harvestIcon;
    public GameObject seedIcon;
    public GameObject coinIcon;
    public Transform cropsPivot;
    public MeshRenderer[] cropMeshes;
    public TextMeshPro text;

    public void Initialize(int index) {
        active = index < 3 ? true : PlayerPrefs.GetInt("Plot" + index + "Active", 0) == 1;
        string cropName = PlayerPrefs.GetString("Plot" + index + "Crop", "");
        if(cropName != "" && CropManager.crops.Contains(cropName)) {
            currentCrop = CropManager.crops[cropName] as ICrop;
            dirtMesh.SetActive(true);
        } else {
            currentCrop = null;
            dirtMesh.SetActive(false);
        }
        harvestCount = PlayerPrefs.GetInt("Plot" + index + "HarvestCount", 0);
        watered = PlayerPrefs.GetInt("Plot" + index + "Watered", 0) == 1;
        growth = PlayerPrefs.GetFloat("Plot" + index + "Growth", 0);
        for(int i = 0; i < cropMeshes.Length; i++) {
            cropMeshes[i].material.color = currentCrop == null ? Color.white : currentCrop.color;
            cropMeshes[i].enabled = currentCrop != null;
        }
        OnWateredChanged();
    }

    void Update() {
        if(currentCrop == null || !watered) return;
        growth = Mathf.Clamp(growth + Time.unscaledDeltaTime / currentCrop.growthTime, 0, 1);
        cropsPivot.transform.localScale = new Vector3(1, growth, 1);
        harvestable = growth == 1;
    }

    public void SetActive(bool active) {
        this.active = active;
    }

    public bool ClampToPlot(Vector3 position) {
        if(Mathf.Abs(position.x - transform.position.x) < 3f && Mathf.Abs(position.z - transform.position.z) < 3f) {
            return true;
        }
        return false;
    }

    public void Place(ICrop crop) {
        currentCrop = crop;
        dirtMesh.SetActive(true);
        growth = 0;
        harvestCount = 0;
        watered = false;
        harvestable = false;
        for(int i = 0; i < cropMeshes.Length; i++) {
            cropMeshes[i].material.color = currentCrop == null ? Color.white : currentCrop.color;
            cropMeshes[i].enabled = currentCrop != null;
        }
        cropsPivot.transform.localScale = new Vector3(1, growth, 1);
        OnWateredChanged();
    }

    public void Harvest() {
        Debug.Assert(growth == 1);
        if(currentCrop == null || growth < 1) return;
        if(harvestCount == 0) {
            ShopManager.AddSeeds(currentCrop.seedYield);
            AudioManager.PlaySound("Seed");
        } else {
            ShopManager.AddCoins(currentCrop.coinYield);
            AudioManager.PlaySound("Coin");
        }
        harvestCount++;
        growth = 0;
        watered = false;
        harvestable = false;
        cropsPivot.transform.localScale = new Vector3(1, growth, 1);
    }

    void OnHarvestableChanged() {
        if(harvestable) {
            if(harvestCount == 0) {
                harvestIcon.color = Palette.instance.seeds;
                text.text = currentCrop.seedYield.ToString();
                text.gameObject.SetActive(currentCrop.seedYield > 1);
                seedIcon.SetActive(true);
                coinIcon.SetActive(false);
            } else {
                harvestIcon.color = Palette.instance.coins;
                text.text = currentCrop.coinYield.ToString();
                text.gameObject.SetActive(currentCrop.coinYield > 1);
                seedIcon.SetActive(false);
                coinIcon.SetActive(true);
            }
            AudioManager.PlaySound("Vegetation");
            harvestIcon.gameObject.SetActive(true);
        } else {
            harvestIcon.gameObject.SetActive(false);
        }
        HarvestableChanged?.Invoke();
    }

    void OnWateredChanged() {
        waterIcon.SetActive(currentCrop != null && !watered);
        WateredChanged?.Invoke();
    }

    public void Water() {
        watered = true;
        AudioManager.PlaySound("Water");
    }

    public void Remove() {
        currentCrop = null;
        dirtMesh.SetActive(false);
        growth = 0;
        harvestCount = 0;
        watered = false;
        harvestable = false;
        for(int i = 0; i < cropMeshes.Length; i++)
            cropMeshes[i].enabled = false;
        OnWateredChanged();
    }
}
