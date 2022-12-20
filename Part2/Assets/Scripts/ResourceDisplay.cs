using UnityEngine;
using TMPro;

public class ResourceDisplay : MonoBehaviour {
    [SerializeField] TextMeshProUGUI seedsText;
    [SerializeField] TextMeshProUGUI coinsText;

    void Awake() {
        ShopManager.SeedsChanged += () => {
            seedsText.text = $"Seeds: {ShopManager.instance.seeds}";
        };
        ShopManager.CoinsChanged += () => {
            coinsText.text = $"Coins: {ShopManager.instance.coins}";
        };
    }
}
