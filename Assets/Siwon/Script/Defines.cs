using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPoolType
{
    SupplyBack1,
    SupplyBack2,
    ProcessBack1,
    ProcessBack2,
    ProduceBack1,
    ProduceBack2,
    PackageBack1,
    PackageBack2,
    ShippingBack1,
    ShippingBack2,
    Gold,
    BigItem,
    BoosterItem,
    Obstacle1,
    Obstacle2,
    Obstacle3,
    Obstacle4,
    Rocket1,
    Press1
}

public enum EVehicleType
{
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


public class Defines : MonoBehaviour
{
    
}
