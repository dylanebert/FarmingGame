public class Scythe : UpgradeBase {
    public override string name => "Scythe";
    public override string description => "Harvest crops even more conveniently";
    public override int seedsCost => 0;
    public override int coinsCost => 250;
    public override int menuIndex => 0;

    protected override string[] GetPrerequisites() {
        return new string[] { "Sickle" };
    }
}