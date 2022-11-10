using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D c2D;
    public int damage;
    public CollisionsManager collisionsManager;

    void OnBecameInvisible()
    {
        collisionsManager.RemoveProjectile(this);
        Destroy(gameObject);
    }
}
