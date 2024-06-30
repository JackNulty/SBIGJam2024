using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    private Quaternion rotation;
    void Start()
    {
        rotation = this.transform.rotation;
    }

    void LateUpdate()
    {
        this.transform.rotation = rotation;
    }
}
