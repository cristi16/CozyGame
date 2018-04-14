using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public List<Transform> targetList;


    public float oSizeMin;
    public float oSizeMax;

    public void Update()
    {
        Vector3 center = Vector3.zero;

        foreach(Transform t in targetList)
        {
            center += t.position;
        }

        if (targetList.Count > 0) {
            center /= targetList.Count;
        }

        center.z = -25f;

        Vector3 translateVector = Vector3.ClampMagnitude(center - transform.position, 0.5f);

        translateVector /= 2f;

        transform.Translate(translateVector);
                
    }

}