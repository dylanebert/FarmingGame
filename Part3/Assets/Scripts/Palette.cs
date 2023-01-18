using UnityEngine;

[CreateAssetMenu(menuName = "Farming Game/Palette")]
public class Palette : SingletonScriptableObject<Palette> {
    public Color seeds;
    public Color coins;
    public Color water;
}
