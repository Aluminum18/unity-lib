using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplePools : MonoBehaviour
{
    [SerializeField]
    private Transform _poolPos;
    [SerializeField]
    private int _perPoolLimit;

    // key = object InstanceId
    Dictionary<int, PoolInMultiplePool> _pools = new Dictionary<int, PoolInMultiplePool>();

    public GameObject SpawnGameObject(GameObject go)
    {
        _pools.TryGetValue(go.GetInstanceID(), out var pool);

        if (pool == null)
        {
            pool = ScriptableObject.CreateInstance<PoolInMultiplePool>();
            pool.Init(go, _poolPos.position, _perPoolLimit);

            _pools[go.GetInstanceID()] = pool;
        }

        return pool.SpawnObject();
    }
}

public class PoolInMultiplePool : ScriptableObject
{
    private GameObject _rootObject;
    private Vector3 _poolPos;
    private int _limit;

    private Stack<GameObject> _available = new Stack<GameObject>();

    public void Init(GameObject go, Vector3 poolPos, int limit)
    {
        _rootObject = go;
        _poolPos = poolPos;
        _limit = limit;
    }

    public GameObject SpawnObject()
    {
        GameObject go;
        if (_available.Count == 0)
        {
            go = Instantiate(_rootObject);
            var objectInPool = go.AddComponent<ObjectInMultiplePoolsPool>();
            objectInPool.AssignParrentPool(this);
            return go;
        }

        go = _available.Pop();

        if (go == null || go.activeSelf)
        {
            go = SpawnObject();
        }

        go.SetActive(true);

        return go;
    }

    public void BackToPool(GameObject go)
    {
        if (_available.Count + 1 > _limit)
        {
            Destroy(go);
            return;
        }

        go.SetActive(false);
        go.transform.position = _rootObject.transform.position;
        _available.Push(go);
    }
}
