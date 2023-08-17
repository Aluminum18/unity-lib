using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolTest : MonoBehaviour
{
    public int poolSize = 10;
    public void SpawnOject(GameObject templateObject)
    {
        var clone = MultiplePools.Instance.SpawnGameObject(templateObject, poolSize);
    }
}
