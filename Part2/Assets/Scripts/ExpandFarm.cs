public abstract class Expand : UpgradeBase {
    public override string description => "Expand your farm";
    public override int seedsCost => 0;
    public override int menuIndex => 3;

    public override void Apply() {
        CropManager.Expand();
    }
}

public class ExpandFarmA : Expand {
    public override string name => "Expand Farm I";
    public override int coinsCost => 10;
}

public class ExpandFarmB : Expand {
    public override string name => "Expand Farm II";
    public override int coinsCost => 100;

    protected override string[] GetPrerequisites() {
        return new string[] { "Expand Farm I" };
    }
}

public class ExpandFarmC : Expand {
    public override string name => "Expand Farm III";
    public override int coinsCost => 250;

    protected override string[] GetPrerequisites() {
        return new string[] { "Expand Farm II" };
    }
}

public class ExpandFarmD : Expand {
    public override string name => "Expand Farm IV";
    public override int coinsCost => 1000;

    protected override string[] GetPrerequisites() {
        return new string[] { "Expand Farm III" };
    }
}