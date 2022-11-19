using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPatternManager : MonoBehaviour
{
    public static AttackPatternManager inst;
    private void Awake() => inst = this;

    public List<GameObject> attackList = new List<GameObject>();
    List<GameObject> attackStart = new List<GameObject>();

    public bool isAttackSummon;
    public int coolTime;
    public int attackDel;

    void Start()
    {
        for (int i = 0; i < attackList.Count; i++)
            attackStart.Add(attackList[i]);

        StartCoroutine(Attack_Spawn());
    }

    void Update()
    {
    }

    IEnumerator Attack_Spawn()
    {


        while (true)
        {
            if (GameManager.Instance.IsGameStart == true)
            {
                if (attackList.Count == 0)
                {
                    for (int i = 0; i < attackStart.Count; i++)
                        attackList.Add(attackStart[i]);
                }
                int attckRandom = Random.Range(0, attackList.Count);
                yield return new WaitForSeconds(coolTime);
                ObstacleSpawner.Instance.canSpawn = false;

                yield return new WaitForSeconds(attackDel);
                Instantiate(attackList[attckRandom], transform.position, Quaternion.identity).transform.parent = gameObject.transform;
                attackList.RemoveAt(attckRandom);
                yield return new WaitForSeconds(3);
                ObstacleSpawner.Instance.canSpawn = true;
            }

            else
                yield return new WaitForSeconds(1.0f);
        }

    }
}

