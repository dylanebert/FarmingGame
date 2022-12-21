using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SerializationManager : MonoBehaviour {
    public static SerializationManager instance { get; private set; }

    int counter = 0;
    float timer;

    void Awake() {
        instance = this;
        counter = 0;
    }

    void Update() {
        timer += Time.unscaledDeltaTime;
        if(timer > 10f) {
            timer -= 10f;
            Serialize();
        }
    }

    public void ResetData() {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

    public static void Serialize() {
        foreach(string upgrade in UpgradeManager.upgrades.Keys) {
            PlayerPrefs.SetInt(upgrade, UpgradeManager.GetUnlocked(upgrade) ? 1 : 0);
        }
        for(int i = 0; i < CropManager.instance.plots.Length; i++) {
            Plot plot = CropManager.instance.plots[i];
            PlayerPrefs.SetInt("Plot" + i + "HarvestCount", plot.harvestCount);
            PlayerPrefs.SetInt("Plot" + i + "Active", plot.active ? 1 : 0);
            PlayerPrefs.SetString("Plot" + i + "Crop", plot.currentCrop == null ? "" : plot.currentCrop.name);
            PlayerPrefs.SetInt("Plot" + i + "Watered", plot.watered ? 1 : 0);
            PlayerPrefs.SetFloat("Plot" + i + "Growth", plot.growth);
        }
        PlayerPrefs.SetInt("Coins", ShopManager.instance.coins);
        PlayerPrefs.SetInt("Seeds", ShopManager.instance.seeds);
        PlayerPrefs.SetInt("CurrentTutorial", TutorialManager.instance.currentTutorial);
        Debug.Log("Saved " + instance.counter);
        instance.counter++;
    }
    
}
