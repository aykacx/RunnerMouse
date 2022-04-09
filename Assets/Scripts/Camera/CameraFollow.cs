using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followObject;
    public int followDistance;

    void Start()
    {
    }


    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, followObject.position, Time.deltaTime * followDistance);
    }
}
