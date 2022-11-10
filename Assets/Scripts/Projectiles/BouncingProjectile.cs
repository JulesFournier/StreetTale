using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingProjectile : Projectile
{
    Vector3 lastVelocity;
    public SpriteRenderer visual;
    public Animator animator;

    void Update()
    {
        lastVelocity = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FightBox")) {
            float speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.GetContact(0).normal);

            rb.velocity = direction * Mathf.Max(speed, 0f);
        }
    }

    public void InvokeDestroy(float timeToInvoke)
    {
        StartCoroutine(DestroyProjectile(timeToInvoke/10f));
    }

    IEnumerator DestroyProjectile(float timeToInvoke)
    {
        yield return new WaitForSeconds(timeToInvoke*9);
        animator.SetTrigger("Disappear");
        yield return new WaitForSeconds(timeToInvoke);
        collisionsManager.RemoveProjectile(this);
        Destroy(gameObject);
    }
}
