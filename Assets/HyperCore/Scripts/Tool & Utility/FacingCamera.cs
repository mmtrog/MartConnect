using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingCamera : MonoBehaviour
{
    private void Start()
    {
        if (CameraController.Instance.Tail == null)
        {
            Debug.LogError("Camera Tail transform not assigned.");
        }
    }

    private void Update()
    {
        transform.LookAt(CameraController.Instance.transform);
    }
}
