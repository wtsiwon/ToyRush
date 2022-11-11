using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingObj : BaseAll
{
    public EPoolType poolType;

    private bool isActive;
    public bool IsActive
    {
        get
        {
            return isActive;
        }
        set
        {
            isActive = value;
            if (isActive == false)
            {

            }
        }
    }

    public virtual void Return()
    {
        //ObjPool.Instance.Return(poolType, this);
    }
}
