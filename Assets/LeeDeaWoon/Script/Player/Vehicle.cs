using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
    protected Animator animator;

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

    protected virtual void Start()
    {
        GetComponents();
    }

    protected virtual void Update()
    {
        //RideVehicle();
    }

    protected virtual void GetComponents()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void RideVehicle()
    {
        switch (state)
        {
            case EVehicleState.None:

                break;
            case EVehicleState.Run:

                break;
            case EVehicleState.Jump:

                break;
            case EVehicleState.Descent:

                break;
            case EVehicleState.Levitation:

                break;
        }
        
    }

    
}
