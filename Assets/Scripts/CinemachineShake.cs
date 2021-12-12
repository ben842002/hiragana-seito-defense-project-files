using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake instance { get; private set; }

    CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBrain cinemaBrain;
    private CinemachineVirtualCamera[] cameras;

    private float shakeTimer;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        cinemaBrain = Camera.main.GetComponent<CinemachineBrain>();
    }

    // you have to wait a frame before cinemachineBrain gets the active camera
    IEnumerator Start()
    {
        yield return null;
        cinemachineVirtualCamera = cinemaBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
        cameras = FindObjectsOfType<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        // set amp and freq values
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = time;

        // begin timer. Countdown is called in Update
        shakeTimer = time;
    }

    // Update is called once per frame
    void Update()
    {
        // find active camera when blending/transitioning between cameras
        if (cinemaBrain.IsBlending == true)
            cinemachineVirtualCamera = cinemaBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();

        // begin camera shake and set timer
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                // loop through camera list, making amplitude and frequency values 0
                for (int i = 0; i < cameras.Length; i++)
                    ResetValues(cameras[i]);
            }
        }
    }

    /// <summary>
    /// Resets camera shake values to 0. This script is also called in GameMaster when the player dies
    /// to avoid the bug where the values dont reset because the cameras switches priority before they can reach 0
    /// </summary>
    public void ResetValues(CinemachineVirtualCamera camera)
    {
        // timer is over
        CinemachineBasicMultiChannelPerlin cinemachineValues =
            camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        // set amplitude and freq to 0
        cinemachineValues.m_AmplitudeGain = 0f;
        cinemachineValues.m_FrequencyGain = 0f;
    }
}
