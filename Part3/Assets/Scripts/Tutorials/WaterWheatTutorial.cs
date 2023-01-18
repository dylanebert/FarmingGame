using UnityEngine;

public class WaterWheatTutorial : Tutorial {
    [SerializeField] UIButton waterButton;

    void OnEnable() {
        waterButton.Clicked += Complete;
    }

    void OnDisable() {
        waterButton.Clicked -= Complete;
    }

    void Complete(UIButton button) {
        Complete();
    }
}
