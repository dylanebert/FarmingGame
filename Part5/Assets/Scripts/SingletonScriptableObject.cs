using UnityEngine;

public class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T> {
    static T _instance;
    public static T instance {
        get {
            if(_instance == null) {
                _instance = Resources.Load<T>(typeof(T).Name);
            }
            return _instance;
        }
    }
}
