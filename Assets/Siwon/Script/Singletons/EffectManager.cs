using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EffectManager : Singleton<EffectManager>
{
    public List<GameObject> effectList = new List<GameObject>();

    /// <summary>
    /// Effect Get�Լ�
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pos"></param>
    public GameObject GetEffect(EEffectType type,Vector3 pos)
    {
        GameObject effect = ObjPool.Instance.GetEffect(pos);
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
        yield return new WaitForSeconds(1.5f);
        ReturnEffect(obj);
    }
}