public class Carrots : CropBase {
    public override string name => "Carrots";
    public override string description => "Carrots";
    public override float growthTime => 30;
    public override int seedYield => 2;
    public override int coinYield => 2;
    public override int seedsCost => 1;
    public override int coinsCost => 1;
    public override int menuIndex => 1;
}
