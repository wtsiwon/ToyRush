using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class AttackPattern : MonoBehaviour
{
    public EAttackPattern eAttackPattern;

    private float waittime = 0.5f;

    private float waitTime = 0f;//0.5f

    public float WaitTime
    {
        get
        {
            if (GadgetManager.Instance.CheckGadgetSlot(EGadgetType.SlowRocket) == true)
                waitTime = waittime + waittime / 3;
            else
                waitTime = waittime;
            return waitTime;
        }
    }
    float warning = 3;

    public SpriteRenderer warningLine;

    [Header("악어 공격")]
    public GameObject crocodile;
    public float shakeWaitTime;

    [Header("군인장난감 공격")]
    public GameObject enemy;
    public GameObject bulletPrefab;
    public GameObject warningSummon;
    public GameObject warningLineRight;

    public int bulletNumber;
    public float enemyMoveSpeed;
    public const int enemeyMovePos = 7;

    public float[] summonPos = new float[4];

    [Header("드릴")]
    public GameObject drill;

    [Header("글러브")]
    public GameObject glove;

    BaseAttack baseAttack;

    private void Start()
    {
        baseAttack = new BaseAttack(this);
        baseAttack.BaseAttackType(eAttackPattern);
        baseAttack.Attack();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("Main"))
            AttackDoKill();
    }

    public T InstantiateSetParent<T>(T obj, Vector3 position, Quaternion rotation) where T : UnityEngine.Object
    {
        return Instantiate(obj, position, Quaternion.identity);
    }

    void AttackDoKill()
    {
        if (ItemManager.inst.isItemTouch)
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
