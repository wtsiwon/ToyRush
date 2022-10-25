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

    [Header("아랫 공격")]
    public Ease ease;
    public GameObject atk;
    public SpriteRenderer warningLine;
    public float shakeWaitTime;

    [Header("오른쪽 공격")]
    public GameObject enemy;
    public GameObject bulletPrefab;
    public int enemyMoveTime;

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
            #region 아랫공격
            case EAttackPattern.Bottom:
                int moveWait = 1;

                StartCoroutine("FadeOn");
                yield return new WaitForSeconds(3.5f);

                StopCoroutine("FadeOn");
                StopCoroutine("FadeOff");
                yield return new WaitForSeconds(0.5f);

                warningLine.DOFade(0, 0);

                atk.transform.DOLocalMoveY(warningLine.transform.position.y, moveWait).SetEase(ease);

                yield return new WaitForSeconds(0.5f);
                Camera.main.transform.DOShakeRotation(shakeWaitTime, new Vector3(1,1,1));

                yield return new WaitForSeconds(1f);

                atk.transform.DOLocalMoveY(warningLine.transform.position.y * 2, moveWait).SetEase(ease);

                yield return new WaitForSeconds(1f);

                atk.transform.DOKill();
                AttackPatternManager.inst.isAttackSummon = true;
                Destroy(this.gameObject);
                break;
            #endregion

            #region 오른쪽 공격
            case EAttackPattern.Right:
                int enemyYValue = 7;
                float waitTime = 0.5f;

                enemy.transform.DOLocalMoveY(enemyYValue, enemyMoveTime);

                for (int i = 0; i < 5; i++)
                {
                    Instantiate(bulletPrefab, enemy.transform.position, Quaternion.identity).transform.parent = gameObject.transform;
                    yield return new WaitForSeconds(waitTime);
                }

                yield return new WaitForSeconds(3f);
                enemy.transform.DOKill();
                bulletPrefab.transform.DOKill();
                Destroy(this.gameObject);
            #endregion
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
