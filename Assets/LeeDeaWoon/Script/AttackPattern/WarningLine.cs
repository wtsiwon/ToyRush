using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class WarningLine : MonoBehaviour
{
    public SpriteRenderer warningLinePrefab;

    void Start()
    {
        StartCoroutine(WarningLine_FadeOnOff());
    }

    void Update()
    {
        if (ItemManager.inst.isItemTouch == true)
        {
            warningLinePrefab.DOKill();
            Destroy(gameObject);
        }
    }

    IEnumerator WarningLine_FadeOnOff()
    {
        AttackPatternManager.inst.isAttackSummon = true;
        warningLinePrefab.DOFade(0, 0.5f).SetLoops(-1, LoopType.Yoyo);
        yield return new WaitForSeconds(3);

        warningLinePrefab.DOKill();
        Destroy(warningLinePrefab);
    }
}
