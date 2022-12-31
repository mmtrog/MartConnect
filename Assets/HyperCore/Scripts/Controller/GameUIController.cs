using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StageScreen
{
    None,
    Home,
    InGame,
    Complete
}

public class GameUIController : MonoBehaviour
{
    public static GameUIController Instance
    {
        get; set;
    }
    [SerializeField] private CanvasScaler[] canvasScalers;
    [SerializeField] private Image cover;
    [SerializeField] private TouchController touchPad;
    [SerializeField] private UIRateUs rateUI;
    [SerializeField] private UIToggleSpriteSwap tgSfx;
    [Header("Start UI")]
    [SerializeField] private Text txtLevel;
    [SerializeField] private GameObject tapToStart;
    [SerializeField] private GameObject shopNoti;
    [SerializeField] private CanvasGroup startUI;
    [Header("In Game UI")]
    [SerializeField] private UILevelProgress levelFillProgress;
    [SerializeField] private Text[] txtProgress;
    [SerializeField] private Text txtLevelCurrent;
    [SerializeField] private Text txtLevelNext;
    [SerializeField] private Text txtCoin;
    [SerializeField] private CanvasGroup inGameUI;
    [Header("End UI")]
    [SerializeField] private Image resultCover;
    [SerializeField] private GameObject resultLabel;
    [SerializeField] private CanvasGroup resultButtons;
    [SerializeField] private GameObject btnNext;
    [SerializeField] private GameObject btnReplay;
    [SerializeField] private GameObject btnNextByAds;
    [SerializeField] private CanvasGroup resultWin;
    [SerializeField] private CanvasGroup resultFail;
    [SerializeField] private ParticleSystem eVictory;

    public bool IsUIMatchWidth
    {
        get
        {
            return GlobalController.ScreenRatio < GlobalController.FixedStageResolution.x / GlobalController.FixedStageResolution.y;
        }
    }


    private void Awake ()
    {
        Instance = this;
    }

    private void Start ()
    {
        for (int i = 0; i < canvasScalers.Length; i++)
        {
            canvasScalers[i].matchWidthOrHeight = IsUIMatchWidth ? 0 : 1;
        }
        tgSfx.IsOn = GlobalController.IsSoundOn;
    }

    public void ShowNewLevel (int level, bool noStartScreen = false)
    {
        if (noStartScreen)
        {
            inGameUI.alpha = 1;
            inGameUI.blocksRaycasts = true;
            startUI.alpha = 0;
            startUI.blocksRaycasts = false;
        }
        else
        {

            inGameUI.alpha = 0;
            inGameUI.blocksRaycasts = false;
            startUI.alpha = 1;
            startUI.blocksRaycasts = true;
        }
        LeanTween.alpha(cover.rectTransform, 0, 0.2f).setOnComplete(() =>
        {
            cover.raycastTarget = false;
            touchPad.gameObject.SetActive(!noStartScreen);
            tapToStart.SetActive(!noStartScreen);
        });
        resultLabel.SetActive(false);
        eVictory.Stop();
        resultButtons.blocksRaycasts = false;
        resultButtons.alpha = 0;
        txtLevelCurrent.text = level.ToString();
        txtLevelNext.text = (level + 1).ToString();
    }

    public void ShowInGameUI ()
    {
        LeanTween.alphaCanvas(inGameUI, 1, 0.2f).setOnComplete(() =>
        {
            inGameUI.blocksRaycasts = true;
        });
        LeanTween.alphaCanvas(startUI, 0, 0.1f).setOnComplete(() =>
        {
            startUI.blocksRaycasts = false;
        });
        tapToStart.SetActive(false);
        levelFillProgress.SetProgress(0, 0);
    }

    public void UpdateCoin (int previousValue, int value, float duration = 1f, Action callback = null)
    {
        // Effects
        LeanTween.cancel(txtCoin.gameObject);
        LeanTween.scale(txtCoin.gameObject, Vector3.one * 1.5f, 0.05f).setOnComplete(() =>
        {
            LeanTween.scale(txtCoin.gameObject, Vector3.one, 0.25f);
        });
        LeanTween.value(previousValue, value, duration).setOnUpdate((float f) =>
        {
            txtCoin.text = f.ToString("0");
            //txtCoinCollected.text = (f - previousValue).ToString("0");
        }).setOnComplete(callback);
    }

