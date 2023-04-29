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
        _cameraStartPos = _cameraObj.transform.position;
    }

    public void Shake(float amount)
    {
        Impulse();
    }

    void Impulse()
    {
        StopAllCoroutines();
        StartCoroutine(DoImpulse());
    }

    IEnumerator DoImpulse()
    {
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            Vector3 shakeOffset = Random.insideUnitSphere * shakeIntensity * shakeFalloffCurve.Evaluate(1 - elapsedTime / shakeDuration);
            _cameraObj.transform.position = _cameraStartPos + shakeOffset;

            elapsedTime += Time.deltaTime;

            if (PauseManager.Instance.IsPaused)
            {
                break;
            }

            yield return null;
        }

        _cameraObj.transform.position = _cameraStartPos;
    }
}
