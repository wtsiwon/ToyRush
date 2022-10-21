using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BottomATK : MonoBehaviour
{
    public GameObject atk;
    public SpriteRenderer warningLine;
    public Ease ease;

    private void Start()
    {
        StartCoroutine(ATK_Start());
    }

    private void Update()
    {
        
    }

    IEnumerator ATK_Start()
    {
        StartCoroutine(FadeOn());

        yield return new WaitForSeconds(4f);

        warningLine.DOPause();
        warningLine.DOFade(0, 0);

        atk.transform.DOLocalMoveY(warningLine.transform.position.y, 1f).SetEase(ease);

        yield return new WaitForSeconds(1f);

        atk.transform.DOLocalMoveY(warningLine.transform.position.y * 2, 1f).SetEase(ease);

        yield return new WaitForSeconds(1f);

        atk.transform.DOKill();
        AttackPatternManager.inst.isAttackSummon = true;
    }

    IEnumerator FadeOn()
    {
        warningLine.DOFade(0f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeOff());
    }

    IEnumerator FadeOff()
    {
        warningLine.DOFade(0.5f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeOn());
    }
}
