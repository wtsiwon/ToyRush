using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPatternManager : MonoBehaviour
{
    public static AttackPatternManager inst;
    private void Awake() => inst = this;

    public List<GameObject> attackList = new List<GameObject>();
    public bool isAttackSummon;
    public int coolTime;

    void Start()
    {
        
    }

    void Update()
    {
        StartCoroutine("Attack_Spawn");
    }

    IEnumerator Attack_Spawn()
    {
        // 소환시간 약 20초로 해놓기

        if (isAttackSummon == true && GameManager.Instance.IsGameStart == true)
        {
            isAttackSummon = false;
            yield return new WaitForSeconds(coolTime);
            Instantiate(attackList[Random.Range(0, attackList.Count)], transform.position, Quaternion.identity).transform.parent = gameObject.transform;
        }

    }
}

