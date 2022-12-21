public class SaveUpTutorial : Tutorial {
    void OnEnable() {
        ShopManager.CoinsChanged += OnCoinsChanged;
    }

    void OnDisable() {
        ShopManager.CoinsChanged -= OnCoinsChanged;
    }

    void OnCoinsChanged() {
        if(ShopManager.instance.coins >= 5)
            Complete();
    }
}
