using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractItem : MovingElement
{
    public EItemType itemType;
    protected virtual IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if(collision is Player)
        {
            OnDelete();
        }

        yield return null;
    }

    protected virtual void OnDelete()
    {
        Destroy(gameObject);

    }

    


}
