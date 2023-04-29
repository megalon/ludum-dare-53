using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolableGameObject : MonoBehaviour
{
    [SerializeField]
    private float _lifetime = 1;
    private float _timer = 0;

    protected ObjectPool<GameObject> _pool;

    public void OnEnable()
    {
        _timer = _lifetime;
    }

    private void Update()
    {
        if (_timer <= 0)
        {
            ReturnToPool();
        } else
        {
            _timer -= Time.deltaTime;
        }
    }

    public void SetPool(ObjectPool<GameObject> pool)
    {
        _pool = pool;
    }

    public virtual void ReturnToPool()
    {
        _pool.Release(gameObject);
    }
}
