using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeManager : MonoBehaviour {
    public static UpgradeManager instance { get; private set; }
    public static event UnityAction Changed;

    public static OrderedDictionary upgrades { get; private set; }
    public static List<IUpgrade> available { get; private set; }
    public static List<IUpgrade> unlocked { get; private set; }

    void Awake() {
        instance = this;
        upgrades = new OrderedDictionary();
        available = new List<IUpgrade>();
        unlocked = new List<IUpgrade>();
        AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(IUpgrade).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .Select(x => (IUpgrade)Activator.CreateInstance(x))
            .OrderBy(x => x.menuIndex).ToList().ForEach(x => {
                upgrades.Add(x.name, x);
            });
        UpdateAvailable();
    }

    public static void UpdateAvailable() {
        available.Clear();
        foreach(IUpgrade upgrade in upgrades.Values) {
            if(unlocked.Contains(upgrade)) continue;
            if(upgrade.prerequisites.Length == 0) {
                available.Add(upgrade);
            } else {
                bool allPrerequisitesMet = true;
                foreach(string prerequisite in upgrade.prerequisites) {
                    if(!unlocked.Contains((IUpgrade)upgrades[prerequisite])) {
                        allPrerequisitesMet = false;
                        break;
                    }
                }
                if(allPrerequisitesMet) {
                    available.Add(upgrade);
                }
            }
        }
        Changed?.Invoke();
    }

    public static void UnlockUpgrade(string key) {
        if(!upgrades.Contains(key)) {
            Debug.LogError("Upgrade not found: " + key);
            return;
        }
        IUpgrade upgrade = (IUpgrade)upgrades[key];
        if(!available.Contains(upgrade)) {
            Debug.LogError("Upgrade not available: " + key);
            return;
        }
        if(unlocked.Contains(upgrade)) {
            Debug.LogError("Upgrade already unlocked: " + key);
            return;
        }
        unlocked.Add(upgrade);
        upgrade.Apply();
        UpdateAvailable();
    }
}
