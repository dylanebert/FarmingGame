using UnityEngine.Events;

public interface IUpgrade : IPurchasable {
    string[] prerequisites { get; }
    void Apply();
}