using UnityEngine;

public class HarvestWheatTutorial : Tutorial {
    [SerializeField] UIButton harvestButton;

    void OnEnable() {
        harvestButton.Clicked += Complete;
    }

    void OnDisable() {
        harvestButton.Clicked -= Complete;
    }

    void Complete(UIButton button) {
        Complete();
    }
}
