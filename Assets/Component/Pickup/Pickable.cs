using System;
using UnityEngine;

public abstract class Pickable: MonoBehaviour
{
    public virtual void DestroySelf()
    {
        Destroy(gameObject);
    }

    public virtual void OnTriggerEnter(Collider collision)
    {
        if(collision.TryGetComponent<Player>(out var player)) DestroySelf();
    }
}
