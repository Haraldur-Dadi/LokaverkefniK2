using UnityEngine;
using Cinemachine;

public class MainCamera : MonoBehaviour {

    public static MainCamera Instance;

    public float shakeDuration = 0.25f;
    public float shakeAmplitude = 0.3f;
    public float shakeFrequency = 1.0f;

    private float shakeElapsedTime = 0f;

    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    private void Awake() {
        if(Instance != null) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    void Start() {
        if (virtualCamera != null) {
            virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }
    }

    void Update() {
        if (virtualCamera != null && virtualCameraNoise != null) {
            if (shakeElapsedTime > 0) {
                virtualCameraNoise.m_AmplitudeGain = shakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = shakeFrequency;

                shakeElapsedTime -= Time.deltaTime;
            } else {
                virtualCameraNoise.m_AmplitudeGain = 0f;
                shakeElapsedTime = 0f;
            }
        }
    }

    public void CameraShake() {
        shakeElapsedTime = shakeDuration;
    }
}
