using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class AttackPattern : MonoBehaviour
{
    public enum EAttackPattern
    {
        Crocodile1,
        Crocodile2,
        Soldier,
        Drill,
        Gloves,
    }
    public EAttackPattern eAttackPattern;

    private float waittime = 0.5f;

    private float waitTime = 0f;//0.5f

    public float WaitTime
    {
        get
        {
            if (GadgetManager.Instance.CheckGadgetSlot(EGadgetType.SlowRocket) == true)
            {
                waitTime = waittime + waittime / 3;
            }
            else
            {
                waitTime = waittime;
            }
            return waitTime;
        }
    }
    float warning = 3;


    public SpriteRenderer warningLine;

    [Header("�Ǿ� ����")]
    public Ease ease;
    public GameObject crocodile;
    public float shakeWaitTime;

    [Header("�����峭�� ����")]
    public GameObject enemy;
    public GameObject bulletPrefab;
    public GameObject warningSummon;
    public GameObject warningLineRight;

    public int bulletNumber;
    public float enemyMoveSpeed;
    public const int enemeyMovePos = 7;

    float[] summonPos = new float[4];

    [Header("�帱")]
    public GameObject drill;

    [Header("�۷���")]
    public GameObject glove;

    private void Start()
    {
        StartCoroutine(ATK_Start());
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("Main"))
            AttackDoKill();
    }


    IEnumerator ATK_Start()
    {
        float shakeWaitTime = 0.4f;


        switch (eAttackPattern)
        {
            #region �Ǿ� ���� 1
            case EAttackPattern.Crocodile1:
                if (SceneManager.GetActiveScene().name == "Main")
                    AttackPatternManager.inst.isAttackSummon = true;

                SoundManager.instance.PlaySoundClip("WarningSFX", SoundType.SFX, SoundManager.instance.soundSFX - 0.5f);

                warningLine.transform.DOLocalMoveY(Player.Instance.transform.position.y, 0);

                warningLine.DOFade(0, WaitTime).SetLoops(-1, LoopType.Yoyo);
                yield return new WaitForSeconds(warning);

                warningLine.DOKill();
                warningLine.DOFade(0, 0);

                crocodile.transform.DOLocalMoveY(warningLine.transform.position.y, WaitTime).SetEase(ease).OnComplete(() =>
                {
                    crocodile.transform.DORotate(new Vector3(0, 0, 30), WaitTime);
                });

                yield return new WaitForSeconds(WaitTime);

                SoundManager.instance.PlaySoundClip("Attack", SoundType.SFX, SoundManager.instance.soundSFX);
                crocodile.transform.DOLocalMoveX(-13, WaitTime * 2).SetEase(ease);


                yield return new WaitForSeconds(shakeWaitTime);
                Camera.main.transform.DOShakePosition(shakeWaitTime, new Vector2(2, 0f));

                yield return new WaitForSeconds(1);

                if (SceneManager.GetActiveScene().name == "Main")
                    AttackPatternManager.inst.isAttackSummon = false;

                crocodile.transform.DOKill();
                Destroy(this.gameObject);
                break;
            #endregion

            #region �Ǿ� ���� 2
            case EAttackPattern.Crocodile2:
                AttackPatternManager.inst.isAttackSummon = true;
                SoundManager.instance.PlaySoundClip("WarningSFX", SoundType.SFX, SoundManager.instance.soundSFX - 0.5f);

                crocodile.transform.DOLocalMoveX(Player.Instance.transform.position.x, 0);
                warningLine.transform.DOLocalMove(new Vector2(Player.Instance.transform.position.x, -1.5f), 0);

                warningLine.DOFade(0, WaitTime).SetLoops(-1, LoopType.Yoyo);
                yield return new WaitForSeconds(warning);

                warningLine.DOKill();
                warningLine.DOFade(0, 0);

                crocodile.transform.DOLocalMoveY(0.5f, WaitTime).SetEase(ease);
                yield return new WaitForSeconds(WaitTime);

                SoundManager.instance.PlaySoundClip("Attack", SoundType.SFX, SoundManager.instance.soundSFX);

                crocodile.transform.DOLocalMoveY(-7, WaitTime).SetEase(ease);
                Camera.main.transform.DOShakePosition(0.4f, new Vector2(0, 1));

                yield return new WaitForSeconds(WaitTime);
                AttackPatternManager.inst.isAttackSummon = false;
                crocodile.transform.DOKill();
                Destroy(this.gameObject);
                break;
            #endregion

            #region �����峭�� ����
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

            #region �帱 ����
            case EAttackPattern.Drill:
                for (int i = 0; i <= 2; i++)
                {
                    AttackPatternManager.inst.isAttackSummon = true;
                    SoundManager.instance.PlaySoundClip("WarningSFX", SoundType.SFX, SoundManager.instance.soundSFX - 0.5f);

                    warningLine.DOFade(0.5f, 0);
                    transform.DORotate(new Vector3(0, 0, Random.Range(-50, 50)), 0);

                    warningLine.DOFade(0, WaitTime).SetLoops(-1, LoopType.Yoyo);
                    yield return new WaitForSeconds(warning);

                    warningLine.DOKill();
                    warningLine.DOFade(0, 0);
                    drill.transform.DOLocalMoveX(16, 0);
                    drill.transform.DOLocalMoveX(-15f, 1).SetEase(ease);
                    SoundManager.instance.PlaySoundClip("Attack", SoundType.SFX, SoundManager.instance.soundSFX);
                    Camera.main.transform.DOShakePosition(shakeWaitTime, new Vector2(1, 1));

                    yield return new WaitForSeconds(0.7f);
                }
                yield return new WaitForSeconds(1);
                AttackPatternManager.inst.isAttackSummon = false;
                drill.transform.DOKill();
                Destroy(this.gameObject);

                break;
            #endregion

            #region �����尩 ����
            case EAttackPattern.Gloves:
                int random = Random.Range(-18, 18);
                AttackPatternManager.inst.isAttackSummon = true;
                SoundManager.instance.PlaySoundClip("WarningSFX", SoundType.SFX, SoundManager.instance.soundSFX - 0.5f);

                transform.DOMoveY(0, 0);

                for (int i = 0; i <= 2; i++)
                {
                    transform.GetChild(i).DOLocalRotate(new Vector3(0, 0, random), WaitTime);
                    transform.GetChild(i).GetChild(1).GetComponent<SpriteRenderer>().DOFade(0, WaitTime).SetLoops(-1, LoopType.Yoyo);
                }

                yield return new WaitForSeconds(3);

                for (int i = 0; i <= 2; i++)
                {
                    transform.GetChild(i).GetChild(1).GetComponent<SpriteRenderer>().DOKill();
                    transform.GetChild(i).GetChild(1).GetComponent<SpriteRenderer>().DOFade(0, 0);
                }

                yield return new WaitForSeconds(WaitTime);

                SoundManager.instance.PlaySoundClip("Attack", SoundType.SFX, SoundManager.instance.soundSFX);
                for (int i = 0; i <= 2; i++)
                    transform.GetChild(i).GetChild(0).DOLocalMoveX(-18, WaitTime);

                yield return new WaitForSeconds(1);
                AttackPatternManager.inst.isAttackSummon = false;
                glove.transform.DOKill();
                Destroy(this.gameObject);
                break;
                #endregion
        }
    }

    void AttackDoKill()
    {
        if (ItemManager.inst.isItemTouch == true)
        {
            warningLine.DOKill();

            switch (eAttackPattern)
            {
                case EAttackPattern.Crocodile1:
                    crocodile.transform.DOKill();
                    break;

                case EAttackPattern.Soldier:
                    enemy.transform.DOKill();
                    break;

                case EAttackPattern.Drill:
                    drill.transform.DOKill();
                    break;

                case EAttackPattern.Gloves:
                    for (int i = 0; i <= 2; i++)
                    {
                        transform.GetChild(i).DOKill();
                        transform.GetChild(i).GetChild(0).DOKill();
                        transform.GetChild(i).GetChild(1).GetComponent<SpriteRenderer>().DOKill();
                    }
                    break;
            }
            Destroy(gameObject);
        }
    }
}
