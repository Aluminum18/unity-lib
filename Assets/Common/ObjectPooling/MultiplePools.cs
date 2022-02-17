using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplePools : MonoSingleton<MultiplePools>
{
    [SerializeField]
    private Transform _poolPos;
    [SerializeField]
    private int _defaultPoolSize = 10;

    // key = object InstanceId
    Dictionary<int, PoolInMultiplePool> _pools = new Dictionary<int, PoolInMultiplePool>();

    public GameObject SpawnGameObject(GameObject go, int poolSize = 0, bool activeOnSpawn = true)
    {
        _pools.TryGetValue(go.GetInstanceID(), out var pool);

        if (pool == null)
        {
            pool = ScriptableObject.CreateInstance<PoolInMultiplePool>();
            pool.Init(go, _poolPos.position, poolSize == 0 ? _defaultPoolSize : poolSize);

            _pools[go.GetInstanceID()] = pool;
        }

        return pool.SpawnObject(activeOnSpawn);
    }
}

public class PoolInMultiplePool : ScriptableObject
{
    private GameObject _rootObject;
    private Vector3 _poolPos;
    private int _poolSize;

    private Stack<GameObject> _available = new Stack<GameObject>();

    public void Init(GameObject go, Vector3 poolPos, int limit)
    {
        _rootObject = go;
        _poolPos = poolPos;
        _poolSize = limit;
    }

    public GameObject SpawnObject(bool activeOnSpawn)
    {
        GameObject go;
        if (_available.Count == 0)
        {
            _rootObject.SetActive(activeOnSpawn);

            go = Instantiate(_rootObject);
            var objectInPool = go.AddComponent<ObjectInMultiplePoolsPool>();
            objectInPool.AssignParrentPool(this);
            return go;
        }

        go = _available.Pop();

        if (go == null || go.activeSelf)
        {
            go = SpawnObject(activeOnSpawn);
        }

        go.SetActive(activeOnSpawn);

        return go;
    }

    public void BackToPool(GameObject go)
    {
        if (_available.Count + 1 > _poolSize)
        {
            Destroy(go);
            return;
        }

        go.SetActive(false);
        go.transform.position = _rootObject.transform.position;
        _available.Push(go);
    }
}
