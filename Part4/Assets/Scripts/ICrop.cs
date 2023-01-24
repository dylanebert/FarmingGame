using UnityEngine;

public interface ICrop : IPurchasable {
    int coinYield { get; }
    int seedYield { get; }
    float growthTime { get; }
    Color color { get; }
}