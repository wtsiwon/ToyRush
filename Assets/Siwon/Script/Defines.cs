using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ǯ���� Types
/// </summary>
public enum EPoolType
{
    Coin,
    Effect,
}

public enum EGadgetType
{
    None,
    GravityBelt,
    Magnet,
    SlowRocket,
    XrayGoggles,
}

/// <summary>
/// Ż��
/// </summary>
public enum EVehicleType
{
    None,//�ƹ��͵� �ƴ�
    BusterMachine,//�����͸ӽ�
    Frog,//������
    Wyvern,//���̹�
    GravitySuit,//�߷¼�Ʈ
    ProfitUFO,
}

public enum ETheme//�׸�
{
    Supply,//����
    Process,//����
    Produce,//����
    Package,//������
    Shipping,//�����
    End,
}

/// <summary>
/// �����۵�
/// </summary>
public enum EItemType : short
{
    Transformation, //����
    Magnet,         //�ڼ�
    Piggybank,      //������
    Booster,        //�ν���
    Coinconverter,  //���κ�ȯ��
    Sizecontrol,    //ũ������
}

/// <summary>
/// �ν��� Ÿ��
/// </summary>
public enum EBoosterType
{
    Booster500,
    Booster1500,
    BoosterItem,
}

/// <summary>
/// ��ֹ� Ÿ��
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
/// ��ֹ� 
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
    piggybank,
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
