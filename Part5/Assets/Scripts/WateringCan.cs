using UnityEngine;

public class WateringCan : UpgradeBase {
    public override string name => "Watering Can";
    public override string description => "Take the hassle out of watering with the efficient and convenient Watering Can, essential for a thriving farm.";
    public override int seedsCost => 0;
    public override int coinsCost => 10;
    public override int menuIndex => 1;

    public override Sprite GetSprite() {
        return Resources.Load<Sprite>("Sprites/WateringCan");
    }
}
