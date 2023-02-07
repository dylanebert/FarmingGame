using UnityEngine;

public class Sickle : UpgradeBase {
    public override string name => "Sickle";
    public override string description => "Streamline your harvesting with the sharp and reliable Sickle, a must-have tool for any farmer.";
    public override int seedsCost => 0;
    public override int coinsCost => 5;
    public override int menuIndex => 0;

    public override Sprite GetSprite() {
        return Resources.Load<Sprite>("Sprites/Sickle");
    }
}
