using UnityEngine;

[CreateAssetMenu(menuName = "Farming Game/Parameters")]
public class Parameters : SingletonScriptableObject<Parameters> {
    public int startingSeeds = 1;
    public int startingCoins = 0;
}
