public class PlaceWheatTutorial : Tutorial {
    void OnEnable() {
        CropManager.CropPlaced += Complete;
    }

    void OnDisable() {
        CropManager.CropPlaced -= Complete;
    }
}
