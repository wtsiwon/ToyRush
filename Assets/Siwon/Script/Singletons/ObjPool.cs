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


    /// <summary>
    /// 가져오는 함수
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    public PoolingObj Get(EPoolType type, Vector3 pos)
    {
        PoolingObj obj = null;

        if(pool.ContainsKey(type) == false)
        {
            pool.Add(type, new Queue<PoolingObj>());
        }
        Queue<PoolingObj> queue = pool[type];
        
        PoolingObj origin = originObjs[(int)type];

        if(queue.Count > 0)
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
        pool.Add(EPoolType.BackGround, new Queue<PoolingObj>());
    }

    /// <summary>
    /// 반환값에 구애 받지 않는 Get함수
    /// 따로 BackGroundGet을 만들지 않아도됨
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    public T Get<T>(EPoolType type, Vector3 pos)
    {
        return Get(type,pos).GetComponent<T>();
    }

    /// <summary>
    /// 아이템 가져오는 함수
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pos"></param>
    public Item GetItem(EItemType type, Vector3 pos)
    {
        Item item = null;
        item = Get(EPoolType.Item, pos).GetComponent<Item>();
        //ItemSprite변경해야함
        return item;
    }

    /// <summary>
    /// 장애물 가져오는 함수
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pos"></param>
    public Obstacle GetObstacle(EObstacleType type, Vector3 pos)
    {
        Obstacle obstacle = null;
        obstacle = Get(EPoolType.Obstacle, pos).GetComponent<Obstacle>();
        return obstacle;

    }

    /// <summary>
    /// Effect 가져오는함수
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pos"></param>
    public GameObject GetEffect(EEffectType type, Vector3 pos)
    {
        GameObject effect = null;
        effect = Get(EPoolType.Effect, pos).GetComponent<GameObject>();

        //이펙트 List에서 가져와서 EffectType에 따라 변경해주기

        return effect;
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
}
