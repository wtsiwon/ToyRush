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
        Drill,
    }
    public EAttackPattern eAttackPattern;

    private float waitTime = 0.5f;

    [Header("악어 공격")]
    public Ease ease;
    public GameObject atk;
    public SpriteRenderer warningLineBottom;
    public float shakeWaitTime;

    [Header("군인장난감 공격")]
    public GameObject enemy;
    public GameObject bulletPrefab;
    public GameObject warningSummon;
    public GameObject warningLineRight;

    public int bulletNumber;
    public float enemyMoveSpeed;
    public const int enemeyMovePos = 7;

    float[] summonPos = new float[4];

    [Header("드릴")]
    public SpriteRenderer drillWarningLine;
    public GameObject drill;

    private void Start()
    {
        StartCoroutine("ATK_Start");

    }

    private void Update()
    {
        AttackDoKill();
    }

    void AttackDoKill()
    {
        if (ItemManager.inst.isItemTouch == true)
        {
            switch (eAttackPattern)
            {
                case EAttackPattern.Crocodile1:
                    atk.transform.DOKill();
                    warningLineBottom.DOKill();
                    Camera.main.transform.DOKill();
                    break;

                case EAttackPattern.Soldier:
                    enemy.transform.DOKill();
                    break;

                case EAttackPattern.Drill:
                    drill.transform.DOKill();
                    drillWarningLine.DOKill();
                    Camera.main.transform.DOKill();
                    break;
            }


            Destroy(gameObject);
        }
    }


    IEnumerator ATK_Start()
    {
        float Warning = 3;
        float shakeWaitTime = 0.4f;


        switch (eAttackPattern)
        {
            #region 악어 공격 1
            case EAttackPattern.Crocodile1:
                AttackPatternManager.inst.isAttackSummon = true;
                SoundManager.instance.PlaySoundClip("WarningSFX", SoundType.SFX, 0.5f);

                warningLineBottom.transform.DOLocalMoveY(Player.Instance.transform.position.y, 0);
                warningLineBottom.DOFade(0, waitTime).SetLoops(-1, LoopType.Yoyo);
                yield return new WaitForSeconds(Warning);

                warningLineBottom.DOKill();
                warningLineBottom.DOFade(0, 0);

                atk.transform.DOLocalMoveY(warningLineBottom.transform.position.y, waitTime).SetEase(ease).OnComplete(() =>
                {
                    atk.transform.DORotate(new Vector3(0, 0, 30), waitTime);
                });

                yield return new WaitForSeconds(waitTime);

                SoundManager.instance.PlaySoundClip("Attack", SoundType.SFX, 1f);
                atk.transform.DOLocalMoveX(-13, waitTime * 2).SetEase(ease);


                yield return new WaitForSeconds(shakeWaitTime);
                Camera.main.transform.DOShakePosition(shakeWaitTime, new Vector2(2, 0f));

                yield return new WaitForSeconds(1);

                AttackPatternManager.inst.isAttackSummon = false;
                atk.transform.DOKill();
                Destroy(this.gameObject);
                break;
            #endregion

            #region 악어 공격 2
            case EAttackPattern.Crocodile2:
                AttackPatternManager.inst.isAttackSummon = true;
                SoundManager.instance.PlaySoundClip("WarningSFX", SoundType.SFX, 0.5f);

                atk.transform.DOLocalMoveX(Player.Instance.transform.position.x, 0);
                warningLineBottom.transform.DOLocalMove(new Vector2(Player.Instance.transform.position.x, -1.5f), 0);
                warningLineBottom.DOFade(0, waitTime).SetLoops(-1, LoopType.Yoyo);
                yield return new WaitForSeconds(Warning);

                warningLineBottom.DOKill();
                warningLineBottom.DOFade(0, 0);

                atk.transform.DOLocalMoveY(0.5f, waitTime).SetEase(ease);
                yield return new WaitForSeconds(waitTime);

                SoundManager.instance.PlaySoundClip("Attack", SoundType.SFX, 1f);

                atk.transform.DOLocalMoveY(-7, waitTime).SetEase(ease);
                Camera.main.transform.DOShakePosition(0.4f, new Vector2(0, 1));

                yield return new WaitForSeconds(waitTime);
                AttackPatternManager.inst.isAttackSummon = false;
                atk.transform.DOKill();
                Destroy(this.gameObject);
                break;
            #endregion

            #region 군인장난감 공격
            case EAttackPattern.Soldier:

                warningSummon.transform.DOLocalMoveY(enemeyMovePos, enemyMoveSpeed);
                for (int i = 0; i < bulletNumber; i++)
                {
                    float warninTimeMin = 0.1f;
                    float warninTimeMax = 0.4f;

                    summonPos[i] = Random.Range(warninTimeMin, warninTimeMax);
                    Instantiate(warningLineRight, new Vector2(0, warningSummon.transform.position.y), Quaternion.identity)/*.transform.parent = gameObject.transform*/;

                    yield return new WaitForSeconds(summonPos[i]);
                }

                yield return new WaitForSeconds(3);

                enemy.transform.DOLocalMoveY(enemeyMovePos, enemyMoveSpeed);
                for (int i = 0; i < bulletNumber; i++)
                {
                    Instantiate(bulletPrefab, new Vector2(enemy.transform.position.x, enemy.transform.position.y), Quaternion.identity).transform.parent = gameObject.transform;
                    yield return new WaitForSeconds(summonPos[i]);
                }

                yield return new WaitForSeconds(3);
                AttackPatternManager.inst.isAttackSummon = false;
                enemy.transform.DOKill();
                bulletPrefab.transform.DOKill();
                Destroy(this.gameObject);
                break;
            #endregion

            #region 드릴 공격
            case EAttackPattern.Drill:
                AttackPatternManager.inst.isAttackSummon = true;
                SoundManager.instance.PlaySoundClip("WarningSFX", SoundType.SFX, 0.5f);

                transform.DORotate(new Vector3(0, 0, Random.Range(-50, 0)), 0);
                drillWarningLine.DOFade(0, waitTime).SetLoops(-1, LoopType.Yoyo);
                yield return new WaitForSeconds(Warning);

                drillWarningLine.DOKill();
                drillWarningLine.DOFade(0, 0);

                drill.transform.DOLocalMoveX(-15f, 1).SetEase(ease);
                SoundManager.instance.PlaySoundClip("Attack", SoundType.SFX, 1f);
                Camera.main.transform.DOShakePosition(shakeWaitTime, new Vector2(1, 1));

                yield return new WaitForSeconds(1);
                AttackPatternManager.inst.isAttackSummon = false;
                drill.transform.DOKill();
                Destroy(this.gameObject);
                break;
                #endregion
        }


    }
}
