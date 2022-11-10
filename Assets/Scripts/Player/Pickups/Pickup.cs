using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupType {
        ATTACK,
        HEALTH
    };
    public PickupType pickupType;
    public Collider2D c2D;
    public CollisionsManager collisionsManager;

    void OnBecameInvisible()
    {
        collisionsManager.RemovePickup(this);
        Destroy(gameObject);
    }
}
