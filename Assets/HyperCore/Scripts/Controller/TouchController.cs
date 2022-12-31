using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : Singleton<TouchController>,
    IPointerDownHandler,
    IPointerClickHandler,
    IPointerUpHandler,
    IDragHandler,
    IBeginDragHandler,
    IEndDragHandler
{
    [SerializeField] private DynamicJoystick joystick;

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (StageController.Instance.CurrentStage)
        {
            case StageScreen.Home:
                break;
            case StageScreen.InGame:
                break;
            case StageScreen.Complete:
                //StageController3D.Instance.NextLevel();
                break;
            default:
                break;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsReady() && StageController.Instance.CurrentStage == StageScreen.Home)
        {
            StageController.Instance.StartLevel();
            //GameUIController.Instance.HideTut();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    private bool IsReady()
    {
        return
            LevelController.Instance != null &&
            LevelController.Instance.Level != null &&
            !StageController.Instance.IsOver;
    }
}
