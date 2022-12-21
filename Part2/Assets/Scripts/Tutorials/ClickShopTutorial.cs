using UnityEngine;

public class ClickShopTutorial : Tutorial {
    [SerializeField] UIButton openShopButton;

    void OnEnable() {
        openShopButton.Clicked += Complete;
    }

    void OnDisable() {
        openShopButton.Clicked -= Complete;
    }

    void Complete(UIButton button) {
        Complete();
    }
}
