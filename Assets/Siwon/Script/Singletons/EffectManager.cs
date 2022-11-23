using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EffectManager : MonoBehaviour
{
    public List<GameObject> effectList = new List<GameObject>();
    public void GetEffect(EEffectType type,Vector3 pos)
    {
        GameObject effect = ObjPool.Instance.GetEffect(type, pos);
        effect = effectList[(int)type];
    }
}
