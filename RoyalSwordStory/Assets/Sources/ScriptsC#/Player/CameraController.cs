using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _cameraShakeTime;
    [SerializeField] private float _cameraShakeIntensity;

    private bool _isCameraShake;
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;


    private void OnEnable()
    {
        EnemyStats.EnemyAnyHitEvent += StartCameraShake;
    }

    private void OnDisable()
    {
        EnemyStats.EnemyAnyHitEvent -= StartCameraShake;
    }

    void Start()
    {
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        _cinemachineBasicMultiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void StartCameraShake()
    {
        if (_isCameraShake) return;
        StartCoroutine(CameraShake());
    }

    private IEnumerator CameraShake()
    {
        _isCameraShake = true;

        if (_cinemachineBasicMultiChannelPerlin)
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = _cameraShakeIntensity;

        yield return new WaitForSeconds(_cameraShakeTime);

        if (_cinemachineBasicMultiChannelPerlin)
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;

        _isCameraShake = false;
    }
}
