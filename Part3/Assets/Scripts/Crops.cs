using UnityEngine;

public class Wheat : CropBase {
    public override string name => "Wheat";
    public override string description => "Wheat";
    public override float growthTime => 15;
    public override int coinYield => 1;
    public override int seedsCost => 1;
    public override int seedYield => 2;
    public override int coinsCost => 0;
    public override int menuIndex => 0;
    public override Color color => Palette.instance.wheat;
}

public class Corn : CropBase {
    public override string name => "Corn";
    public override string description => "Corn";
    public override float growthTime => 35;
    public override int coinYield => 5;
    public override int seedsCost => 2;
    public override int seedYield => 2;
    public override int coinsCost => 1;
    public override int menuIndex => 1;
    public override Color color => Palette.instance.corn;
}

public class Potato : CropBase {
    public override string name => "Potato";
    public override string description => "Potato";
    public override float growthTime => 70;
    public override int coinYield => 10;
    public override int seedsCost => 2;
    public override int seedYield => 2;
    public override int coinsCost => 1;
    public override int menuIndex => 1;
    public override Color color => Palette.instance.potato;
}

public class Tomato : CropBase {
    public override string name => "Tomato";
    public override string description => "Tomato";
    public override float growthTime => 50;
    public override int coinYield => 10;
    public override int seedsCost => 3;
    public override int seedYield => 4;
    public override int coinsCost => 25;
    public override int menuIndex => 2;
    public override Color color => Palette.instance.tomato;
}

public class Strawberry : CropBase {
    public override string name => "Strawberry";
    public override string description => "Strawberry";
    public override float growthTime => 25;
    public override int coinYield => 5;
    public override int seedsCost => 3;
    public override int seedYield => 4;
    public override int coinsCost => 25;
    public override int menuIndex => 3;
    public override Color color => Palette.instance.strawberry;
}

public class Pumpkin : CropBase {
    public override string name => "Pumpkin";
    public override string description => "Pumpkin";
    public override float growthTime => 120;
    public override int coinYield => 25;
    public override int seedsCost => 4;
    public override int seedYield => 4;
    public override int coinsCost => 50;
    public override int menuIndex => 4;
    public override Color color => Palette.instance.pumpkin;
}

public class Peppers : CropBase {
    public override string name => "Peppers";
    public override string description => "Peppers";
    public override float growthTime => 50;
    public override int coinYield => 20;
    public override int seedsCost => 5;
    public override int seedYield => 4;
    public override int coinsCost => 100;
    public override int menuIndex => 5;
    public override Color color => Palette.instance.peppers;
}

public class Blueberries : CropBase {
    public override string name => "Blueberries";
    public override string description => "Blueberries";
    public override float growthTime => 25;
    public override int coinYield => 10;
    public override int seedsCost => 5;
    public override int seedYield => 4;
    public override int coinsCost => 100;
    public override int menuIndex => 5;
    public override Color color => Palette.instance.blueberries;
}

public class Rhubarb : CropBase {
    public override string name => "Rhubarb";
    public override string description => "Rhubarb";
    public override float growthTime => 75;
    public override int coinYield => 30;
    public override int seedsCost => 5;
    public override int seedYield => 4;
    public override int coinsCost => 100;
    public override int menuIndex => 5;
    public override Color color => Palette.instance.rhubarb;
}