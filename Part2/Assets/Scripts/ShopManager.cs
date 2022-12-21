using UnityEngine;
using UnityEngine.Events;

public class ShopManager : MonoBehaviour {
    public static ShopManager instance { get; private set; }
    public static event UnityAction SeedsChanged;
    public static event UnityAction CoinsChanged;

    int m_seeds;
    public int seeds {
        get {
            return m_seeds;
        }
        set {
            if(m_seeds != value) {
                m_seeds = value;
                SeedsChanged?.Invoke();
            }
        }
    }

    int m_coins;
    public int coins {
        get {
            return m_coins;
        }
        set {
            if(m_coins != value) {
                m_coins = value;
                CoinsChanged?.Invoke();
            }
        }
    }

    void Awake() {
        instance = this;
        seeds = PlayerPrefs.GetInt("Seeds", Parameters.instance.startingSeeds);
        coins = PlayerPrefs.GetInt("Coins", Parameters.instance.startingCoins);
    }

    public static void TryBuyCrop(ICrop crop) {
        instance.TryBuyCropInternal(crop);
    }

    void TryBuyCropInternal(ICrop crop) {
        if(CanAfford(crop)) {
            seeds -= crop.seedsCost;
            coins -= crop.coinsCost;
            CropManager.BeginPlacingCrop(crop);
        }
    }

    public static void TryBuyUpgrade(IUpgrade upgrade) {
        instance.TryBuyUpgradeInternal(upgrade);
    }

    void TryBuyUpgradeInternal(IUpgrade upgrade) {
        if(CanAfford(upgrade)) {
            seeds -= upgrade.seedsCost;
            coins -= upgrade.coinsCost;
            UpgradeManager.UnlockUpgrade(upgrade.name);
        }
    }

    public static void Salvage(ICrop crop) {
        instance.SalvageInternal(crop);
    }

    void SalvageInternal(ICrop crop) {
        seeds += crop.seedsCost;
    }

    public static void Refund(IPurchasable purchasable) {
        instance.RefundInternal(purchasable);
    }

    void RefundInternal(IPurchasable purchasable) {
        seeds += purchasable.seedsCost;
        coins += purchasable.coinsCost;
    }

    public static bool CanAfford(IPurchasable purchasable) {
        return instance.CanAffordInternal(purchasable);
    }

    bool CanAffordInternal(IPurchasable purchasable) {
        return seeds >= purchasable.seedsCost && coins >= purchasable.coinsCost;
    }

    public static void AddSeeds(int seeds) {
        instance.seeds += seeds;
    }

    public static void AddCoins(int coins) {
        instance.coins += coins;
    }
}
