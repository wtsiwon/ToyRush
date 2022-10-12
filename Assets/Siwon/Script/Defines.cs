using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 풀링할 Types
/// </summary>
public enum EPoolType
{
    BackGround,
    Coin,
    Item,
    Obstacle,
    Rocket,
    Press,
    Effect,
    Sound,

}

public enum EGadgetType
{
    GravityBelt,

}

/// <summary>
/// 탈것
/// </summary>
public enum EVehicleType
{
    None,//아무것도 아님
    BusterMachine,//버스터머신
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
public enum EItemType
{
    Big,
    Booster,
    Magnet,
    ChangeCoin,
    TakeVehicle,
    PiggyBank,
}

/// <summary>
/// 장애물 타입
/// </summary>
public enum EObstacleType
{
    Type1,
    Type2,
    Type3,
}

/// <summary>
/// 코인패턴들
/// </summary>
public enum ECoinPatternType
{
    CoinPattern1,
    CoinPattern2,
    CoinPattern3,
    CoinPattern4,
    CoinPattern5,
}

public enum EEffectType
{

}

public enum ESoundType
{

}



public class Defines : MonoBehaviour
{
    
}
