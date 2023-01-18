using UnityEngine;
using UnityEngine.Events;

public abstract class Tutorial : MonoBehaviour {
    public event UnityAction Completed;

    protected virtual void Complete() {
        Completed?.Invoke();
    }
}
