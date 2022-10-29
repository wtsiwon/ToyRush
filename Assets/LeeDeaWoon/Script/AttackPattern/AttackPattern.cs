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
    public GameObject questionMarkPrefab;

    public int enemyMoveSpeed;
    public float bulletSummonSpeed;
    public int bulletNumber;

    public const int enemeyMovePos = 7;

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
                float moveWait = 0.5f;

                StartCoroutine("FadeOn");
                yield return new WaitForSeconds(3.5f);

                StopCoroutine("FadeOn");
                StopCoroutine("FadeOff");

                warningLine.DOFade(0, 0);

                atk.transform.DOLocalMoveY(warningLine.transform.position.y, moveWait).SetEase(ease);

                yield return new WaitForSeconds(0.2f);
                Camera.main.transform.DOShakeRotation(shakeWaitTime, new Vector3(0.5f, 0.5f, 0f));

                yield return new WaitForSeconds(0.5f);

                atk.transform.DOLocalMoveY(warningLine.transform.position.y * 2, moveWait).SetEase(ease);

                yield return new WaitForSeconds(0.5f);

                atk.transform.DOKill();
                Destroy(this.gameObject);
                break;
            #endregion

            #region 오른쪽 공격
            case EAttackPattern.Right:

                enemy.transform.DOLocalMoveY(enemeyMovePos, enemyMoveSpeed);

                for (int i = 0; i < bulletNumber; i++)
                {
                    if (transform.position.y <= 11)
                    {
                        Instantiate(questionMarkPrefab, enemy.transform.position, Quaternion.identity).transform.parent = gameObject.transform;
                        yield return new WaitForSeconds(bulletSummonSpeed);
                    }
                }

                yield return new WaitForSeconds(5);
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
