public class BuyWheatTutorial : Tutorial {
    void OnEnable() {
        ShopManager.SeedsChanged += Complete;
    }

    void OnDisable() {
        ShopManager.SeedsChanged -= Complete;
    }
}