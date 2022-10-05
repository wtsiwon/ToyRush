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
        
        return obj;
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
    /// 다시 풀로 넣는 함수
    /// </summary>
    /// <param name="type"></param>
    /// <param name="obj"></param>
    public void Return(EPoolType type, PoolingObj obj)
    { 
        obj.Return();
        obj.gameObject.SetActive(false);
        pool[type].Enqueue(obj);
    }
}
