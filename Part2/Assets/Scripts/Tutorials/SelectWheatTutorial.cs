public class SelectWheatTutorial : Tutorial {
    void OnEnable() {
        CropManager.PlotSelected += Complete;
    }

    void OnDisable() {
        CropManager.PlotSelected -= Complete;
    }
}
