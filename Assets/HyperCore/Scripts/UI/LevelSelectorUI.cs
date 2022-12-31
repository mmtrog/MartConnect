using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorUI : MonoBehaviour
{
    [SerializeField]
    private Transform grid;
    [SerializeField]
    private ScrollRect scrollRect;

    private void Start()
    {
        //for (int i = 0; i < LevelController.Instance.LevelTextures.Length; i++)
        //{
        //    LevelItem lv = ObjectPool.Spawn<LevelItem>("LevelItem", grid);
        //    lv.SetUp(i);
        //}
    }

    private void OnEnable()
    {
        StartCoroutine(CoScrollToTop());
    }

    IEnumerator CoScrollToTop()
    {
        yield return new WaitForEndOfFrame();
        scrollRect.verticalNormalizedPosition = 1;
    }
}
