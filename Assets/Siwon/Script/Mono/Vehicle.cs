using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
    protected Animator animator;

    protected Player player;

    private EVehicleState state;
    public EVehicleState State
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
            RideVehicle();
        }
    }

    public Vehicle(EVehicleState state)
    {
        State = state;
    }
    protected virtual void Start()
    {
        GetComponents();
        player = Player.Instance;
    }

    protected virtual void Update()
    {
        ChangeState();
    }

    protected virtual void GetComponents()
    {
        animator = GetComponent<Animator>();
    }

    public abstract void InputKey();

    /// <summary>
    /// runtimeAnimationController�ٲ��ֱ�
    /// </summary>
    protected abstract void RideVehicle();

    /// <summary>
    /// ���� ��ȭ �Լ�
    /// </summary>
    protected abstract void ChangeState();
}
