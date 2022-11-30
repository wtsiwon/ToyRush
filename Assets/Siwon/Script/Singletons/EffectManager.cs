using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EffectManager : MonoBehaviour
{
    public List<GameObject> effectList = new List<GameObject>();

    /// <summary>
    /// Effect GetÇÔ¼ö
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pos"></param>
    public void GetEffect(EEffectType type,Vector3 pos)
    {
        GameObject effect = ObjPool.Instance.GetEffect(pos);
        effect = effectList[(int)type];
    }
}