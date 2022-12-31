using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class StageController : MonoBehaviour
{
    public static StageController Instance { get; set; }
    public LevelController Level { get; set; }
    public int CurrentLevel { get; set; }
    public StageScreen CurrentStage { get; set; }
    public bool IsOver { get; set; }
    private float playTimeInSeconds;

    public HyperCharacter MainCharacter;
    [SerializeField] private Camera cameraGame;
    [SerializeField] private int levelLimit;
    [SerializeField] private bool noStartScreen;
    [SerializeField] private bool useSavedLevel;
    [SerializeField] private AudioClip sfxWin;
    private int currentLevel;
    private int mapIndex;
    private int numGoalsDone;
    private int numTotalGoals;

    public bool IsWaitingForSkinOptions;

    private void Awake()
    {
        if (GlobalController.StartSceneName == SceneManager.GetActiveScene().name)
        {
            SceneManager.LoadScene("Splash");
            return;
        }
        Instance = this;
        StartCoroutine(CoStart());
    }

    IEnumerator CoStart()
    {
        // Use saved data
        if (useSavedLevel)
        {
            if (DataController.Instance != null)
            {
                if (DataController.Instance.Data.LevelIndex <= 0)
                {
                    DataController.Instance.Data.LevelIndex = 1;
                }
                currentLevel = DataController.Instance.Data.LevelIndex <= 0 ? 1 : DataController.Instance.Data.LevelIndex;
                if (GlobalController.ReplayingLevel > 0)
                {
                    currentLevel = GlobalController.ReplayingLevel;
                }
            }
            else
            {
                currentLevel = 1;
            }
        }
        else
        {
            currentLevel = GlobalController.CurrentLevelIndex <= 0 ? 1 : GlobalController.CurrentLevelIndex;
        }
        GlobalController.CurrentLevelIndex = currentLevel;
        CurrentStage = StageScreen.Home;
        mapIndex = LevelController.Instance.LoadLevel(currentLevel, levelLimit);

        yield return new WaitForSeconds(0.0f);
        SetUpLevel();
        GlobalController.Instance.ShowBanner();
    }

    private void SetUpLevel()
    {
        CurrentStage = StageScreen.Home;
        IsOver = false;
        StartCoroutine(CoSetUpLevel());
    }

    IEnumerator CoSetUpLevel()
    {
        // Setup level
        LevelController.Instance.SetUpLevel();
        GlobalController.CurrentLevelIndex = currentLevel = LevelController.Instance.CurrentLevel;
        yield return new WaitForSeconds(0.02f);
        if (LevelController.Instance.Level != null)
        {
            LevelController.Instance.Level.SetUp();
            numGoalsDone = 0;
            GameUIController.Instance.UpdateLevelProgress(0);
            GameUIController.Instance.ShowNewLevel(currentLevel, noStartScreen);
        }
    }

    public void StartLevel()
    {
        CurrentStage = StageScreen.InGame;
        GameUIController.Instance.ShowInGameUI();
        LevelController.Instance.Level.StartLevel();
        playTimeInSeconds = Time.realtimeSinceStartup;
    }

    public void UpdateScore()
    {
        numGoalsDone++;
        GameUIController.Instance.UpdateLevelProgress(numGoalsDone / (float)numTotalGoals);
        if (numGoalsDone == numTotalGoals)
        {
            End(true);
        }
    }

    public void End(bool win)
    {
        if (IsOver) return;
        IsOver = true;
        StartCoroutine(CoEnd(win));
    }

    IEnumerator CoEnd(bool win)
    {
        if (win)
        {
            yield return new WaitForSeconds(2f);
            //AnalyticsController.Instance.LogLevelComplete(GlobalController.CurrentLevelIndex, (int)(Time.realtimeSinceStartup - playTimeInSeconds), GlobalController.ReplayCount);
            //AnalyticsController.Instance.LogCustomEvent("checkpoint_" + DataController.Instance.Data.LevelIndex.ToString("000"), "", "");
            GlobalController.CurrentLevelIndex++;
            if (GlobalController.CurrentLevelIndex > DataController.Instance.Data.LevelIndex)
            {
                DataController.Instance.Data.LevelIndex = GlobalController.CurrentLevelIndex;
            }
            DataController.Instance.SaveData();
            StartCoroutine(CoShowEndGameUI(true));
            GlobalController.ReplayCount = 0;
        }
        else
        {
            yield return new WaitForSeconds(1f);
            //AnalyticsController.Instance.LogLevelFail(GlobalController.CurrentLevelIndex, (int)(Time.realtimeSinceStartup - playTimeInSeconds), GlobalController.ReplayCount);
            StartCoroutine(CoShowEndGameUI(false));
        }
    }

    int earning;
    int earningX;
    public bool showingInter = true;

    public int BonusEarning;

    public void WatchAdsEarnX()
    {
        GlobalController.Instance.ShowRewardedVideo(EarnRewardX);
    }

    private void EarnRewardX()
    {
        //AnalyticsController.Instance.LogEarnCurrency("cash", earningX, "x5watchads");
        DataController.Instance.SaveData();
        showingInter = false;
    }

    IEnumerator CoShowEndGameUI(bool win)
    {
        GameUIController.Instance.SetEndResultLabelActive(win, true);
        if (win)
        {
            SoundController.Instance.PlaySingle(sfxWin);
            GameUIController.Instance.ShowConfetti();
            yield return new WaitForSeconds(0.5f);
            GameUIController.Instance.ShowGameEnd(win);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            GameUIController.Instance.ShowGameEnd(win);
        }
    }

    public void OnLevelSkip()
    {
        GlobalController.Instance.ShowRewardedVideo(SkipLevel);
    }

    private void SkipLevel()
    {
        showingInter = false;
        //AnalyticsController.Instance.LogLevelSkip(DataController.Instance.Data.LevelIndex, (int)(Time.realtimeSinceStartup - playTimeInSeconds), GlobalController.ReplayCount);
        GlobalController.ReplayCount = 0;
        GlobalController.CurrentLevelIndex++;

        if (GlobalController.CurrentLevelIndex > DataController.Instance.Data.LevelIndex)
        {
            DataController.Instance.Data.LevelIndex = GlobalController.CurrentLevelIndex;
        }
        DataController.Instance.SaveData();
        NextLevel();
    }

    public void Next()
    {
        DataController.Instance.Data.Coin += earning;
        DataController.Instance.SaveData();
        if (DataController.Instance.Data.LevelIndex % 4 == 0 && !GlobalController.IsRated)
        {
            GameUIController.Instance.ShowRateUs(NextLevel, NextLevelAfterRate);
        }
        else
        {
            NextLevel();
        }
    }

    private void NextLevelAfterRate()
    {
        showingInter = false;
        NextLevel();
    }

    public void NextLevel()
    {
        GlobalController.ReplayingLevel = 0;
        GameUIController.Instance.ClearResultScreen(ReloadScene);
    }

    public void Restart()
    {
        GlobalController.ReplayingLevel = mapIndex;
        GlobalController.ReplayCount++;
        GameUIController.Instance.ClearResultScreen(ReloadScene);
    }

    private void ReloadScene()
    {
        if (showingInter)
        {
            GlobalController.Instance.ShowInterstitial();
        }
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
