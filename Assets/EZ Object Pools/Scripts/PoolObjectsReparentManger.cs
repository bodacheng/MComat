using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjectsReparentManger : MonoBehaviour
{
    private int PoolObjectReparentStep = 0;
    // Update is called once per frame
    void Update()
    {
        if (PoolObjectReparentStep == 10)
        {
            EffectAndHurtObjectLoading.Instance.ReparentPooledObjects(false);
            PoolObjectReparentStep = 0;
        }
        PoolObjectReparentStep++;
    }
}
