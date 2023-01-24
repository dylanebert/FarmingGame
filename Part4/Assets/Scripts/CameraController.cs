using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraController : MonoBehaviour {
    public static CameraController instance { get; private set; }

    [SerializeField] CinemachineVirtualCamera focusCam;
    [SerializeField] CinemachineVirtualCamera defaultCam;

    void Awake() {
        instance = this;
    }

    public static void SetFocus(Plot plot) {
        if(plot != null) {
            instance.focusCam.Follow = plot.transform;
            instance.focusCam.Priority = 20;
        } else {
            instance.focusCam.Priority = 0;
        }
    }

    public static void Shake(float magnitude = 1f) {
        instance.defaultCam.transform.DOShakePosition(magnitude * .1f, magnitude * .3f, 60);
    }
}
