using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AttackPattern : MonoBehaviour
{
    public enum EAttackPattern
    {
        Crocodile1,
        Crocodile2,
        Soldier,
    }
    public EAttackPattern eAttackPattern;

    [Header("아랫 공격")]
    public Ease ease;
    public GameObject atk;
    public SpriteRenderer warningLineBottom;
    public float shakeWaitTime;

    [Header("오른쪽 공격")]
    public GameObject enemy;
    public GameObject bulletPrefab;
    public GameObject warningSummon;
    public GameObject warningLineRight;

    public int bulletNumber;
    public float enemyMoveSpeed;
    public const int enemeyMovePos = 7;

    float[] summonPos = new float[4];

    private void Start()
    {
        StartCoroutine("ATK_Start");

    }

    private void Update()
    {
        if (ItemManager.inst.isItemTouch == true)
        {
            if (eAttackPattern == EAttackPattern.Crocodile1)
            {
                atk.transform.DOKill();
                warningLineBottom.DOKill();
                Camera.main.transform.DOKill();
            }

            else
            {
                enemy.transform.DOKill();
            }

            Destroy(gameObject);
        }
    }


    IEnumerator ATK_Start()
    {
        float moveWait = 0.5f;
        float warningWaitTime = 0.5f;
        float shakeWaitTime = 0.2f;

        switch (eAttackPattern)
        {
            #region 악어 공격 1
            case EAttackPattern.Crocodile1:
                AttackPatternManager.inst.isAttackSummon = true;
                SoundManager.instance.PlaySoundClip("WarningSFX", SoundType.SFX, 0.5f);

                warningLineBottom.transform.DOLocalMove(Player.Instance.transform.position, 0);
                warningLineBottom.DOFade(0, warningWaitTime).SetLoops(-1, LoopType.Yoyo);
                yield return new WaitForSeconds(3);

                warningLineBottom.DOKill();
                warningLineBottom.DOFade(0, 0);
                atk.transform.DOLocalMoveY(warningLineBottom.transform.position.y, moveWait).SetEase(ease);

                yield return new WaitForSeconds(0.5f);

                atk.transform.DOLocalMoveX(-13, moveWait * 2).SetEase(ease);

                SoundManager.instance.PlaySoundClip("Attack", SoundType.SFX, 1f);

                yield return new WaitForSeconds(shakeWaitTime);
                Camera.main.transform.DOShakeRotation(shakeWaitTime, new Vector3(1, 1, 0f));
                yield return new WaitForSeconds(shakeWaitTime);

                //atk.transform.DOLocalMoveY(-8.5f, moveWait).SetEase(ease);

                yield return new WaitForSeconds(0.5f);

                AttackPatternManager.inst.isAttackSummon = false;
                atk.transform.DOKill();
                Destroy(this.gameObject);
                break;
            #endregion

            //#region 악어 공격 2
            //case EAttackPattern.Crocodile2:
            //    AttackPatternManager.inst.isAttackSummon = true;
            //    SoundManager.instance.PlaySoundClip("WarningSFX", SoundType.SFX, 0.5f);

            //    warningLineBottom.transform.DOLocalMove(Player.Instance.transform.position, 0);
            //    warningLineBottom.DOFade(0, warningWaitTime).SetLoops(-1, LoopType.Yoyo);
            //    yield return new WaitForSeconds(3);

            //    warningLineBottom.DOKill();
            //    warningLineBottom.DOFade(0, 0);
            //    atk.transform.DOLocalMoveY(warningLineBottom.transform.position.y, moveWait).SetEase(ease);

            //    yield return new WaitForSeconds(0.5f);

            //    atk.transform.DOLocalMoveX(-13, moveWait * 2).SetEase(ease);

            //    SoundManager.instance.PlaySoundClip("Attack", SoundType.SFX, 1f);

            //    yield return new WaitForSeconds(shakeWaitTime);
            //    Camera.main.transform.DOShakeRotation(shakeWaitTime, new Vector3(1, 1, 0f));
            //    yield return new WaitForSeconds(shakeWaitTime);

            //    //atk.transform.DOLocalMoveY(-8.5f, moveWait).SetEase(ease);

            //    yield return new WaitForSeconds(0.5f);

            //    AttackPatternManager.inst.isAttackSummon = false;
            //    atk.transform.DOKill();
            //    Destroy(this.gameObject);
            //    break;
            //#endregion

            #region 오른쪽 공격
            case EAttackPattern.Soldier:
                int waitTime = 3;

                warningSummon.transform.DOLocalMoveY(enemeyMovePos, enemyMoveSpeed);
                for (int i = 0; i < bulletNumber; i++)
                {
                    float warninTimeMin = 0.1f;
                    float warninTimeMax = 0.4f;

                    summonPos[i] = Random.Range(warninTimeMin, warninTimeMax);
                    Instantiate(warningLineRight, new Vector2(0, warningSummon.transform.position.y), Quaternion.identity)/*.transform.parent = gameObject.transform*/;

                    yield return new WaitForSeconds(summonPos[i]);
                }

                yield return new WaitForSeconds(waitTime);

                enemy.transform.DOLocalMoveY(enemeyMovePos, enemyMoveSpeed);
                for (int i = 0; i < bulletNumber; i++)
                {
                    Instantiate(bulletPrefab, new Vector2(enemy.transform.position.x, enemy.transform.position.y), Quaternion.identity).transform.parent = gameObject.transform;
                    yield return new WaitForSeconds(summonPos[i]);
                }

                yield return new WaitForSeconds(waitTime);
                AttackPatternManager.inst.isAttackSummon = false;
                enemy.transform.DOKill();
                bulletPrefab.transform.DOKill();
                Destroy(this.gameObject);
                #endregion
                break;
        }


    }
}
