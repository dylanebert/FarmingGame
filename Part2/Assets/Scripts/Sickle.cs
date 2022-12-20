public class Sickle : UpgradeBase {
    public override string name => "Sickle";
    public override string description => "Harvest crops more conveniently";
    public override int seedsCost => 0;
    public override int coinsCost => 5;
    public override int menuIndex => 0;

    public override void Apply() {
        throw new System.NotImplementedException();
    }
}