    public void UpdateShopNoti ()
    {
        for (int i = 0; i < AssetController.Instance.ListSkinItemData.Count; i++)
        {
            if (AssetController.Instance.ListSkinItemData[i].IsUnlocked &&
                !DataController.Instance.Data.SkinIDs.Contains(AssetController.Instance.ListSkinItemData[i].ID))
            {
                shopNoti.SetActive(true);
                return;
            }
        }
        for (int i = 0; i < AssetController.Instance.ListHeadsetItemData.Count; i++)
        {
            if (AssetController.Instance.ListHeadsetItemData[i].IsUnlocked &&
                !DataController.Instance.Data.HeadsetIDs.Contains(AssetController.Instance.ListHeadsetItemData[i].ID))
            {
                shopNoti.SetActive(true);
                return;
            }
        }
        shopNoti.SetActive(false);
    }

    public void UpdateLevelProgress (float progress)
    {
        levelFillProgress.SetProgress(progress, 0, 0.2f);
    }

    public void ShowConfetti ()
    {
        if (eVictory != null)
        {
            eVictory.Play();
        }
    }

    public void ToggleSound (bool isOn)
    {
        GlobalController.IsSoundOn = !GlobalController.IsSoundOn;
        if (GlobalController.IsSoundOn)
        {
            SoundController.Instance.Unmute();
        }
        else
        {
            SoundController.Instance.Mute();
        }
        PlayerPrefs.SetInt("IsSoundOn", GlobalController.IsSoundOn ? 1 : 0);
    }

    public void SetEndResultLabelActive (bool win, bool isActive)
    {
        resultLabel.SetActive(isActive);
        if (win)
        {
            LeanTween.alphaCanvas(resultWin, 1, isActive ? 0.2f : 0);
        }
        else
        {
            LeanTween.alphaCanvas(resultFail, 1, isActive ? 0.2f : 0);
        }
    }

    public void ShowLevelBreak (Action callback, float duration = 0.2f, float delay = 0f)
    {
        cover.raycastTarget = true;
        LeanTween.alpha(cover.rectTransform, 1, duration).setOnComplete(() =>
        {
            callback?.Invoke();
            LeanTween.alpha(cover.rectTransform, 0, duration).setOnComplete(() =>
            {
                cover.raycastTarget = false;
            }).setDelay(delay);
        });
    }

    public void ShowRateUs (Action onClose, Action onRatePositive)
    {
        rateUI.OnClose = onClose;
        rateUI.OnRatePositive = onRatePositive;
        rateUI.Show();
    }

    public void ShowGameEnd (bool win)
    {
        resultLabel.SetActive(true);
        LeanTween.alpha(resultCover.rectTransform, 0.5f, 0.2f);
        inGameUI.blocksRaycasts = false;
        LeanTween.alphaCanvas(inGameUI, 0, 0.2f);
        if (win)
        {
            ShowConfetti();
            LeanTween.alphaCanvas(resultWin, 1, 0.2f);
        }
        else
        {
            LeanTween.alphaCanvas(resultFail, 1, 0.2f);
        }
        btnNext.SetActive(win);
        btnReplay.SetActive(!win);
        btnNextByAds.SetActive(!win);
        LeanTween.alphaCanvas(resultButtons, 1, 0.2f).setOnComplete(() =>
        {
            resultButtons.blocksRaycasts = true;
        });
    }

    public void ClearResultScreen (Action callback)
    {
        LeanTween.alpha(resultCover.rectTransform, 0, 0.2f);
        LeanTween.scale(resultLabel, Vector2.zero, 0.2f).setOnComplete(() =>
        {
            resultLabel.SetActive(false);
        });
        eVictory.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        LeanTween.alphaCanvas(resultButtons, 0, 0.2f).setDelay(0.2f);
        resultButtons.blocksRaycasts = false;
        LeanTween.scale(resultButtons.gameObject, Vector3.zero, 0.4f);
        cover.raycastTarget = true;
        LeanTween.alpha(cover.rectTransform, 1, 0.5f).setOnComplete(callback);
    }
}
