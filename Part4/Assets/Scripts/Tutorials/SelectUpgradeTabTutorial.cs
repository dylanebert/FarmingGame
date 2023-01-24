using UnityEngine;

public class SelectUpgradeTabTutorial : Tutorial {
    [SerializeField] UIButton upgradeTabButton;

    void OnEnable() {
        upgradeTabButton.Clicked += Complete;
        if(UIShop.instance.currentTab == UIShop.Tab.Upgrades) {
            Complete();
        }
    }

    void OnDisable() {
        upgradeTabButton.Clicked -= Complete;
    }

    void Complete(UIButton button) {
        Complete();
    }
}
