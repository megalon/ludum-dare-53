using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class SpawnerPooled : MonoBehaviour
{
    [SerializeField]
    private GameObject _objToSpawn;
    [SerializeField]
    private int _poolSizeAtStart = 100;
    [SerializeField]
    private int _maxPoolSize = 1000;

    private ObjectPool<GameObject> _pool;

    private void Start()
    {
        _pool = new ObjectPool<GameObject>(CreateObj, TakeFromPool, ReturnedToPool, DestroyPoolObject, true, _poolSizeAtStart, _maxPoolSize);
    }

    public GameObject Spawn()
    {
        return _pool.Get();
    }

    public GameObject Spawn(Vector3 velocity)
    {
        GameObject obj = _pool.Get();
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(velocity, ForceMode.Impulse);
        return obj;
    }

    private GameObject CreateObj()
    {
        GameObject obj = Instantiate(_objToSpawn.gameObject, transform);
        PoolableGameObject poolableObj = obj.GetComponent<PoolableGameObject>();
        poolableObj.SetPool(_pool);
        obj.SetActive(false);
        return obj;
    }

    protected void TakeFromPool(GameObject obj)
    {
        obj.transform.parent = null;
        obj.gameObject.SetActive(true);

        OnTakeFromPool(obj);
    }

    protected void ReturnedToPool(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        obj.transform.parent = transform;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        obj.gameObject.SetActive(false);

        OnReturnedToPool(obj);
    }

    private void DestroyPoolObject(GameObject obj)
    {
        Destroy(obj.gameObject);
    }

    protected abstract void OnTakeFromPool(GameObject obj);
    protected abstract void OnReturnedToPool(GameObject obj);
    protected abstract void OnDestroyPoolObject(GameObject obj);
}
