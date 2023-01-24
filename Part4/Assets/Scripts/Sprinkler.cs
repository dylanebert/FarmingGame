using UnityEngine;

public class Sprinkler : UpgradeBase {
    public override string name => "Sprinkler";
    public override string description => "Water crops even more conveniently";
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