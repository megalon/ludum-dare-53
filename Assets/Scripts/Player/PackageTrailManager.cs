using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageTrailManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _packageObj;

    [SerializeField]
    private float _lerpSpeed;

    [SerializeField]
    public Damageable _damageable;

    [SerializeField]
    public Vector3 _packageSpacing;

    [SerializeField]
    private GameObject _parentBoxesToThis;

    private List<GameObject> _packageTrail;

    private void Start()
    {
        _packageTrail = new List<GameObject>();

        for (int i = 0; i < _damageable.Health; ++i)
        {
            GameObject obj = Instantiate(_packageObj, _parentBoxesToThis.transform);
            _packageTrail.Add(obj);
        }
    }

    private void Update()
    {
        if (_packageTrail.Count <= 0) return;

        // The first package is special because it follows the boat
        LerpPackage(_packageTrail[0], gameObject.transform.parent.localPosition + transform.localPosition);

        // Note the i = 1 here
        for (int i = 1; i < _packageTrail.Count; ++i)  
        {
            // Follow the package in front of this package, but add spacing between packages
            Vector3 target = _packageTrail[i - 1].transform.localPosition + (_packageSpacing);

            LerpPackage(_packageTrail[i], target);
        }
    }

    private void LerpPackage(GameObject package, Vector3 target)
    {
        package.transform.localPosition = Vector3.Lerp(package.transform.localPosition, target, _lerpSpeed * Time.deltaTime);
    }

    public void Hurt(float amount)
    {
        for (int i = 0; _packageTrail.Count > 0 && i < amount; ++i)
        {
            GameObject obj = _packageTrail[0];
            _packageTrail.RemoveAt(0);

            Destroy(obj);
        }
    }
}
