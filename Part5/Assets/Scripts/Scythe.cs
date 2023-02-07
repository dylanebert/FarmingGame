using UnityEngine;

public class Scythe : UpgradeBase {
    public override string name => "Scythe";
    public override string description => "Maximize your harvests with the efficient and powerful Scythe, great for harvesting all your crops at once.";
    public override int seedsCost => 0;
    public override int coinsCost => 250;
    public override int menuIndex => 0;

    protected override string[] GetPrerequisites() {
        return new string[] { "Sickle" };
    }

    public override Sprite GetSprite() {
        return Resources.Load<Sprite>("Sprites/Scythe");
    }
}