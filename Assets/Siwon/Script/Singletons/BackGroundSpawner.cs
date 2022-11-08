using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class BackGroundSpawner : Singleton<BackGroundSpawner>
{
    [Tooltip("배경 속도")]
    public float backgroundSpd;

    public BackGround standardBackGround;

    [SerializeField]
    private Sprite[] backgroundSprites;

    [Tooltip("현재 배경 Index")]
    public int currentBackgroundIndex = 1;

    private void Start()
    {
        //SpawnBackGround();
    }

    public void SpawnBackGround()
    {
        if (currentBackgroundIndex == 3)
        {
            BackGround back = (BackGround)ObjPool.Instance.Get(EPoolType.BackGround, transform.position);
            back.GetComponent<SpriteRenderer>().sprite = backgroundSprites[currentBackgroundIndex];
            currentBackgroundIndex = 0;
        }
        else
        {
            BackGround back = (BackGround)ObjPool.Instance.Get(EPoolType.BackGround, transform.position);
            back.GetComponent<SpriteRenderer>().sprite = backgroundSprites[currentBackgroundIndex];
        }
        currentBackgroundIndex++;
        print(currentBackgroundIndex);
    }
}
