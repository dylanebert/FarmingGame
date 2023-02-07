public class WaitGrowTutorial : Tutorial {
    void OnEnable() {
        CropManager.HarvestableChanged += Complete;
        for(int i = 0; i < CropManager.instance.plots.Length; i++) {
            Plot plot = CropManager.instance.plots[i];
            if(plot.active && plot.currentCrop != null && plot.harvestable) {
                Complete();
                return;
            }
        }
    }

    void OnDisable() {
        CropManager.HarvestableChanged -= Complete;
    }
}
