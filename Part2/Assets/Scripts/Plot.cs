using UnityEngine;

public class Plot : MonoBehaviour {
    public ICrop currentCrop { get; private set; }
    public float growth { get; private set; }
    public bool active { get; private set; }

    public GameObject dirtMesh;
    public GameObject highlight;

    int harvestCount;

    void Awake() {
        growth = 0;
        harvestCount = 0;
    }

    void Update() {
        if(currentCrop == null) return;
        growth = Mathf.Clamp(growth + Time.deltaTime / currentCrop.growthTime, 0, 1);
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
    }

    public void Remove() {
        currentCrop = null;
        dirtMesh.SetActive(false);
        growth = 0;
        harvestCount = 0;
    }
}
