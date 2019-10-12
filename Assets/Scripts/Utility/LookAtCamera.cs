using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private GameObject[] objects;
    [SerializeField]
    private GameObject cameraOBJ;

    private Quaternion rotation;

    // Update is called once per frame
    void LateUpdate()
    {
        if (objects!= null)
        {
            LookCamera();
        }
    }

    private void LookCamera()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].transform.rotation = cameraOBJ.transform.rotation;
        }
    }
}
