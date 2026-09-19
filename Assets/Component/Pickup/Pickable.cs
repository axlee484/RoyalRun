using System;
using UnityEngine;

public class Pickable: MonoBehaviour
{
    public virtual void DestroySelf()
    {
        Destroy(gameObject);
    }

    public virtual void OnTriggerEnter(Collider collision)
    {
        if(collision.GetComponentInParent<Player>())
        {
            DestroySelf();
        }
    }
}
