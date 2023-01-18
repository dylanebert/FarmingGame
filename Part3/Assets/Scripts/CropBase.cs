using UnityEngine;

public abstract class CropBase : ICrop {
    public abstract string name { get; }
    public abstract string description { get; }
    public abstract float growthTime { get; }
    public abstract int coinYield { get; }
    public abstract int seedsCost { get; }
    public abstract int coinsCost { get; }
    public abstract int menuIndex { get; }
    public abstract int seedYield { get; }
    public abstract Color color { get; }
}