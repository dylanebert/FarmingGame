using UnityEngine;

public class WateringCan : UpgradeBase {
    public override string name => "Watering Can";
    public override string description => "Water crops more conveniently";
    public override int seedsCost => 0;
    public override int coinsCost => 10;
    public override int menuIndex => 1;

    public override Sprite GetSprite() {
        return null;
    }
}
