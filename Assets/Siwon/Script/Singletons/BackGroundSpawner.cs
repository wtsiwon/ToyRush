using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSpawner : Singleton<BackGroundSpawner>
{
    [Tooltip("배경 속도")]
    public float backgroundSpd;

    [SerializeField]
    private List<Sprite> backgroundSpriteList = new List<Sprite>();

    [Tooltip("현재 배경 Index")]
    public int currentBackgroundIndex = 1;

    private void Start()
    {
        SpawnBackGround();
    }

    public void SpawnBackGround()
    {
        if (currentBackgroundIndex == 3)
        {
            currentBackgroundIndex = 0;
            BackGround back = (BackGround)ObjPool.Instance.Get(EPoolType.BackGround, transform.position);
            back.GetComponent<SpriteRenderer>().sprite = backgroundSpriteList[currentBackgroundIndex];
        }
        else
        {
            BackGround back = (BackGround)ObjPool.Instance.Get(EPoolType.BackGround, transform.position);
            back.GetComponent<SpriteRenderer>().sprite = backgroundSpriteList[currentBackgroundIndex];
        }
        currentBackgroundIndex++;
    }
}
