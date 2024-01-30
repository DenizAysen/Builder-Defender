using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemachineShake : MonoBehaviour
{
    #region Private
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _cinemachineMultiChannelPerling;

    private float _timer;
    private float _timerMax;
    private float _startingIntensity; 
    #endregion

    public static CinemachineShake Instance { get; private set; }
    #region Unity Methods
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _cinemachineMultiChannelPerling = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    private void Update()
    {
        if (_timer < _timerMax)
        {
            _timer += Time.deltaTime;
            float amplitude = Mathf.Lerp(_startingIntensity, 0f, _timer / _timerMax);
            _cinemachineMultiChannelPerling.m_AmplitudeGain = amplitude;
        }
    } 
    #endregion
    public void ShakeCamera(float intensity, float timerMax)
    {
        _startingIntensity = intensity;
        _timerMax = timerMax;
        _timer = 0f;
        _cinemachineMultiChannelPerling.m_AmplitudeGain = intensity;
    }
}
