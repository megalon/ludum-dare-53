using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshakeManager : MonoBehaviour
{
    public static ScreenshakeManager Instance;

    public float shakeIntensity = 0.1f;
    public float shakeDuration = 0.5f;
    public AnimationCurve shakeFalloffCurve;

    private GameObject _cameraObj;
    private Vector3 _cameraStartPos;

    [SerializeField] private float _landingShakeAmount;
    [SerializeField] private float _hitShakeAmount;

    public float LandingShakeAmount { get => _landingShakeAmount; }
    public float HitShakeAmount { get => _hitShakeAmount; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
    }

    void Start()
    {
        _cameraObj = Camera.main.gameObject;
        _cameraStartPos = _cameraObj.transform.localPosition;
    }

    public void Shake(float amount)
    {
        Impulse(amount);
    }

    void Impulse(float amount)
    {
        StopAllCoroutines();
        StartCoroutine(DoImpulse(amount));
    }

    IEnumerator DoImpulse(float intensity)
    {
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            Vector3 shakeOffset = Random.insideUnitSphere * intensity * shakeFalloffCurve.Evaluate(1 - elapsedTime / shakeDuration);
            _cameraObj.transform.localPosition = _cameraStartPos + shakeOffset;

            elapsedTime += Time.deltaTime;

            if (PauseManager.Instance.IsPaused)
            {
                break;
            }

            yield return null;
        }

        _cameraObj.transform.localPosition = _cameraStartPos;
    }
}
