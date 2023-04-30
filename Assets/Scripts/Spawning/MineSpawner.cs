using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpawner : SpawnerPooled
{
    protected override void OnDestroyPoolObject(GameObject obj)
    {
    }

    protected override void OnReturnedToPool(GameObject obj)
    {
    }

    protected override void OnTakeFromPool(GameObject obj)
    {
    }
}
