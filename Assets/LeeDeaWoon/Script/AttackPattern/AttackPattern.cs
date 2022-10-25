using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AttackPattern : MonoBehaviour
{
    public enum EAttackPattern
    {
        Bottom,
        Right
    }
    public EAttackPattern eAttackPattern;

    [Header("¾Æ·§ °ø°Ý")]
    public Ease ease;
    public GameObject atk;
    public SpriteRenderer warningLine;

    private void Start()
    {
        StartCoroutine("ATK_Start");
    }

    private void Update()
    {

    }


    IEnumerator ATK_Start()
    {
        switch (eAttackPattern)
        {
            #region ¾Æ·§°ø°Ý
            case EAttackPattern.Bottom:
                StartCoroutine("FadeOn");
                yield return new WaitForSeconds(3.5f);

                StopCoroutine("FadeOn");
                StopCoroutine("FadeOff");
                yield return new WaitForSeconds(0.5f);

                warningLine.DOFade(0, 0);

                Camera.main.transform.DOShakeRotation(1, new Vector3(1,1,1));
                atk.transform.DOLocalMoveY(warningLine.transform.position.y, 1f).SetEase(ease);

                yield return new WaitForSeconds(1f);

                atk.transform.DOLocalMoveY(warningLine.transform.position.y * 2, 1f).SetEase(ease);

                yield return new WaitForSeconds(1f);

                atk.transform.DOKill();
                AttackPatternManager.inst.isAttackSummon = true;
                Destroy(this.gameObject);
                break;
            #endregion

            case EAttackPattern.Right:

                break;
        }


    }

    #region Fade On / Off
    IEnumerator FadeOn()
    {
        warningLine.DOFade(0f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("FadeOff");
    }

    IEnumerator FadeOff()
    {
        warningLine.DOFade(0.5f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("FadeOn");
    }
    #endregion
}
