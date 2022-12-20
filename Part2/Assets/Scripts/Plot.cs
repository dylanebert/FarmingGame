using TMPro;
using UnityEngine;

public class Plot : MonoBehaviour {
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
                WateredChanged();
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
                HarvestableChanged();
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
    public TextMeshPro text;

    void Awake() {
        growth = 0;
        harvestCount = 0;
        harvestable = false;
        watered = false;
    }

    void Update() {
        if(currentCrop == null || !watered) return;
        growth = Mathf.Clamp(growth + Time.deltaTime / currentCrop.growthTime, 0, 1);
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
        WateredChanged();
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
    }

    void HarvestableChanged() {
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
            harvestIcon.gameObject.SetActive(true);
        } else {
            harvestIcon.gameObject.SetActive(false);
        }
    }

    void WateredChanged() {
        waterIcon.SetActive(currentCrop != null && !watered);
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
        WateredChanged();
    }
}
