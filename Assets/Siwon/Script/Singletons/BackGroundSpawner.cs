using System.Collections;
using System.Collections.Generic;
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

}