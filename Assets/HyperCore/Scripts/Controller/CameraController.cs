using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public Camera GameCamera;
    public Camera UICamera;
    public Transform Tail;
    [SerializeField] private float minOrthoSize = 8f;
    [SerializeField] private float minPerspectiveFOV = 35f;
    [SerializeField] private float maxPerspectiveFOV = 40f;
    [SerializeField] private Animator anim;
    [SerializeField] private TransformFollow followScript;

    private void Start()
    {
        //float fixedRatio = GlobalController.FixedStageResolution.x / GlobalController.FixedStageResolution.y;
        //if (Screen.currentResolution.width / Screen.currentResolution.height > fixedRatio)
        //{
        //    GameCamera.fieldOfView = minPerspectiveFOV;
        //}
        //else
        //{
        //    GameCamera.fieldOfView = maxPerspectiveFOV;
        //}
        //GameCamera.orthographicSize *= fixedRatio / GlobalController.ScreenRatio;
        //if (GameCamera.orthographicSize < minOrthoSize)
        //{
        //    GameCamera.orthographicSize = minOrthoSize;
        //}
        //UICamera.orthographicSize = GameCamera.orthographicSize;
    }

    public void Shake()
    {
        anim.Play("Shake", 0, 0);
    }

    public void ChangeCameraColor(Color color)
    {

    }

    public void FollowTarget(Transform target)
    {
        followScript.Target = target;
    }

    public void SetFollowOffset(Vector3 v)
    {
        followScript.Offset = v;
    }

    public void ChangeCameraAngle(Vector3 v)
    {
        LeanTween.rotateLocal(gameObject, v, 0.5f);
    }
}
