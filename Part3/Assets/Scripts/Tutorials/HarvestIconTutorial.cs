public class HarvestIconTutorial : Tutorial {
    void OnEnable() {
        PointerManager.HarvestIconClicked += Complete;
    }

    void OnDisable() {
        PointerManager.HarvestIconClicked -= Complete;
    }
}