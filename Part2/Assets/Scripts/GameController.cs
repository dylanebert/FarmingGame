using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour {
    public static GameController instance { get; private set; }

    public static int phase { get; private set; }

    void Awake() {
        instance = this;
    }

    void Update() {
        if(Keyboard.current.f1Key.wasPressedThisFrame) {
            Time.timeScale = 1;
        }
        if(Keyboard.current.f2Key.wasPressedThisFrame) {
            Time.timeScale = 5;
        }
    }
}
