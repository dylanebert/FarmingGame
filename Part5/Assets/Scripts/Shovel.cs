using UnityEngine;

public class Shovel : UpgradeBase {
    public override string name => "Shovel";
    public override string description => "Easily remove crops and recover seeds with the sturdy and versatile Shovel, an essential tool for farming.";
    public override int seedsCost => 0;
    public override int coinsCost => 50;
    public override int menuIndex => 2;

    public override Sprite GetSprite() {
        return Resources.Load<Sprite>("Sprites/Shovel");
    }
}
