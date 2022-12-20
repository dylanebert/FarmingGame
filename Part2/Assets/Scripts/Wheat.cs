public class Wheat : CropBase {
    public override string name => "Wheat";
    public override string description => "Wheat";
    public override float growthTime => 15;
    public override int coinYield => 1;
    public override int seedsCost => 1;
    public override int coinsCost => 0;
    public override int menuIndex => 0;
}
