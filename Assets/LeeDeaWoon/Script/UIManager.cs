using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager inst;
    private void Awake() => inst = this;

    public TextMeshProUGUI touchToStart;

    void Start()
    {
        StartCoroutine(TouchToStart_FadeOut());
    }

    void Update()
    {

    }



    #region 터치시 시작 페이드인아웃

    public IEnumerator TouchToStart_FadeOut()
    {
        touchToStart.DOFade(0, 1);
        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(TouchToStart_FadeIn());
    }

    public IEnumerator TouchToStart_FadeIn()
    {
        touchToStart.DOFade(1, 1);
        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(TouchToStart_FadeOut());
    }

    #endregion
}
