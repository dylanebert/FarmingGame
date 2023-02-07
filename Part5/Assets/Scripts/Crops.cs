using UnityEngine;

public class Wheat : CropBase {
    public override string name => "Wheat";
    public override string description => "The staple of any successful farm, plant Wheat for a steady and reliable harvest.";
    public override float growthTime => 15;
    public override int coinYield => 1;
    public override int seedsCost => 1;
    public override int seedYield => 2;
    public override int coinsCost => 0;
    public override int menuIndex => 0;
    public override Color color => Palette.instance.wheat;

    public override Sprite GetSprite() {
        return Resources.Load<Sprite>("Sprites/Wheat");
    }
}

public class Corn : CropBase {
    public override string name => "Corn";
    public override string description => "Bring a touch of the countryside to your farm with the sweet and sturdy Corn.";
    public override float growthTime => 35;
    public override int coinYield => 5;
    public override int seedsCost => 2;
    public override int seedYield => 2;
    public override int coinsCost => 1;
    public override int menuIndex => 1;
    public override Color color => Palette.instance.corn;

    public override Sprite GetSprite() {
        return Resources.Load<Sprite>("Sprites/Corn");
    }
}

public class Potato : CropBase {
    public override string name => "Potato";
    public override string description => "Add a versatile crop to your farm with the humble and hardy Potato.";
    public override float growthTime => 70;
    public override int coinYield => 10;
    public override int seedsCost => 2;
    public override int seedYield => 2;
    public override int coinsCost => 1;
    public override int menuIndex => 1;
    public override Color color => Palette.instance.potato;

    public override Sprite GetSprite() {
        return Resources.Load<Sprite>("Sprites/Potato");
    }
}

public class Tomato : CropBase {
    public override string name => "Tomato";
    public override string description => "Brighten up your farm with the juicy and flavorful Tomato, perfect for salads or sauces.";
    public override float growthTime => 50;
    public override int coinYield => 10;
    public override int seedsCost => 3;
    public override int seedYield => 4;
    public override int coinsCost => 25;
    public override int menuIndex => 2;
    public override Color color => Palette.instance.tomato;

    public override Sprite GetSprite() {
        return Resources.Load<Sprite>("Sprites/Tomato");
    }
}

public class Strawberry : CropBase {
    public override string name => "Strawberry";
    public override string description => "Sweeten up your farm with the luscious and beloved Strawberry, a summer favorite.";
    public override float growthTime => 25;
    public override int coinYield => 5;
    public override int seedsCost => 3;
    public override int seedYield => 4;
    public override int coinsCost => 25;
    public override int menuIndex => 3;
    public override Color color => Palette.instance.strawberry;

    public override Sprite GetSprite() {
        return Resources.Load<Sprite>("Sprites/Strawberry");
    }
}

public class Pumpkin : CropBase {
    public override string name => "Pumpkin";
    public override string description => "Get into the autumn spirit with the big and versatile Pumpkin, perfect for pies and decor.";
    public override float growthTime => 120;
    public override int coinYield => 25;
    public override int seedsCost => 4;
    public override int seedYield => 4;
    public override int coinsCost => 50;
    public override int menuIndex => 4;
    public override Color color => Palette.instance.pumpkin;

    public override Sprite GetSprite() {
        return Resources.Load<Sprite>("Sprites/Pumpkin");
    }
}

public class Peppers : CropBase {
    public override string name => "Peppers";
    public override string description => "Add some heat to your farm with these spicy green Peppers, a staple in many cuisines.";
    public override float growthTime => 50;
    public override int coinYield => 20;
    public override int seedsCost => 5;
    public override int seedYield => 4;
    public override int coinsCost => 100;
    public override int menuIndex => 5;
    public override Color color => Palette.instance.peppers;

    public override Sprite GetSprite() {
        return Resources.Load<Sprite>("Sprites/Peppers");
    }
}

public class Blueberries : CropBase {
    public override string name => "Blueberries";
    public override string description => "Treat yourself to the tangy and nutritious Blueberries, a favorite for snacking and baking.";
    public override float growthTime => 25;
    public override int coinYield => 10;
    public override int seedsCost => 5;
    public override int seedYield => 4;
    public override int coinsCost => 100;
    public override int menuIndex => 5;
    public override Color color => Palette.instance.blueberries;

    public override Sprite GetSprite() {
        return Resources.Load<Sprite>("Sprites/Blueberries");
    }
}

public class Rhubarb : CropBase {
    public override string name => "Rhubarb";
    public override string description => "Add a touch of the unexpected to your farm with the tart and unique Rhubarb, perfect for pies and preserves.";
    public override float growthTime => 75;
    public override int coinYield => 30;
    public override int seedsCost => 5;
    public override int seedYield => 4;
    public override int coinsCost => 100;
    public override int menuIndex => 5;
    public override Color color => Palette.instance.rhubarb;

    public override Sprite GetSprite() {
        return Resources.Load<Sprite>("Sprites/Rhubarb");
    }
}