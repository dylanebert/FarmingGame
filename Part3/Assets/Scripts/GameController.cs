using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController instance { get; private set; }

    public static int phase { get; private set; }

    void Awake() {
        instance = this;
        Application.targetFrameRate = 60;
    }
}
