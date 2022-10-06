using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;

public class UI_Manager : MonoBehaviour
{
    public TextMeshProUGUI TouchToStart;

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
        TouchToStart.DOFade(0, 1);
        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(TouchToStart_FadeIn());
    }

    public IEnumerator TouchToStart_FadeIn()
    {
        TouchToStart.DOFade(1, 1);
        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(TouchToStart_FadeOut());
    }

    #endregion
}
