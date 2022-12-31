using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelItem : MonoBehaviour
{
    [SerializeField]
    private Image avatar;
    [SerializeField]
    private int Level;
    [SerializeField]
    private GameObject btnPlay;
    [SerializeField]
    private GameObject unknownMark;
    [SerializeField]
    private Text txtLevel;

    public void SetUp(int id)
    {
        Level = id;
        //avatar.sprite = LevelController.Instance.LevelTextures[id];
        //txtLevel.text = "LEVEL " + Level;
#if !VIDEO
        btnPlay.SetActive(DataController.Instance.Data.LevelIndex >= id);
        avatar.gameObject.SetActive(DataController.Instance.Data.LevelIndex >= id);
        unknownMark.SetActive(DataController.Instance.Data.LevelIndex < id);
        txtLevel.gameObject.SetActive(DataController.Instance.Data.LevelIndex < id);
#else
        unknownMark.SetActive(false);
        txtLevel.gameObject.SetActive(false);
#endif
    }

    public void OnPlay()
    {
        //StageController3D.Instance.SetUpLevel(Level);
        //StageController3D.Instance.ToggleLevelSelector();
    }
}
