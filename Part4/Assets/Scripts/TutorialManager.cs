using UnityEngine;

public class TutorialManager : MonoBehaviour {
    public static TutorialManager instance { get; private set; }

    [SerializeField] Tutorial[] tutorials;

    public int currentTutorial { get; private set; }

    void Awake() {
        instance = this;
        currentTutorial = PlayerPrefs.GetInt("CurrentTutorial", 0);
    }

    void Start() {
        ShowCurrentTutorial();
    }

    void ShowCurrentTutorial() {
        if(currentTutorial < tutorials.Length) {
            tutorials[currentTutorial].gameObject.SetActive(true);
            tutorials[currentTutorial].Completed += OnTutorialCompleted;
        }
    }

    void OnTutorialCompleted() {
        tutorials[currentTutorial].Completed -= OnTutorialCompleted;
        tutorials[currentTutorial].gameObject.SetActive(false);
        currentTutorial++;
        ShowCurrentTutorial();
    }
}
