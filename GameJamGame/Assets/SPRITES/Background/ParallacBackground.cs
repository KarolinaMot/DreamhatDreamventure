using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallacBackground : MonoBehaviour
{
    private Transform camera;
    private Vector3 lastCameraPosition;
    public float multiplier = 0.5f;

    private void Start()
    {
        camera = Camera.main.transform;
        lastCameraPosition = camera.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = camera.position - lastCameraPosition;
        transform.position += deltaMovement * multiplier;
        lastCameraPosition = camera.position;
    }

}
