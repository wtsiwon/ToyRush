using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EffectManager : Singleton<EffectManager>
{
    public List<GameObject> effectList = new List<GameObject>();

    /// <summary>
    /// Effect GetÇÔ¼ö
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pos"></param>
    public GameObject GetEffect(EEffectType type,Vector3 pos, bool isCoin = false)
    {
        GameObject effect = Instantiate(effectList[0]);
        effect = effectList[(int)type];
        StartCoroutine(CDestroyEffect(effect));

        return effect;
    }

    public void ReturnEffect(GameObject obj)
    {
        ObjPool.Instance.EffectReturn(obj);
    }

    public IEnumerator CDestroyEffect(GameObject obj)
    {
        yield return new WaitForSeconds(0.5f);
        ReturnEffect(obj);
    }
}