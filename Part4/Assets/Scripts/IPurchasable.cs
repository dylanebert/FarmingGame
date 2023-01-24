using UnityEngine;

public interface IPurchasable {
    string name { get; }
    string description { get; }
    int seedsCost { get; }
    int coinsCost { get; }
    int menuIndex { get; }
    Sprite GetSprite();
}
