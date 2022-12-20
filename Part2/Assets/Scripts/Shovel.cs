public class Shovel : UpgradeBase {
    public override string name => "Shovel";
    public override string description => "Remove crops and recover seeds";
    public override int seedsCost => 0;
    public override int coinsCost => 50;
    public override int menuIndex => 2;

    public override void Apply() {
        CropInfoDisplay.instance.ToggleRemoveButton(true);
    }
}
