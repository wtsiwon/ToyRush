using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractItem : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision is Player)
        {
            
        }
    }

    protected virtual void OnDelete()
    {
        Destroy(gameObject);
        //¿Ã∆Â∆Æ
    }


}
