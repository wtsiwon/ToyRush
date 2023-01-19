using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : Singleton<ObjPool>
{
    public EPoolType poolType;

    [SerializeField]
    [Tooltip("poolingObjs")]
    private PoolingObj[] originObjs;

    public Dictionary<EPoolType, Queue<PoolingObj>> pool = new Dictionary<EPoolType, Queue<PoolingObj>>();

    public Queue<GameObject> effectQ = new Queue<GameObject>();

    /// <summary>
    /// 가져오는 함수
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    public PoolingObj Get(EPoolType type, Vector3 pos)
    {
        PoolingObj obj = null;

        if (pool.ContainsKey(type) == false)
        {
            pool.Add(type, new Queue<PoolingObj>());
        }
        Queue<PoolingObj> queue = pool[type];

        PoolingObj origin = originObjs[(int)type];

        if (queue.Count > 0)
        {
            obj = queue.Dequeue();
        }
        else
        {
            obj = Instantiate(origin);
        }

        obj.transform.position = pos;
        obj.gameObject.SetActive(true);

        return obj;
    }

    private void OnEnable()
    {
        InitializeKey();
    }
    public void InitializeKey()
    {
        pool.Add(EPoolType.Coin, new Queue<PoolingObj>());
    }

    /// <summary>
    /// 장애물 가져오는 함수
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pos"></param>
    public Obstacle GetObstacle(Vector3 pos)
    {
        Obstacle obstacle = null;
        //obstacle = Get(EPoolType.Obstacle, pos).GetComponent<Obstacle>();
        return obstacle;

    }

    /// <summary>
    /// Effect 가져오는함수
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pos"></param>
    public GameObject GetEffect(Vector3 pos, bool isCoin = false)
    {
        GameObject effect = null;

        if (effectQ.Count > 0)
        {
            effect = effectQ.Dequeue();
        }
        else
        {
            effect = Instantiate(EffectManager.Instance.effectList[(int)EEffectType.Coin]);
        }

        if (isCoin == true)
        {
            effect.transform.localScale = new Vector3(0.4f, 0.4f, 1);
        }
        else
        {
            effect.transform.localScale = new Vector3(2, 2, 1);
        }
        effect.transform.position = pos;
        effect.SetActive(true);

        return effect;
    }

    private void InitializeCoinObj()
    {
        for (int i = 0; i < 50; i++)
        {
            effectQ.Enqueue(EffectManager.Instance.effectList[1]);
        }
    }


    /// <summary>
    /// 사운드 가져오는 함수
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pos"></param>
    public AudioClip GetSound(ESoundType type)
    {
        AudioClip audioClip = null;
        return audioClip;
    }

    /// <summary>
    /// 다시 풀로 넣는 함수
    /// </summary>
    /// <param name="type"></param>
    /// <param name="obj"></param>
    public void Return(EPoolType type, PoolingObj obj)
    {
        obj.gameObject.SetActive(false);
        pool[type].Enqueue(obj);
    }

    public void EffectReturn(GameObject obj)
    {
        obj.SetActive(false);
        effectQ.Enqueue(obj);
    }
}
