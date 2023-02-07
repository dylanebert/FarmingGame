using UnityEngine;

public abstract class UpgradeBase : IUpgrade {
    public abstract string name { get; }
    public abstract string description { get; }
    public abstract int seedsCost { get; }
    public abstract int coinsCost { get; }
    public abstract int menuIndex { get; }
    public string[] prerequisites => m_prerequisites;

    string[] m_prerequisites;

    public UpgradeBase() {
        m_prerequisites = GetPrerequisites();
    }

    protected virtual string[] GetPrerequisites() {
        return new string[0];
    }

    public virtual void Apply() { }

    public abstract Sprite GetSprite();
}
