public class BuySickleTutorial : Tutorial {
    void OnEnable() {
        UpgradeManager.Changed += OnUpgradeChanged;
    }

    void OnDisable() {
        UpgradeManager.Changed -= OnUpgradeChanged;
    }

    void OnUpgradeChanged() {
        if(UpgradeManager.GetUnlocked("Sickle"))
            Complete();
    }
}
