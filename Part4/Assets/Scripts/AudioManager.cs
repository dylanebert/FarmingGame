using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance { get; private set; }

    Dictionary<string, List<AudioSource>> audioSources = new Dictionary<string, List<AudioSource>>();
    Dictionary<string, float> cooldowns = new Dictionary<string, float>();

    void Awake() {
        instance = this;
        cooldowns = new Dictionary<string, float>();
        foreach(Transform child in transform) {
            audioSources.Add(child.name, child.GetComponentsInChildren<AudioSource>().ToList());
        }
    }

    void Update() {
        foreach(string key in cooldowns.Keys.ToList()) {
            cooldowns[key] = Mathf.Max(cooldowns[key] - Time.unscaledDeltaTime, 0);
        }
    }

    public static void PlaySound(string name) {
        instance.PlaySoundInternal(name);
    }

    void PlaySoundInternal(string name) {
        if(!audioSources.TryGetValue(name, out List<AudioSource> sources)) {
            Debug.LogError("AudioManager: No audio source with name " + name);
            return;
        }
        if(!cooldowns.TryGetValue(name, out float cooldown))
            cooldowns.Add(name, 0);
        if(cooldown == 0) {
            sources[Random.Range(0, sources.Count)].Play();
            cooldowns[name] = 0.1f;
        }
    }
}
