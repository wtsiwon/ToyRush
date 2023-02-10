using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 풀링할 Types
/// </summary>
public enum EPoolType
{
    Effect,
    Coin,
}

public enum EVehicleState
{
    None,//탈것을 안탔음
    Run,
    Jump,
    Descent,
    Levitation,//공중에 뜬 상태
}

public enum EGadgetType
{
    None,
    GravityBelt,//중력 벨트
    Magnet,//자석
    SlowRocket,//슬로우 로켓
    XrayGoggles,//엑스레이 고글
    IceSheet,//빙판
    LifeRing,//생명의 반지
}

/// <summary>
/// 탈것
/// </summary>
public enum EVehicleType
{
    None,//아무것도 아님
    BirdMachine,//버드머신
    Frog,//개구리
    Wyvern,//와이번
    GravitySuit,//중력수트
    ProfitUFO,
}

public enum ETheme//테마
{
    Supply,//수급
    Process,//가공
    Produce,//생산
    Package,//포장장
    Shipping,//배송장
    End,
}

/// <summary>
/// 아이템들
/// </summary>
public enum EItemType : short
{
    Transformation, //변신
    Magnet,         //자석
    Piggybank,      //저금통
    Booster,        //부스터
    Coinconverter,  //코인변환기
    Sizecontrol,    //크기조절
    Healing,//체력
}

/// <summary>
/// 부스터 타입
/// </summary>
public enum EBoosterType
{
    Booster500,
    Booster1500,
    BoosterItem,
}

/// <summary>
/// 장애물 타입
/// </summary>
public enum EObstacleType
{
    Gear,
    Drill,
    BlowFish,
    Fist,
    End,
}

/// <summary>
/// 장애물 
/// </summary>
public enum EObstacleColorType
{
    Yellow,
    Green,
    //LightGreen,
    Red,
    End,
}

public enum EEffectType
{
    BigDirector,
    Coin,
}

public enum ESoundType
{

}

public enum EDir
{
    Up,
    Down,
    Left,
    Right,
    Cross1,
    Cross2,
    End,
}

public enum ECurrentSpawnType
{
    Obstacle,
    AttackPattern,
    Stop,
}

[System.Serializable]
public struct Array<T>
{
    public List<T> list;
}

public class Defines : MonoBehaviour
{
    
}
