using UnityEngine;

public class Sprinkler : UpgradeBase {
    public override string name => "Sprinkler";
    public override string description => "Save time and water wisely with the innovative Sprinkler, perfect for watering multiple crops at once.";
    public override int seedsCost => 0;
    public override int coinsCost => 500;
    public override int menuIndex => 1;

    protected override string[] GetPrerequisites() {
        return new string[] { "Watering Can" };
    }

    public override Sprite GetSprite() {
        return Resources.Load<Sprite>("Sprites/Sprinkler");
    }
}