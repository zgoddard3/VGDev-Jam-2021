using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    [Range(0,1)]
    public float damping;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = target.position;
        targetPosition.z = transform.position.z;
        print(targetPosition);
        transform.position = Vector3.Lerp(transform.position, targetPosition, damping);
    }
}
