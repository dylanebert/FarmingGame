using UnityEngine;

[CreateAssetMenu(menuName = "Farming Game/Palette")]
public class Palette : SingletonScriptableObject<Palette> {
    public Color seeds;
    public Color coins;
    public Color water;

    public Color wheat;
    public Color corn;
    public Color potato;
    public Color tomato;
    public Color strawberry;
    public Color pumpkin;
    public Color peppers;
    public Color blueberries;
    public Color rhubarb;
}
