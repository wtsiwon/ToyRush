using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class WarningLine : MonoBehaviour
{
    public SpriteRenderer warningLinePrefab;

    void Start()
    {
        StartCoroutine(WarningLine_FadeOnOff());
    }

    void Update()
    {
        
    }

    IEnumerator WarningLine_FadeOnOff()
    {
        warningLinePrefab.DOFade(0, 0.5f).SetLoops(-1, LoopType.Yoyo);

        yield return new WaitForSeconds(3);

        warningLinePrefab.DOKill();
        warningLinePrefab.DOFade(0, 0);
    }
}
